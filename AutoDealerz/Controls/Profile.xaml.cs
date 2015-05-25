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
using Coding4Fun.Toolkit.Controls;

namespace AutoDealerz.Controls
{
    public partial class Profile : UserControl
    {
      
        public Profile()
        {
            InitializeComponent();
        }

        bool have_data = false;
        public void GetData()
        {
            if (!have_data)
                GetMyProfile();
        }
        MyProfileObject data;
        private async void GetMyProfile()
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("language", "en"),
                          new KeyValuePair<string, string>("customer_id",App.Customer_ID)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiMyprofile(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
               data = JsonConvert.DeserializeObject<MyProfileObject>(responseString);

                if (data.status == "true")
                {
                    have_data = true;
                    Layout_content.DataContext = data.customer[0];
                    LoadCountry();
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;

            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void LoadCountry()
        {
            if (CB_country.ItemsSource == null)
            {
                if (App.ListCountry.Count > 0)
                {
                    CB_country.ItemsSource = App.ListCountry;
                    GetMappingCountry();
                    return;
                }
                loading_country.Visibility = System.Windows.Visibility.Visible;
                ListCountryModel LC = new ListCountryModel();
                LC.LoadData();
                LC.OKAction += () =>
                {
                    loading_country.Visibility = System.Windows.Visibility.Collapsed;
                    CB_country.ItemsSource = App.ListCountry;
                    GetMappingCountry();
                };
            }
        }
        private void GetMappingCountry()
        {
            foreach(var item in App.ListCountry)
            {
                if(item.country_name == data.customer[0].country_name)
                {
                    CB_country.SelectedItem = item;
                    break;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_first_name.Text))
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
            if (CB_country.SelectedItem == null)
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
            UpdateProfile();
        }


        private async void UpdateProfile()
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
                        new KeyValuePair<string, string>("language", "en"),
                        new KeyValuePair<string, string>("customer_id", App.Customer_ID)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiUpdateProfile(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<UpdateProfileObject>(responseString);
              
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
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }




        ListStateModel state_model;
        private void CB_country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_country.SelectedItem == null || CB_country.SelectedIndex < 0) return;
            loading_state.Visibility = System.Windows.Visibility.Visible;
            Country c_data = CB_country.SelectedItem as Country;
            state_model = new ListStateModel();
            state_model.OKAction += () =>
            {
                loading_state.Visibility = System.Windows.Visibility.Collapsed;
                CB_state.ItemsSource = state_model.ListState;
                GetMappingState();
            };
            state_model.LoadData(c_data.country_code);
        }
        private void GetMappingState()
        {
            foreach (var item in state_model.ListState)
            {
                if (item.state_id == data.customer[0].state_id)
                {
                    CB_state.SelectedItem = item;
                    break;
                }
            }
        }


        ListCityModel city_model;
        private void CB_state_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_state.SelectedItem == null || CB_state.SelectedIndex < 0) return;
            loading_city.Visibility = System.Windows.Visibility.Visible;
            State data = CB_state.SelectedItem as State;
            city_model = new ListCityModel();
            city_model.OKAction += () =>
            {
                loading_city.Visibility = System.Windows.Visibility.Collapsed;
                CB_city.ItemsSource = city_model.ListCity;
                GetMappingCity();
            };
            city_model.LoadData(data.state_id);
        }
        private void GetMappingCity()
        {
            foreach (var item in city_model.ListCity)
            {
                if (item.city_id == data.customer[0].city_id)
                {
                    CB_city.SelectedItem = item;
                    break;
                }
            }
        }

    }
}
