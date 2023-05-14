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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HPlayer
{
    /// <summary>
    /// 設定圖片於指定位置時做切換
    /// </summary>
    public struct IconPoint
    {
        /// <summary>
        /// 指定的Source
        /// </summary>
        public ImageSource Source;
        /// <summary>
        /// 位置百分比，範圍0~1;
        /// </summary>
        public double Location;
        /// <summary>
        /// 初始化並設定值
        /// </summary>
        /// <param name="Source">指定的Source</param>
        /// <param name="Location">位置百分比，範圍0~1</param>
        public IconPoint(ImageSource Source, double Location)
        {
            this.Source = Source;
            Location = Location < 0 ? 0 : Location;
            Location = Location > 1 ? 1 : Location;
            this.Location = Location;
        }
    }
    public delegate void ProgressValueChangeHandler(object sender, double Value);
    public delegate void ProgressIconChangeHandler(object sender, IconChangeArgs Args);
    /// <summary>
    /// 附帶圖片的進度條
    /// </summary>
    public class IconChangeArgs
    {
        public int IconIndex;
        public ImageSource Source;
        public IconChangeArgs(int IconIndex, ImageSource Source)
        {
            this.IconIndex = IconIndex;
            this.Source = Source;
        }
    }
    public partial class IconProgress : UserControl
    {
        /// <summary>
        /// 當Value值發生改變觸發
        /// </summary>
        public event ProgressValueChangeHandler OnValueChange;
        /// <summary>
        /// 當Icon改變時觸發
        /// </summary>
        public event ProgressIconChangeHandler OnIconChange;
        /// <summary>
        /// 當IconBar被按下(包括進度軸以及Icon)
        /// </summary>
        public event EventHandler BarMouseDown;
        /// <summary>
        /// 當滑鼠在IconBar上移動(包括進度軸以及Icon)
        /// </summary>
        public event EventHandler BarMouseMove;
        /// <summary>
        /// 當IconBar被釋放(包括進度軸以及Icon)
        /// </summary>
        public event EventHandler BarMouseUp;

        /// <summary>
        /// 紀錄目前圖片Index
        /// </summary>
        int IconIndex = 0;
        /// <summary>
        /// 紀錄上一次Value，用於判斷圖片應該往後偵測還是往前偵測
        /// </summary>
        double LastValue = 0;
        /// <summary>
        /// 存放Icon之清單
        /// </summary>
        List<IconPoint> IconList = new List<IconPoint>();
        double Percent = 0; //用於進度運算暫存，避免重複運算
        int IndexBuffer = 0; //用於進度圖片Index運算暫存
        bool RunNext = false; //用於紀錄圖片是否繼續偵測
        public IconProgress()
        {
            InitializeComponent();
            InitializeEvents();
        }

        /// <summary>
        /// Progress進度條Margin
        /// </summary>
        public Thickness ProgressMargin
        {
            get => Progress.Margin;
            set => Progress.Margin = value;
        }

        /// <summary>
        /// Progress的HorizontalAlignment
        /// </summary>
        public HorizontalAlignment ProgressHorizontalAlignment
        {
            get => Progress.HorizontalAlignment;
            set => Progress.HorizontalAlignment = value;
        }

        /// <summary>
        /// Progress的VerticalAlignment
        /// </summary>
        public VerticalAlignment ProgressVerticalAlignment
        {
            get => Progress.VerticalAlignment;
            set => Progress.VerticalAlignment = value;
        }
        bool _FullHeight = false;
        public bool FullHeight
        {
            get => _FullHeight;
            set
            {
                _FullHeight = value;
                Progress.Height = value ? double.NaN : 3.986;
            }
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        void InitializeEvents()
        {
            Progress.OnValueChange += OnProgressValueChange;
            IconGrid.MouseDown += (ss, ee) => { (ss as Grid).CaptureMouse(); LocXGrid = ee.GetPosition(ss as Grid).X; BarMouseDown?.Invoke(this, null); };
            IconGrid.MouseMove += OnIconGridMouseMove;
            IconGrid.MouseUp += (ss, ee) => { (ss as Grid).ReleaseMouseCapture(); BarMouseUp?.Invoke(this, null); };
            Progress.MouseDown += (ss, ee) => BarMouseDown?.Invoke(this, null);
            Progress.MouseMove += (ss, ee) => BarMouseMove?.Invoke(this, null);
            Progress.MouseUp += (ss, ee) => BarMouseUp?.Invoke(this, null);
        }

        double LocXGrid, LocBuffer, WidthBuffer; //滑鼠X暫存，位置暫存，寬度暫存
        private void OnIconGridMouseMove(object sender, MouseEventArgs e)
        {
            if ((sender as Grid).IsMouseCaptureWithin)
            {
                WidthBuffer = IconGrid.ActualWidth * 0.5;
                LocBuffer = Progress.Margin.Left - WidthBuffer;
                Thickness th = (sender as Grid).Margin;
                th.Left += e.GetPosition(sender as Grid).X - LocXGrid;
                th.Left = th.Left < LocBuffer ? LocBuffer : th.Left;
                LocBuffer += Progress.ActualWidth;
                th.Left = th.Left > LocBuffer ? LocBuffer : th.Left;
                (sender as Grid).Margin = th;
                Progress.Value = ((th.Left + WidthBuffer) - Progress.Margin.Left) / Progress.ActualWidth * Progress.MaxmumValue;
            }
            BarMouseMove?.Invoke(this, null);
        }

        /// <summary>
        /// Icon的Visibility
        /// </summary>
        public Visibility IconVisibility
        {
            get => IconGrid.Visibility;
            set => IconGrid.Visibility = value;
        }


        /// <summary>
        /// 取得進度軸物件
        /// </summary>
        public ProgressBar ProgressBar { get => Progress; }
        /// <summary>
        /// 進度條背景色
        /// </summary>
        public Brush ProgressBackground
        {
            get => Progress.Background;
            set => Progress.Background = value;
        }
        /// <summary>
        /// 進度條進度色
        /// </summary>
        public Brush ProgressForeground
        {
            get => Progress.ProgressColor;
            set => Progress.ProgressColor = value;
        }
        /// <summary>
        /// 進度條最大值
        /// </summary>
        public double MaxmumValue
        {
            get => Progress.MaxmumValue;
            set { Progress.MaxmumValue = value; UpdateGUI(); }
        }
        /// <summary>
        /// 目前值
        /// </summary>
        public double Value
        {
            get => Progress.Value;
            set { Progress.Value = value; UpdateGUI(); }
        }
        /// <summary>
        /// 更新Icon與Icon位置
        /// </summary>
        void UpdateGUI()
        {
            Percent = Progress.Value / Progress.MaxmumValue;
            if (!IconGrid.IsMouseCaptureWithin)
            {
                Thickness th = IconGrid.Margin;
                th.Left = Progress.Margin.Left + (Progress.ActualWidth * Percent) - IconGrid.ActualWidth * 0.5;
                IconGrid.Margin = th;
            }
            if (Value < LastValue) //數值變小，往前偵測
            {
                do
                {
                    RunNext = false;
                    IndexBuffer = IconIndex - 1;
                    if (IndexBuffer > -1)
                    {
                        if (IconList[IconIndex].Location >= Percent)
                        {
                            Icon.Source = IconList[IndexBuffer].Source;
                            IconIndex--;
                            RunNext = true;
                            OnIconChange?.Invoke(this, new IconChangeArgs(IconIndex, Icon.Source));
                        }
                    }
                } while (RunNext);
            }
            else //數值變大，往後偵測
            {
                do
                {
                    RunNext = false;
                    IndexBuffer = IconIndex + 1;
                    if (IndexBuffer < IconList.Count)
                    {
                        if (IconList[IndexBuffer].Location <= Percent)
                        {
                            Icon.Source = IconList[IndexBuffer].Source;
                            IconIndex++;
                            RunNext = true;
                            OnIconChange?.Invoke(this, new IconChangeArgs(IconIndex, Icon.Source));
                        }
                    }
                } while (RunNext);
            }
            LastValue = Value;
        }

        /// <summary>
        /// 取得或設定Icon清單
        /// </summary>
        public List<IconPoint> IconPointList
        {
            get => IconList;
            set => IconList = value;
        }

        /// <summary>
        /// 進度軸值改變觸發
        /// </summary>
        void OnProgressValueChange(object sender, double Value)
        {
            UpdateGUI();
            OnValueChange?.Invoke(this, Value);
        }
    }
}
