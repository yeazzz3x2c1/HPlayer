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

namespace HPlayer.UI.Member
{
    public partial class Member_Register : Window
    {
        Player Host = null;
        public Member_Register(Player host)
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
        public void Clear()
        {
            Account.Text = "";
            Password.Password = "";
            Confirm_Password.Password = "";
            Email.Text = "";
        }
        private void Register_Clicked(object sender, RoutedEventArgs e)
        {
            if(Account.Text == "")
            {
                MessageBox.Show("帳號不能為空");
                return;
            }
            if (Password.Password == "")
            {
                MessageBox.Show("密碼不能為空");
                return;
            }
            if(Email.Text == "")
            {
                MessageBox.Show("電子郵件不能為空");
                return;
            }
            if(Password.Password != Confirm_Password.Password)
            {
                MessageBox.Show("密碼與確認密碼不一致");
                return;
            }
            Host.connection.Send_Register_Information(Account.Text, Password.Password, Email.Text);
            Register.IsEnabled = false;
        }
    }
}
