using HPlayer.Controls.OBS_Controls;
using HPlayer.Plugins;
using HPlayer_Plugin_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HPlayer.UI
{
    /// <summary>
    /// OBS_Stream_Setting.xaml 的互動邏輯
    /// </summary>
    public partial class OBS_Stream_Setting : Window
    {
        Stream_Output s = new Stream_Output();
        public OBS_Stream_Setting()
        {
            InitializeComponent();
            Loaded += delegate
            {
                s.Show();
                s.Start_Render(Output_Area);
                foreach (HPlayer_Plugin_Interface plugin in Plugin_Manager.Get_All_Plugins())
                {
                    Plugin_Item p = new Plugin_Item() { Margin = new Thickness(5) };
                    p.Plugin_Name = plugin.Plugin_Name();
                    p.Plugin_Interface = plugin;
                    p.MouseDoubleClick += delegate { Register_Plugin_Control(p.Plugin_Interface); };
                    Plugin_List.Add(p);
                }
            };
            Closing += delegate { s.Close(); };
        }
        void Register_Plugin_Control(HPlayer_Plugin_Interface Plugin)
        {
            UserControl u = Plugin.Get_Control();
            if (u != null)
            {
                u.HorizontalAlignment = HorizontalAlignment.Left;
                u.VerticalAlignment = VerticalAlignment.Top;
                u.MouseRightButtonDown += delegate { Stream_Output_Grid.Children.Remove(u); };
                bool Can_Move = false;
                Point Previous_Point = new Point(0, 0);
                u.MouseDown += (s, e) =>
                {
                    u.CaptureMouse();
                    Previous_Point.X = e.GetPosition(u).X;
                    Previous_Point.Y = e.GetPosition(u).Y;
                    Can_Move = true;
                };
                u.MouseMove += (s, e) =>
                {
                    if (Can_Move)
                    {
                        double X = e.GetPosition(u).X;
                        double Y = e.GetPosition(u).Y;
                        Thickness t = u.Margin;
                        t.Left += X - Previous_Point.X;
                        t.Top += Y - Previous_Point.Y;
                        u.Margin = t;
                    }
                };
                u.MouseUp += delegate { u.ReleaseMouseCapture(); Can_Move = false; };
                Stream_Output_Grid.Children.Add(u);
            }
        }
    }
}
