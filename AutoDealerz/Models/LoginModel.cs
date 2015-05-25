using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class LoginObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string customer_id { get; set; }
        public string city_id { get; set; }
        public string state_id { get; set; }
        public string name { get; set; }
        public string mobile_no { get; set; }
    }

    public class ForgetPassObject
    {
        public string status { get; set; }
        public string msg { get; set; }
    }
}
