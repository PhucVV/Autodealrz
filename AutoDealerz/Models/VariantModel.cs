using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class Varient
    {
        public string car_id { get; set; }
        public string varient_name { get; set; }
        public string price { get; set; }
        public string engine_cc { get; set; }
        public string mileage { get; set; }
        public string fuel_type { get; set; }
        public string transmission { get; set; }
    }

    public class AvailableBranch
    {
        public string branch_id { get; set; }
        public string branch_name { get; set; }
        public string branch_address { get; set; }
        public string branch_email_id { get; set; }
        public string branch_contact_no { get; set; }
        public string branch_working_hours { get; set; }
    }

    public class NewCarVariantObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string mileage { get; set; }
        public string brand_name { get; set; }
        public string model_name { get; set; }
        public string price_range { get; set; }
        public string upto_engine_cc { get; set; }
        public string car_image { get; set; }
        public List<Varient> varients { get; set; }
        public List<AvailableBranch> available_branches { get; set; }
    }
}
