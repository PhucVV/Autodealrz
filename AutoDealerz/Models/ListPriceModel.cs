using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    class ListPriceModel
    {
           public delegate void OKDelegate();
        public event OKDelegate OKAction;

       public List<Prices> ListPrices = new List<Prices>();
        public ListPriceModel()
        {
         
        }

        public async void LoadData()
        {
            
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                     
                           new KeyValuePair<string, string>("dealer_id",App.Dealer_ID),
                      
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetprices(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<PricesObject>(responseString);
                if(data.status=="true")
                {
                    foreach (var item in data.prices)
                    {
                        ListPrices.Add(item);
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
