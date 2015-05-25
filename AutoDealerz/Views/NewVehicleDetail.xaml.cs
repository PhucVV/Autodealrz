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
    public partial class NewVehicleDetail : PhoneApplicationPage
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
        public NewVehicleDetail()
        {
            InitializeComponent();
            stateview = StateView.none;
        }

        Varient variandata;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            variandata = PhoneApplicationService.Current.State["new_vehicle_variant"] as Varient;
            LoadCarDetail(variandata.car_id);
         
        }
        NewCarDetailObject datadetail;
        private async void LoadCarDetail(string code)
        {
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
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiNewcarDetail(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                datadetail = JsonConvert.DeserializeObject<NewCarDetailObject>(responseString);
                if (datadetail.status == "true")
                {

                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                    TB_variant_name.Text = datadetail.brand_name + ", " + datadetail.model_name + "-" + datadetail.variant;
                    LayoutRoot.DataContext = datadetail;
                    List_available_brach.ItemsSource = datadetail.available_branches;
                    CB_branch_brochure.ItemsSource = datadetail.available_branches;
                    CB_branch_name.ItemsSource = datadetail.available_branches;
                    CB_branch_enquiry.ItemsSource = datadetail.available_branches;
                }

            }
            catch
            {

                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            switch(stateview)
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
                        Layout_brochure.Visibility = System.Windows.Visibility.Collapsed;
                    stateview = StateView.none;
                    e.Cancel = true;
                    break;
                case StateView.test_drive:
                     Layout_test_drive.Visibility = System.Windows.Visibility.Collapsed;
                    stateview = StateView.none;
                    e.Cancel = true;
                    break;
            }
        }

        private void BT_branches_Click(object sender, RoutedEventArgs e)
        {
            Layout_braches.Visibility = System.Windows.Visibility.Visible;
            stateview = StateView.branches;
        }

        private void BT_test_dive_Click(object sender, RoutedEventArgs e)
        {
            Layout_test_drive.Visibility = System.Windows.Visibility.Visible;
            stateview = StateView.test_drive;
        }

        private void BT_brochure_Click(object sender, RoutedEventArgs e)
        {
            Layout_brochure.Visibility = System.Windows.Visibility.Visible;
            stateview = StateView.prochu;
        }

        private void BT_enquiry_Click(object sender, RoutedEventArgs e)
        {
            Layout_enquiry.Visibility = System.Windows.Visibility.Visible;
            stateview = StateView.enquiri;
        }

        private async void BT_test_drive_send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                progress_bar.Visibility = System.Windows.Visibility.Visible;
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                         new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                         new KeyValuePair<string, string>("customer_id", App.Customer_ID),
                             new KeyValuePair<string, string>("car_id", variandata.car_id),
               new KeyValuePair<string, string>("date_time", CB_date.Value.Value.Date.ToShortDateString() + " " +
                           CB_time.Value.Value.Hour+":"+CB_time.Value.Value.Minute),
                      new KeyValuePair<string, string>("branch_id", (CB_branch_name.SelectedItem as AvailableBranch).branch_id),
                          new KeyValuePair<string, string>("Send mail - brand_name",  (CB_branch_name.SelectedItem as AvailableBranch).branch_email_id),
                                new KeyValuePair<string, string>("model_name",  datadetail.model_name),
                                     new KeyValuePair<string, string>("price",  datadetail.price),
                  new KeyValuePair<string, string>("variant",  datadetail.variant),
                    new KeyValuePair<string, string>("model_year_month",  datadetail.month_year),
               new KeyValuePair<string, string>("car_image",  datadetail.car_image),
                  new KeyValuePair<string, string>("customer_name",""),
                     new KeyValuePair<string, string>("customer_email_id", ""),
                          new KeyValuePair<string, string>("customer_contact", ""),
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiTestDrive(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<SimpleMyRequestObject>(responseString);
                if (data.status == "true")
                {
                    MessageBox.Show(data.msg);
                }
                Layout_test_drive.Visibility = System.Windows.Visibility.Collapsed;
                stateview = StateView.none;
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch
            {

                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private async void BT_brochure_send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                progress_bar.Visibility = System.Windows.Visibility.Visible;
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                         new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                         new KeyValuePair<string, string>("customer_id", App.Customer_ID),
                             new KeyValuePair<string, string>("car_id", variandata.car_id),
               new KeyValuePair<string, string>("date_time", CB_date.Value.Value.Date.ToShortDateString() + " " +
                           CB_time.Value.Value.Hour+":"+CB_time.Value.Value.Minute),
                      new KeyValuePair<string, string>("branch_id", (CB_branch_name.SelectedItem as AvailableBranch).branch_id),
                          new KeyValuePair<string, string>("Send mail - brand_name",  (CB_branch_name.SelectedItem as AvailableBranch).branch_email_id),
                                new KeyValuePair<string, string>("model_name",  datadetail.model_name),
                                     new KeyValuePair<string, string>("price",  datadetail.price),
                  new KeyValuePair<string, string>("variant",  datadetail.variant),
                    new KeyValuePair<string, string>("model_year_month",  datadetail.month_year),
               new KeyValuePair<string, string>("car_image",  datadetail.car_image),
                  new KeyValuePair<string, string>("customer_name",""),
                     new KeyValuePair<string, string>("customer_email_id", ""),
                          new KeyValuePair<string, string>("customer_contact", ""),
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiTestDrive(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<SimpleMyRequestObject>(responseString);
                if (data.status == "true")
                {
                    MessageBox.Show(data.msg);
                }
                Layout_brochure.Visibility = System.Windows.Visibility.Collapsed;
                stateview = StateView.none;
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch
            {

                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void BT_brochure_cancel_Click(object sender, RoutedEventArgs e)
        {
            Layout_brochure.Visibility = System.Windows.Visibility.Collapsed;
            stateview = StateView.none;
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
                                new KeyValuePair<string, string>("car_id", variandata.car_id),
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

        private void TB_equiry_title_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_equiry_title.Text == "Enquiry Title")
                TB_equiry_title.Text = "";
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

        private void TB_comment_feed_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_comment_feed.Text))
                TB_comment_feed.Text = "Description";
        }

        private void TB_equiry_title_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_comment_feed.Focus();
        }

        private void TB_comment_feed_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void BT_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }



    }
}