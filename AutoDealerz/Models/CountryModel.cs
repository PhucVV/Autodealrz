using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class Country
    {
        public string country_id { get; set; }
        public string country_name { get; set; }
        public string country_code { get; set; }
    }

    public class CountryObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<Country> countries { get; set; }
    }
}
