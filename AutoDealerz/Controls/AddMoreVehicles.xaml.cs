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
using Coding4Fun.Toolkit.Controls;
using System.Net.Http;
using Newtonsoft.Json;

namespace AutoDealerz.Controls
{
    public partial class AddMoreVehicles : UserControl
    {
        List<string> Month = new List<string>() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
      
        public AddMoreVehicles()
        {
            InitializeComponent();
            CB_month.ItemsSource = Month;
        }

        public void LoadData()
        {
            if (CB_brand_name.ItemsSource == null)
            {
                if (App.ListBrand.Count > 0)
                {
                    CB_brand_name.ItemsSource = App.ListBrand;
                    return;
                }
                loading_brand_name.Visibility = System.Windows.Visibility.Visible;
                ListBrandModel model = new ListBrandModel();
                model.OKAction += () =>
                {
                    loading_brand_name.Visibility = System.Windows.Visibility.Collapsed;
                    CB_brand_name.ItemsSource = App.ListBrand;
                };
                model.LoadData();
            }
        }

        private void BT_add_Click(object sender, RoutedEventArgs e)
        {
            if (CB_brand_name.SelectedItem == null)
            {
                var toast = new ToastPrompt
                {
                    Title = "Please select brand name",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (CB_model_name.SelectedItem == null)
            {
                var toast = new ToastPrompt
                {
                    Title = "Please select model name",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (string.IsNullOrEmpty(TB_variant.Text))
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter viriant",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (string.IsNullOrEmpty(TB_subvariant.Text))
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter sub viriant",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
          
            if (string.IsNullOrEmpty(TB_year.Text))
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter year",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (string.IsNullOrEmpty(TB_vin_no.Text))
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter Vin no",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (TB_vin_no.Text.Length != 17)
            {
                var toast = new ToastPrompt
                {
                    Title = "VIN No. Should be 17 digit long (Alphanumeric)",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            Add();
        }

        private async void Add()
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    { 
                        new KeyValuePair<string, string>("language", "en"),
                        new KeyValuePair<string, string>("customer_id",App.Customer_ID),
                        new KeyValuePair<string, string>("brand_id", (CB_brand_name.SelectedItem as Brand).brand_id),

                        new KeyValuePair<string, string>("model_no", (CB_model_name.SelectedItem as Model).model_id),
                         new KeyValuePair<string, string>("model_year", TB_year.Text),
                        new KeyValuePair<string, string>("variant", TB_variant.Text),
                        new KeyValuePair<string, string>("sub_variant",TB_subvariant.Text),
                       
                        new KeyValuePair<string, string>("vin_vo", TB_vin_no.Text),
                        
                        new KeyValuePair<string, string>("model_month", CB_month.SelectedItem as string)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiAddVehicle(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
               
                var data = JsonConvert.DeserializeObject<RegisterObject2>(responseString);
                if (data.status == "true")
                {
                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                    var toast = new ToastPrompt
                    {
                        Title = data.msg,
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Message = "",
                        TextWrapping = TextWrapping.Wrap
                    };
                    toast.Show();

                    TB_variant.Text = "";
                    TB_subvariant.Text = "";
                    TB_year.Text = "";
                    TB_vin_no.Text = "";
            
                }
                else
                {
                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                    var toast = new ToastPrompt
                    {
                        Title = data.msg,
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Message = "",
                        TextWrapping = TextWrapping.Wrap
                    };
                    toast.Show();
                }
            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }


        private void CB_brand_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_brand_name.SelectedItem == null) return;
            Brand data = CB_brand_name.SelectedItem as Brand;
            loading_model_name.Visibility = System.Windows.Visibility.Visible;
            ListModelModel model = new ListModelModel();
            model.OKAction += () =>
            {
                loading_model_name.Visibility = System.Windows.Visibility.Collapsed;
                CB_model_name.ItemsSource = model.ListModel;
            };
            model.LoadData(data.brand_id);
        }

        private void CB_model_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CB_brand_name_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void TB_variant_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_subvariant.Focus();
        }

        private void TB_subvariant_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                CB_month.Focus();
        }

        private void TB_month_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_year.Focus();
        }

        private void TB_year_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_vin_no.Focus();
        }

        private void TB_vin_no_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                BT_add.Focus();
        }
    }
}
