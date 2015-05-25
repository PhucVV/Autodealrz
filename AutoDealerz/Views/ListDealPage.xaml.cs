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

namespace AutoDealerz.Views
{
    public partial class ListDealPage : PhoneApplicationPage
    {
        public ListDealPage()
        {
            InitializeComponent();
   
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string type = "";
            if( NavigationContext.QueryString.TryGetValue("type", out type))
            {
                progress_bar.Visibility = System.Windows.Visibility.Visible;
                try
                {
                    var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                         new KeyValuePair<string, string>("status", type),
              
                        new KeyValuePair<string, string>("language", "en")
                    };

                    var httpClient = new HttpClient(new HttpClientHandler());
                    HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiListDeals(), new FormUrlEncodedContent(values));
                    response.EnsureSuccessStatusCode();
                    var responseString = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<NewsObject>(responseString);
                    if (data.status == "true")
                    {
                        List_deals.ItemsSource = data.news;
                    }
                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                }
                catch
                {
                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }

        private void List_deals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_deals.SelectedIndex < 0) return;
            PhoneApplicationService.Current.State["news"] = List_deals.SelectedItem as News;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/NewsPage.xaml?title=DEALS", UriKind.Relative));
            List_deals.SelectedIndex = -1;
        }


    }
}