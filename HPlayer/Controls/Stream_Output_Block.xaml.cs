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

namespace HPlayer.Controls
{
    /// <summary>
    /// Stream_Output_Block.xaml 的互動邏輯
    /// </summary>
    public partial class Stream_Output_Block : UserControl
    {
        public Stream_Output_Block()
        {
            InitializeComponent();
        }
        public void Set_Visual(Visual s)
        {
            Copy.Visual = s;
        }
    }
}
