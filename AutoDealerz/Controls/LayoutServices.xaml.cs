using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.Http;
using AutoDealerz.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Coding4Fun.Toolkit.Controls;
using System.Globalization;
using System.Collections;

namespace AutoDealerz.Controls
{
    public partial class LayoutServices : UserControl
    {
        public LayoutServices()
        {
            InitializeComponent();
       Unloaded+= LayoutServices_Unloaded;
        }

        void LayoutServices_Unloaded(object sender, RoutedEventArgs e)
        {
            my_message.Hide();
        }

         ListCityService LSC;
        public void CheckData()
        {
            if (CB_city.ItemsSource == null)
            {
                if (App.ListServiceCity.Count > 0)
                {
                    CB_city.ItemsSource = App.ListServiceCity;
                    return;
                }
                loading_city.Visibility = System.Windows.Visibility.Visible;
                LSC = new ListCityService();
                LSC.LoadData();
                LSC.OKAction += () =>
                {
                    loading_city.Visibility = System.Windows.Visibility.Collapsed;
                    CB_city.ItemsSource = App.ListServiceCity;
                };
            }
        }

        private void MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(MainPivot.SelectedIndex)
            {
                case 0: break;
                case 1:
                    if (List_my_request.ItemsSource == null)
                        GetMyRequest();
                    break;
                case 2:
                    GetCompleteService();
                   
                    break;
            }
        }
        private async void GetCompleteService()
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("customer_id", App.Customer_ID),
                        new KeyValuePair<string, string>("language", "en")
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetCompleteService(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CompleteServiceObject>(responseString);
                if (data.status == "true")
                {
                    CB_vehicle_feed.ItemsSource = data.services;
                }
                else
                {
                    MessageBox.Show("No new service was completed. So you can't give a feedback", "Message", MessageBoxButton.OK);
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }



        ObservableCollection<RequestData> ListRequest = new ObservableCollection<RequestData>();
        private async void GetMyRequest()
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("customer_id", App.Customer_ID),
                        new KeyValuePair<string, string>("language", "en")
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetMyRequest(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<MyRequestObject>(responseString);
                if(data.status=="true")
                {
                    foreach (var item in data.services)
                    {
                        item.service_description = item.service_description +","+ item.other_service;
                        ListRequest.Add(item);
                    }
                    List_my_request.ItemsSource = ListRequest;
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void CB_city_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private async void BT_delete_my_request_Click(object sender, RoutedEventArgs e)
        {
            string id = (sender as Button).Tag.ToString();
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                        new KeyValuePair<string, string>("language", "en"),
                            new KeyValuePair<string, string>("c_s_r_id",id)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiDeleteMyrequest(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<DeleteMyRequestObject>(responseString);
                if (data.status == "true")
                {
                       for (int i = 0; i < ListRequest.Count; i++)
                       {
                           if(ListRequest[i].c_s_r_id==id)
                           
                       ListRequest.RemoveAt(i);
                       }
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }

        }



        #region feedback
        private void BT_send_feed_Click(object sender, RoutedEventArgs e)
        {
            if(CB_vehicle_feed.SelectedItem == null)
            {
                var toast = new ToastPrompt
                {
                    Title = "Please select Vehicle",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if(TB_comment_feed.Text.Trim()=="" || TB_comment_feed.Text=="Comments")
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter comments",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            SendFeedback();
        }
        private async void SendFeedback()
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("vehical_id", (CB_vehicle_feed.SelectedItem as CompleteService).c_v_id),
                        new KeyValuePair<string, string>("comments",TB_comment_feed.Text),
                        new KeyValuePair<string, string>("branch_id", (CB_branch_name.SelectedItem as Branch).branch_id),
                        new KeyValuePair<string, string>("language", "en"),
                          new KeyValuePair<string, string>("customer_id", App.Customer_ID),
                           new KeyValuePair<string, string>("feedback", quality_feed)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiSendFeedback(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<FeedBackObject>(responseString);
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                var toast = new ToastPrompt
                {
                    Title = "",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = data.msg,
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void RBT_excellent_Checked(object sender, RoutedEventArgs e)
        {
            quality_feed = "Excellent";
        }

        string quality_feed = "Excellent";
        private void RBT_good_Checked(object sender, RoutedEventArgs e)
        {
            quality_feed = "Good";
        }

        private void RBT_poor_Checked(object sender, RoutedEventArgs e)
        {
            quality_feed = "Poor";
        }

        private void TB_comment_feed_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_comment_feed.Text == "Comments")
                TB_comment_feed.Text = "";
        }

        private void TB_comment_feed_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_comment_feed.Text))
                TB_comment_feed.Text = "Comments";
        }
        #endregion

        private void CB_city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_city.SelectedItem == null || CB_city.SelectedIndex < 0) return;
            loading_brand_name.Visibility = System.Windows.Visibility.Visible;
            ServiceCity data = CB_city.SelectedItem as ServiceCity;
            ListBranchModel model = new ListBranchModel();
            model.OKAction += () =>
            {
                loading_brand_name.Visibility = System.Windows.Visibility.Collapsed;
                CB_branch_name.ItemsSource = model.ListBranch;
            };
            model.LoadData(data.city_id);
        }

        private void CB_branch_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_vehicle.ItemsSource == null)
            {
                loading_vehicle_feed.Visibility = System.Windows.Visibility.Visible;
                ListCustomerVehicle cusve = new ListCustomerVehicle();
                cusve.OKAction += () =>
                {
                    loading_vehicle_feed.Visibility = System.Windows.Visibility.Collapsed;
                    CB_vehicle.ItemsSource = cusve.ListVehicle;
                };
                cusve.LoadData();
            }
        }
        ListServiceType ListServiceType;
        private void CB_vehicle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_service_type.ItemsSource == null)
            {
                loading_service_type.Visibility = System.Windows.Visibility.Visible;
                ListServiceType = new ListServiceType();
                ListServiceType.OKAction += () =>
                {
                    loading_service_type.Visibility = System.Windows.Visibility.Collapsed;
                    CB_service_type.ItemsSource = ListServiceType.list_service;
                };
                ListServiceType.LoadData();
            }
        }

        private void BT_send_Click(object sender, RoutedEventArgs e)
        {
             if (CB_city.SelectedItem == null)
            {
                var toast = new ToastPrompt
                {
                    Title = "Please select city",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (CB_branch_name.SelectedItem == null)
            {
                var toast = new ToastPrompt
                {
                    Title = "Please select branch",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (CB_vehicle.SelectedItem == null)
            {
                var toast = new ToastPrompt
                {
                    Title = "Please select vehicle",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (CB_service_type.SelectedItems == null && string.IsNullOrEmpty(TB_other.Text))
            {
                var toast = new ToastPrompt
                {
                    Title = "Please select Service Type or enter Other field",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            
            SendServiceRequest();
        }

     

        private async void SendServiceRequest()
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try

            {
                string service_name = "";
                foreach (var item in CB_service_type.SelectedItems)
                {
                    service_name += (item as ServiceType).service_type_id + ",";
                }
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("customer_id", App.Customer_ID),
                        new KeyValuePair<string, string>("c_v_id",(CB_vehicle.SelectedItem as Vehicle).c_v_id),
                       new KeyValuePair<string, string>("city_id",(CB_city.SelectedItem as ServiceCity).city_id),
                       new KeyValuePair<string, string>("branch_id",(CB_branch_name.SelectedItem as Branch).branch_id),
                        new KeyValuePair<string, string>("service_description",service_name),
                       new KeyValuePair<string, string>("schedule_datetime",CB_date.Value.Value.Date.ToShortDateString() + " " +
                           CB_time.Value.Value.Hour+":"+CB_time.Value.Value.Minute),
                        new KeyValuePair<string, string>("language", "en"),
                            new KeyValuePair<string, string>("other_service",TB_other.Text)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.SendServiceRequest(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ServiceRequestObject>(responseString);
                if (data.status == "true")
                {
                    MessageBox.Show(data.msg);
                    if(App.Notification==false)
                    my_message.Show("Message", "Alerts Disabled – To receive updates on Dealerships, Special promotions, New inventory arrivals and other important information. Kindly enable Notifications in the App Setting section on main Menu", "", "OK", 1);
                    TB_other.Text = "";
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void CB_service_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CB_service_type.SummaryForSelectedItemsDelegate = SummaryForSelectedItemsDelegate;
        }
        private string SummaryForSelectedItemsDelegate(System.Collections.IList list)
        {
            var summary = String.Empty;
            for (var i = 0; i < list.Count; i++)
            {
                var isLast = i == list.Count - 1;
                var item = (ServiceType)list[i];
                summary = String.Concat(summary, item.service_type_name);
                summary += isLast ? string.Empty : ", ";
            }
            if (summary == String.Empty)
            {
                summary = "No selection";
            }
            return summary;
        }
    }
}
