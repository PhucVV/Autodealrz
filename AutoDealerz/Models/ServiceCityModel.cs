using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class ServiceCity
    {
        public string city_id { get; set; }
        public string city_name { get; set; }
        public string state_name { get; set; }
    }

    public class ServiceCityObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<ServiceCity> cities { get; set; }
    }
}
