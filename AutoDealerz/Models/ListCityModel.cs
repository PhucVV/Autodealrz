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
    class ListCityModel
    {
         public delegate void OKDelegate();
        public event OKDelegate OKAction;
        public ObservableCollection<City> ListCity = new ObservableCollection<City>();
        public ListCityModel()
        {
         
        }

        public async void LoadData(string code)
        {
          ListCity.Clear();
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                         new KeyValuePair<string, string>("state_id", code)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetcity(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CityObject>(responseString);
                if(data.status=="true")
                {
                    foreach (var item in data.cities)
                        ListCity.Add(item);
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
