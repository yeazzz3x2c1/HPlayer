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

namespace HPlayer
{
    /// <summary>
    /// OnlineItem.xaml 的互動邏輯
    /// </summary>
    public partial class OnlineItem : UserControl
    {
        public OnlineItem()
        {
            InitializeComponent();
        }
        public string Video_ID { get; set; } = "";
        public string Video_URL { get; set; } = "";
        public string Video_Title { get => TitleL.Text; set => TitleL.Text = value; }
        public string Video_Length { get => LengthL.Text; set => LengthL.Text = value; }
        public string Image_URL { set => Img.Source = new BitmapImage(new Uri(value, UriKind.Absolute)); }
    }
}
