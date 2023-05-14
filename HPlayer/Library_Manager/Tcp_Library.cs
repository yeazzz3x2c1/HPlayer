using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Tcp_Library
{
    public class Tcp_Base
    {
        public delegate void Receive_Event();
        public delegate void Disconnect_Event();
        public delegate void On_Receive_Message(params string[] Message);
        public event Receive_Event Start_Receive;
        public event Receive_Event End_Receive;
        public event Disconnect_Event On_Disconnect;
        public event On_Receive_Message On_Receive;
        public Socket socket = null;
        byte[] _Key = new byte[0];
        public Tcp_Base()
        {
            AES_Key = "Tcp_Lib";
        }
        public Tcp_Base(Socket sck)
        {
            socket = sck;
            AES_Key = "Tcp_Lib";
        }
        const byte Prefix_Byte = 2;
        const byte Suffix_Byte = 3;
        public void Listen_Socket()
        {
            BackgroundWorker bk = new BackgroundWorker() { WorkerReportsProgress = true };
            bk.DoWork += delegate
            {
                socket.ReceiveBufferSize = 4096;
                EndPoint ep = socket.RemoteEndPoint;
                List<byte> decode_container = new List<byte>();
                List<string> report_msg = new List<string>();
                bk.ReportProgress(0);
                try
                {
                    while (true)
                    {
                        byte[] receive_temp = new byte[1024];
                        int rec_len = socket.Receive(receive_temp);
                        //   Array.Resize(ref receive_temp, rec_len);
                        // msg_container.AddRange(receive_temp);

                        for (int i = 0; i < rec_len; i++)
                        {
                            byte temp = receive_temp[i];
                            if (temp == 3)
                            {
                                if (decode_container[0] == 2)
                                {
                                    decode_container.RemoveAt(0);
                                    try { report_msg.Add(AES_Lib.decrypt(decode_container.ToArray(), _Key)); }
                                    catch { }
                                }
                                decode_container.Clear();
                            }
                            else
                                decode_container.Add(temp);
                        }

                        if (report_msg.Count > 0)
                        {
                            bk.ReportProgress(2, report_msg.ToArray());
                            report_msg.Clear();
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine("Rec error: " + ex.Message + ", detal: " + ex.StackTrace); }//Console.WriteLine(ex.StackTrace); }
                bk.ReportProgress(1);
            };
            bk.ProgressChanged += (s, e) =>
            {
                switch (e.ProgressPercentage)
                {
                    case 0:
                        Start_Receive?.Invoke();
                        break;
                    case 1:
                        End_Receive?.Invoke();
                        socket.Close();
                        On_Disconnect?.Invoke();
                        socket.Dispose();
                        break;
                    case 2:
                        string[] text = (string[])e.UserState;
                        On_Receive?.Invoke(text);
                        break;
                }
            };
            bk.RunWorkerAsync();
        }

        public string AES_Key
        {
            get => Encoding.Unicode.GetString(_Key);
            set => _Key = Encoding.Unicode.GetBytes(value);
        }

        List<byte> Send_Temp = new List<byte>();
        public void Send(string Message)
        {
            try
            {
                Send_Temp.Add(Prefix_Byte);
                Send_Temp.AddRange(AES_Lib.encrypt(Message, _Key));
                Send_Temp.Add(Suffix_Byte);
                socket.Send(Send_Temp.ToArray());
                Send_Temp.Clear();
            }
            catch { }
        }
    }
    public class Tcp_Client : Tcp_Base
    {
        public delegate void Connect_Event(bool Result);
        public event Connect_Event Connection;
        public void Connect(IPEndPoint Destination)
        {
            BackgroundWorker bk = new BackgroundWorker() { WorkerReportsProgress = true };
            bk.DoWork += delegate
            {
                try
                {
                    if (socket != null)
                        socket.Dispose();
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(Destination);
                    bk.ReportProgress(0);
                }
                catch (Exception ex) { Console.WriteLine(ex.StackTrace); bk.ReportProgress(1); }
            };
            bk.ProgressChanged += (s, e) =>
            {
                switch (e.ProgressPercentage)
                {
                    case 0:
                        Listen_Socket();
                        Connection?.Invoke(true);
                        break;
                    case 1:
                        Connection?.Invoke(false);
                        break;
                }
            };
            bk.RunWorkerAsync();
        }
    }
    public class Tcp_Server : Tcp_Base
    {
        public delegate void Server_Build_Event(bool Result);
        public delegate void Client_Online_Event(Tcp_Base Client);
        public delegate void Client_Offline_Event(Tcp_Base Client);
        public delegate void Client_Respond_Event(Tcp_Base Client, string[] Message);
        public event Server_Build_Event On_Build;
        public event Client_Online_Event On_Client_Online;
        public event Client_Offline_Event On_Client_Offline;
        public event Client_Respond_Event On_Client_Respond;
        List<Tcp_Base> Client = new List<Tcp_Base>();
        public void Build_Up(IPEndPoint Remote_Point, int Connect_Limit)
        {
            BackgroundWorker bk = new BackgroundWorker() { WorkerReportsProgress = true };
            bk.DoWork += delegate
            {
                try
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Bind(Remote_Point);
                    socket.Listen(Connect_Limit);
                    bk.ReportProgress(0);
                    while (true)
                    {
                        Socket client_sck = socket.Accept();
                        bk.ReportProgress(2, client_sck);
                    }
                }
                catch { bk.ReportProgress(1); }
            };
            bk.ProgressChanged += (s, e) =>
            {
                switch (e.ProgressPercentage)
                {
                    case 0:
                        On_Build?.Invoke(true);
                        break;
                    case 1:
                        On_Build?.Invoke(false);
                        break;
                    case 2:
                        Tcp_Base client = new Tcp_Base((Socket)e.UserState);
                        #region Online
                        Client.Add(client);
                        Console.WriteLine("Online Count: " + Client.Count);
                        client.Listen_Socket();
                        On_Client_Online?.Invoke(client);
                        #endregion
                        client.On_Receive += (message) => { On_Client_Respond?.Invoke(client, message); };
                        #region Offline
                        client.End_Receive += delegate
                        {
                            Client.Remove(client);
                            On_Client_Offline?.Invoke(client);
                            Console.WriteLine("Offline Count: " + Client.Count);
                        };
                        #endregion
                        break;
                }
            };
            bk.RunWorkerAsync();
        }
        public int Get_Client_Count()
        {
            return Client.Count;
        }
        public void Send(Tcp_Base Client, string Message)
        {
            Client.Send(Message);
        }
        public void Broadcast(string Message)
        {
            foreach (Tcp_Base client in Client.ToArray())
                Send(client, Message);
        }
    }
    public static class AES_Lib
    {
        static RijndaelManaged AES;
        static MD5CryptoServiceProvider MD5;
        //AES 加密
        public static byte[] encrypt(string plainText, byte[] key)
        {
            return Encoding.ASCII.GetBytes(encrypt(Encoding.Unicode.GetBytes(plainText), key));
        }
        public static string encrypt(byte[] plainText, byte[] key)
        {
            AES = new RijndaelManaged();
            MD5 = new MD5CryptoServiceProvider();
            byte[] plainTextData = plainText;
            byte[] keyData = MD5.ComputeHash(key);
            byte[] IVData = MD5.ComputeHash(Encoding.Unicode.GetBytes("T!B@Y"));
            ICryptoTransform transform = AES.CreateEncryptor(keyData, IVData);
            byte[] outputData = transform.TransformFinalBlock(plainTextData, 0, plainTextData.Length);
            // return outputData;
            return Convert.ToBase64String(outputData);
        }
        public static string decrypt(byte[] cipherText, byte[] key)
        {
            return Encoding.Unicode.GetString(decrypt(Encoding.ASCII.GetString(cipherText), key));
        }
        public static byte[] decrypt(string cipherTextData, byte[] key)
        {
            byte[] temp = Convert.FromBase64String(cipherTextData);
            AES = new RijndaelManaged();
            MD5 = new MD5CryptoServiceProvider();
            byte[] keyData = MD5.ComputeHash(key);
            byte[] IVData = MD5.ComputeHash(Encoding.Unicode.GetBytes("T!B@Y"));
            ICryptoTransform transform = AES.CreateDecryptor(keyData, IVData);
            byte[] outputData = transform.TransformFinalBlock(temp, 0, temp.Length);
            return outputData;
        }
    }
}
