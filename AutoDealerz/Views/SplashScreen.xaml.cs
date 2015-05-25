using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Notification;
using System.Net.Http;
using AutoDealerz.Models;
using Newtonsoft.Json;

namespace AutoDealerz.Views
{
    public partial class SplashScreen : PhoneApplicationPage
    {
        string key_push = "1";

        public SplashScreen()
        {
            InitializeComponent();
            HttpNotificationChannel pushChannel;
            string channelName = "AutoDealerz";
          

            pushChannel = HttpNotificationChannel.Find(channelName);
            if (pushChannel == null)
            {
                pushChannel = new HttpNotificationChannel(channelName);
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);
                pushChannel.HttpNotificationReceived += pushChannel_HttpNotificationReceived;
                pushChannel.Open();
                pushChannel.BindToShellToast();

            }
            else
            {
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);
                pushChannel.HttpNotificationReceived += pushChannel_HttpNotificationReceived;
            }
            Splash_Screen();
        }

        void pushChannel_HttpNotificationReceived(object sender, HttpNotificationEventArgs e)
        {
         
        }
        void PushChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            key_push = e.ChannelUri.ToString();

        }
        void PushChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.ToString());
        }
        void PushChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains("SHOW_PUSH"))
            {
               
            }
        }

        async void Splash_Screen()
        {
            GetDealerInfo();
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (!settings.Contains("USERNAME") || !settings.Contains("PASSWORD"))
            {
                await Task.Delay(TimeSpan.FromSeconds(2)); // set your desired delay
                NavigationService.Navigate(new Uri("/Views/LoginScreen.xaml?key=" + HttpUtility.UrlEncode(key_push), UriKind.Relative));
            }
            else
            {
                Login(settings["USERNAME"].ToString(), settings["PASSWORD"].ToString());
            }

        }

        private async void Login(string name, string pass)
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("email_id", name),
                        new KeyValuePair<string, string>("password", pass),
                        new KeyValuePair<string, string>("gcm_id", key_push),
                        new KeyValuePair<string, string>("language", "en"),
                          new KeyValuePair<string, string>("user_type", "3"),
                           new KeyValuePair<string, string>("dealer_id", App.Dealer_ID)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiLogin(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<LoginObject>(responseString);
                if (data.status!="true")
                {
                    NavigationService.Navigate(new Uri("/Views/LoginScreen.xaml?key=" + HttpUtility.UrlEncode(key_push), UriKind.Relative));
                }
                else
                {
                    App.Customer_ID = data.customer_id;
                    NavigationService.Navigate(new Uri("/Views/MainPage.xaml", UriKind.Relative));
                }

            }
            catch
            {
                NavigationService.Navigate(new Uri("/Views/LoginScreen.xaml?key=" + HttpUtility.UrlEncode(key_push), UriKind.Relative));
            }
        }

        private async void GetDealerInfo()
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("app_package_name", "Basic"),
                        new KeyValuePair<string, string>("language", "en")
                  
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetdialerInfo(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<DealerObject>(responseString);
                if (data.status == "true")
                    App.Dealer_ID = data.dealer_id;

            }
            catch
            {
               
            }
        }
     
    }
}