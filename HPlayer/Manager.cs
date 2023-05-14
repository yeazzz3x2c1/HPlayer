using System;
using System.Collections;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ManagerTool
{
    public static class Manager
    {
        public static void LoadSetting(ref Hashtable SettingTable, string SettingName)
        {
            string path = @"Setting\" + SettingName + ".musicbox";
            if (File.Exists(path))
            {
                try
                {
                    StreamReader r = new StreamReader(path);
                    string[] buf;
                    while (r.Peek() > -1)
                    {
                        buf = r.ReadLine().Split(':');
                        SetSetting(ref SettingTable, buf[0], buf[1]);
                    }
                }
                catch { }
            }
        }
        public static void SaveSetting(ref Hashtable SettingTable, string SettingName)
        {
            try
            {
                string Path = @"Setting\" + SettingName + ".musicbox";
                StreamWriter w = new StreamWriter(Path, false);
                foreach (string i in SettingTable.Keys)
                    w.WriteLine(i + ":" + SettingTable[i]);
                w.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        public static void SetSetting(ref Hashtable SettingTable, string Key, object Value)
        {
            try
            {
                if (SettingTable.ContainsKey(Key))
                    SettingTable[Key] = Value;
                else SettingTable.Add(Key, Value);
            }
            catch { }
        }
        public static void AddMoveEvent(Window W)
        {
            double x = 0, y = 0;
            void OnMove(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    W.Left += e.GetPosition(W).X - x;
                    W.Top += e.GetPosition(W).Y - y;
                }
                else
                    W.MouseMove -= OnMove;
            }
            W.MouseDown += (s, e) =>
            {
                if ((s as Window).IsMouseCaptureWithin) return;
                W.MouseMove += OnMove;
                x = e.GetPosition(s as Window).X;
                y = e.GetPosition(s as Window).Y;
                W.CaptureMouse();
            };
            W.MouseUp += (s, e) =>
            {
                W.MouseMove -= OnMove; W.ReleaseMouseCapture();
            };
        }
    }
}
