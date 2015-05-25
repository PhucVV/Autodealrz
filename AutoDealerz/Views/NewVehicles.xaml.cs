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

namespace AutoDealerz.Views
{
    public partial class NewVehicles : PhoneApplicationPage
    {
        public NewVehicles()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Car data = PhoneApplicationService.Current.State["new_vehicle"] as Car;
            LayoutRoot.DataContext = data;
            LoadCarDetail(data.car_id);
            ListVariantModel model = new ListVariantModel();
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            model.OKAction += () =>
                {
                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                    List_variant.ItemsSource = model.ListVariant;
                };
            model.LoadData(data.car_id);
        }

        private async void LoadCarDetail(string code)
        {
            try
            {
                progress_bar.Visibility = System.Windows.Visibility.Visible;
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                         new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                         new KeyValuePair<string, string>("car_id", code)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiNewcarDetail(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<NewCarDetailObject>(responseString);
                if (data.status == "true")
                {
                    TB_upto.Text = data.upto_engine_cc;
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed; 
            }
        }

        private void List_variant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_variant.SelectedIndex < 0) return;
            PhoneApplicationService.Current.State["new_vehicle_variant"] = List_variant.SelectedItem as Varient;
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Views/NewVehicleDetail.xaml", UriKind.Relative));
            List_variant.SelectedIndex = -1;
        }

        private void BT_back_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.GoBack();
        }
    }
}