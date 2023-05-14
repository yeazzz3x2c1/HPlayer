using HPlayer.Library_Manager;
using HPlayer.Plugins;
using HPlayer.UI;
using HPlayer.UI.Member;
using ManagerTool;
using Microsoft.Win32;
using Procotol_Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Un4seen.Bass;
using YFH_YouTube_Library;
using static HPlayer.EQ;
using static HPlayer.MyLib;

namespace HPlayer
{
    public partial class Player : Window
    {

        #region Window
        public Member_Interface member_interface;
        public Member_Login member_Login;
        public Member_Register member_register;
        public ChatRoom chat_room;
        /// <summary>
        /// 設定視窗
        /// </summary>
        Setting setting;
        /// <summary>
        /// 線上搜尋視窗
        /// </summary>
        Search search;
        #endregion
        #region Hashtable List
        //Volume 音量
        //List 最後選擇的清單(啟動時預設清單)
        #endregion
        #region Parameter
        Random RandomItem = new Random();
        /// <summary>
        /// 紀錄上一次清單名稱，用於判斷隨機播放清單是否要刷新
        /// </summary>
        string LastListName = "";

        /// <summary>
        /// 用於暫存隨機播放之清單
        /// </summary>
        List<PlayerListItem> RandomList = new List<PlayerListItem>();

        /// <summary>
        /// 播放狀態
        /// </summary>
        BASSActive Status = 0;
        /// <summary>
        /// 是否顯示清單(false=顯示歌曲列表，true=顯示清單列表)
        /// </summary>
        bool ShowAllList = false;
        public Connection connection = new Connection();
        Hashtable SettingTable = new Hashtable();
        public static Un4seen.Bass.Misc.Visuals VIS = new Un4seen.Bass.Misc.Visuals();
        RotateTransform r = new RotateTransform();
        /// <summary>
        /// 表單淡入Timer物件
        /// </summary>
        YFH_Timer ShowTimer = new YFH_Timer() { Interval = new TimeSpan(250000) };
        /// <summary>
        /// 清單與歌曲列表切換Timer物件
        /// </summary>
        YFH_Timer ListSwitchTimer = new YFH_Timer() { Interval = new TimeSpan(10000) };
        int SD;
        /// <summary>
        /// 紀錄進度條按下時播放狀態
        /// </summary>
        bool IsMouseDown = false;
        #endregion
        #region PlayRowT 按鈕旋轉
        YFH_Timer PlayRowT = new YFH_Timer();
        bool POP = true;
        #endregion
        #region SongInfo
        YFH_Timer SongInfo = new YFH_Timer();
        double NowTimeD = 0;
        double AllTimeD = 0;
        string NowTimeS = "00:00";
        string AllTimeS = "00:00";
        #endregion
        #region VolumeInfo
        #endregion
        #region Play Mode
        enum PlayMode
        {
            Single,
            List,
            Random
        }
        PlayMode NowMode = PlayMode.Random;
        List<ImageSource> ModeImage = new List<ImageSource>();
        void OnModeMouseDown(object sender, EventArgs e)
        {
            NowMode = NowMode == PlayMode.Random ? PlayMode.Single : NowMode + 1;
            SetPlayMode(NowMode);
        }
        void SetPlayMode(PlayMode PlayMode)
        {
            Mode.Source = ModeImage[(int)PlayMode];
            SetSetting("PlayMode", (int)PlayMode);
        }
        #endregion
        #region Hashtable Control
        //object GetSetting(string Key) { try { return SettingTable[Key]; } catch { return null; } }
        float GetSettingFloat(string Key)
        {
            try
            {
                if (!SettingTable.ContainsKey(Key))
                    SetSetting(Key, -1);
                return Convert.ToSingle(SettingTable[Key]);
            }
            catch { return -1; }
        }
        string GetSettingString(string Key)
        {
            try
            {
                if (!SettingTable.ContainsKey(Key))
                    SetSetting(Key, "");
                return Convert.ToString(SettingTable[Key]);
            }
            catch { return ""; }
        }

        float GetVolume() => GetSettingFloat("Volume");
        System.Windows.Forms.NotifyIcon NF = null;
        #endregion
        #region Initialize
        public Player()
        {
            Information.Main = this;
            //  Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 1 });
            InitializeComponent();
            InitEvents();
            Opacity = 0;
        }
        void InitEvents()
        {
            ShowTimer.Tick += new EventHandler(OnShowTime);
            ListSwitchTimer.Tick += new EventHandler(OnListSwitch);
            Loaded += new RoutedEventHandler(OnPlayerLoaded);
            Closing += new CancelEventHandler(OnPlayerClosing);
            PowerOff.MouseDown += (ss, ee) => ClosePlayer();
            PowerOffGrid.MouseDown += (ss, ee) => ClosePlayer();
            Minimum.MouseDown += (ss, ee) => WindowState = WindowState.Minimized;
            MinimumGrid.MouseDown += (ss, ee) => WindowState = WindowState.Minimized;
            SongList.ItemDoubleClick += (ss, ee) => { StartPlaySong(ee.Item); };
            AllList.ItemDoubleClick += (ss, ee) => { SetList(ee.Item.Text); ChangeListStatus(false); };
            MusicPogress.BarMouseDown += OnProgressMouseDown;
            MusicPogress.BarMouseMove += OnProgressMouseMove;
            MusicPogress.BarMouseUp += OnProgressMouseUp;
            VolumeBar.OnValueChange += VolumeBarValueChange;
            VolumeBar.OnIconChange += (s, e) => { VolumeImage.Source = e.Source; };
        }
        void SaveList() => SaveList(GetSettingString("List"));
        void SaveList(string ListName)
        {
            try
            {
                string path = @"List\" + ListName + ".musiclist";
                using (StreamWriter w = new StreamWriter(path, false))
                {
                    foreach (PlayerListItem i in SongList.Items)
                        w.WriteLine(i.Path);
                    w.Close();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); MessageBox.Show("發生錯誤", ex.Message); }
        }

        //台灣未來的海洋的貨幣
        //台灣海洋的未來的貨幣


        void Init()
        {
            Play.RenderTransformOrigin = new Point(0.5, 0.5);
            Play.UseLayoutRounding = true;
            PlayRowT.Interval = new TimeSpan(300000);
            PlayRowT.Tick += new EventHandler(PR);
            // CompositionTarget.Rendering += PR;
            SongInfo.Interval = new TimeSpan(100000);
            SongInfo.Tick += new EventHandler(UpdateInfo);
            CompositionTarget.Rendering += delegate { spectrum.Update_Spectrum(strm); };
            SongInfo.Start();
            spectrum.Visuals = VIS;
            LoadSD();
            InitFolder();
            InitSetting();
        }
        void InitFolder()
        {
            if (!Directory.Exists("Setting")) Directory.CreateDirectory("Setting");
            if (!Directory.Exists("List")) Directory.CreateDirectory("List");
            if (!Directory.Exists("Plugins")) Directory.CreateDirectory("Plugins");
        }
        void LoadSetting() => Manager.LoadSetting(ref SettingTable, "Setting");
        void SetSetting(string key, object value) => Manager.SetSetting(ref SettingTable, key, value);
        void SetParamater()
        {
            string key, value;
            float vol;
            List<string> keys = new List<string>();
            foreach (string Tkey in SettingTable.Keys)
                keys.Add(Tkey);
            for (int i = 0; i < keys.Count; i++)
            {
                key = keys[i];
                value = SettingTable[key].ToString();
                switch (key)
                {
                    case "Volume":
                        vol = float.Parse(value);
                        VolumeBar.Value = vol == -1 ? 100 : vol;
                        break;
                    case "PlayMode":
                        SetPlayMode((PlayMode)int.Parse(value));
                        break;
                }
            }
            keys.Clear();
            keys = null;
        }
        void LoadList()
        {
            try
            {
                var Files = Directory.GetFiles("List", "*.musiclist");
                if (Files.Length == 0)
                {
                    StreamWriter w = new StreamWriter(@"List\My List.musicList", true);
                    w.WriteLine();
                    w.Close();
                    SetSetting("List", "My List");
                    LoadList();
                }
                else
                {
                    foreach (string i in Files)
                        try { AllList.Add(i); }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        void SetList(string ListName)
        {
            if (ListName == "") ListName = "My List";
            SongList.Clear();
            try
            {
                string path = @"List\" + ListName + ".musiclist";
                List_Name.Content = ListName;
                if (File.Exists(path))
                    using (StreamReader r = new StreamReader(path))
                    {
                        while (r.Peek() > -1)
                            SongList.Add(r.ReadLine());
                        r.Close();
                    }
                string lastList = GetSettingString("List");
                if (LastListName != lastList) LastListName = lastList;
                SetSetting("List", ListName);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); MessageBox.Show(ex.Message, "發生錯誤"); }
        }
        void InitSetting()
        {
            LoadSetting();
            LoadList();
            SetParamater();
            SetList(GetSettingString("List"));
        }
        void LoadSD()
        {
            int n = 0;
            BASS_DEVICEINFO info = new BASS_DEVICEINFO();
            SoundCard.Items.Clear();
            while (Bass.BASS_GetDeviceInfo(n, info))
            {
                SoundCard.Items.Add(info.ToString());
                if (info.IsDefault) SoundCard.SelectedIndex = n;
                Bass.BASS_Init(n, 44100, BASSInit.BASS_DEVICE_DEFAULT, (IntPtr)Process.GetCurrentProcess().MainWindowHandle.ToInt32());
                n += 1;
            }
        }
        #endregion
        #region Method
        public void ResetRandomList()
        {
            Console.WriteLine("Reset random list");
            int index = 0;
            RandomList.Clear();
            List<PlayerListItem> buffer = new List<PlayerListItem>(SongList.Items);
            for (int i = 0; i < SongList.Items.Count; i++)
            {
                index = RandomItem.Next(0, buffer.Count);
                if (index > buffer.Count - 1) index = buffer.Count - 1;
                RandomList.Add(buffer[index]);
                buffer.RemoveAt(index);
            }
        }

        public void StartPlaySong(PlayerListItem SongItem)
        {
            if (SongItem == null) return;
            if (!SongItem.FileExist) return;
            LastPlayItem = SongItem;
            StartPlaySong(SongItem.Path);
            SongList.ClearSelected();
        }
        public void StartPlaySong(YouTube_Decode_Object YTObj)
        {
            if (YTObj == null)
                return;
            StartPlaySong(YTObj.Url, true);
            Set_Display_Song_Name(YTObj.Title);
        }
        public void StartPlaySong(string pR)
        {
            bool IsYouTube = Path.GetExtension(pR) == ".YouTube";
            if (IsYouTube)
            {
                StartPlaySong(YouTube_Decoder.Get_YouTube_Link(pR.Split(':')[0])?[0].Url, true);
                Set_Display_Song_Name(Path.GetFileNameWithoutExtension(pR));
            }
            else
                StartPlaySong(pR, false);
        }
        public void Stop_Play_Song()
        {
            if (strm != 0) Bass.BASS_StreamFree(strm);
        }
        public void StartPlaySong(string pR, bool OnlineP)
        {
            if (strm != 0) Bass.BASS_StreamFree(strm);
            strm = 0;
            Bass.BASS_SetDevice(SD);
            if (OnlineP)
            {
                strm = Bass.BASS_StreamCreateURL(pR, 0, BASSFlag.BASS_DEFAULT, null, IntPtr.Zero);
            }
            else
            {
                if (System.IO.File.Exists(pR) == false) return;
                strm = Bass.BASS_StreamCreateFile(pR, 0, 0, BASSFlag.BASS_DEFAULT);
                Set_Display_Song_Name(Path.GetFileNameWithoutExtension(pR));
            }
            AllTimeD = Bass.BASS_ChannelBytes2Seconds(strm, Bass.BASS_ChannelGetLength(strm, BASSMode.BASS_POS_BYTES));
            MusicPogress.MaxmumValue = AllTimeD;
            string t = TimeSpan.FromSeconds(Math.Round(AllTimeD)).ToString();
            AllTimeS = t.Substring(t.Length - 5, 5);
            SetVolume((float)(Math.Pow(GetVolume(), 2.5) * 0.001));
            Bass.BASS_ChannelPlay(strm, true);
            PlayRowT.Start();
        }
        public void Set_Display_Song_Name(string Song_Name)
        {
            SongName.Text = Song_Name;
            Plugin_Event_Trigger.On_Song_Name_Change(Song_Name);
        }
        public void PreviousSong() => StartPlaySong(FindPreviousSong(LastPlayItem));
        public void NextSong() => StartPlaySong(FindNextSong(LastPlayItem));
        public void PlaySong()
        {
            if (strm == 0)
            {
                PreviousSong();
                return;
            }
            if (Bass.BASS_ChannelIsActive(strm) == BASSActive.BASS_ACTIVE_PLAYING) return;
            Bass.BASS_ChannelPlay(strm, false);
            POP = true;
            PlayRowT.Stop();
            PlayRowT.Start();
        }

        public void PauseSong()
        {
            if (Bass.BASS_ChannelIsActive(strm) == BASSActive.BASS_ACTIVE_PAUSED || Bass.BASS_ChannelIsActive(strm) == BASSActive.BASS_ACTIVE_STOPPED) return;
            Bass.BASS_ChannelPause(strm);
            POP = false;
            PlayRowT.Stop();
            PlayRowT.Start();
            Play.RenderTransform = r;
        }
        public void AddSong()
        {
            OpenFileDialog f = new OpenFileDialog() { Multiselect = true, AddExtension = true, Title = "加入清單", Filter = "多媒體|*.mp3;*.mp4;*.wav;*.wma;*.m4a;*.avi|所有檔案|*.*" };
            if (f.ShowDialog() == true)
                foreach (string i in f.FileNames)
                    SongList.Add(i);
            SaveList();
        }
        public void AddSong(string Path)
        {
            SongList.Add(Path);
            SaveList();
        }

        void RemoveSong()
        {
            List<PlayerListItem> Buffer = new List<PlayerListItem>();
            foreach (PlayerListItem i in SongList.AllSelected)
                Buffer.Add(i);
            foreach (PlayerListItem i in Buffer)
                SongList.Remove(i);
            Buffer.Clear();
            Buffer = null;
            SaveList();
        }

        void ClosePlayer()
        {
            SaveSetting();
            YFH_Timer t = new YFH_Timer() { Interval = new TimeSpan(10000) };
            t.Tick += (ss, ee) =>
            {
                Opacity -= 0.05;
                if (Opacity <= 0)
                    Application.Current.Shutdown();
            };
            t.Start();
        }
        PlayerListItem FindNextSong(PlayerListItem LastSong)
        {
            if (SongList.Items.Count == 0) return null;
            int index = SongList.Items.IndexOf(LastSong);
            index = index == -1 ? 0 : index;
            int indAdd1 = index + 1;
            for (int i = indAdd1; i < SongList.Items.Count; i++)
                if (SongList[i].FileExist) return SongList[i];
            for (int i = 0; i < indAdd1; i++)
                if (SongList[i].FileExist) return SongList[i];
            return null;
        }
        PlayerListItem FindPreviousSong(PlayerListItem PreviousSong)
        {
            if (SongList.Items.Count == 0) return null;
            int index = SongList.Items.IndexOf(PreviousSong);
            index = index == -1 ? 0 : index;
            int indMinus1 = index - 1;

            for (int i = indMinus1; i > -1; i--)
                if (SongList[i].FileExist) return SongList[i];
            for (int i = SongList.Items.Count - 1; i > indMinus1; i--)
                if (SongList[i].FileExist) return SongList[i];
            return null;
        }
        #endregion
        #region Paramater
        PlayerListItem LastPlayItem = null;
        #endregion

        private void OnProgressMouseDown(object sender, EventArgs e)
        {
            IsMouseDown = Status == BASSActive.BASS_ACTIVE_PLAYING;
            if (IsMouseDown) PauseSong(); //如果播放中暫停播放
        }
        private void OnProgressMouseMove(object sender, EventArgs e)
        {
            if ((sender as Control).IsMouseCaptureWithin)
                Bass.BASS_ChannelSetPosition(strm, Bass.BASS_ChannelSeconds2Bytes(strm, MusicPogress.Value));
        }
        private void OnProgressMouseUp(object sender, EventArgs e)
        {
            if (IsMouseDown) PlaySong(); //如果原本是播放中則繼續播放
        }
        void OnShowTime(object sender, EventArgs e)
        {
            Opacity += 0.05;
            if (Opacity >= 1)
            {
                Opacity = 1;
                Console.WriteLine("T!");
                ShowTimer.Stop();
            }
        }
        /// <summary>
        /// 設定清單是否顯示
        /// </summary>
        /// <param name="Status">true = 顯示，反之</param>
        void ChangeListStatus(bool Status)
        {
            ShowAllList = Status;
            ListSwitchTimer.Start();
        }
        void OnListSwitch(object sender, EventArgs e)
        {
            if (ShowAllList)
            {
                SongList_Container.Width *= 0.8;
                if (SongList_Container.ActualWidth < 0.1)
                {
                    SongList_Container.Width = 0;
                    ListSwitchTimer.Stop();
                }
            }
            else
            {
                SongList_Container.Width += (AllList_Container.ActualWidth - SongList_Container.ActualWidth) * 0.2;
                if (AllList_Container.ActualWidth - SongList_Container.ActualWidth < 0.1)
                {
                    SongList_Container.Width = AllList_Container.Width;
                    ListSwitchTimer.Stop();
                }
            }
        }
        void OnPlayerClosing(object sender, CancelEventArgs e)
        {
            if (NF != null) NF.Visible = false;
            e.Cancel = true;
            ClosePlayer();
        }
        void SaveSetting() => Manager.SaveSetting(ref SettingTable, "Setting");

        private void UpdateInfo(object sender, EventArgs e)
        {
            try
            {
                NowTimeD = Bass.BASS_ChannelBytes2Seconds(strm, Bass.BASS_ChannelGetPosition(strm, BASSMode.BASS_POS_BYTES));
                string t = TimeSpan.FromSeconds(Math.Round(NowTimeD)).ToString();
                NowTimeS = t.Substring(t.Length - 5, 5);
                AllTimeL.Content = AllTimeS;
                NowTimeL.Content = NowTimeS;
                StateChange(Bass.BASS_ChannelIsActive(strm));
                if (Status == BASSActive.BASS_ACTIVE_PLAYING)
                    MusicPogress.Value = NowTimeD;
                EQShow.Source = BitmapToBitmapSource(VIS.CreateSpectrumLinePeak(strm, (int)200, (int)100, System.Drawing.Color.FromArgb(128, 128, 128), System.Drawing.Color.FromArgb(128, 128, 128), System.Drawing.Color.FromArgb(128, 128, 128), System.Drawing.Color.FromArgb(0, 128, 128, 128), 6, 2, 2, 50, true, true, false));
                Plugin_Event_Trigger.On_Song_Name_Change(NowTimeS);
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 更改Play/Pause按鈕圖式以及Status狀態
        /// </summary>
        /// <param name="Status"></param>
        private void StateChange(BASSActive Status)
        {
            if (this.Status == Status) return;
            this.Status = Status;
            switch (this.Status)
            {
                case BASSActive.BASS_ACTIVE_PAUSED:
                    PlayRowT.Interval = new TimeSpan(100000);
                    Play.Source = LoadImg("/HPlayer;component/Source/play.png");
                    break;
                case BASSActive.BASS_ACTIVE_PLAYING:
                    PlayRowT.Interval = new TimeSpan(300000);
                    Play.Source = LoadImg("/HPlayer;component/Source/pause.png");
                    break;
                case BASSActive.BASS_ACTIVE_STOPPED:
                    switch (NowMode)
                    {
                        case PlayMode.List:
                            NextSong();
                            break;
                        case PlayMode.Random:
                            if (RandomList.Count == 0 || LastListName != GetSettingString("List"))
                            {
                                LastListName = GetSettingString("List");
                                Console.WriteLine("Set last list to: " + LastListName + " (Used to random play)");
                                ResetRandomList();
                            }
                            if (RandomList.Count == 0)
                            {
                                this.Status = BASSActive.BASS_ACTIVE_PAUSED;
                                return;
                            }
                            StartPlaySong(RandomList[0]);
                            RandomList.RemoveAt(0);
                            break;
                        case PlayMode.Single:
                            PlaySong();
                            break;
                    }
                    break;
            }
        }
        /// <summary>
        /// 播放/暫停旋轉Timer的Tick
        /// </summary>
        private void PR(object sender, EventArgs e)
        {
            r.Angle += 3;
            if (Status == BASSActive.BASS_ACTIVE_PLAYING)
            {
                if (r.Angle >= 360) r.Angle -= 360;
                if (POP)
                {
                    Play.Opacity -= 0.02;
                    if (Play.Opacity <= 0.5) POP = !POP;
                }
                else
                {
                    Play.Opacity += 0.02;
                    if (Play.Opacity >= 1) POP = !POP;
                }
            }
            else
            {
                r.Angle += 2;
                Play.Opacity += 0.02;
                if (r.Angle >= 360) r.Angle = 0;
                if (Play.Opacity >= 1 && r.Angle == 0)
                {
                    Play.Opacity = 1;
                    PlayRowT.Stop();
                }
            }
            Play.RenderTransform = r;
        }
        /// <summary>
        /// 初始化圖示進度軸
        /// </summary>
        void InitializeIconProgressIcon()
        {
            MusicPogress.IconPointList = new List<IconPoint>() { new IconPoint(LoadImg("/HPlayer;component/Source/Progress/v0.png"), 0), new IconPoint(LoadImg("/HPlayer;component/Source/Progress/v1.png"), 0.1), new IconPoint(LoadImg("/HPlayer;component/Source/Progress/v2.png"), 0.9) };
            VolumeBar.IconPointList = new List<IconPoint>() { new IconPoint(LoadImg("/HPlayer;component/Source/Volume/volume0.png"), 0), new IconPoint(LoadImg("/HPlayer;component/Source/Volume/volume1.png"), 0.1), new IconPoint(LoadImg("/HPlayer;component/Source/Volume/volume2.png"), 0.3), new IconPoint(LoadImg("/HPlayer;component/Source/Volume/volume3.png"), 0.65), new IconPoint(LoadImg("/HPlayer;component/Source/Volume/volume4.png"), 0.95) };
        }
        /// <summary>
        /// 初始化系統右下角小圖示
        /// </summary>
        void InitializeNotifyIcon()
        {
            NF = new System.Windows.Forms.NotifyIcon();
            Stream fs = Application.GetResourceStream(new Uri("pack://application:,,,/Source/MyIcon.ico")).Stream;
            NF.Icon = new System.Drawing.Icon(fs);
            NF.Text = "HPlayer";
            NF.Visible = true;
        }
        /// <summary>
        /// 初始化循環模式之圖示
        /// </summary>
        void InitializeModeIcon()
        {
            ModeImage.Add(LoadImg("/HPlayer;component/Source/Mode/Single.png"));
            ModeImage.Add(LoadImg("/HPlayer;component/Source/Mode/List.png"));
            ModeImage.Add(LoadImg("/HPlayer;component/Source/Mode/Random.png"));
        }
        /// <summary>
        /// 初始化與伺服器之連線
        /// </summary>
        void InitializeConnection()
        {
            #region ConnectMethod
            connection.Set_Reconnect_Time(10);
            connection.Auto_Reconnect_Enabled(true);
            bool First_Connect = true;
            connection.Connection += (result) =>
            {
                if (result)
                {
                    chat_room.Clear_Chat();
                    connection.Send_Player_Version();
                    connection.Send_Check_Version(setting.DontCheckUpdate.IsChecked == true ? "false" : "true");
                    Broadcast.Text = "已連線";
                }
                else if (First_Connect)
                {
                    First_Connect = false;
                    CheckVersion();
                }
            };
            connection.On_Disconnect += delegate
            {
                Broadcast.Text = "連線與伺服器中斷";
            };
            connection.On_Receive += Connection_On_Receive;
            connection.Connect();
            #endregion
        }

        void Connection_On_Receive(params string[] Message)
        {
            foreach (string s in Message)
                Decode_Procotol(s);
        }
        void Decode_Procotol(string Message)
        {
            string[] msg = Message.Split(',');
            switch (Procotol.Flag.Get_Flag_Type(msg[0]))
            {
                case Flag_Type.Version:
                    VersionUpdate(msg[1]);
                    break;
                case Flag_Type.Broadcast:
                    Broadcast.Text = msg[1];
                    break;
                case Flag_Type.Login_Result:
                    switch (msg[1])
                    {
                        case "1":
                            Information.Member_Status.IsLogin = true;
                            Information.Member_Status.ID = member_Login.Account.Text;
                            member_Login.CloseWindow();
                            member_interface.Username.Text = member_Login.Account.Text;
                            member_interface.Email.Text = msg[2];
                            member_Login.Clear();
                            member_interface.OpenWindow();
                            break;
                        case "0":
                            MessageBox.Show("密碼錯誤");
                            member_Login.Login.IsEnabled = true;
                            break;
                        case "2":
                            MessageBox.Show("使用者不存在");
                            member_Login.Login.IsEnabled = true;
                            break;
                        case "3":
                            MessageBox.Show("該帳號已經在別處登入!");
                            member_Login.Login.IsEnabled = true;
                            break;
                    }
                    break;
                case Flag_Type.Register_Result:
                    if (msg[1] == "1")
                    {
                        MessageBox.Show("註冊成功");
                        member_register.Clear();
                        member_register.CloseWindow();
                        member_Login.OpenWindow();
                    }
                    else
                        MessageBox.Show("該帳戶已經存在");
                    member_register.Register.IsEnabled = true;
                    break;
                case Flag_Type.Chat:
                    chat_room.On_Receive(msg[1], msg[2], msg[3]);
                    break;
                case Flag_Type.Upload_Song_Result:
                    member_interface.Upload_State.Text = "就緒.";
                    member_interface.Add_Song(msg[1], msg[2]);
                    break;
                case Flag_Type.Download_Song_Result:
                    if (msg[1] == "")
                    {
                        MessageBox.Show("伺服器查無此歌曲");
                        break;
                    }
                    Stop_Play_Song();
                    if (File.Exists("temp"))
                        File.Delete("temp");
                    using (FileStream fs = new FileStream("temp", FileMode.CreateNew))
                    {
                        byte[] song = Convert.FromBase64String(msg[2]);
                        fs.Write(song, 0, song.Length);
                        fs.Close();
                    }
                    StartPlaySong("temp", false);
                    member_interface.Upload_State.Text = "就緒.";
                    Set_Display_Song_Name(msg[1]);
                    break;
                case Flag_Type.Remove_Song_Result:
                    member_interface.Remove_Song(msg[1]);
                    break;
            }
        }

        /// <summary>
        /// 檢查最新版本
        /// </summary>
        public void CheckVersion()
        {
            WebClient wc = new WebClient(); //request version number from google site
            wc.DownloadStringCompleted += (sender, args) =>
            {
                if (args.Error == null)
                    VersionUpdate(args.Result);
                wc.Dispose();
            };
            wc.DownloadStringAsync(new Uri("https://sites.google.com/site/jiushiaivb/jiu-shi-ai-vb/musicplayerpatch.txt"));
        }
        void VersionUpdate(string Version)
        {
            this.Version.Text = "最新版本: " + Version;
        }
        /// <summary>
        /// 初始化所有視窗
        /// </summary>
        void InitializeWindow()
        {
            Manager.AddMoveEvent(this);
            #region Setting視窗
            setting = new Setting(this);
            setting.Show();
            setting.Hide();
            Manager.AddMoveEvent(setting);
            #endregion
            #region 線上搜尋視窗
            search = new Search(this);
            search.Show();
            search.Hide();
            Manager.AddMoveEvent(search);
            #endregion
            member_Login = new Member_Login(this);
            member_Login.Show();
            member_Login.Hide();
            Manager.AddMoveEvent(member_Login);
            member_interface = new Member_Interface(this);
            member_interface.Show();
            member_interface.Hide();
            Manager.AddMoveEvent(member_interface);
            member_register = new Member_Register(this);
            member_register.Show();
            member_register.Hide();
            Manager.AddMoveEvent(member_register);
            chat_room = new ChatRoom(this);
            chat_room.Show();
            chat_room.Hide();
            Manager.AddMoveEvent(chat_room);
        }
        private void OnPlayerLoaded(object sender, RoutedEventArgs e)
        {
            Plugin_Manager.Initialize_Plugins();
            InitializeConnection();
            InitializeWindow();
            InitializeNotifyIcon();
            InitializeModeIcon();
            InitializeIconProgressIcon();
            Init();
            ShowTimer.Start();
        }

        private void SoundCard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SD = SoundCard.SelectedIndex;
            Bass.BASS_ChannelSetDevice(strm, SoundCard.SelectedIndex);
        }


        private void Play_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Status != BASSActive.BASS_ACTIVE_PLAYING)
                PlaySong();
            else
                PauseSong();
        }

        private void VolumeBarValueChange(object sender, double Volume)
        {
            SettingTable["Volume"] = Volume;
            SetVolume((float)(Math.Pow(Volume, 2.5) * 0.001));
        }
        void SetVolume(float Volume)
        {
            Bass.BASS_ChannelSetAttribute(strm, BASSAttribute.BASS_ATTRIB_VOL, Volume / 100);
        }
        private void AddSongClick(object sender, RoutedEventArgs e) => AddSong();

        private void RemoveSongClick(object sender, RoutedEventArgs e) => RemoveSong();

        private void NextSong(object sender, MouseButtonEventArgs e) => NextSong();

        private void PreviousSong(object sender, MouseButtonEventArgs e) => PreviousSong();

        private void SettingButton_Click(object sender, RoutedEventArgs e) => setting.OpenWindow();

        private void ChangeListDisplayStatus(object sender, MouseButtonEventArgs e) => ChangeListStatus(!ShowAllList);

        private void Search_MouseDown(object sender, MouseButtonEventArgs e) => search.OpenWindow();

        private void On_Member_Click(object sender, MouseButtonEventArgs e)
        {
            if (Information.Member_Status.IsLogin)
                member_interface.OpenWindow();
            else
                member_Login.OpenWindow();
        }

        private void On_Chat_Click(object sender, MouseButtonEventArgs e)
        {
            chat_room.OpenWindow();
        }

        private void OBS_Setting_Click(object sender, RoutedEventArgs e)
        {
            OBS_Stream_Setting obs = new OBS_Stream_Setting();
            obs.Show();
        }
    }
}
