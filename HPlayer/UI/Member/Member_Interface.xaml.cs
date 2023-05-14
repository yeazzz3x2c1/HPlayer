using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class Member_Interface : Window
    {
        Player Host = null;
        public Member_Interface(Player host)
        {
            Host = host;
            InitializeComponent();
            Loaded += delegate
            {
                SongList.ItemDoubleClick += (ss, ee) =>
                {
                    Upload_State.Text = "檔案接收中...";
                    host.connection.Send_Download_Song(ee.Item.Path.Split(':')[0]);
                };
            };
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

        private void On_Upload(object sender, RoutedEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog() { AddExtension = true, Title = "上傳歌曲", Filter = "多媒體|*.mp3;*.mp4;*.wav;*.wma;*.m4a;*.avi|所有檔案|*.*" };
            if (f.ShowDialog() == true)
            {
                using (FileStream stream = new FileStream(f.FileName, FileMode.Open))
                {
                    byte[] trans = new byte[stream.Length];
                    stream.Read(trans, 0, trans.Length);
                    Upload_State.Text = "Sending...";
                    Host.connection.Send_Upload_Song(System.IO.Path.GetFileNameWithoutExtension(f.FileName), Convert.ToBase64String(trans));
                }
            }
        }
        public void Add_Song(string Song_Name, string Song_Key)
        {
            SongList.Add(Song_Key + ":\\" + Song_Name + ".YouTube");
        }
        Dictionary<string, PlayerListItem> Remove_Queue = new Dictionary<string, PlayerListItem>();
        public void Remove_Song(string key)
        {
            SongList.Remove(Remove_Queue[key]);
        }
        private void On_Deleted(object sender, RoutedEventArgs e)
        {
            Remove_Queue.Clear();
            foreach (PlayerListItem l in SongList.AllSelected)
            {
                string key = l.Path.Split(':')[0];
                Remove_Queue.Add(key, l);
                Host.connection.Send_Remove_Song(key);
            }
        }
    }
}