﻿#pragma checksum "..\..\VolumeBar.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9CC69F1F1EF3598CF115D9B4B0F0A2D63BA26918"
//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.42000
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

using HPlayer;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace HPlayer {
    
    
    /// <summary>
    /// VolumeBar
    /// </summary>
    public partial class VolumeBar : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\VolumeBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HPlayer.TheBestPercent TP;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\VolumeBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid PG;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\VolumeBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image people;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HPlayer;component/volumebar.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\VolumeBar.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 7 "..\..\VolumeBar.xaml"
            ((HPlayer.VolumeBar)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.UserControl_SizeChanged);
            
            #line default
            #line hidden
            
            #line 7 "..\..\VolumeBar.xaml"
            ((HPlayer.VolumeBar)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TP = ((HPlayer.TheBestPercent)(target));
            return;
            case 3:
            this.PG = ((System.Windows.Controls.Grid)(target));
            
            #line 21 "..\..\VolumeBar.xaml"
            this.PG.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.People_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 21 "..\..\VolumeBar.xaml"
            this.PG.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.People_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 21 "..\..\VolumeBar.xaml"
            this.PG.MouseMove += new System.Windows.Input.MouseEventHandler(this.People_MouseMove);
            
            #line default
            #line hidden
            return;
            case 4:
            this.people = ((System.Windows.Controls.Image)(target));
            
            #line 22 "..\..\VolumeBar.xaml"
            this.people.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.People_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 22 "..\..\VolumeBar.xaml"
            this.people.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.People_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 22 "..\..\VolumeBar.xaml"
            this.people.MouseMove += new System.Windows.Input.MouseEventHandler(this.People_MouseMove);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

