using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Net;
using Procotol_Base;
using Tcp_Library;

namespace HPlayer.Library_Manager
{
    public class Connection : Tcp_Library.Tcp_Client
    {
        public Connection()
        {
            Reconnect.Tick += delegate
            {
                if (!socket.Connected)
                    Connect();
            };
            Reconnect.Tick += delegate
            {
                if (socket.Connected)
                    Send("Connection test");
            };
            Reconnect.Start();
        }
        YFH_Timer Reconnect = new YFH_Timer() { Interval = new TimeSpan(0, 0, 10) };
        YFH_Timer Check_Connection = new YFH_Timer() { Interval = new TimeSpan(0, 0, 10) };
        public void Set_Reconnect_Time(int Seconds) => Reconnect.Interval = new TimeSpan(0, 0, Seconds);
        public void Auto_Reconnect_Enabled(bool Enabled)
        {
            if (Enabled)
                Reconnect.Start();
            else
                Reconnect.Stop();
        }

        public void Connect()
        {
            Connect(new IPEndPoint(Dns.GetHostAddresses("musicplayer.thebestyea.net")[0], 30971));
        }
        public void Send(string Flag, string Message)
        {
            Send(Flag + "," + Message);
        }
        public void Send(Flag_Type type, string Message)
        {
            Send(Procotol.Flag.Get_Flag_String(type) + "," + Message);
        }
        public void Send_Player_Version()
        {
            Send(Flag_Type.Version, Information.Version);
        }
        public void Send_Check_Version(string Value)
        {
            Send(Flag_Type.Check_Update, Value);
        }
        public void Send_Login_Information(string Username, string Password)
        {
            byte[] pwd = Encoding.Unicode.GetBytes(Password);
            Send(Flag_Type.Login, Username + "," + AES_Lib.encrypt(pwd, pwd));
        }
        public void Send_Register_Information(string Username, string Password, string Email)
        {
            byte[] pwd = Encoding.Unicode.GetBytes(Password);
            Send(Flag_Type.Register, Username + "," + AES_Lib.encrypt(pwd, pwd) + "," + Email);
        }
        public void Send_Chat(string Chat)
        {
            Send(Flag_Type.Chat, Information.Member_Status.ID + "," + Chat);
        }
        public void Send_Upload_Song(string Song_Name, string Song_File)
        {
            Send(Flag_Type.Upload_Song, Information.Member_Status.ID + "," + Song_Name + "," + Song_File);
        }
        public void Send_Download_Song(string Song_Key)
        {
            Send(Flag_Type.Download_Song, Information.Member_Status.ID + "," + Song_Key);
        }
        public void Send_Remove_Song(string Song_Key)
        {
            Send(Flag_Type.Remove_Song, Information.Member_Status.ID + "," + Song_Key);
        }
    }
}
