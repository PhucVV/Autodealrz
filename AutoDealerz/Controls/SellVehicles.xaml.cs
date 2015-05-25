using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.IO;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using Coding4Fun.Toolkit.Controls;
using Windows.Storage;
using RestSharp;
using AutoDealerz.Models;
using Newtonsoft.Json;

namespace AutoDealerz.Controls
{
    public class fuel
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public partial class SellVehicles : UserControl
    {

        List<string> Month = new List<string>() {"January","February","March","April","May","June","July","August","September","October","November","December"};
        List<fuel> Fuel = new List<fuel>();
        public SellVehicles()
        {
            InitializeComponent();
            for (int i = 1; i < 5;i++ )
            {
                switch(i)
                {
                    case 1:
                        Fuel.Add(new fuel() { id = i.ToString(), name = "Petrol" });
                        break;
                    case 2:
                        Fuel.Add(new fuel() { id = i.ToString(), name = "Diesel" });
                        break;
                    case 3:
                        Fuel.Add(new fuel() { id = i.ToString(), name = "LPG" });
                        break;
                    case 4:
                        Fuel.Add(new fuel() { id = i.ToString(), name = "CNG" });
                        break;
                }
            }
                CB_month.ItemsSource = Month;
            CB_fuel_type.ItemsSource = Fuel;
        }

        PhotoChooserTask choosertask; 
        private void Border_thumb_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            choosertask = new PhotoChooserTask();
            choosertask.Completed += choosertask_Completed;
            choosertask.ShowCamera = true;
            choosertask.PixelHeight = 300;
            choosertask.PixelWidth = 400;
            choosertask.Show();
        }

        void choosertask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                SaveImageFromCamera(e.ChosenPhoto, "Shared\\Transfers\\sell_thumb.jpg");
                BitmapImage bi = new BitmapImage();
                bi.SetSource(e.ChosenPhoto);
                Img_thumb.Source = bi;
            }
        }
        private void SaveImageFromCamera(Stream imageStream, string fileName)
        {

            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (myIsolatedStorage.FileExists(fileName))
                {
                    myIsolatedStorage.DeleteFile(fileName);
                }

                using (IsolatedStorageFileStream fileStream = myIsolatedStorage.CreateFile(fileName))
                {
                    imageStream.CopyTo(fileStream);
                    fileStream.Dispose();

                    fileStream.Close();
                }
                myIsolatedStorage.Dispose();

            }
        }

        private void TB_brand_name_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_brand_name.Text == "Brand name:")
                TB_brand_name.Text = "";
        }

        private void TB_brand_name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_brand_name.Text))
                TB_brand_name.Text = "Brand name:";
        }

        private void TB_brand_name_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_model_name.Focus();
        }

        private void TB_model_name_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_model_name.Text == "Model name:")
                TB_model_name.Text = "";
        }

        private void TB_model_name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_model_name.Text))
                TB_model_name.Text = "";
        }

        private void TB_model_name_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_variant.Focus();
        }

        private void TB_variant_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_variant.Text == "Variant:")
                TB_variant.Text = "";
        }

        private void TB_variant_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_variant.Text))
                TB_variant.Text = "Variant:";
        }

        private void TB_variant_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_color.Focus();

        }

        private void TB_color_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_color.Text == "Color")
                TB_color.Text = "";
        }

        private void TB_color_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_color.Text))
                TB_color.Text = "Color";

        }

        private void TB_color_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                CB_month.Focus();
        }

     

        private void TB_month_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_year.Focus();
        }

        private void TB_year_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_year.Text == "Model year:")
                TB_year.Text = "";
        }

        private void TB_year_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_year.Text))
                TB_year.Text = "Model year:";
        }

        private void TB_year_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_expected_price.Focus();
        }

        private void TB_expected_price_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_expected_price.Text == "Expected price")
                TB_expected_price.Text = "";
        }

        private void TB_expected_price_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_expected_price.Text))
                TB_expected_price.Text = "Expected price";
        }

        private void TB_expected_price_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_kilometer.Focus();
        }

        private void TB_kilometer_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_kilometer.Text == "Kilometer Driven")
                TB_kilometer.Text = "";
        }

        private void TB_kilometer_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_kilometer.Text))
                TB_kilometer.Text = "Kilometer Driven";
        }

        private void TB_kilometer_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_registrasion_no.Focus();
        }

        private void TB_registrasion_no_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_registrasion_no.Text == "Registrasion No.")
                TB_registrasion_no.Text = "";
        }

        private void TB_registrasion_no_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_registrasion_no.Text))
                TB_registrasion_no.Text = "Registrasion No.";
        }

        private void TB_registrasion_no_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_registrasion_place.Focus();
        }

        private void TB_registrasion_place_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_registrasion_place.Text == "Registrasion Place")
                TB_registrasion_place.Text = "";
        }

        private void TB_registrasion_place_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_registrasion_place.Text))
                TB_registrasion_place.Text = "Registrasion Place";
        }

        private void TB_registrasion_place_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_note.Focus();
        }

        private void TB_note_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_note.Text == "Note")
                TB_note.Text = "";
        }

        private void TB_note_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_note.Text))
                TB_note.Text = "Note";
        }

        private void TB_note_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                BT_send.Focus();
        }

        private void BT_send_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_brand_name.Text) || TB_brand_name.Text == "Brand name:")
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter brand name",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (string.IsNullOrEmpty(TB_model_name.Text) || TB_model_name.Text == "Model name:")
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter model name",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (string.IsNullOrEmpty(TB_variant.Text) || TB_variant.Text == "Variant:")
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter variant",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (string.IsNullOrEmpty(TB_color.Text) || TB_color.Text == "Color")
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter color",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (CB_month.SelectedItem==null)
            {
                var toast = new ToastPrompt
                {
                    Title = "Please select month",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (string.IsNullOrEmpty(TB_year.Text) || TB_year.Text == "Model year:")
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
            if (string.IsNullOrEmpty(TB_expected_price.Text) || TB_expected_price.Text == "Expected price")
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter Expected price",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (string.IsNullOrEmpty(TB_kilometer.Text) || TB_kilometer.Text == "Kilometer Driven")
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter Kilometer Driven",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (string.IsNullOrEmpty(TB_registrasion_no.Text) || TB_registrasion_no.Text == "Registrasion No.")
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter Registrasion No.",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (string.IsNullOrEmpty(TB_registrasion_place.Text) || TB_registrasion_place.Text == "Registrasion Place")
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter Registrasion Place",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
          
            if (string.IsNullOrEmpty(owner))
            {
                var toast = new ToastPrompt
                {
                    Title = "Please select owner",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            SellVehicle();
        }

        private async void SellVehicle()
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            string path = "Shared\\Transfers";
            StorageFile fileStream = null;
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            folder = await folder.GetFolderAsync(path);
            fileStream = await folder.GetFileAsync("sell_thumb.jpg");
            var file = await fileStream.OpenReadAsync();
            byte[] fileDataBytes;
            fileDataBytes = new byte[file.Size];
            for (int i = 0; i < fileDataBytes.Length; i++)
            {
                fileDataBytes[i] = (byte)file.AsStreamForRead().ReadByte();
            }
            //preparing RestRequest by adding server url, parameteres and files...
            RestRequest request = new RestRequest(APIs.ApiSellVehicle(), Method.POST);
            request.AddParameter("language", "en");
            request.AddParameter("customer_id", App.Customer_ID);
            request.AddParameter("brand_id", TB_brand_name.Text);
            request.AddParameter("model_id", TB_model_name.Text);
            request.AddParameter("variant", TB_variant.Text);
            request.AddParameter("expected_price", TB_expected_price.Text);
            request.AddParameter("kms_driven", TB_kilometer.Text);
            request.AddParameter("color", TB_color.Text);
            request.AddParameter("registration_no", TB_registrasion_no.Text);
            request.AddParameter("month_year", (CB_month.SelectedItem as string)+","+TB_year.Text);
            request.AddParameter("dealer_id", App.Dealer_ID);
            request.AddParameter("fuel_type", (CB_fuel_type.SelectedItem as fuel).name);

            request.AddParameter("owner", owner);

            request.AddParameter("registration_place", TB_registrasion_place.Text);
            request.AddParameter("special_note", TB_note.Text == "Note"?"":TB_note.Text);


            request.AddFile("car_img", fileDataBytes, "sell_thumb.jpg");

            //calling server with restClient
            RestClient restClient = new RestClient();
            restClient.ExecuteAsync(request, (response) =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    try
                    {
                        string json = response.Content;
                        if (!string.IsNullOrEmpty(json))
                        {
                            var data = JsonConvert.DeserializeObject<SendPartsObject>(json);
                            MessageBox.Show(data.msg);
                            if (data.status == "true")
                            {
                                if (App.Notification == false)
                                    my_message.Show("Message", "Alerts Disabled – To receive updates on Dealerships, Special promotions, New inventory arrivals and other important information. Kindly enable Notifications in the App Setting section on main Menu", "", "OK", 1);
                   


                                Img_thumb.Source = null;
                                TB_brand_name.Text = "Brand name:";
                                TB_model_name.Text = "Model name:";
                                TB_variant.Text = "Variant:";
                                TB_color.Text = "Color";
                                TB_year.Text = "Model year:";
                                TB_expected_price.Text = "Expected price";
                                TB_kilometer.Text = "Kilometer Driven";
                                TB_registrasion_no.Text = "Registrasion No.";
                                TB_registrasion_place.Text = "Registrasion Place";
                                TB_note.Text = "Note";
                               
                            }
                        }
                    }
                    catch
                    {
                    }

                }
                else
                {

                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            });

        }

        string owner = "";
        private void own_click(object sender, RoutedEventArgs e)
        {
            owner = (sender as RadioButton).Tag.ToString();
        }
    }
}
