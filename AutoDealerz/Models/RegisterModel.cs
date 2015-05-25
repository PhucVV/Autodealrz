using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class RegisterObject
    {
        public string status { get; set; }
        public string customer_id { get; set; }
        public string msg { get; set; }
    }

    public class RegisterObject2
    {
        public string status { get; set; }
        public string customer_id { get; set; }
        public int c_v_id { get; set; }
        public string model_no { get; set; }
        public string brand_id { get; set; }
        public string car_brand_name { get; set; }
        public string msg { get; set; }
    }
}
