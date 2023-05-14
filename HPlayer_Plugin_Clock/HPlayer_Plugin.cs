using System;
using System.Windows;
using System.Windows.Controls;
using HPlayer_Plugin_Manager;

namespace HPlayer_Plugin_Clock
{
    public class Parameter
    {
        public static string Now_Song_Name = "";
    }
    public class HPlayer_Plugin : HPlayer_Plugin_Interface
    {
        public delegate void Song_Name_Update(string Song_Name);
        public static event Song_Name_Update On_Song_Name_Update;
        public string Plugin_Name()
        {
            return "指針時鐘2";
        }


        public UserControl Get_Control()
        {
            return new Clock_UserControl();
        }

        public void On_Now_Song_Name_Update(string Song_Name)
        {
            Parameter.Now_Song_Name = Song_Name;
        }

        public void On_Disabled()
        {
        }


        public void On_Enabled()
        {
 
        }


        public void On_Total_Time_Update(string Total_Time)
        {
        }

        public void On_Now_Time_Update(string Now_Time)
        {
        }

    }
}
