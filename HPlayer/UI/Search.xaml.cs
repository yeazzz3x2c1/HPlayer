using System;
using System.Collections.Generic;
using System.Web;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using YFH_YouTube_Library;

namespace HPlayer
{
    /// <summary>
    /// Search.xaml 的互動邏輯
    /// </summary>
    public partial class Search : Window
    {
        Player host;
        OnlineItem NowSelect = null;
        public Search(Player host)
        {
            this.host = host;
            Opacity = 0;
            Loaded += (s, e) =>
            {
                Scroll.Opacity = 0;
                Scroll.OnValueChange += delegate
                {
                    ResultContianer.Margin = new Thickness(0, -Scroll.Value, 10, 0);
                };
                SearchResultContainer.PreviewMouseWheel += (ss, ee) => { Scroll.AddForce(ee.Delta); };
            };
            InitializeComponent();
            Closing += (s, e) => { e.Cancel = true; CloseWindow(); };
        }
        void StartSearch(object sender, EventArgs e)
        {
            SearchYouTube();
        }
        OnlineItem Get_Online_Item(YouTube_Search_Object Search_Item)
        {
            OnlineItem oi = new OnlineItem() { Image_URL = Search_Item.Image_Url, Video_Length = Search_Item.Length, Video_Title = HttpUtility.HtmlDecode(Search_Item.Title), Video_URL = "https://www.youtube.com/watch?v=" + Search_Item.ID, Video_ID = Search_Item.ID };
            oi.MouseDoubleClick += (ss, ee) =>
            {
                YouTube_Decode_Object[] obj = YouTube_Decoder.Get_YouTube_Link((ss as OnlineItem).Video_ID);
                Console.WriteLine(obj[0].Url.ToString());
                host.StartPlaySong(obj[0]);
            };
            oi.PreviewMouseDown += (ss, ee) =>
            {
                if (NowSelect != null) NowSelect.Background = new SolidColorBrush(Colors.Transparent);
                NowSelect = ss as OnlineItem;
                NowSelect.Background = BorderBrush;
                if (ee.RightButton == MouseButtonState.Pressed)
                {
                    OnlineRight right = new OnlineRight(this, oi);
                    right.Left = Left + ee.GetPosition(this).X;
                    right.Top = Top + ee.GetPosition(this).Y;
                    right.Show();
                }
            };
            return oi;
        }
        void Refresh_Search_Result_UI(OnlineItem[] Result_Items)
        {
            ResultContianer.Children.Clear();
            NowSelect = null;
            SearchTip.Text = "Processing...";

            foreach (OnlineItem Item in Result_Items)
                ResultContianer.Children.Add(Item);
            if (ResultContianer.Children.Count == 0)
            {
                SearchTip.Text = "沒有相關的搜尋結果";
                SearchTip.Visibility = Visibility.Visible;
            }
            else
                SearchTip.Visibility = Visibility.Collapsed;
            ResultContianer.UpdateLayout();
            if (ResultContianer.ActualHeight > SearchResultContainer.ActualHeight)
            {
                Scroll.Value = 0;
                Scroll.MaxValue = ResultContianer.ActualHeight - SearchResultContainer.ActualHeight;
                Scroll.Opacity = 1;
            }
            else
                Scroll.Opacity = 0;
        }

        YouTube_Search_Object[] Get_Result(string Input_Key)
        {

            if (YouTube_Str.Search_From_Input(@"playlist\?list.+?", Input_Key) == "")
                return YouTube_Searcher.Search(SearchKey.Text);
            else
                return YouTube_List_Searcher.Search(Input_Key).Search_Result;
        }
        void SearchYouTube()
        {

            YouTube_Search_Object[] result = Get_Result(SearchKey.Text);
            List<OnlineItem> Items = new List<OnlineItem>();
            foreach (YouTube_Search_Object r in result)
                Items.Add(Get_Online_Item(r));
            Refresh_Search_Result_UI(Items.ToArray());
        }
        public void CloseWindow()
        {
            Opacity = 0;
            Hide();
        }
        public void OpenWindow()
        {
            Opacity = 1;
            Show();
        }
        void OnCloseWindowMouseDown(object sender, EventArgs e)
        {
            CloseWindow();
        }
        private void SearchEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) SearchYouTube();
        }
    }
}
