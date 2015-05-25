using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class ListCustomerVehicle
    {
           public delegate void OKDelegate();
        public event OKDelegate OKAction;
        public ObservableCollection<Vehicle> ListVehicle = new ObservableCollection<Vehicle>();
        public ListCustomerVehicle()
        {
         
        }

        public async void LoadData()
        {
            ListVehicle.Clear();
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                         new KeyValuePair<string, string>("customer_id", App.Customer_ID)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiCustomerVehicle(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CustomerVehicleObject>(responseString);
                if(data.status=="true")
                {
                    foreach (var item in data.vehicles)
                       ListVehicle.Add(item);
                }
                if (OKAction != null)
                    OKAction();
            }
            catch
            {
                if (OKAction != null)
                    OKAction();
            }
        }
    }
}
