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

namespace HPlayer.List_Controls
{
    /// <summary>
    /// View_List.xaml 的互動邏輯
    /// </summary>
    public partial class View_List : UserControl
    {
        public View_List()
        {
            InitializeComponent();
            Loaded += delegate
            {
                Scroll.Opacity = 0;
                Scroll.OnValueChange += delegate
                {
                    Items_Container.Margin = new Thickness(0, -Scroll.Value, 10, 0);
                };
                Host_Container.PreviewMouseWheel += (ss, ee) => { Scroll.AddForce(ee.Delta); };
            };
        }

        public void Clear()
        {
            Items_Container.Children.Clear();
        }

        public void Add(UserControl Control)
        {
            Items_Container.Children.Add(Control);
            Items_Container.UpdateLayout();
            if (Items_Container.ActualHeight > Host_Container.ActualHeight)
            {
                Scroll.Value = 0;
                Scroll.MaxValue = Items_Container.ActualHeight - Host_Container.ActualHeight;
                Scroll.Opacity = 1;
            }
            else
                Scroll.Opacity = 0;
        }
    }
}
