using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Coding4Fun.Toolkit.Controls;
using System.Net.Http;
using AutoDealerz.Models;
using Newtonsoft.Json;

namespace AutoDealerz.Controls
{
    public partial class ForgotPassword : UserControl
    {
 
        public ForgotPassword()
        {
            InitializeComponent();
        }
        public bool IsShow = false;
        public void Show()
        {
            IsShow = true;
            LayoutRoot.Visibility = System.Windows.Visibility.Visible;
        }

        public void Hide()
        {
            IsShow = false;
            progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            LayoutRoot.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void send_click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_email.Text))
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
            SendMail(TB_email.Text);
        }

        private async void SendMail(string mail)
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("email_id", mail),
          
                        new KeyValuePair<string, string>("language", "en"),
                          new KeyValuePair<string, string>("user_type", "1"),
                               new KeyValuePair<string, string>("dealer_id",App.Dealer_ID)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiForgotPass(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ForgetPassObject>(responseString);

                var toast = new ToastPrompt
                {
                    Title = data.msg,
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();

               if(data.status=="false")
               {
                  
                   progress_bar.Visibility = System.Windows.Visibility.Collapsed;
               }
               else
               {
                   Hide();
               }

            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

    }
}
