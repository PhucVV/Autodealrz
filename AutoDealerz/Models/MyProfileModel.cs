using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class Customer
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email_id { get; set; }
        public string mobile_number { get; set; }
        public string address { get; set; }
        public string street_name { get; set; }
        public string city_id { get; set; }
        public string pincode { get; set; }
        public string city_name { get; set; }
        public string state_id { get; set; }
        public string state_name { get; set; }
        public string country_name { get; set; }
    }

    public class MyProfileObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<Customer> customer { get; set; }
    }

    public class UpdateProfileObject
    {
        public string status { get; set; }
        public string msg { get; set; }
    }
}
