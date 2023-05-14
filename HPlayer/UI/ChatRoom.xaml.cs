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

namespace HPlayer
{
    /// <summary>
    /// ChatRoom.xaml 的互動邏輯
    /// </summary>
    public partial class ChatRoom : Window
    {
        Player Host = null;
        public ChatRoom(Player host)
        {
            Host = host;
            InitializeComponent();
            PowerOff.MouseDown += delegate { CloseWindow(); };
            Closing += (s, e) => { e.Cancel = true; CloseWindow(); };
        }
        public void CloseWindow()
        {
            Opacity = 0;
            Hide();
        }
        public void OpenWindow()
        {
            Opacity = 1;
            Show();
        }

        void On_Send(object sender, RoutedEventArgs e)
        {
            if (Information.Member_Status.IsLogin)
            {
                Host.connection.Send_Chat(Input.Text);
                Input.Text = "";
            }
            else
                MessageBox.Show("請先登入後再發言");
        }
        public void On_Receive(string Time, string User, string Message)
        {
            Chat_List.Add("[" + Time + "] " + User + ": " + Message, false, 25.0);
            while (Chat_List.Items.Count > 100)
                Chat_List.RemoveAt(0);
            Chat_List.Add_Scroll_Force(-120);
        }
        public void Clear_Chat()
        {
            Chat_List.Clear();
        }
        private void Key_Down(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                On_Send(null, null);
        }
    }
}
