﻿#pragma checksum "D:\Freelancer\AutoDealer\AutoDealerz\AutoDealerz\Controls\Notification.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BA227C25632ECF287C7BFB1D058213C7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AutoDealerz.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;


namespace AutoDealerz.Controls {
    
    
    public partial class Notification : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.ListBox List_notifi;
        
        internal AutoDealerz.Controls.MyMessageBox my_message;
        
        internal System.Windows.Controls.Grid progress_bar;
        
        internal Telerik.Windows.Controls.RadBusyIndicator loading;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/AutoDealerz;component/Controls/Notification.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.List_notifi = ((System.Windows.Controls.ListBox)(this.FindName("List_notifi")));
            this.my_message = ((AutoDealerz.Controls.MyMessageBox)(this.FindName("my_message")));
            this.progress_bar = ((System.Windows.Controls.Grid)(this.FindName("progress_bar")));
            this.loading = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("loading")));
        }
    }
}
