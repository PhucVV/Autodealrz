﻿#pragma checksum "D:\Freelancer\AutoDealer\AutoDealerz\AutoDealerz\Controls\LayoutServices.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EE5A59C6F50E39C44164AE2E91FC3F27"
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


namespace AutoDealerz.Controls {
    
    
    public partial class LayoutServices : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.ListBox ImageBar;
        
        internal Microsoft.Phone.Controls.Pivot MainPivot;
        
        internal Microsoft.Phone.Controls.ListPicker CB_city;
        
        internal Telerik.Windows.Controls.RadBusyIndicator loading_city;
        
        internal Microsoft.Phone.Controls.ListPicker CB_branch_name;
        
        internal Telerik.Windows.Controls.RadBusyIndicator loading_brand_name;
        
        internal Microsoft.Phone.Controls.ListPicker CB_vehicle;
        
        internal Telerik.Windows.Controls.RadBusyIndicator loading_vehicle;
        
        internal Microsoft.Phone.Controls.DatePicker CB_date;
        
        internal Microsoft.Phone.Controls.TimePicker CB_time;
        
        internal Microsoft.Phone.Controls.ListPicker CB_service_type;
        
        internal Telerik.Windows.Controls.RadBusyIndicator loading_service_type;
        
        internal System.Windows.Controls.TextBox TB_other;
        
        internal System.Windows.Controls.Button BT_send;
        
        internal System.Windows.Controls.ListBox List_my_request;
        
        internal Microsoft.Phone.Controls.ListPicker CB_vehicle_feed;
        
        internal Telerik.Windows.Controls.RadBusyIndicator loading_vehicle_feed;
        
        internal System.Windows.Controls.TextBox TB_comment_feed;
        
        internal System.Windows.Controls.RadioButton RBT_excellent;
        
        internal System.Windows.Controls.RadioButton RBT_good;
        
        internal System.Windows.Controls.RadioButton RBT_poor;
        
        internal System.Windows.Controls.Button BT_send_feed;
        
        internal System.Windows.Controls.Grid progress_bar;
        
        internal Telerik.Windows.Controls.RadBusyIndicator loading;
        
        internal AutoDealerz.Controls.MyMessageBox my_message;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/AutoDealerz;component/Controls/LayoutServices.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ImageBar = ((System.Windows.Controls.ListBox)(this.FindName("ImageBar")));
            this.MainPivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("MainPivot")));
            this.CB_city = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("CB_city")));
            this.loading_city = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("loading_city")));
            this.CB_branch_name = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("CB_branch_name")));
            this.loading_brand_name = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("loading_brand_name")));
            this.CB_vehicle = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("CB_vehicle")));
            this.loading_vehicle = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("loading_vehicle")));
            this.CB_date = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("CB_date")));
            this.CB_time = ((Microsoft.Phone.Controls.TimePicker)(this.FindName("CB_time")));
            this.CB_service_type = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("CB_service_type")));
            this.loading_service_type = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("loading_service_type")));
            this.TB_other = ((System.Windows.Controls.TextBox)(this.FindName("TB_other")));
            this.BT_send = ((System.Windows.Controls.Button)(this.FindName("BT_send")));
            this.List_my_request = ((System.Windows.Controls.ListBox)(this.FindName("List_my_request")));
            this.CB_vehicle_feed = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("CB_vehicle_feed")));
            this.loading_vehicle_feed = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("loading_vehicle_feed")));
            this.TB_comment_feed = ((System.Windows.Controls.TextBox)(this.FindName("TB_comment_feed")));
            this.RBT_excellent = ((System.Windows.Controls.RadioButton)(this.FindName("RBT_excellent")));
            this.RBT_good = ((System.Windows.Controls.RadioButton)(this.FindName("RBT_good")));
            this.RBT_poor = ((System.Windows.Controls.RadioButton)(this.FindName("RBT_poor")));
            this.BT_send_feed = ((System.Windows.Controls.Button)(this.FindName("BT_send_feed")));
            this.progress_bar = ((System.Windows.Controls.Grid)(this.FindName("progress_bar")));
            this.loading = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("loading")));
            this.my_message = ((AutoDealerz.Controls.MyMessageBox)(this.FindName("my_message")));
        }
    }
}

