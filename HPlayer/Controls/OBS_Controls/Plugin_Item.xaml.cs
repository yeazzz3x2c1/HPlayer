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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HPlayer.Controls.OBS_Controls
{
    /// <summary>
    /// Plugin_Item.xaml 的互動邏輯
    /// </summary>
    public partial class Plugin_Item : UserControl
    {
        public Plugin_Item()
        {
            InitializeComponent();
        }
        public string Plugin_Name { get => Plugin_NameL.Text; set => Plugin_NameL.Text = value; }
        public HPlayer_Plugin_Interface Plugin_Interface { get; set; }
    }
}
