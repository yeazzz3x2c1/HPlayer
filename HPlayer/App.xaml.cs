﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HPlayer
{
    /// <summary>
    /// App.xaml 的互動邏輯
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            // RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;
        }

        private void SendError(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
        }

    }
}
