﻿#pragma checksum "D:\Freelancer\AutoDealer\AutoDealerz\AutoDealerz\Views\NewVehicles.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CCEEB0464DE85797440578B21F437069"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
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


namespace AutoDealerz.Views {
    
    
    public partial class NewVehicles : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Button BT_back;
        
        internal System.Windows.Controls.TextBlock TB_title;
        
        internal System.Windows.Controls.TextBlock TB_upto;
        
        internal System.Windows.Controls.ListBox List_variant;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/AutoDealerz;component/Views/NewVehicles.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.BT_back = ((System.Windows.Controls.Button)(this.FindName("BT_back")));
            this.TB_title = ((System.Windows.Controls.TextBlock)(this.FindName("TB_title")));
            this.TB_upto = ((System.Windows.Controls.TextBlock)(this.FindName("TB_upto")));
            this.List_variant = ((System.Windows.Controls.ListBox)(this.FindName("List_variant")));
            this.progress_bar = ((System.Windows.Controls.Grid)(this.FindName("progress_bar")));
            this.loading = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("loading")));
        }
    }
}

