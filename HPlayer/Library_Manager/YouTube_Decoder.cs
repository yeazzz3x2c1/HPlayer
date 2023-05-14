using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows;

namespace YFH_YouTube_Library
{

    enum Decode_Method
    {
        Reverse,
        Splice,
        Swap,
        Null
    }
    class Encode_Method
    {

        public string Class_Name = "";
        public string Class_Method_Name = "";
        public int Parameter = 0;
        public Decode_Method Method = Decode_Method.Null;
    }
    public class YouTube_Decode_Object
    {
        public string Title = "";
        public string Url = "";
        public double Second = 0;
        public YouTube_Decode_Object(string Url)
        {
            this.Url = Url;
        }
    }
    public class YouTube_Decoder
    {
        static WebClient w = new WebClient();
        static char[] Decode_Slice(char[] array, int Code)
        {
            char[] result = new char[array.Length - Code];
            for (int i = Code; i < array.Length; i++)
                result[i - Code] = array[i];
            return result;
        }
        static char[] Decode_Reverse(char[] array)
        {
            char[] result = new char[array.Length];
            for (int i = 0; i < array.Length; i++)
                result[array.Length - 1 - i] = array[i];
            return result;
        }
        static char[] Decode_Swap(char[] array, int location)
        {
            location = location % array.Length;
            char temp = array[0];
            array[0] = array[location];
            array[location] = temp;
            return array;
        }
        static string Decode_Sp(string input, Encode_Method[] Methods)
        {
            char[] array = input.ToCharArray();
            foreach (Encode_Method m in Methods)
            {
                switch (m.Method)
                {
                    case Decode_Method.Reverse:
                        array = Decode_Reverse(array);
                        break;
                    case Decode_Method.Splice:
                        array = Decode_Slice(array, m.Parameter);
                        break;
                    case Decode_Method.Swap:
                        array = Decode_Swap(array, m.Parameter);
                        break;
                    default:
                        break;
                }
            }
            return new string(array);
        }
        static YouTube_Decode_Object Decode(JToken token, Encode_Method[] Methods)
        {
            int fmt = int.Parse(token["itag"].ToString());
            string Url = "";
            if (token["url"] != null)
                Url = Decode_Url(token["url"].ToString());
            else if (token["cipher"] != null)
            {
                JToken cipher = token["cipher"];
                NameValueCollection value = Decode_Query_String(cipher.ToString());
                string u = Decode_Url(value["url"].ToString());
                string sp = value["sp"].ToString();
                string key = Decode_Sp(value["s"].ToString(), Methods);
                Url = u + '&' + sp + '=' + key;
            }
            else if (token["signatureCipher"] != null)
            {
                JToken cipher = token["signatureCipher"];
                NameValueCollection value = Decode_Query_String(cipher.ToString());
                string u = Decode_Url(value["url"].ToString());
                string sp = value["sp"].ToString();
                string key = Decode_Sp(value["s"].ToString(), Methods);
                Url = u + '&' + sp + '=' + key;
            }
            return new YouTube_Decode_Object(Url);
        }



        static public YouTube_Decode_Object[] Get_YouTube_Link(string ID)
        {
            string Original_Html = "";
            string Content = "";
            Encode_Method[] Methods;
            try
            {
                Original_Html = Encoding.UTF8.GetString(w.DownloadData(@"https://www.youtube.com/watch?v=" + ID));
                Methods = YouTube_Js_Decoder.Get_Decode_Rule(Original_Html);
                Content = YouTube_API_Decoder.Get_API(Original_Html);
            }
            catch(Exception ex) { MessageBox.Show("無法取得媒體資訊，請確認網際網路連線"); return null; }
            try
            {
                JToken jt = JToken.Parse(Content);
                string Title = jt["videoDetails"]["title"].ToString();
                JToken classic = jt["streamingData"]["formats"];
                List<YouTube_Decode_Object> list = new List<YouTube_Decode_Object>();
                for (int i = 0; i < classic.Count(); i++)
                {
                    YouTube_Decode_Object obj = Decode(classic[i], Methods);
                    obj.Title = Title;
                    obj.Url += "&title=" + Encode_Url(Title);
                    obj.Second = double.Parse(classic[i]["approxDurationMs"].ToString()) * 0.001;
                    list.Add(obj);
                }
                return list.ToArray();
            }
            catch
            {
                MessageBox.Show("該媒體無法使用，可能是因為該影片設定為私人、限制級、或是僅供YouTube網站播放");
                return null;
            }
        }
        static string Decode_Html(string Content)
        {
            return HttpUtility.HtmlDecode(Content);
        }
        static string Decode_Url(string Content)
        {
            return HttpUtility.UrlDecode(Content);
        }
        static string Encode_Url(string Content)
        {
            return HttpUtility.UrlEncode(Content);
        }
        static NameValueCollection Decode_Query_String(string Content)
        {
            return HttpUtility.ParseQueryString(Content);
        }
    }
    class YouTube_API_Decoder
    {
        public static string Get_API(string Html)
        {
            string Result = YouTube_Str.Search_From_Input(@"var ytInitialPlayerResponse = .+?.</script>", Html);
            //Result = YouTube_Str.Search_From_Input(@"ytplayer\.config = .+?.ytplayer", Result);
            Result = Result.Substring(30, Result.Length - 40);
            return Result;
        }
    }
    class YouTube_Js_Decoder
    {
        static Encode_Method[] Get_Method(string Input)
        {
            string[] sp = Input.Split(';');
            string[] Result_Str = new string[sp.Length - 2];
            for (int i = 0; i < Result_Str.Length; i++)
                Result_Str[i] = sp[i + 1];
            Encode_Method[] Result = new Encode_Method[Result_Str.Length];
            for (int i = 0; i < Result_Str.Length; i++)
            {
                Result[i] = new Encode_Method();
                string[] decode = Result_Str[i].Split('.');
                string Class_Name = decode[0];
                string[] Method = decode[1].Split(',');
                string Method_Name = Method[0].Split('(')[0];
                string Method_Parameter = Method[1].Split(')')[0];
                Result[i].Class_Name = Class_Name;
                Result[i].Class_Method_Name = Method_Name;
                Result[i].Parameter = int.Parse(Method_Parameter);
            }
            return Result;
        }
        static Decode_Method Get_Decode_Method(string Method_Name, string input)
        {
            string[] content = YouTube_Str.Search_Values_From_Input(Method_Name + @":function\(a.+?.}", input);
            foreach (string Get_Method_Content in content)
            {
                if (YouTube_Str.Is_String_Content(Get_Method_Content, "c=a[0];a[0]=a[b%a.length];a[b%a.length]=c"))
                    return Decode_Method.Swap;
                if (YouTube_Str.Is_String_Content(Get_Method_Content, "splice"))
                    return Decode_Method.Splice;
                if (YouTube_Str.Is_String_Content(Get_Method_Content, "reverse"))
                    return Decode_Method.Reverse;
            }
            return Decode_Method.Null;
        }

        public static Encode_Method[] Get_Decode_Rule(string Input_Html)
        {
            //ytplayer\.config =.+?ytplayer
            WebClient w = new WebClient();

            string js_url = YouTube_Str.Search_From_Input(@"s\/player.+?.base\.js", Input_Html);
            Input_Html = w.DownloadString(@"https://www.youtube.com/" + js_url);
            w.Dispose();


            string Read_Result = YouTube_Str.Search_From_Input(@"a\.split\(\""\""\).+?.a\.join", Input_Html);
            Encode_Method[] Methods = Get_Method(Read_Result);
            foreach (Encode_Method m in Methods)
                m.Method = Get_Decode_Method(m.Class_Method_Name, Input_Html);
            return Methods;
        }
    }
    class YouTube_Str
    {
        public static string Search_From_Input(string Search_Key, string Input)
        {
            Regex regex = new Regex(Search_Key, RegexOptions.None);
            MatchCollection matches = regex.Matches(Input);
            return matches.Count == 0 ? "" : matches[0].Value.ToString();
        }
        public static string[] Search_Values_From_Input(string Search_Key, string Input)
        {
            Regex regex = new Regex(Search_Key, RegexOptions.None);
            MatchCollection matches = regex.Matches(Input);
            string[] Result = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
                Result[i] = matches[i].Value.ToString();
            return matches.Count == 0 ? null : Result;
        }
        public static bool Is_String_Content(string Input, string Target)
        {
            int length = Input.Length - Target.Length;
            for (int i = 0; i < length; i++)
                if (Input.Substring(i, Target.Length).Equals(Target))
                    return true;
            return false;
        }
    }
    public class YouTube_Search_Object
    {
        public string Title = "";
        public string ID = "";
        public string Image_Url = "";
        public string Length = "";
    }
    public class YouTube_Searcher
    {
        public static YouTube_Search_Object[] Search(string Keywrod)
        {
            // var request = HttpWebRequest.Create(@"https://www.youtube.com/results?search_query=" + Keywrod);
            //var result = request.GetResponse();
            //  Stream s = result.GetResponseStream();
            //     s.Length
            WebClient w = new WebClient();
            w.Headers.Add("user-agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36");
            string Data = Encoding.UTF8.GetString(w.DownloadData(@"https://www.youtube.com/results?search_query=" + Keywrod));
            string Cut_Data = YouTube_Str.Search_From_Input(@"ytInitialData.+?.};", Data);
            Cut_Data = Cut_Data.Substring(15, Cut_Data.Length - 16);

            JToken jt = JToken.Parse(Cut_Data);
            jt = jt["contents"]["twoColumnSearchResultsRenderer"]["primaryContents"]["sectionListRenderer"]["contents"];
            JArray array = (JArray)jt;
            JToken array_obj = array[0]["itemSectionRenderer"]["contents"];
            //string Next_Search = array[0]["itemSectionRenderer"]["continuations"][0]["nextContinuationData"]["continuation"].ToString();

            List<YouTube_Search_Object> result = new List<YouTube_Search_Object>();

            foreach (JObject content in array_obj.Children<JObject>())
            {
                foreach (JProperty prop in content.Properties())
                {
                    try
                    {
                        if (prop.Name == "videoRenderer")
                        {
                            JToken val = prop.Value;
                            string Video_ID = val["videoId"].ToString();
                            string Video_Title = val["title"]["runs"][0]["text"].ToString();
                            string Video_Length = val["lengthText"]["simpleText"].ToString();
                            string Image_Url = val["thumbnail"]["thumbnails"][0]["url"].ToString();
                            YouTube_Search_Object obj = new YouTube_Search_Object();
                            obj.ID = Video_ID;
                            obj.Title = Video_Title;
                            obj.Length = Video_Length;
                            obj.Image_Url = Image_Url;
                            result.Add(obj);
                        }
                    }
                    catch { continue; }
                }
            }
            return result.ToArray();
        }
    }
    public class YouTube_List_Search_Object
    {
        public string List_Name = "";
        public YouTube_Search_Object[] Search_Result = null;
        public YouTube_List_Search_Object(string List_Name, YouTube_Search_Object[] Search_Result)
        {
            this.List_Name = List_Name;
            this.Search_Result = Search_Result;
        }
    }
    public class YouTube_List_Searcher
    {
        public static YouTube_List_Search_Object Search(string List_Link)
        {
            WebClient w = new WebClient();
            w.Headers.Add("user-agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36");
            string Data = Encoding.UTF8.GetString(w.DownloadData(List_Link));
            string Cut_Data = YouTube_Str.Search_From_Input(@"window\[""ytInitialData""\].*?.;\n", Data);
            Cut_Data = Cut_Data.Substring(26, Cut_Data.Length - 28);
            JToken j = JToken.Parse(Cut_Data);
            string List_Name = j["microformat"]["microformatDataRenderer"]["title"].ToString();
            JArray Song_Array = (JArray)j["contents"]["twoColumnBrowseResultsRenderer"]["tabs"][0]["tabRenderer"]["content"]["sectionListRenderer"]["contents"][0]["itemSectionRenderer"]["contents"][0]["playlistVideoListRenderer"]["contents"];
            List<YouTube_Search_Object> result = new List<YouTube_Search_Object>();

            foreach (JObject content in Song_Array.Children<JObject>())
            {
                foreach (JProperty prop in content.Properties())
                {
                    if (prop.Name == "playlistVideoRenderer")
                    {
                        JToken val = prop.Value;
                        string Video_ID = val["videoId"].ToString();
                        string Video_Title = val["title"]["runs"][0]["text"].ToString();
                        string Video_Length = val["lengthText"]["simpleText"].ToString();
                        string Image_Url = val["thumbnail"]["thumbnails"][0]["url"].ToString();
                        YouTube_Search_Object obj = new YouTube_Search_Object();
                        obj.ID = Video_ID;
                        obj.Title = Video_Title;
                        obj.Length = Video_Length;
                        obj.Image_Url = Image_Url;
                        result.Add(obj);
                    }
                }
            }
            return new YouTube_List_Search_Object(List_Name, result.ToArray());
        }
    }
}
