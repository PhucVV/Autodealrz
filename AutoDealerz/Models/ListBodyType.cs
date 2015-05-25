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
    class ListBodyType
    {
          public delegate void OKDelegate();
        public event OKDelegate OKAction;
        public ObservableCollection<BodyType> ListBody = new ObservableCollection<BodyType>();
        public ListBodyType()
        {
         
        }

        public async void LoadData()
        {
        
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en")
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiBodyType(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<BodyObject>(responseString);
                if(data.status=="true")
                {
                    foreach (var item in data.body_type)
                        ListBody.Add(item);
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
