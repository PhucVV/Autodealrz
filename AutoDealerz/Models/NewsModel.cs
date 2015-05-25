using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class News
    {
        public string news_id { get; set; }
        public string news_title { get; set; }
        public string image_url { get; set; }
        public string description { get; set; }
        public string date_time { get; set; }
        public string recall_status { get; set; }
    }

    public class NewsObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<News> news { get; set; }
    }
}
