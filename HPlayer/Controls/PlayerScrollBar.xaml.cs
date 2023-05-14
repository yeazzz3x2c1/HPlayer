using HPlayer.Library_Manager;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HPlayer
{
    /// <summary>
    /// PlayerScrollBar.xaml 的互動邏輯
    /// </summary>
    public delegate void ValueChangeHandler(object sender, EventArgs e);
    public partial class PlayerScrollBar : UserControl
    {
        public event ValueChangeHandler OnValueChange;
        bool NoLoaded = true;
        void InitEvent()
        {
            Loaded += new RoutedEventHandler(OnLoaded);
            ScrollBar.MouseDown += new MouseButtonEventHandler(ScrollBarMouseDown);
            ScrollBar.MouseMove += new MouseEventHandler(ScrollBarMouseMove);
            ScrollBar.MouseUp += new MouseButtonEventHandler(ScrollBarMouseUp);
            ForceTimer.Tick += new EventHandler(ForceTick);
        }
        public PlayerScrollBar()
        {
            InitializeComponent();
            InitEvent();
        }
        double LocHeight;
        double _MaxValue = 100.0;
        public double ScrollSpeed { get; set; } = 0.3;
        public double MaxValue
        {
            get => _MaxValue;
            set
            {
                _MaxValue = value;
                if (Value > value) Value = value;
                UpdateUI();
            }
        }
        double _MinValue = 0.0;
        public double MinValue
        {
            get => _MinValue;
            set
            {
                _MinValue = value;
                if (Value < value) Value = value;
                UpdateUI();
            }
        }
        double _Value = 0.0;
        public double Value
        {
            get => _Value;
            set
            {
                if (value > _MaxValue) value = _MaxValue;
                if (value < _MinValue) value = _MinValue;
                _Value = value;
                OnValueChange?.Invoke(this, new EventArgs());
                UpdateUI();
            }
        }
        double _LocPercent = 0;
        public double LocPercent
        {
            get => _LocPercent;
            set
            {
                _LocPercent = value;
                UpdateUI();
            }
        }
        double _HeightPercent = 0.5;
        public double HeightPercent
        {
            get => _HeightPercent;
            set
            {
                if (value > 1) value = 1;
                _HeightPercent = value;
                UpdateUI();
            }
        }
        #region Force
        YFH_Timer ForceTimer = new YFH_Timer() { Interval = new TimeSpan(100000) };
        double ForceBuf = 0;
        void ForceTick(object sender, EventArgs e)
        {
            if (Math.Abs(_Force) < 0.1)
            {
                Value -= _Force;
                _Force = 0;
                ForceTimer.Stop();
                return;
            }
            ForceBuf = _Force * ScrollSpeed;
            Value -= ForceBuf;
            _Force -= ForceBuf;
            if (_Value == _MaxValue || _Value == _MinValue)
            {
                _Force = 0;
                ForceTimer.Stop();
                return;
            }
        }
        double _Force = 0;
        public double Force
        {
            get => _Force;
            set
            {
                _Force = value;
                ForceTimer.Start();
            }
        }

        public void AddForce(double force)
        {
            ForceBuf = _Force + force;
            if (Math.Abs(ForceBuf) > Math.Abs(_Force - force))
                _Force = ForceBuf;
            else
                _Force = force;
            ForceTimer.Start();
        }
        #endregion
        #region MouseEvent
        double oldY = 0, DistanceY = 0, NowY = 0;
        void ScrollBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            _Force = 0;
            oldY = e.GetPosition(ScrollBar).Y;
            ScrollBar.CaptureMouse();
        }
        void ScrollBarMouseMove(object sender, MouseEventArgs e)
        {
            if (ScrollBar.IsMouseCaptured)
            {
                NowY = e.GetPosition(ScrollBar).Y;
                DistanceY = ScrollBar.Margin.Top - (oldY - NowY);
                if (DistanceY < 0) DistanceY = 0;
                if (DistanceY > LocHeight) DistanceY = LocHeight;
                ScrollBar.Margin = new Thickness(0, DistanceY, 0, 0);
                _LocPercent = DistanceY / LocHeight;
                _Value = _MaxValue * _LocPercent;
                OnValueChange?.Invoke(this, new EventArgs());
            }
        }
        void ScrollBarMouseUp(object sender, MouseButtonEventArgs e) => ScrollBar.ReleaseMouseCapture();
        #endregion
        #region UI
        double _MinimumHeight = 10.0;
        public double MinimumHeight
        {
            get => _MinimumHeight;
            set
            {
                _MinimumHeight = value;
                UpdateUI();
            }
        }
        public Brush ScrollBackground
        {
            get => ScrollBar.Background;
            set => ScrollBar.Background = value;
        }
        double HeightBuffer;
        void UpdateUI()
        {
            if (NoLoaded) return;
            HeightBuffer = ActualHeight * _HeightPercent;
            if (HeightBuffer < MinimumHeight) HeightBuffer = MinimumHeight;
            ScrollBar.Height = HeightBuffer;
            _LocPercent = (_Value - _MinValue) / (_MaxValue - _MinValue);
            UpdateLayout();
            UpdateHeight();
            ScrollBar.Margin = new Thickness(0, LocHeight * _LocPercent, 0, 0);
        }
        void UpdateHeight() => LocHeight = ActualHeight - ScrollBar.ActualHeight;
        void OnLoaded(object sender, RoutedEventArgs e)
        {
            NoLoaded = false;
            UpdateUI();
        }
        #endregion
    }
}
