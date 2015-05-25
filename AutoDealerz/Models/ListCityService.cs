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
    class ListCityService
    {
         public delegate void OKDelegate();
        public event OKDelegate OKAction;
  
        public ListCityService()
        {
         
        }

        public async void LoadData()
        {
            App.ListServiceCity.Clear();
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                         new KeyValuePair<string, string>("dealer_id", App.Dealer_ID)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetServiceCity(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ServiceCityObject>(responseString);
                if(data.status=="true")
                {
                    foreach (var item in data.cities)
                        App.ListServiceCity.Add(item);
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
