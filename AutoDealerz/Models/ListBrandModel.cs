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
    public class ListBrandModel
    {
           public delegate void OKDelegate();
        public event OKDelegate OKAction;

       
        public ListBrandModel()
        {
         
        }

        public async void LoadData()
        {
            App.ListBrand.Clear();
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                         new KeyValuePair<string, string>("flag","1"),
                           new KeyValuePair<string, string>("dealer_id",App.Dealer_ID),
                        new KeyValuePair<string, string>("flag_type","1")
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetbrand(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<BrandObject>(responseString);
                if(data.status=="true")
                {
                    foreach (var item in data.brands)
                    {
                        if(!string.IsNullOrEmpty(item.brand_name))
                        App.ListBrand.Add(item);
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
