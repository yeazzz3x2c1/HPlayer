using HPlayer_Plugin_Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace HPlayer.Plugins
{
    class Plugin_Manager
    {
        private static List<HPlayer_Plugin_Interface> All_Plugins = new List<HPlayer_Plugin_Interface>();
        static private void Load_Plugin(string Path)
        {
            try
            {
                Assembly asmb = Assembly.LoadFrom(Path);
                foreach (Type t in asmb.ExportedTypes)
                {
                    string name = t.FullName;
                    foreach (Type interfaces in t.GetInterfaces())
                    {
                        string interfaces_name = interfaces.Name;
                        if (interfaces_name.Equals("HPlayer_Plugin_Interface"))
                        {
                            HPlayer_Plugin_Interface plugin = (HPlayer_Plugin_Interface)asmb.CreateInstance(name);
                            All_Plugins.Add(plugin);
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("讀取插件發生問題: \n" + ex.Message + "\n\n於" + Path); }
        }
        public static void Initialize_Plugins()
        {

            try
            {
                All_Plugins.Clear();
                string[] Files = Directory.GetFiles("Plugins", "*.dll");
                for (int i = 0; i < Files.Length; i++)
                {
                    Load_Plugin(Files[i]);
                }
            }
            catch (Exception ex) { MessageBox.Show("載入插件時發生問題\n" + ex.Message); }
        }
        public static HPlayer_Plugin_Interface[] Get_All_Plugins() => All_Plugins.ToArray();
    }
}
