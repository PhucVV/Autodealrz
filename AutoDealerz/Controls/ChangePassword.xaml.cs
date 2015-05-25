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
    public partial class ChangePassword : UserControl
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void BT_change_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_old_pass_word.Password))
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter old password",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (string.IsNullOrEmpty(TB_new_pass_word.Password))
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter new password",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (string.IsNullOrEmpty(TB_confirm_pass_word.Password))
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter confirm password",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }

            if (TB_new_pass_word.Password != TB_confirm_pass_word.Password)
            {
                var toast = new ToastPrompt
                {
                    Title = "Password does not match the confirm password",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            ChangePass();
        }

        private async void ChangePass()
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("customer_id", App.Customer_ID),
                        new KeyValuePair<string, string>("language", "en"),
                          new KeyValuePair<string, string>("old_password", TB_old_pass_word.Password),
                            new KeyValuePair<string, string>("new_password", TB_new_pass_word.Password),
                               new KeyValuePair<string, string>("dealer_id", App.Dealer_ID)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiChangePass(), new FormUrlEncodedContent(values));
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
                if(data.status=="true")
                {
                    TB_confirm_pass_word.Password = "";
                    TB_new_pass_word.Password = "";
                    TB_old_pass_word.Password = "";
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;

            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void TB_old_pass_word_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_new_pass_word.Focus();
        }

        private void TB_new_pass_word_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_confirm_pass_word.Focus();
        }

        private void TB_confirm_pass_word_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                BT_change.Focus();
        }

    }
}
