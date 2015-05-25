using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace AutoDealerz.Controls
{
    public partial class Home : UserControl
    {
        public delegate void TitleDelegate(string s);
        public event TitleDelegate changeTitle;

       public enum StateViewHome
        {
            none,
            service,
            part,
            news,
            sales
        }
       public StateViewHome stateviewhome;
        public Home()
        {
            InitializeComponent();
            stateviewhome = StateViewHome.none;
        }


        public void ResetPlayout()
        {
            Layout_service.Visibility = System.Windows.Visibility.Collapsed;
            Layout_service.my_message.Hide();

            layout_news.Visibility = System.Windows.Visibility.Collapsed;
            layout_parts.Visibility = System.Windows.Visibility.Collapsed;
            layout_parts.my_message.Hide();

            layout_sales.Visibility = System.Windows.Visibility.Collapsed;
            stateviewhome = StateViewHome.none;
        }

        private void BT_services_Click(object sender, RoutedEventArgs e)
        {
            if (changeTitle != null)
                changeTitle("SERVICES");
            Layout_service.CheckData();
            Layout_service.Visibility = System.Windows.Visibility.Visible;
            stateviewhome = StateViewHome.service;
        }

        private void BT_part_Click(object sender, RoutedEventArgs e)
        {
            if (changeTitle != null)
                changeTitle("PARTS");
            layout_parts.CheckData();
            layout_parts.Visibility = System.Windows.Visibility.Visible;
            stateviewhome = StateViewHome.part;
        }

        private void BT_new_Click(object sender, RoutedEventArgs e)
        {
            if (changeTitle != null)
                changeTitle("NEWS");
            layout_news.Visibility = System.Windows.Visibility.Visible;
            stateviewhome = StateViewHome.news;
        }

        private void BT_sales_Click(object sender, RoutedEventArgs e)
        {
            if (changeTitle != null)
                changeTitle("SALES");
            layout_sales.LoadData();
            layout_sales.Visibility = System.Windows.Visibility.Visible;
            stateviewhome = StateViewHome.sales;
        }
    }
}
