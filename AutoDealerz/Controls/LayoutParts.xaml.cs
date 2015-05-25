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
using AutoDealerz.Models;
using Coding4Fun.Toolkit.Controls;
using Windows.Storage;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace AutoDealerz.Controls
{
    public partial class LayoutParts : UserControl
    {
        public LayoutParts()
        {
            InitializeComponent();
        }
        ListCityService LSC;
        public void CheckData()
        {
            if (CB_city.ItemsSource == null)
            {
                if (App.ListServiceCity.Count > 0)
                {
                    CB_city.ItemsSource = App.ListServiceCity;
                    return;
                }
                loading_city.Visibility = System.Windows.Visibility.Visible;
                LSC = new ListCityService();
                LSC.LoadData();
                LSC.OKAction += () =>
                {
                    loading_city.Visibility = System.Windows.Visibility.Collapsed;
                    CB_city.ItemsSource = App.ListServiceCity;
                };
            }
        }

        private void MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (MainPivot.SelectedIndex)
            {
                case 0: break;
                case 1:
                    if (List_my_request.ItemsSource == null)
                        GetMyRequest();
                    break;
            }
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
                SaveImageFromCamera(e.ChosenPhoto, "Shared\\Transfers\\part_thumb.jpg");
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

        private void TB_parts_name_GotFocus(object sender, RoutedEventArgs e)
        {
            if(TB_parts_name.Text=="Parts name")
            {
                TB_parts_name.Text = "";
            }
        }

        private void TB_parts_name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_parts_name.Text))
                TB_parts_name.Text = "Parts name";
        }

        private void TB_comment_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_comment.Text == "Description")
                TB_comment.Text = "";
        }

        private void TB_comment_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB_comment.Text.Trim()))
                TB_comment.Text = "Description";
        }

        private void TB_parts_name_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                TB_comment.Focus();
        }

        private void BT_send_Click(object sender, RoutedEventArgs e)
        {
            if (CB_city.SelectedItem == null)
            {
                var toast = new ToastPrompt
                {
                    Title = "Please select city",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (CB_branch_name.SelectedItem == null)
            {
                var toast = new ToastPrompt
                {
                    Title = "Please select branch",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (CB_vehicle.SelectedItem == null)
            {
                var toast = new ToastPrompt
                {
                    Title = "Please select vehicle",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (string.IsNullOrEmpty(TB_parts_name.Text) || TB_parts_name.Text=="Parts name")
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter parts name",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            if (string.IsNullOrEmpty(TB_comment.Text) || TB_comment.Text == "Description")
            {
                var toast = new ToastPrompt
                {
                    Title = "Please enter comment",
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Message = "",
                    TextWrapping = TextWrapping.Wrap
                };
                toast.Show();
                return;
            }
            SendPart();

        }

        private async void SendPart()
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            string path = "Shared\\Transfers";
            StorageFile fileStream = null;
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            folder = await folder.GetFolderAsync(path);
            fileStream = await folder.GetFileAsync("part_thumb.jpg");
            var file = await fileStream.OpenReadAsync();
            byte[] fileDataBytes;
            fileDataBytes = new byte[file.Size];
            for (int i = 0; i < fileDataBytes.Length; i++)
            {
                fileDataBytes[i] = (byte)file.AsStreamForRead().ReadByte();
            }
            //preparing RestRequest by adding server url, parameteres and files...
            RestRequest request = new RestRequest(APIs.ApiSendParts(), Method.POST);
            request.AddParameter("language", "en");
            request.AddParameter("city_id", (CB_city.SelectedItem as ServiceCity).city_id);
            request.AddParameter("branch_id", (CB_branch_name.SelectedItem as Branch).branch_id!="-1"?(CB_branch_name.SelectedItem as Branch).branch_id:"");
            request.AddParameter("branch_status",(CB_branch_name.SelectedItem as Branch).branch_id!="-1"?"1":"0");
            request.AddParameter("parts_name", TB_parts_name.Text);
            request.AddParameter("parts_description", TB_comment.Text);
     
            request.AddParameter("customer_id", App.Customer_ID);
            request.AddParameter("c_v_id", (CB_vehicle.SelectedItem as Vehicle).c_v_id);
            request.AddParameter("dealer_id", App.Dealer_ID);
            request.AddFile("parts_image", fileDataBytes, "part_thumb.jpg");

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
                                TB_comment.Text = "Description";
                                TB_parts_name.Text = "Parts name";
                            }
                        }
                    }
                    catch { 
                    }
                 
                }
                else
                {
                    
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            });

        }

        private async void BT_delete_my_request_Click(object sender, RoutedEventArgs e)
        {
            string id = (sender as Button).Tag.ToString();
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                        new KeyValuePair<string, string>("language", "en"),
                            new KeyValuePair<string, string>("parts_req_id",id)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiDeletePartRequest(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<DeleteMyRequestObject>(responseString);
                if (data.status == "true")
                {
                    for (int i = 0; i < ListRequest.Count; i++)
                    {
                        if (ListRequest[i].c_p_r_id == id)

                            ListRequest.RemoveAt(i);
                    }
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void CB_city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_city.SelectedItem == null || CB_city.SelectedIndex < 0) return;
            loading_brand_name.Visibility = System.Windows.Visibility.Visible;
            ServiceCity data = CB_city.SelectedItem as ServiceCity;
            ListBranchModel model = new ListBranchModel();
            model.OKAction += () =>
            {
                loading_brand_name.Visibility = System.Windows.Visibility.Collapsed;
                model.ListBranch.Insert(0, new Branch() { branch_name = "All Branches", branch_address = "All", branch_id = "-1" });
                CB_branch_name.ItemsSource = model.ListBranch;
            };
            model.LoadData(data.city_id);
        }

        private void CB_branch_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_vehicle.ItemsSource == null)
            {
                loading_vehicle.Visibility = System.Windows.Visibility.Visible;
                ListCustomerVehicle cusve = new ListCustomerVehicle();
                cusve.OKAction += () =>
                {
                    loading_vehicle.Visibility = System.Windows.Visibility.Collapsed;
                    CB_vehicle.ItemsSource = cusve.ListVehicle;
                };
                cusve.LoadData();
            }
        }

        private void loading_brand_name_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        ObservableCollection<PartsRequestData> ListRequest = new ObservableCollection<PartsRequestData>();
        private async void GetMyRequest()
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("customer_id", App.Customer_ID),
                        new KeyValuePair<string, string>("language", "en")
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetPartRequest(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<PartsRequestObject>(responseString);
                if (data.status == "true")
                {
                    foreach (var item in data.services)
                    {
                        item.mystring = item.branch_name + ", " + item.branch_address + ", " + item.city;
                        ListRequest.Add(item);
                    }
                    List_my_request.ItemsSource = ListRequest;
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
