using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace HPlayer.Controls
{
    /// <summary>
    /// SpectrumAnalyze.xaml 的互動邏輯
    /// </summary>
    public partial class SpectrumAnalyze : UserControl
    {
        [Category("Parameter")]
        public Un4seen.Bass.Misc.Visuals Visuals { get; set; } = null;
        int Frequency_Distance = 35;
        int _Maximum_Frequency = 8000;
        [Category("Parameter")]
        public int Maximum_Frequency
        {
            get => _Maximum_Frequency;
            set
            {
                if (value <= _Minimum_Frequency)
                    return;
                _Maximum_Frequency = value;
                Frequency_Distance = (_Maximum_Frequency - _Minimum_Frequency) / _Line_Count;
            }
        }
        int _Minimum_Frequency = 30;
        [Category("Parameter")]
        public int Minimum_Frequency
        {
            get => _Minimum_Frequency;
            set
            {
                if (value >= _Maximum_Frequency)
                    return;
                if (value < 0)
                    return;
                _Minimum_Frequency = value;
                Frequency_Distance = (_Maximum_Frequency - _Minimum_Frequency) / _Line_Count;
            }
        }
        [Category("Parameter")]
        public SolidColorBrush Line_Color { get; set; } = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
        PathGeometry pg = new PathGeometry();
        double _Distance = 0;
        [Category("Parameter")]
        public double Distance
        {
            get => _Distance;
            set
            {
                if (value < 0)
                    return;
                _Distance = value;
                Initialize_Line();
            }
        }
        int _Line_Count = 10;
        [Category("Parameter")]
        public int Line_Count
        {
            get => _Line_Count;
            set
            {
                if (value < 1)
                    return;
                _Line_Count = value;
                Frequency_Distance = (_Maximum_Frequency - _Minimum_Frequency) / _Line_Count;
                Initialize_Line();
            }
        }
        double d = 1.0 / 255.0;
        public void Set_Line_Value(int Line_Index, double Value)
        {
            var p = pg.Figures[Line_Index];
            var h1 = (LineSegment)p.Segments[1];
            var h2 = (LineSegment)p.Segments[2];
            var p1 = h1.Point;
            var p2 = h2.Point;
            p1.Y = (255 - Value) * d * ActualHeight;
            p2.Y = p1.Y;
            h1.Point = p1;
            h2.Point = p2;
        }
        public void Initialize_Line()
        {
            if (!IsLoaded)
                return;
            pg.Clear();
            double Height = ActualHeight;
            double Width = ActualWidth;
            double Line_Width = (Width - ((_Line_Count - 1) * _Distance)) / _Line_Count;
            double Blank_Width = (Width - Line_Width * _Line_Count) / (_Line_Count - 1);
            double Start_X = 0;
            double End_X = 0;
            for (int i = 0; i < _Line_Count; i++)
            {
                Start_X = i * (Line_Width + Blank_Width);
                End_X = Start_X + Line_Width;
                PathFigure pf = new PathFigure();
                pf.StartPoint = new Point(Start_X, Height);
                pf.IsClosed = true;
                pf.Segments.Add(new LineSegment(new Point(End_X, Height), true));
                pf.Segments.Add(new LineSegment(new Point(End_X, 0), true));
                pf.Segments.Add(new LineSegment(new Point(Start_X, 0), true));
                pg.Figures.Add(pf);
            }
        }
        public void Update_Spectrum(int Stream)
        {
            try
            {
                if (Visuals == null)
                    return;
                int temp = 0;
                float val = 0;
                for (int i = 0; i < _Line_Count; i++)
                {
                    temp = _Minimum_Frequency + Frequency_Distance * i;
                    val = Visuals.DetectFrequency(Stream, temp, temp + Frequency_Distance, false) * 255;
                    Set_Line_Value(i, val);
                }
            }
            catch { }
        }
        public SpectrumAnalyze()
        {
            InitializeComponent();
            Loaded += delegate
            {
                path.Stroke = Line_Color;
                path.Fill = Line_Color;
                path.Data = pg;
                Initialize_Line();
            };
        }
    }
}
