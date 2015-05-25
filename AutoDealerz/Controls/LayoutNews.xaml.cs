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
    public partial class LayoutNews : UserControl
    {
        public LayoutNews()
        {
            InitializeComponent();
        }

        private void MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(MainPivot.SelectedIndex)
            {
                case 0:
                    if(List_news.ItemsSource==null)
                    GetNews();
                    break;
                case 1:
                    if (string.IsNullOrEmpty(TB_parts_count.Text))
                        GetDealCount();
                    break;
                case 2:
                    Browser_face.Navigate(new Uri("https://www.facebook.com/autodealrz"));
                    break;
                case 3:
                    Browser_twitter.Navigate(new Uri("https://twitter.com/autodealrz"));
                    break;
            }
        }

        private async void GetNews()
        {
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
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetNews(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                NewsObject data = JsonConvert.DeserializeObject<NewsObject>(responseString);
                if (data.status == "true")
                {
                    for(int i = data.news.Count-1;i>=0; i--)
                    {
                        if (data.news[i].recall_status == "0")
                            data.news[i].recall_status = "";
                        if (string.IsNullOrEmpty(data.news[i].news_id))
                            data.news.RemoveAt(i);
                    }
                    List_news.ItemsSource = data.news;
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;

            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private async void GetDealCount()
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
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiDealCount(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                DealCountObject data = JsonConvert.DeserializeObject<DealCountObject>(responseString);
                if (data.status == "true")
                {
                    TB_parts_count.Text = data.parts_count.ToString();
                    TB_sales_count.Text = data.sales_count.ToString();
                    TB_services_count.Text = data.services_count.ToString();
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;

            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void List_news_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_news.SelectedIndex < 0) return;
            PhoneApplicationService.Current.State["news"] = List_news.SelectedItem as News;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/NewsPage.xaml?title=NEWS", UriKind.Relative));
            List_news.SelectedIndex = -1;
        }

        private void Service_tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/ListDealPage.xaml?type=2", UriKind.Relative));
        }

        private void Sale_tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/ListDealPage.xaml?type=1", UriKind.Relative));
        }

        private void Parts_tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/ListDealPage.xaml?type=3", UriKind.Relative));
        }
    }
}
