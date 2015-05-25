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
using System.Net.Http;
using Newtonsoft.Json;
using Coding4Fun.Toolkit.Controls;

namespace AutoDealerz.Views
{
    public partial class UsedVehicleDetail : PhoneApplicationPage
    {
        private enum StateView
        {
            none,
            branches,
            test_drive,
            prochu,
            enquiri
        }
        StateView stateview;

        public UsedVehicleDetail()
        {
            InitializeComponent();
            stateview = StateView.none;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            switch (stateview)
            {
                case StateView.branches:
                    Layout_braches.Visibility = System.Windows.Visibility.Collapsed;
                    stateview = StateView.none;
                    e.Cancel = true;
                    break;
                case StateView.enquiri:
                    Layout_enquiry.Visibility = System.Windows.Visibility.Collapsed;
                    stateview = StateView.none;
                    e.Cancel = true;
                    break;
                case StateView.none:
                    break;
                case StateView.prochu:
                   
                    break;
                case StateView.test_drive:
            
                    break;
            }
        }
        Car datacar;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            datacar = PhoneApplicationService.Current.State["used_vehicle"] as Car;
            LayoutRoot.DataContext = datacar;
            LoadCarDetail(datacar.car_id);
        }
        OldCarDetailsObject datadetail;
        private async void LoadCarDetail(string code)
        {
            if (CB_branch_enquiry.ItemsSource != null) return;
            try
            {
                progress_bar.Visibility = System.Windows.Visibility.Visible;
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                         new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                         new KeyValuePair<string, string>("car_id", code)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiUsedCarDetail(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                datadetail = JsonConvert.DeserializeObject<OldCarDetailsObject>(responseString);
                if (datadetail.status == "true")
                {
                    datadetail.colour_exterior = datadetail.colour_exterior + "," + datadetail.colour_interior;
                    LayoutRoot.DataContext = datadetail;
                    List_available_brach.ItemsSource = datadetail.available_branches;
     
                    CB_branch_enquiry.ItemsSource = datadetail.available_branches;
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch
            {

                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void branches_tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Layout_braches.Visibility = System.Windows.Visibility.Visible;
            stateview = StateView.branches;
        }

        private void enquiry_tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Layout_enquiry.Visibility = System.Windows.Visibility.Visible;
            stateview = StateView.enquiri;
        }

        private void TB_equiry_title_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_equiry_title.Text == "Enquiry Title")
                TB_equiry_title.Text = "";
        }

        private void TB_equiry_title_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_comment_feed.Focus();
        }

        private void TB_equiry_title_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_equiry_title.Text))
                TB_equiry_title.Text = "Enquiry Title";
        }

        private void TB_comment_feed_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_comment_feed.Text == "Description")
                TB_comment_feed.Text = "";
        }

        private void TB_comment_feed_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void TB_comment_feed_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_comment_feed.Text))
                TB_comment_feed.Text = "Description";
        }

        private async void BT_enquiry_send_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_equiry_title.Text) || TB_equiry_title.Text == "Enquiry Title")
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter enquiry title",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (string.IsNullOrEmpty(TB_comment_feed.Text) || TB_comment_feed.Text == "Description")
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter description",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }

            try
            {
                progress_bar.Visibility = System.Windows.Visibility.Visible;
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                         new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                         new KeyValuePair<string, string>("enquiry_title", TB_equiry_title.Text),
                             new KeyValuePair<string, string>("enquiry_description", TB_comment_feed.Text),
                                new KeyValuePair<string, string>("car_id", datacar.car_id),
                                   new KeyValuePair<string, string>("customer_id",App.Customer_ID),

               new KeyValuePair<string, string>("branch_id", (CB_branch_enquiry.SelectedItem as AvailableBranch).branch_id),
                new KeyValuePair<string, string>("Send mail - brand_name",  (CB_branch_enquiry.SelectedItem as AvailableBranch).branch_email_id),
                                new KeyValuePair<string, string>("model_name",  datadetail.model_name),
                                     new KeyValuePair<string, string>("price",  datadetail.price),
                  new KeyValuePair<string, string>("variant",  datadetail.variant),
                    new KeyValuePair<string, string>("model_year_month",  datadetail.month_year),

                   
                         
               new KeyValuePair<string, string>("car_image",  datadetail.car_image)
              
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiEnquiry(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<SimpleMyRequestObject>(responseString);
                if (data.status == "true")
                {
                    MessageBox.Show(data.msg);
                }
                Layout_enquiry.Visibility = System.Windows.Visibility.Collapsed;
                stateview = StateView.none;
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch
            {

                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void BT_enquiry_cancel_Click(object sender, RoutedEventArgs e)
        {
            Layout_enquiry.Visibility = System.Windows.Visibility.Collapsed;
            stateview = StateView.none;
        }

        private void BT_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}