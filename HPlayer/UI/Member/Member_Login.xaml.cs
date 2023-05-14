using HPlayer.Library_Manager;
using System;
using System.Windows;

namespace HPlayer.UI.Member
{
    /// <summary>
    /// Member.xaml 的互動邏輯
    /// </summary>
    public partial class Member_Login : Window
    {
        Player Host;
        YFH_Timer Open_Btn = new YFH_Timer() { Interval = new TimeSpan(0, 0, 2) };
        public Member_Login(Player host)
        {
            Host = host;
            InitializeComponent();
            PowerOff.MouseDown += delegate { CloseWindow(); };
            Closing += (s, e) => { e.Cancel = true; CloseWindow(); };
            Open_Btn.Tick += delegate { Login.IsEnabled = true; Open_Btn.Stop(); };
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Host.member_register.OpenWindow();
            CloseWindow();
        }

        private void Login_Clicked(object sender, RoutedEventArgs e)
        {
            Host.connection.Send_Login_Information(Account.Text, Password.Password);
            Login.IsEnabled = false;
            Open_Btn.Start();
        }
        public void Clear()
        {
            Account.Text = "";
            Password.Password = "";
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
    }
}
