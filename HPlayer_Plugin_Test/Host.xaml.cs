using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HPlayer_Plugin_Song_Name_Display
{
    /// <summary>
    /// Host.xaml 的互動邏輯
    /// </summary>
    public partial class Host : UserControl
    {
        public Host()
        {
            InitializeComponent();
            HPlayer_Plugin.On_Song_Name_Update += HPlayer_Plugin_On_Song_Name_Update;
        }

        private void HPlayer_Plugin_On_Song_Name_Update(string Song_Name)
        {
            Song_Name_Display.Text = Song_Name;
        }
    }
}
