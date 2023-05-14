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
    public partial class ProgressBar : UserControl
    {
        /// <summary>
        /// 當Value改變時觸發
        /// </summary>
        public event ProgressValueChangeHandler OnValueChange;
        public ProgressBar()
        {
            InitializeComponent();
            InitializeEvents();
        }
        /// <summary>
        /// 初始化事件
        /// </summary>
        void InitializeEvents()
        {
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
        }
        double LocXBuffer = 0; //OnMouseMove事件暫存
        void OnMouseDown(object sender, MouseButtonEventArgs e) => CaptureMouse();
        void OnMouseMove(object sender, MouseEventArgs e)
        {
            if ((sender as Control).IsMouseCaptureWithin)
            {
                LocXBuffer = e.GetPosition(sender as Control).X;
                LocXBuffer = LocXBuffer < 0 ? 0 : LocXBuffer;
                LocXBuffer = LocXBuffer > ActualWidth ? ActualWidth : LocXBuffer;
                Progress.Width = LocXBuffer;
                Value = Distance * (Progress.Width / ActualWidth) + _MinmumValue;
            }
        }
        void OnMouseUp(object sender, MouseButtonEventArgs e) => ReleaseMouseCapture();

        public Brush ProgressColor
        {
            get => Progress.Background;
            set => Progress.Background = value;
        }
        /// <summary>
        /// 最大值與最小值的差
        /// </summary>
        double Distance = 100;
        double _MaxmumValue = 100;
        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxmumValue
        {
            get => _MaxmumValue;
            set
            {
                _MaxmumValue = value;
                if (_MaxmumValue < _MinmumValue) _MaxmumValue = _MinmumValue;
                if (_Value > _MaxmumValue) _Value = _MaxmumValue;
                Distance = _MaxmumValue - _MinmumValue;
                if(Distance == 0)
                {
                    MaxmumValue = _MinmumValue + 1;
                    return;
                }
                UpdateGUI();
            }
        }

        double _MinmumValue = 0;
        /// <summary>
        /// 最小值
        /// </summary>
        public double MinmumValue
        {
            get => _MinmumValue;
            set
            {
                _MinmumValue = value;
                if (_MinmumValue > _MaxmumValue) _MinmumValue = _MaxmumValue;
                if (_Value < _MinmumValue) _Value = _MinmumValue;
                Distance = _MaxmumValue - _MinmumValue;
                if (Distance == 0)
                {
                    MaxmumValue = _MinmumValue + 1;
                    return;
                }
                UpdateGUI();
            }
        }

        double _Value = 0;
        /// <summary>
        /// 目前值
        /// </summary>
        public double Value
        {
            get => _Value;
            set
            {
                _Value = value;
                if (_Value > _MaxmumValue) _Value = _MaxmumValue;
                if (_Value < _MinmumValue) _Value = _MinmumValue;
                OnValueChange?.Invoke(this, _Value);
                UpdateGUI();
            }
        }

        /// <summary>
        /// 更新顯示畫面
        /// </summary>
        public void UpdateGUI()
        {
            Progress.Width = ActualWidth * ((_Value - _MinmumValue) / Distance);
        }
    }
}
