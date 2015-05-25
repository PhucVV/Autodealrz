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
    class ListModelModel
    {
           public delegate void OKDelegate();
        public event OKDelegate OKAction;

        public ObservableCollection<Model> ListModel = new ObservableCollection<Model>();

        public ListModelModel()
        {
         
        }

        public async void LoadData(string id)
        {
            ListModel.Clear();
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                         new KeyValuePair<string, string>("brand_id",id)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetmodel(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ModelObject>(responseString);
                if(data.status=="true")
                {
                    foreach (var item in data.models)
                    {
                        ListModel.Add(item);
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
