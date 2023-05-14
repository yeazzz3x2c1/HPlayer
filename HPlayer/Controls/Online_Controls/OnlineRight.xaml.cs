using HPlayer.Library_Manager;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using YFH_YouTube_Library;

namespace HPlayer
{
    /// <summary>
    /// OnlineRight.xaml 的互動邏輯
    /// </summary>
    public partial class OnlineRight : Window
    {
        Search host = null;
        OnlineItem target = null;
        /// <summary>
        /// 表示滑鼠移動到上面時該往右移動多少距離
        /// </summary>
        public double Distance = 10;
        /// <summary>
        /// 表示移動速度(0~1 = 0~100%)
        /// </summary>
        public double MoveSpeed = 0.3;
        /// <summary>
        /// 表示移動暫存
        /// </summary>
        double MoveBuffer;
        void AddToList(object sender, EventArgs e)
        {
            Information.Main.AddSong(target.Video_ID + @":\" + target.Video_Title + ".YouTube");
            host.Focus();
        }
        void PlaySong(object sender, EventArgs e)
        {
            Information.Main.StartPlaySong(YouTube_Decoder.Get_YouTube_Link(target.Video_ID)?[0].Url);
            host.Focus();
        }
        void OpenSourcePage(object sender , EventArgs e)
        {
            Process.Start(target.Video_URL);
        }

        protected override void OnDeactivated(EventArgs e) //視窗失去焦點
        {
            base.OnDeactivated(e);
            Hide();
            //之所以做這個延遲是因為沒有延遲的話搜尋視窗在第一次點擊時會獲取不到焦點，造成使用者困擾
            YFH_Timer closeDelay = new YFH_Timer() { Interval = new TimeSpan(1) };
            closeDelay.Tick += delegate
            {
                try
                { 
                    closeDelay.Stop();
                    closeDelay = null;
                    Close();
                }
                catch { } //用try包起來是為了避免重複觸發
            };
            closeDelay.Start();

        }
        YFH_Timer TitleMove = new YFH_Timer() { Interval = new TimeSpan(0, 0, 0, 0, 10) };
        public OnlineRight(Search host, OnlineItem target)
        {
            this.target = target;
            this.host = host;
            InitializeComponent();
            Closed += delegate {
                TitleMove.Stop();
                TitleMove = null;
                GC.Collect();
            };
            Loaded += delegate
            {
                Song_Name.Text = target.Video_Title;
                Song_Name.UpdateLayout();
                if (Song_Name.ActualWidth > Name_Container.ActualWidth)
                {
                    Song_Name.HorizontalAlignment = HorizontalAlignment.Left;
                    Song_Name.Margin = new Thickness(ActualWidth, 0, 0, 0);
                    int MoveDelay = 0;
                    bool MoveRight = true;
                    double MoveToTarget = ActualWidth - Song_Name.ActualWidth - 10;
                    double MoveSpeed = 0;
                    TitleMove.Tick += delegate
                    {
                        Thickness th = Song_Name.Margin;
                        if (MoveDelay == 0)
                        {
                            if (MoveRight)
                            {
                                if (th.Left < 0)
                                {
                                    MoveSpeed += 0.025;
                                    th.Left -= MoveSpeed;
                                    if (th.Left < -Song_Name.ActualWidth)
                                        th.Left = ActualWidth;
                                }
                                else
                                {
                                    th.Left -= (th.Left - 10) * 0.05;
                                    if (Math.Abs(th.Left - 10) < 0.1)
                                    {
                                        MoveSpeed = 0;
                                        th.Left = 10;
                                        MoveRight = false;
                                        MoveDelay = 10;
                                    }
                                }
                            }
                            else
                            {
                                if (MoveSpeed < 0.5)
                                    MoveSpeed += 0.01;
                                th.Left -= MoveSpeed;
                                if (th.Left < MoveToTarget)
                                {
                                    MoveSpeed = 0;
                                    MoveRight = true;
                                    MoveDelay = 25;
                                }
                            }
                            Song_Name.Margin = th;
                        }
                        else
                            MoveDelay--;
                    };
                    TitleMove.Start();
                }
                foreach (Grid Items in Function_Block.Children)
                {
                    var i = Items.Children[0] as TextBlock;
                    bool MoveRight = false;
                    YFH_Timer t = new YFH_Timer() { Interval = new TimeSpan(0, 0, 0, 0, 10) };
                    t.Tick += delegate
                    {
                        Thickness th = i.Margin;
                        if (MoveRight)
                            MoveBuffer = (Distance - th.Left) * MoveSpeed;
                        else
                            MoveBuffer = (5 - th.Left) * MoveSpeed;
                        th.Left += MoveBuffer;
                        if (Math.Abs(MoveBuffer) < 0.1)
                        {
                            th.Left = MoveRight ? Distance : 5;
                            t.Stop();
                        }
                        i.Margin = th;
                    };
                    Items.MouseEnter += delegate { MoveRight = true; t.Start(); };
                    Items.MouseLeave += delegate { MoveRight = false; t.Start(); };
                }
            };
        }
    }
}
