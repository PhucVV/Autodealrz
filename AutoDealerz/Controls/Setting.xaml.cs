using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.Net.Http;
using AutoDealerz.Models;
using Newtonsoft.Json;

namespace AutoDealerz.Controls
{
    public partial class Setting : UserControl
    {
        public Setting()
        {
            InitializeComponent();
        }

        private async void BT_notifi_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if(BT_notifi.Tag.ToString()=="on")
            {
                try
                {
                    progress_bar.Visibility = System.Windows.Visibility.Visible;
                    var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                             new KeyValuePair<string, string>("dealer_id",App.Dealer_ID),
                                  new KeyValuePair<string, string>("customer_id",App.Customer_ID),
                                    new KeyValuePair<string, string>("type","2"),
                                     new KeyValuePair<string, string>("onoff_status","2")
                    };

                    var httpClient = new HttpClient(new HttpClientHandler());
                    HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiSettingMailOnOff(), new FormUrlEncodedContent(values));
                    response.EnsureSuccessStatusCode();
                    var responseString = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ChangeSettingObject>(responseString);
                    if (data.status == "true")
                    {
                        App.Notification = false;
                        BitmapImage bm = new BitmapImage(new Uri("/Images/toggle_off.png", UriKind.RelativeOrAbsolute));
                        BT_notifi.Tag = "off";
                        BT_notifi.Source = bm;
                        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                        if (settings.Contains("SHOW_PUSH"))
                        {
                            settings.Remove("SHOW_PUSH");
                        }
                        settings.Save();
                    }
                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                }
                catch
                {
                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                }
              
              
            }
            else
            {

                try
                {
                    progress_bar.Visibility = System.Windows.Visibility.Visible;
                    var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                             new KeyValuePair<string, string>("dealer_id",App.Dealer_ID),
                                  new KeyValuePair<string, string>("customer_id",App.Customer_ID),
                                    new KeyValuePair<string, string>("type","2"),
                                     new KeyValuePair<string, string>("onoff_status","1")
                    };

                    var httpClient = new HttpClient(new HttpClientHandler());
                    HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiSettingMailOnOff(), new FormUrlEncodedContent(values));
                    response.EnsureSuccessStatusCode();
                    var responseString = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ChangeSettingObject>(responseString);
                    if (data.status == "true")
                    {
                        App.Notification = true;
                        BitmapImage bm = new BitmapImage(new Uri("/Images/toggle_on.png", UriKind.RelativeOrAbsolute));
                        BT_notifi.Tag = "on";
                        BT_notifi.Source = bm;
                        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                        if (!settings.Contains("SHOW_PUSH"))
                        {
                            settings.Add("SHOW_PUSH", DateTime.Now.ToString());
                        }
                        settings.Save();
                    }
                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                }
                catch
                {
                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                }
              

               
            }
        }

        private async void BT_sell_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (BT_sell.Tag.ToString() == "on")
            {
                try
                {
                    progress_bar.Visibility = System.Windows.Visibility.Visible;
                    var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                             new KeyValuePair<string, string>("dealer_id",App.Dealer_ID),
                                  new KeyValuePair<string, string>("customer_id",App.Customer_ID),
                                    new KeyValuePair<string, string>("type","1"),
                                     new KeyValuePair<string, string>("onoff_status","2")
                    };

                    var httpClient = new HttpClient(new HttpClientHandler());
                    HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiSettingMailOnOff(), new FormUrlEncodedContent(values));
                    response.EnsureSuccessStatusCode();
                    var responseString = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ChangeSettingObject>(responseString);
                    if (data.status == "true")
                    {
                        BitmapImage bm = new BitmapImage(new Uri("/Images/toggle_off.png", UriKind.RelativeOrAbsolute));
                        BT_sell.Tag = "off";
                        BT_sell.Source = bm;
                    }
                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                }
                catch
                {
                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                }

            }
            else
            {
                try
                {
                    progress_bar.Visibility = System.Windows.Visibility.Visible;
                    var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                             new KeyValuePair<string, string>("dealer_id",App.Dealer_ID),
                                  new KeyValuePair<string, string>("customer_id",App.Customer_ID),
                                    new KeyValuePair<string, string>("type","1"),
                                     new KeyValuePair<string, string>("onoff_status","1")
                    };

                    var httpClient = new HttpClient(new HttpClientHandler());
                    HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiSettingMailOnOff(), new FormUrlEncodedContent(values));
                    response.EnsureSuccessStatusCode();
                    var responseString = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ChangeSettingObject>(responseString);
                    if (data.status == "true")
                    {
                        BitmapImage bm = new BitmapImage(new Uri("/Images/toggle_on.png", UriKind.RelativeOrAbsolute));
                        BT_sell.Tag = "on";
                        BT_sell.Source = bm;
                    }
                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                }
                catch
                {
                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                }
              
            
            }
        }
  
        public async void LoadSetting()
        {

            try
            {
                progress_bar.Visibility = System.Windows.Visibility.Visible;
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                             new KeyValuePair<string, string>("dealer_id",App.Dealer_ID),
                                  new KeyValuePair<string, string>("customer_id",App.Customer_ID)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiSettingStatus(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CheckSettingObject>(responseString);
                if(data.status=="true")
                {
                    if (data.notifcation_onoff_status!="1")
                    {
                        App.Notification = false;
                        BitmapImage bm = new BitmapImage(new Uri("/Images/toggle_off.png", UriKind.RelativeOrAbsolute));
                        BT_notifi.Tag = "off";
                        BT_notifi.Source = bm;
                        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                        if (settings.Contains("SHOW_PUSH"))
                        {
                            settings.Remove("SHOW_PUSH");
                        }
                        settings.Save();
                    }
                    else
                    {
                        App.Notification = true;
                        BitmapImage bm = new BitmapImage(new Uri("/Images/toggle_on.png", UriKind.RelativeOrAbsolute));
                        BT_notifi.Tag = "on";
                        BT_notifi.Source = bm;
                        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                        if (!settings.Contains("SHOW_PUSH"))
                        {
                            settings.Add("SHOW_PUSH", DateTime.Now.ToString());
                        }
                        settings.Save();
                    }


                    if (data.email_onoff_status=="1")
                        {
                            BitmapImage bm = new BitmapImage(new Uri("/Images/toggle_on.png", UriKind.RelativeOrAbsolute));
                            BT_sell.Tag = "on";
                            BT_sell.Source = bm;
                        }
                        else
                        {
                            BitmapImage bm = new BitmapImage(new Uri("/Images/toggle_off.png", UriKind.RelativeOrAbsolute));
                            BT_sell.Tag = "off";
                            BT_sell.Source = bm;
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
