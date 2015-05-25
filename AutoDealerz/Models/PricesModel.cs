using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class Prices
    {
        public string price_id { get; set; }
        public string price { get; set; }
    }

    public class PricesObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<Prices> prices { get; set; }
    }
}
