using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class Vehicle
    {
        public string c_v_id { get; set; }
        public string model_name { get; set; }
        public string brand_name { get; set; }
    }

    public class CustomerVehicleObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<Vehicle> vehicles { get; set; }
    }
}
