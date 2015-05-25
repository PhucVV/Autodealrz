using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AutoDealerz.Models;

namespace AutoDealerz.Views
{
    public partial class NewsPage : PhoneApplicationPage
    {
        public NewsPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string tt = "";
            NavigationContext.QueryString.TryGetValue("title", out tt);
            TB_title.Text = tt;

            News data = PhoneApplicationService.Current.State["news"] as News;
            content.DataContext = data;
        }
        private void BT_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}