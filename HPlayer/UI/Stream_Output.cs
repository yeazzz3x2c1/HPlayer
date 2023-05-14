using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace HPlayer.UI
{
    public partial class Stream_Output : Form
    {
        public Stream_Output()
        {
            InitializeComponent();
        }
        public void Start_Render(Visual v)
        {
            Render_Output_Block.Set_Visual(v);
        }
    }
}
