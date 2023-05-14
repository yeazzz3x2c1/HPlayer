using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace HPlayer_Plugin_Clock
{
    /// <summary>
    /// Clock.xaml 的互動邏輯
    /// </summary>
    public partial class Clock_UserControl : UserControl
    {

        static DoubleAnimationUsingKeyFrames a = new DoubleAnimationUsingKeyFrames();
        public static void RotateToAngle(RotateTransform rt, double Target_Angle, int Second)
        {
            double val = rt.Angle;
            if (Target_Angle < val)
            {
                val %= 360;
                val -= 360;
            }
            a.KeyFrames.Clear();
            a.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0)), Value = val, EasingFunction = new QuinticEase() { EasingMode = EasingMode.EaseOut } });
            a.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, Second)), Value = Target_Angle, EasingFunction = new QuinticEase() { EasingMode = EasingMode.EaseOut } });

            rt.BeginAnimation(RotateTransform.AngleProperty, a, HandoffBehavior.SnapshotAndReplace);
        }
        public Grid getLine(Brush color, double height)
        {
            Grid g = new Grid() { Background = color, RenderTransformOrigin = new Point(0.5, 1), Width = 5, Height = height, Margin = new Thickness(0, Clock_Container.ActualHeight * 0.5 - height, 0, 0), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top };
            Clock_Container.Children.Insert(1, g);
            return g;
        }
        public RotateTransform getNewRotateTransform(Grid l)
        {
            RotateTransform RT = new RotateTransform();
            l.RenderTransform = RT;
            return RT;
        }

        public Clock_UserControl()
        {
            InitializeComponent();
            Loaded += delegate
            {
                TextBlock tb = new TextBlock() { Text = "00:00:00", FontFamily = new FontFamily("Bahnschrift Light"), FontSize = 56, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(0, Clock_Container.ActualHeight * 0.6, 0, 0), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top };
                Clock_Container.Children.Add(tb);
                Grid Sec = getLine(new SolidColorBrush(Colors.White), Clock_Container.ActualHeight * 0.4);
                Grid Min = getLine(new SolidColorBrush(Colors.Orange), Clock_Container.ActualHeight * 0.3);
                Grid Hour = getLine(new SolidColorBrush(Colors.Green), Clock_Container.ActualHeight * 0.25);
                RotateTransform secRT = getNewRotateTransform(Sec);
                RotateTransform minRT = getNewRotateTransform(Min);
                RotateTransform hourRT = getNewRotateTransform(Hour);
                DispatcherTimer dt = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
                dt.Tick += delegate
                {
                    double angSec = DateTime.Now.Second * 6.0;
                    double angMin = DateTime.Now.Minute * 6.0 + angSec / 60.0;
                    double angHour = DateTime.Now.Hour * 30.0 + angMin / 12.0;
                    RotateToAngle(secRT, angSec, 1);
                    RotateToAngle(minRT, angMin, 1);
                    RotateToAngle(hourRT, angHour, 1);
                    tb.Text = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');
                };
                dt.Start();
            };
        }
    }
}
