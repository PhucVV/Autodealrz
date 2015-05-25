using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using AutoDealerz.Models;
using System.Net.Http;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Notification;
using Newtonsoft.Json;

namespace AutoDealerz.Views
{
    public partial class LoginScreen : PhoneApplicationPage
    {
  
        public LoginScreen()
        {
            InitializeComponent();
            Loaded += LoginScreen_Loaded;
        }

        void LoginScreen_Loaded(object sender, RoutedEventArgs e)
        {
            while (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }
        }

       

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            if(my_message.IsShow)
            {
                my_message.Hide();
                e.Cancel = true;

            }
            else if(for_got_pass.IsShow)
            {
                for_got_pass.Hide();
                e.Cancel = true;
            }
            else
            {
                Application.Current.Terminate();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavigationContext.QueryString.TryGetValue("key", out key_push);

            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (!settings.Contains("ThankForDownload"))
            {
                settings.Add("ThankForDownload", DateTime.Now.ToString());
                my_message.Show("Message", "Thank you for downloading the dealership App, we are just a click away from you", "", "OK", 1);
            }
            settings.Save();
        }

        private void BT_login_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(TB_username.Text))
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter Email id",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if(string.IsNullOrEmpty(TB_pass.Password))
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter Password",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            Login(TB_username.Text, TB_pass.Password);
 
        }

        string key_push = "1";
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
               if(data.status!="true")
               {
                   var toast = new ToastPrompt
                   {
                       Title = "Login fail!",
                       TextOrientation = System.Windows.Controls.Orientation.Vertical,
                       Message = "",
                       TextWrapping = TextWrapping.Wrap
                   };
                   toast.Show();
                   progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                   return;
               }
               else
               {
                   App.Customer_ID = data.customer_id;
                   var toast = new ToastPrompt
                   {
                       Title = data.msg,
                       TextOrientation = System.Windows.Controls.Orientation.Vertical,
                       Message = "",
                       TextWrapping = TextWrapping.Wrap
                   };
                   toast.Show();
                   progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                   IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                   if (!settings.Contains("USERNAME"))
                   {
                       settings.Add("USERNAME", name);
                   }
                   else
                   {
                       settings["USERNAME"] = name;
                   }
                   if (!settings.Contains("PASSWORD"))
                   {
                       settings.Add("PASSWORD", pass);
                   }
                   else
                   {
                       settings["PASSWORD"] = pass;
                   }
                   settings.Save();
                   NavigationService.Navigate(new Uri("/Views/MainPage.xaml?key=" + HttpUtility.UrlEncode(key_push), UriKind.Relative));
               }

           }
            catch
           {
               progress_bar.Visibility = System.Windows.Visibility.Collapsed;
           }
        }

        private void BT_sign_up_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/InfoScreen.xaml?key="+key_push, UriKind.Relative));
        }

        private void TB_forgot_pass_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            for_got_pass.Show();
        }

        private void TB_username_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_pass.Focus();
        }

        private void TB_pass_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                BT_login.Focus();
        }
    }
}