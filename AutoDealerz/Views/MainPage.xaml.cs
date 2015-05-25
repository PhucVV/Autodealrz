using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AutoDealerz.Resources;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using AutoDealerz.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Phone.Tasks;

namespace AutoDealerz
{
    public partial class MainPage : PhoneApplicationPage
    {
        enum StateView
        {
            none,
            home,
            profile,
            dealerinfo,
            sellvehicles,
            addmorevehicles,
            notifications,
            help,
            setting,
            changepass
        }
        StateView stateview;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            stateview = StateView.none;
            Loaded += MainPage_Loaded;
            layout_home.changeTitle += layout_home_changeTitle;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            if (Main_slide_view.SelectedIndex == 0)
            {
                Main_slide_view.SelectedIndex = 1;
                e.Cancel = true;
                return;
            }
            switch(stateview)
            {
                case StateView.home:
                    if(layout_home.stateviewhome!= AutoDealerz.Controls.Home.StateViewHome.none)
                    {
                        layout_home.ResetPlayout();
                        TB_title.Text = "";
                        e.Cancel = true;
                    }break;
                case StateView.notifications:
                    if(layout_notification.Message_show)
                    {
                        layout_notification.HideMessage();
                        e.Cancel = true;
                    }
                    break;
            }
        }

        void layout_home_changeTitle(string s)
        {
            TB_title.Text = s;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavigationContext.QueryString.TryGetValue("key", out key_push);
            if (List_mainmenu.SelectedIndex < 0)
                List_mainmenu.SelectedIndex = 0;
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (!settings.Contains("ConfigPush"))
            {
                settings.Add("ConfigPush", DateTime.Now.ToString());
                my_message.Show("Message", "Auto DealrZ Pvt Ltd Would Like to send you Push Notification. Notifications may include alerts, sound and icon badges. These can be configured in setting", "Don't Allow", "OK", 2);
                my_message.OKAction += my_message_OKAction;
                my_message.CancelAction += my_message_CancelAction;
            }
            settings.Save();

            GetNotificationCount();
        }

        public async void GetNotificationCount()
        {

            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                             new KeyValuePair<string, string>("dealer_id",App.Dealer_ID),
                                  new KeyValuePair<string, string>("customer_id",App.Customer_ID)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetNotifiCount(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<NotifiCountObject>(responseString);
                if (data.status == "true" && data.unread_count!="0")
                {
                    tb_number_nor.Text = data.unread_count;
                    border_number_nor.Visibility = System.Windows.Visibility.Visible;
                }
                else
                    border_number_nor.Visibility = System.Windows.Visibility.Collapsed;
              
            }
            catch
            {
               
            }
        }

        public async void UpdateNor()
        {

            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                             new KeyValuePair<string, string>("dealer_id",App.Dealer_ID),
                                  new KeyValuePair<string, string>("customer_id",App.Customer_ID),
                                     new KeyValuePair<string, string>("status","1"),
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiUpdateNorStatus(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<NotifiCountObject>(responseString);
                

            }
            catch
            {

            }
        }

       async void my_message_CancelAction()
        {
            try
            {
                progress_bar.Visibility = System.Windows.Visibility.Visible;
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                             new KeyValuePair<string, string>("dealer_id",App.Dealer_ID),
                                  new KeyValuePair<string, string>("customer_id",App.Customer_ID),
                                    new KeyValuePair<string, string>("type","2"),
                                     new KeyValuePair<string, string>("onoff_status","2")
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiSettingMailOnOff(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ChangeSettingObject>(responseString);
                if (data.status == "true")
                {
                    App.Notification = false;
                    IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                    if (settings.Contains("SHOW_PUSH"))
                    {
                        settings.Remove("SHOW_PUSH");
                    }
                    settings.Save();
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }

          
        }

      async  void my_message_OKAction()
        {
            try
            {
                progress_bar.Visibility = System.Windows.Visibility.Visible;
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                             new KeyValuePair<string, string>("dealer_id",App.Dealer_ID),
                                  new KeyValuePair<string, string>("customer_id",App.Customer_ID),
                                    new KeyValuePair<string, string>("type","2"),
                                     new KeyValuePair<string, string>("onoff_status","1")
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiSettingMailOnOff(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ChangeSettingObject>(responseString);
                if (data.status == "true")
                {
                    App.Notification = true;
                    IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                    if (!settings.Contains("SHOW_PUSH"))
                    {
                        settings.Add("SHOW_PUSH", DateTime.Now.ToString());
                    }
                    settings.Save();
                }
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }

          
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            layout_setting.LoadSetting();
            while (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }
        }


        private void Home_tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if(List_mainmenu.SelectedIndex==0)
            {
                layout_home.ResetPlayout();
                Main_slide_view.SelectedIndex = 1;
            }
        }

        string[] TitleMenu = new string[] { "home","profile","dealer info","sell vehicles","add more vehicles","notifications","help","setting","change password","logout"};
        private async void List_mainmenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_mainmenu.SelectedIndex < 0) return;
            if (List_mainmenu.SelectedIndex == 0) TB_title.Text = "";
            else
            {
                TB_title.Text = TitleMenu[List_mainmenu.SelectedIndex].ToUpper();
            }
            ResetLayout();
            switch(List_mainmenu.SelectedIndex)
            {
                case 0:
                    layout_home.ResetPlayout();
                    layout_home.Visibility = System.Windows.Visibility.Visible;
                    stateview = StateView.home;
                    break;
                case 1:
                    layout_profile.GetData();
                    layout_profile.Visibility = System.Windows.Visibility.Visible;
                    stateview = StateView.profile;
                    break;
                case 2:
                    layout_dialer.Visibility = System.Windows.Visibility.Visible;
                    layout_dialer.LoadData();
                    stateview = StateView.dealerinfo;
                    break;
                case 3:
                    layout_sell.Visibility = System.Windows.Visibility.Visible;
                    stateview = StateView.sellvehicles;
                    break;
                case 4:
                    layout_addmore_vehicles.LoadData();
                    layout_addmore_vehicles.Visibility = System.Windows.Visibility.Visible;
                    stateview = StateView.addmorevehicles;
                    break;
                case 5:
                   // UpdateNor();
                    border_number_nor.Visibility = System.Windows.Visibility.Collapsed;
                    layout_notification.Visibility = System.Windows.Visibility.Visible;
                    layout_notification.LoadData();
                    stateview = StateView.notifications;
                    break;
                case 6:
                    layout_help.Visibility = System.Windows.Visibility.Visible;
                    stateview = StateView.help;
                    break;
                case 7:
                    layout_setting.LoadSetting();
                    layout_setting.Visibility = System.Windows.Visibility.Visible;
                    stateview = StateView.setting;
                    break;
                case 8:
                    layout_change_pass.Visibility = System.Windows.Visibility.Visible;
                    stateview = StateView.changepass;
                    break;
                case 9: LogOut();
                    
                    break;
        
            }
            await Task.Delay(TimeSpan.FromMilliseconds(100)); 
            Main_slide_view.SelectedIndex = 1;
        }
        string key_push = "";

        private async void LogOut()
        {
            progress_bar.Visibility = System.Windows.Visibility.Visible;
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("customer_id",App.Customer_ID),
                  
                        new KeyValuePair<string, string>("language", "en"),
                      
                           new KeyValuePair<string, string>("dealer_id", App.Dealer_ID)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiLogout(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();

                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains("USERNAME") || settings.Contains("PASSWORD"))
                {
                    settings.Remove("USERNAME");
                    settings.Remove("PASSWORD");
                }
           
                 if (settings.Contains("ConfigPush"))
                 {
                     settings.Remove("ConfigPush");
                 }
                 settings.Save();
                NavigationService.Navigate(new Uri("/Views/LoginScreen.xaml?key=" + HttpUtility.UrlEncode(key_push), UriKind.Relative));
            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }


        private void ResetLayout()
        {
            stateview = StateView.none;

            layout_home.Visibility = System.Windows.Visibility.Collapsed;
            layout_dialer.Visibility = System.Windows.Visibility.Collapsed;
            layout_addmore_vehicles.Visibility = System.Windows.Visibility.Collapsed;
            layout_change_pass.Visibility = System.Windows.Visibility.Collapsed;
        
            layout_help.Visibility = System.Windows.Visibility.Collapsed;
            layout_notification.Visibility = System.Windows.Visibility.Collapsed;
            layout_profile.Visibility = System.Windows.Visibility.Collapsed;
            layout_sell.Visibility = System.Windows.Visibility.Collapsed;
            layout_sell.my_message.Hide();

            layout_setting.Visibility = System.Windows.Visibility.Collapsed;
            
        }

        private void BT_menu_Click(object sender, RoutedEventArgs e)
        {
            if (Main_slide_view.SelectedIndex == 0)
                Main_slide_view.SelectedIndex = 1;
            else
                Main_slide_view.SelectedIndex = 0;
        }

        private async void BT_call_Click(object sender, RoutedEventArgs e)
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
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiRoadAsisst(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<RoadAsissObject>(responseString);
               
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
               if(data.status == "true")
               {
                   PhoneCallTask phoneCallTask = new PhoneCallTask();

                   phoneCallTask.PhoneNumber = data.phone_no;


                   phoneCallTask.Show();
               }
            }
            catch
            {
                progress_bar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

     
  

    }
}