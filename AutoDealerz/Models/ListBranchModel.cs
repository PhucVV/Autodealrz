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
    class ListBranchModel
    {
         public delegate void OKDelegate();
        public event OKDelegate OKAction;
        public ObservableCollection<Branch> ListBranch = new ObservableCollection<Branch>();
        public ListBranchModel()
        {
         
        }

        public async void LoadData(string code)
        {
            ListBranch.Clear();
            try
            {
                var values = new List<KeyValuePair<string, string>>
                    {
         
                        new KeyValuePair<string, string>("language", "en"),
                         new KeyValuePair<string, string>("dealer_id", App.Dealer_ID),
                         new KeyValuePair<string, string>("city_id", code)
                    };

                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsync(APIs.ApiGetBranches(), new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<BranchObject>(responseString);
                if(data.status=="true")
                {
                    foreach (var item in data.branches)
                        ListBranch.Add(item);
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
