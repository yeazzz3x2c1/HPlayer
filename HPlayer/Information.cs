using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HPlayer
{
    static public class Information
    {
        /// <summary>
        /// 表示主要視窗之執行個體
        /// </summary>
        static public Player Main;
        /// <summary>
        /// 表示目前版本號
        /// </summary>
        static public string Version = "N1.0.0.0";
        public static class Member_Status
        {
            public static bool IsLogin = false;
            public static string ID = "";
        }
    }
}
