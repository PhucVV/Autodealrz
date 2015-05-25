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
using Microsoft.Phone.Tasks;

namespace AutoDealerz.Controls
{
    public partial class Help : UserControl
    {
        public Help()
        {
            InitializeComponent();
        }

        private async void BT_call_Click(object sender, RoutedEventArgs e)
        {
           
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

              
                if (data.status == "true")
                {
                    PhoneCallTask phoneCallTask = new PhoneCallTask();

                    phoneCallTask.PhoneNumber = data.phone_no;


                    phoneCallTask.Show();
                }
            }
            catch
            {
             
            }
        }

        private  void BT_Language_Click(object sender, RoutedEventArgs e)
        {
         //  await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-bluetooth:"));
        }
    }
}
