﻿#pragma checksum "D:\Freelancer\AutoDealer\AutoDealerz\AutoDealerz\Views\NewVehicleDetail.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "97155A45127E2361AA84051FC06743EB"
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
    
    
    public partial class NewVehicleDetail : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Button BT_back;
        
        internal System.Windows.Controls.TextBlock TB_title;
        
        internal System.Windows.Controls.TextBlock TB_variant_name;
        
        internal System.Windows.Controls.Button BT_branches;
        
        internal System.Windows.Controls.Button BT_test_dive;
        
        internal System.Windows.Controls.Button BT_brochure;
        
        internal System.Windows.Controls.Button BT_enquiry;
        
        internal System.Windows.Controls.Grid Layout_braches;
        
        internal System.Windows.Controls.Border MSB;
        
        internal System.Windows.Controls.ListBox List_available_brach;
        
        internal System.Windows.Controls.Grid Layout_test_drive;
        
        internal Microsoft.Phone.Controls.DatePicker CB_date;
        
        internal Microsoft.Phone.Controls.TimePicker CB_time;
        
        internal Microsoft.Phone.Controls.ListPicker CB_branch_name;
        
        internal Telerik.Windows.Controls.RadBusyIndicator loading_brand_name;
        
        internal System.Windows.Controls.Button BT_test_drive_send;
        
        internal System.Windows.Controls.Grid Layout_brochure;
        
        internal Microsoft.Phone.Controls.ListPicker CB_branch_brochure;
        
        internal Telerik.Windows.Controls.RadBusyIndicator loading_brand_brochure;
        
        internal System.Windows.Controls.Button BT_brochure_send;
        
        internal System.Windows.Controls.Button BT_brochure_cancel;
        
        internal System.Windows.Controls.Grid Layout_enquiry;
        
        internal System.Windows.Controls.TextBox TB_equiry_title;
        
        internal System.Windows.Controls.TextBox TB_comment_feed;
        
        internal Microsoft.Phone.Controls.ListPicker CB_branch_enquiry;
        
        internal Telerik.Windows.Controls.RadBusyIndicator loading_brand_enquiry;
        
        internal System.Windows.Controls.Button BT_enquiry_send;
        
        internal System.Windows.Controls.Button BT_enquiry_cancel;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/AutoDealerz;component/Views/NewVehicleDetail.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.BT_back = ((System.Windows.Controls.Button)(this.FindName("BT_back")));
            this.TB_title = ((System.Windows.Controls.TextBlock)(this.FindName("TB_title")));
            this.TB_variant_name = ((System.Windows.Controls.TextBlock)(this.FindName("TB_variant_name")));
            this.BT_branches = ((System.Windows.Controls.Button)(this.FindName("BT_branches")));
            this.BT_test_dive = ((System.Windows.Controls.Button)(this.FindName("BT_test_dive")));
            this.BT_brochure = ((System.Windows.Controls.Button)(this.FindName("BT_brochure")));
            this.BT_enquiry = ((System.Windows.Controls.Button)(this.FindName("BT_enquiry")));
            this.Layout_braches = ((System.Windows.Controls.Grid)(this.FindName("Layout_braches")));
            this.MSB = ((System.Windows.Controls.Border)(this.FindName("MSB")));
            this.List_available_brach = ((System.Windows.Controls.ListBox)(this.FindName("List_available_brach")));
            this.Layout_test_drive = ((System.Windows.Controls.Grid)(this.FindName("Layout_test_drive")));
            this.CB_date = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("CB_date")));
            this.CB_time = ((Microsoft.Phone.Controls.TimePicker)(this.FindName("CB_time")));
            this.CB_branch_name = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("CB_branch_name")));
            this.loading_brand_name = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("loading_brand_name")));
            this.BT_test_drive_send = ((System.Windows.Controls.Button)(this.FindName("BT_test_drive_send")));
            this.Layout_brochure = ((System.Windows.Controls.Grid)(this.FindName("Layout_brochure")));
            this.CB_branch_brochure = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("CB_branch_brochure")));
            this.loading_brand_brochure = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("loading_brand_brochure")));
            this.BT_brochure_send = ((System.Windows.Controls.Button)(this.FindName("BT_brochure_send")));
            this.BT_brochure_cancel = ((System.Windows.Controls.Button)(this.FindName("BT_brochure_cancel")));
            this.Layout_enquiry = ((System.Windows.Controls.Grid)(this.FindName("Layout_enquiry")));
            this.TB_equiry_title = ((System.Windows.Controls.TextBox)(this.FindName("TB_equiry_title")));
            this.TB_comment_feed = ((System.Windows.Controls.TextBox)(this.FindName("TB_comment_feed")));
            this.CB_branch_enquiry = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("CB_branch_enquiry")));
            this.loading_brand_enquiry = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("loading_brand_enquiry")));
            this.BT_enquiry_send = ((System.Windows.Controls.Button)(this.FindName("BT_enquiry_send")));
            this.BT_enquiry_cancel = ((System.Windows.Controls.Button)(this.FindName("BT_enquiry_cancel")));
            this.progress_bar = ((System.Windows.Controls.Grid)(this.FindName("progress_bar")));
            this.loading = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("loading")));
        }
    }
}

