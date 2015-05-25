using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class ListCountryModel
    {
        public delegate void OKDelegate();
        public event OKDelegate OKAction;

        public ListCountryModel()
        {
         
        }

        public async void LoadData()
        {
            App.ListCountry.Clear();
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en")
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetCountry(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CountryObject>(responseString);
                if(data.status=="true")
                {
                    foreach (var item in data.countries)
                        App.ListCountry.Add(item);
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
