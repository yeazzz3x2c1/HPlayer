﻿#pragma checksum "..\..\IconBar.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DC8095C89539ABDD67D8DA544B2052AE0DB56EC7"
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
    /// IconBar
    /// </summary>
    public partial class IconBar : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\IconBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HPlayer.TheBestPercent ProgressBar;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\IconBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid PeopleGrid;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\IconBar.xaml"
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
            System.Uri resourceLocater = new System.Uri("/HPlayer;component/iconbar.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\IconBar.xaml"
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
            
            #line 7 "..\..\IconBar.xaml"
            ((HPlayer.IconBar)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ProgressBar = ((HPlayer.TheBestPercent)(target));
            return;
            case 3:
            this.PeopleGrid = ((System.Windows.Controls.Grid)(target));
            
            #line 10 "..\..\IconBar.xaml"
            this.PeopleGrid.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.People_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 10 "..\..\IconBar.xaml"
            this.PeopleGrid.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.People_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 10 "..\..\IconBar.xaml"
            this.PeopleGrid.MouseMove += new System.Windows.Input.MouseEventHandler(this.People_MouseMove);
            
            #line default
            #line hidden
            return;
            case 4:
            this.people = ((System.Windows.Controls.Image)(target));
            
            #line 11 "..\..\IconBar.xaml"
            this.people.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.People_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 11 "..\..\IconBar.xaml"
            this.people.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.People_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 11 "..\..\IconBar.xaml"
            this.people.MouseMove += new System.Windows.Input.MouseEventHandler(this.People_MouseMove);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

