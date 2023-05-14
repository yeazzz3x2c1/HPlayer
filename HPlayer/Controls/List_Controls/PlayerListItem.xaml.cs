using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace HPlayer
{
    public delegate void SelectedChangeHandler(PlayerListItem sender, bool result);
    public partial class PlayerListItem : UserControl
    {

        #region Event
        public event SelectedChangeHandler OnSelectedChange;
        #endregion
        #region Para
        public bool FileExist { get; set; } = true;
        #endregion
        void Initialize()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(PlayerItemLoaded);
            CompositionTarget.Rendering += SelectedTickPerFrame;
        }
        public PlayerListItem()
        {
            Initialize();
        }
        public PlayerListItem(string Path, bool Replace_Path)
        {
            Initialize();
            this.Path = Path;
            ItemNameL.Text = Path;
            if (Replace_Path)
            {
                if (ScanFileExist)
                    if (!(System.IO.Path.GetExtension(Path) == ".YouTube"))
                        FileExist = File.Exists(Path);
                ItemNameL.Text = System.IO.Path.GetFileNameWithoutExtension(ItemNameL.Text);
            }
            UpdateUI();
        }
        public string Path { get; set; } = "";
        public bool ScanFileExist { get; set; } = true;
        public string Text
        {
            get => ItemNameL.Text;
            set => ItemNameL.Text = value;
        }
        Brush _Foreground = new SolidColorBrush(Colors.Black);
        public new Brush Foreground
        {
            get => _Foreground;
            set { _Foreground = value; UpdateUI(); }
        }

        #region Selected
        /// <summary>
        /// 被選擇時位置 單位:Percent，範圍0~1
        /// </summary>
        public double SelectedPosition { get; set; } = 0.1;
        /// <summary>
        /// 表示每移動一次為幾Percent，範圍0~1
        /// </summary>
        public double MovePercent { get; set; } = 0.2;
        bool _Selected = false;
        /// <summary>
        /// 表示是否被選取
        /// </summary>
        public bool Selected
        {
            get => _Selected;
            set
            {
                _Selected = value;
                OnSelectedChange?.Invoke(this, value);
                Now_Move = true;
            }
        }
        //    YFH_Timer SelectedTimer = new YFH_Timer() { Interval = new TimeSpan(0, 0, 0, 0, 10) };
        bool Now_Move = false;
        double PosBuffer;
        double Distination;
        void SelectedTickPerFrame(object sender , EventArgs e)
        {
            if (Now_Move)
            {
                Thickness th = Main.Margin;
                Distination = ActualWidth * SelectedPosition;
                PosBuffer = Distination - th.Left;
                if (_Selected)
                {
                    th.Left += PosBuffer * MovePercent;
                    if (PosBuffer < 0.1)
                    {
                        th.Left = Distination;
                        Now_Move = false;
                    }
                }
                else
                {
                    th.Left -= th.Left * MovePercent;
                    if (th.Left < 0.1) { th.Left = 0; Now_Move = false; }
                }
                Main.Margin = th;
            }
        }
        #endregion
        Brush _FileNotExistForeground = new SolidColorBrush(Colors.Red);
        public Brush FileNotExistForeground
        {
            get => _FileNotExistForeground;
            set { _FileNotExistForeground = value; UpdateUI(); }
        }

        void UpdateUI() => ItemNameL.Foreground = FileExist ? _Foreground : _FileNotExistForeground;
        void PlayerItemLoaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
