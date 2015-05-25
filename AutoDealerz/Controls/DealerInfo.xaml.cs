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

namespace AutoDealerz.Controls
{
    public partial class DealerInfo : UserControl
    {
        public DealerInfo()
        {
            InitializeComponent();
        }

        public async void LoadData()
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                        new KeyValuePair<string, string>("language", "en")
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetDealer(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<DealerInfoObject>(responseString);
                if (data.status == "true")
                {
                    LayoutRoot.DataContext = data;
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void List_branch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_branch.SelectedIndex < 0) return;
            PhoneApplicationService.Current.State["employee"] = List_branch.SelectedItem as AllBranch;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/DealerInfoDetail.xaml", UriKind.Relative));
            List_branch.SelectedIndex = -1;
        }


    }
}
