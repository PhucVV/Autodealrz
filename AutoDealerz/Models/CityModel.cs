using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class City
    {
        public string city_id { get; set; }
        public string city_name { get; set; }
        public string state_name { get; set; }
    }

    public class CityObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<City> cities { get; set; }
    }
}
