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
    class ListVariantModel
    {
         public delegate void OKDelegate();
        public event OKDelegate OKAction;
        public ObservableCollection<Varient> ListVariant = new ObservableCollection<Varient>();
        public ListVariantModel()
        {
         
        }

        public async void LoadData(string code)
        {
           
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                         new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                         new KeyValuePair<string, string>("car_id", code)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiNewCarvarian(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<NewCarVariantObject>(responseString);
                if(data.status=="true")
                {
                    foreach (var item in data.varients)
                    {
                        item.varient_name = item.varient_name + " (" + item.fuel_type+")";
                        item.engine_cc = item.engine_cc + "," + item.mileage + "," + item.fuel_type + "," + item.transmission;
                        ListVariant.Add(item);
                    }
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
