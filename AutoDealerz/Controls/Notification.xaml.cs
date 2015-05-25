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

namespace AutoDealerz.Controls
{
    public partial class Notification : UserControl
    {

        ObservableCollection<NotificationData> Model = new ObservableCollection<NotificationData>();


        public Notification()
        {
            InitializeComponent();
        }

        public async void LoadData()
        {
            Model.Clear();
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                              new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                        new KeyValuePair<string, string>("language", "en"),
                          new KeyValuePair<string, string>("customer_id",App.Customer_ID)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiAllNotification(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                NotificationObject data = JsonConvert.DeserializeObject<NotificationObject>(responseString);
                if (data.status == "true")
                {
                    foreach(var item in data.notifications)
                    {
                        if (item.notification_status == "2" || item.notification_status == "9")
                            item.show_accept = true;
                        else
                            item.show_accept = false;
                        Model.Add(item);
                    }
                    List_notifi.ItemsSource = Model;
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;

            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

      public  bool Message_show = false;
        public void HideMessage()
      {
          my_message.Hide();
      }
        private string WorkOnItem = "";

        private void BT_accept_Click(object sender, RoutedEventArgs e)
        {
            Message_show = true;
            WorkOnItem = (sender as Button).Tag.ToString();
            my_message.Show("Message", "Do you want to confirm appointment ?", "Reject", "Confirm", 2);
            my_message.OKAction += async() =>
                {
                    Message_show = false;
                    foreach(var item in Model)
                    {
                        if(item.notification_id == WorkOnItem)
                        {
                            if(item.notification_status =="2")
                            {
                                try
                                {
                                    progress_bar.Visibility = System.Windows.Visibility.Visible;
                                    var values = new List<KeyValuePair<string, string>>
                                        {
                                                  new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                                            new KeyValuePair<string, string>("language", "en"),
                                              new KeyValuePair<string, string>("customer_id",App.Customer_ID),
                                                new KeyValuePair<string, string>("c_s_r_id",item.notification_common_id),
                                                 new KeyValuePair<string, string>("notification_id",item.notification_id),
                                                   new KeyValuePair<string, string>("status","4")
                                        };

                                    var httpClient = new HttpClient(new HttpClientHandler());
                                    HttpResponseMessage response = await httpClient.PostAsync("http://72.167.41.165/autodealer/webservices/service_reschedule_accept_reject.php", new FormUrlEncodedContent(values));
                                    response.EnsureSuccessStatusCode();
                                    var responseString = await response.Content.ReadAsStringAsync();
                                    SendPartsObject data = JsonConvert.DeserializeObject<SendPartsObject>(responseString);
                                    if (data.status == "true")
                                    {
                                        item.show_accept = false;
                                    }
                                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                                    var toast = new ToastPrompt
                                    {
                                        Title = data.msg,
                                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                                        Message = "",
                                        TextWrapping = TextWrapping.Wrap
                                    };
                                    toast.Show();
                                }
                                catch
                                {
                                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                                }
                            }
                            if(item.notification_status == "9")
                            {
                                try
                                {
                                    progress_bar.Visibility = System.Windows.Visibility.Visible;
                                    var values = new List<KeyValuePair<string, string>>
                                        {
                                                  new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                                            new KeyValuePair<string, string>("language", "en"),
                                              new KeyValuePair<string, string>("customer_id",App.Customer_ID),
                                                new KeyValuePair<string, string>("t_d_id",item.notification_common_id),
                                                 new KeyValuePair<string, string>("notification_id",item.notification_id),
                                                   new KeyValuePair<string, string>("status","4")
                                        };

                                    var httpClient = new HttpClient(new HttpClientHandler());
                                    HttpResponseMessage response = await httpClient.PostAsync("http://72.167.41.165/autodealer/webservices/testdrive_reschedule_accept_reject.php", new FormUrlEncodedContent(values));
                                    response.EnsureSuccessStatusCode();
                                    var responseString = await response.Content.ReadAsStringAsync();
                                    SendPartsObject data = JsonConvert.DeserializeObject<SendPartsObject>(responseString);
                                    if (data.status == "true")
                                    {
                                        item.show_accept = false;
                                    }
                                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                                    var toast = new ToastPrompt
                                    {
                                        Title = data.msg,
                                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                                        Message = "",
                                        TextWrapping = TextWrapping.Wrap
                                    };
                                    toast.Show();
                                }
                                catch
                                {
                                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                                }
                            }
                            break;
                        }
                    }

                };
            my_message.CancelAction += async() =>
                {
                    Message_show = false;
                    foreach (var item in Model)
                    {
                        if (item.notification_id == WorkOnItem)
                        {
                            if (item.notification_status == "2")
                            {
                                try
                                {
                                    progress_bar.Visibility = System.Windows.Visibility.Visible;
                                    var values = new List<KeyValuePair<string, string>>
                                        {
                                                  new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                                            new KeyValuePair<string, string>("language", "en"),
                                              new KeyValuePair<string, string>("customer_id",App.Customer_ID),
                                                new KeyValuePair<string, string>("c_s_r_id",item.notification_common_id),
                                                 new KeyValuePair<string, string>("notification_id",item.notification_id),
                                                   new KeyValuePair<string, string>("status","5")
                                        };

                                    var httpClient = new HttpClient(new HttpClientHandler());
                                    HttpResponseMessage response = await httpClient.PostAsync("http://72.167.41.165/autodealer/webservices/service_reschedule_accept_reject.php", new FormUrlEncodedContent(values));
                                    response.EnsureSuccessStatusCode();
                                    var responseString = await response.Content.ReadAsStringAsync();
                                    SendPartsObject data = JsonConvert.DeserializeObject<SendPartsObject>(responseString);
                                    if (data.status == "true")
                                    {
                                        item.show_accept = false;
                                    }
                                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                                    var toast = new ToastPrompt
                                    {
                                        Title = data.msg,
                                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                                        Message = "",
                                        TextWrapping = TextWrapping.Wrap
                                    };
                                    toast.Show();
                                }
                                catch
                                {
                                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                                }
                            }
                            if (item.notification_status == "9")
                            {
                                try
                                {
                                    progress_bar.Visibility = System.Windows.Visibility.Visible;
                                    var values = new List<KeyValuePair<string, string>>
                                        {
                                                  new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                                            new KeyValuePair<string, string>("language", "en"),
                                              new KeyValuePair<string, string>("customer_id",App.Customer_ID),
                                                new KeyValuePair<string, string>("t_d_id",item.notification_common_id),
                                                 new KeyValuePair<string, string>("notification_id",item.notification_id),
                                                   new KeyValuePair<string, string>("status","5")
                                        };

                                    var httpClient = new HttpClient(new HttpClientHandler());
                                    HttpResponseMessage response = await httpClient.PostAsync("http://72.167.41.165/autodealer/webservices/testdrive_reschedule_accept_reject.php", new FormUrlEncodedContent(values));
                                    response.EnsureSuccessStatusCode();
                                    var responseString = await response.Content.ReadAsStringAsync();
                                    SendPartsObject data = JsonConvert.DeserializeObject<SendPartsObject>(responseString);
                                    if (data.status == "true")
                                    {
                                        item.show_accept = false;
                                    }
                                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                                    var toast = new ToastPrompt
                                    {
                                        Title = data.msg,
                                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                                        Message = "",
                                        TextWrapping = TextWrapping.Wrap
                                    };
                                    toast.Show();
                                }
                                catch
                                {
                                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                                }
                            }
                            break;
                        }
                    }
                };
         
           
        }

        private async void BT_delete_Click(object sender, RoutedEventArgs e)
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                              new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                        new KeyValuePair<string, string>("language", "en"),
                          new KeyValuePair<string, string>("not_id",(sender as Button).Tag.ToString())
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiDeleteNotifi(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                SendPartsObject data = JsonConvert.DeserializeObject<SendPartsObject>(responseString);
                if (data.status == "true")
                {
                   foreach(var item in Model)
                   {
                       if(item.notification_id == (sender as Button).Tag.ToString())
                       {
                           Model.Remove(item);
                           break;
                       }
                   }
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;

            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
