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
using System.Threading.Tasks;

namespace AutoDealerz.Views
{
    public partial class InfoScreen : PhoneApplicationPage
    {
        List<string> Month = new List<string>() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
       
        public InfoScreen()
        {
            InitializeComponent();
            Storyboard.Completed += Storyboard_Completed;
            CB_month.ItemsSource = Month;
        }

        void Storyboard_Completed(object sender, EventArgs e)
        {
            if (CB_brand_name.ItemsSource == null)
           {
                if(App.ListBrand.Count>0)
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
        ListCountryModel LC;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationContext.QueryString.TryGetValue("key", out key_push);
            if(CB_country.ItemsSource == null)
            {
                if(App.ListCountry.Count>0)
                {
                    CB_country.ItemsSource = App.ListCountry;
                    return;
                }
                loading_country.Visibility = System.Windows.Visibility.Visible;
                LC = new ListCountryModel();
                LC.LoadData();
                LC.OKAction += () =>
                {
                    loading_country.Visibility = System.Windows.Visibility.Collapsed;
                    CB_country.ItemsSource = App.ListCountry;
                };
            }
            base.OnNavigatedTo(e);

        }

        private void BT_next_Click(object sender, RoutedEventArgs e)
        {
            
            if(string.IsNullOrEmpty(TB_first_name.Text))
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter first name",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }

                if(string.IsNullOrEmpty(TB_last_name.Text))
                {
                    var toast = new ToastPrompt
                    {
                        Title = "Please enter last name",
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Message = "",
                        TextWrapping = TextWrapping.Wrap
                    };
                    toast.Show();
                    return;
                }

                if (string.IsNullOrEmpty(TB_email.Text))
                {
                    var toast = new ToastPrompt
                    {
                        Title = "Please enter email",
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Message = "",
                        TextWrapping = TextWrapping.Wrap
                    };
                    toast.Show();
                    return;
                }
                if (string.IsNullOrEmpty(TB_pass_word.Text))
                {
                    var toast = new ToastPrompt
                    {
                        Title = "Please enter password",
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Message = "",
                        TextWrapping = TextWrapping.Wrap
                    };
                    toast.Show();
                    return;
                }
                if (string.IsNullOrEmpty(TB_phone.Text))
                {
                    var toast = new ToastPrompt
                    {
                        Title = "Please enter mobile number",
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Message = "",
                        TextWrapping = TextWrapping.Wrap
                    };
                    toast.Show();
                    return;
                }
                if (string.IsNullOrEmpty(TB_address.Text))
                {
                    var toast = new ToastPrompt
                    {
                        Title = "Please enter address",
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Message = "",
                        TextWrapping = TextWrapping.Wrap
                    };
                    toast.Show();
                    return;
                }
                if (string.IsNullOrEmpty(TB_street_name.Text))
                {
                    var toast = new ToastPrompt
                    {
                        Title = "Please enter street name",
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Message = "",
                        TextWrapping = TextWrapping.Wrap
                    };
                    toast.Show();
                    return;
                }
                if (string.IsNullOrEmpty(TB_last_name.Text))
                {
                    var toast = new ToastPrompt
                    {
                        Title = "Please enter last name",
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Message = "",
                        TextWrapping = TextWrapping.Wrap
                    };
                    toast.Show();
                    return;
                }
                if (CB_country.SelectedItem==null)
                {
                    var toast = new ToastPrompt
                    {
                        Title = "Please enter country",
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Message = "",
                        TextWrapping = TextWrapping.Wrap
                    };
                    toast.Show();
                    return;
                }
                if (CB_state.SelectedItem == null)
                {
                    var toast = new ToastPrompt
                    {
                        Title = "Please enter state",
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Message = "",
                        TextWrapping = TextWrapping.Wrap
                    };
                    toast.Show();
                    return;
                }
                if (CB_city.SelectedItem == null)
                {
                    var toast = new ToastPrompt
                    {
                        Title = "Please enter city",
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Message = "",
                        TextWrapping = TextWrapping.Wrap
                    };
                    toast.Show();
                    return;
                }
                if (string.IsNullOrEmpty(TB_pincode.Text))
                {
                    var toast = new ToastPrompt
                    {
                        Title = "Please enter pincode",
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Message = "",
                        TextWrapping = TextWrapping.Wrap
                    };
                    toast.Show();
                    return;
                }


                Register1();

        }

        string key_push = "";
        private async void Register1()
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("first_name", TB_first_name.Text),
                        new KeyValuePair<string, string>("last_name", TB_last_name.Text),
                        new KeyValuePair<string, string>("email_id", TB_email.Text),
                        new KeyValuePair<string, string>("mobile_no", TB_phone.Text),
                        new KeyValuePair<string, string>("address", TB_address.Text),
                        new KeyValuePair<string, string>("street_name", TB_street_name.Text),
                        new KeyValuePair<string, string>("city_id", (CB_city.SelectedItem as City).city_id),
                        new KeyValuePair<string, string>("pincode", TB_pincode.Text),
                        new KeyValuePair<string, string>("gcm_id",App.DeviceID),
                        new KeyValuePair<string, string>("language", "en"),
                        new KeyValuePair<string, string>("user_type", "1"),
                        new KeyValuePair<string, string>("dealer_id",App.Dealer_ID),
                        new KeyValuePair<string, string>("password", TB_pass_word.Text)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiRegister(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<RegisterObject>(responseString);
                if (data.status == "true")
                {
                    progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                    Storyboard.Begin();
                    App.Customer_ID = data.customer_id;
                    var toast = new ToastPrompt
                    {
                        Title = data.msg,
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Message = "",
                        TextWrapping = TextWrapping.Wrap
                    };
                    toast.Show();
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

    
        private async void Register2()
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("car_model", (CB_model_name.SelectedItem as Model).model_id),
                        new KeyValuePair<string, string>("varient", TB_variant.Text),
                        new KeyValuePair<string, string>("subvarient",TB_subvariant.Text),
                        new KeyValuePair<string, string>("model_year", TB_year.Text),
                        new KeyValuePair<string, string>("VIN", TB_vin_no.Text),
                        new KeyValuePair<string, string>("brand_id", (CB_brand_name.SelectedItem as Brand).brand_id),
                        new KeyValuePair<string, string>("model_month", CB_month.SelectedItem as string),
                        new KeyValuePair<string, string>("customer_id", App.Customer_ID),
             
                        new KeyValuePair<string, string>("language", "en"),

                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiRegister2(), new FormUrlEncodedContent(values));
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

                    await Task.Delay(TimeSpan.FromSeconds(1));
                    NavigationService.GoBack();
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


        private void CB_country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_country.SelectedItem == null || CB_country.SelectedIndex<0) return;  
            loading_state.Visibility = System.Windows.Visibility.Visible;
            Country data = CB_country.SelectedItem as Country;
            ListStateModel model = new ListStateModel();
            model.OKAction += () =>
                {
                    loading_state.Visibility = System.Windows.Visibility.Collapsed;
                    CB_state.ItemsSource = model.ListState;
                };
            model.LoadData(data.country_code);
        }

        private void CB_state_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_state.SelectedItem == null || CB_state.SelectedIndex < 0) return;
            loading_city.Visibility = System.Windows.Visibility.Visible;
            State data = CB_state.SelectedItem as State;
            ListCityModel model = new ListCityModel();
            model.OKAction += () =>
            {
                loading_city.Visibility = System.Windows.Visibility.Collapsed;
                CB_city.ItemsSource = model.ListCity;
            };
            model.LoadData(data.state_id);
        }

        private void CB_city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BT_save_Click(object sender, RoutedEventArgs e)
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
            //if (string.IsNullOrEmpty(TB_month.Text))
            //{
            //    var toast = new ToastPrompt
            //    {
            //        Title = "Please enter month",
            //        TextOrientation = System.Windows.Controls.Orientation.Vertical,
            //        Message = "",
            //        TextWrapping = TextWrapping.Wrap
            //    };
            //    toast.Show();
            //    return;
            //}
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
            if (TB_vin_no.Text.Length!=17)
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
            Register2();
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

        private void TB_vin_no_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                BT_save.Focus();
        }

        private void TB_year_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_vin_no.Focus();
        }

        private void TB_month_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_year.Focus();
        }

        private void TB_subvariant_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_year.Focus();
        }

        private void TB_variant_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_subvariant.Focus();
        }

        private void TB_street_name_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_pincode.Focus();
        }

        private void TB_address_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_street_name.Focus();
        }

        private void TB_phone_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_address.Focus();
        }

        private void TB_pass_word_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_phone.Focus();
        }

        private void TB_email_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_pass_word.Focus();
        }

        private void TB_last_name_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_email.Focus();
        }

        private void TB_first_name_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_last_name.Focus();
        }

        private void TB_pincode_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                BT_next.Focus();
        }


    }
}