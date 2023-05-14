namespace Procotol_Base
{
    public enum Flag_Type
    {
        Version,
        Check_Update,
        Broadcast,
        Register,
        Register_Result,
        Login,
        Login_Result,
        Chat,
        Upload_Song,
        Upload_Song_Result,
        Download_Song,
        Download_Song_Result,
        Remove_Song,
        Remove_Song_Result,
        NULL
    }
    class Procotol
    {

        public static class Flag
        {
            public static Flag_Type Get_Flag_Type(string Flag)
            {
                switch (Flag)
                {
                    case "version":
                        return Flag_Type.Version;
                    case "check_update":
                        return Flag_Type.Check_Update;
                    case "broadcast":
                        return Flag_Type.Broadcast;
                    case "register":
                        return Flag_Type.Register;
                    case "register_result":
                        return Flag_Type.Register_Result;
                    case "login":
                        return Flag_Type.Login;
                    case "login_result":
                        return Flag_Type.Login_Result;
                    case "chat":
                        return Flag_Type.Chat;
                    case "upload_song":
                        return Flag_Type.Upload_Song;
                    case "upload_song_result":
                        return Flag_Type.Upload_Song_Result;
                    case "download_song":
                        return Flag_Type.Download_Song;
                    case "download_song_result":
                        return Flag_Type.Download_Song_Result;
                    case "remove_song":
                        return Flag_Type.Remove_Song;
                    case "remove_song_result":
                        return Flag_Type.Remove_Song_Result;
                    default:
                        return Flag_Type.NULL;
                }
            }
            public static string Get_Flag_String(Flag_Type type)
            {
                switch (type)
                {
                    case Flag_Type.Version:
                        return "version";
                    case Flag_Type.Check_Update:
                        return "check_update";
                    case Flag_Type.Broadcast:
                        return "broadcast";
                    case Flag_Type.Register:
                        return "register";
                    case Flag_Type.Register_Result:
                        return "register_result";
                    case Flag_Type.Login:
                        return "login";
                    case Flag_Type.Login_Result:
                        return "login_result";
                    case Flag_Type.Chat:
                        return "chat";
                    case Flag_Type.Upload_Song:
                        return "upload_song";
                    case Flag_Type.Upload_Song_Result:
                        return "upload_song_result";
                    case Flag_Type.Download_Song:
                        return "download_song";
                    case Flag_Type.Download_Song_Result:
                        return "download_song_result";
                    case Flag_Type.Remove_Song:
                        return "remove_song";
                    case Flag_Type.Remove_Song_Result:
                        return "remove_song_result";
                    default:
                        return "null";
                }
            }
        }
    }
}
