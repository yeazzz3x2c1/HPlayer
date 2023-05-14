using HPlayer_Plugin_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPlayer.Plugins
{
    class Plugin_Event_Trigger
    {
        public static void On_Song_Name_Change(string Song_Name)
        {
            foreach (HPlayer_Plugin_Interface plugin in Plugin_Manager.Get_All_Plugins())
            {
                try
                {
                    plugin.On_Now_Song_Name_Update(Song_Name);
                }
                catch { }
            }
        }
        public static void On_Song_Time_Change(string Song_Time)
        {
            foreach (HPlayer_Plugin_Interface plugin in Plugin_Manager.Get_All_Plugins())
            {
                try
                {
                    plugin.On_Now_Time_Update(Song_Time);
                }
                catch { }
            }
        }
    }
}
