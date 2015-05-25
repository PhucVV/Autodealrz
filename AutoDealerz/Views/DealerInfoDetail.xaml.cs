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
using Microsoft.Phone.Tasks;

namespace AutoDealerz.Views
{
    public partial class DealerInfoDetail : PhoneApplicationPage
    {
        public DealerInfoDetail()
        {
            InitializeComponent();
        }

        private void BT_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            AllBranch data = PhoneApplicationService.Current.State["employee"] as AllBranch;
            LoadData(data.branch_id);
        }

        private async void LoadData(string code)
        {
            try
            {
                progress_bar.Visibility = System.Windows.Visibility.Visible;
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                         new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                         new KeyValuePair<string, string>("branch_id", code)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiBranchEmployee(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<EmployeeObject>(responseString);
                if (data.status == "true")
                {
                    List_data.ItemsSource = data.services;
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void Email_click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = "";
            emailComposeTask.Body = "";
            emailComposeTask.To = (sender as Button).Tag.ToString();
            emailComposeTask.Cc = "";
            emailComposeTask.Bcc = "";

            emailComposeTask.Show();
        }

        private void call_click(object sender, RoutedEventArgs e)
        {
            PhoneCallTask phoneCallTask = new PhoneCallTask();

            phoneCallTask.PhoneNumber = (sender as Button).Tag.ToString();
         

            phoneCallTask.Show();
        }
    }
}