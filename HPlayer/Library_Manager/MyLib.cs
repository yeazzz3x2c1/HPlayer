using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HPlayer
{
    public class MyLib
    {
        [DllImport("gdi32.dll")] public static extern bool DeleteObject(IntPtr hObject);
        static public ImageSource LoadImg(string K)
        {
            return new BitmapImage(new Uri(K, UriKind.Relative));
        }
        static public BitmapSource BitmapToBitmapSource(System.Drawing.Bitmap bitmap)
        {
            if (bitmap == null)
                return null;
            IntPtr ptr = bitmap.GetHbitmap();
            BitmapSource result =
              Imaging.CreateBitmapSourceFromHBitmap(
                    ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(ptr);
            return result;
        }
    }
}
