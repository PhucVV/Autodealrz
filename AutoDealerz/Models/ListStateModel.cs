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
    public class ListStateModel
    {
         public delegate void OKDelegate();
        public event OKDelegate OKAction;

        public ObservableCollection<State> ListState = new ObservableCollection<State>();
    
        public ListStateModel()
        {
         
        }

        public async void LoadData(string code)
        {
            ListState.Clear();
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("language", "en"),
                        new KeyValuePair<string, string>("country_code", code)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetstate(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<StateObject>(responseString);
                if(data.status=="true")
                {
                    foreach (var item in data.state)
                        ListState.Add(item);
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
