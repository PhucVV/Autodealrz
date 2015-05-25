using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
 

    public class OldCarDetailsObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string brand_name { get; set; }
        public string model_name { get; set; }
        public string price { get; set; }
        public string Kms_driven { get; set; }
        public string dealer { get; set; }
        public string registration_no { get; set; }
        public string owner { get; set; }
        public string variant { get; set; }
        public string subvariant { get; set; }
        public string month_year { get; set; }
        public string insurance { get; set; }
        public string car_body_type { get; set; }
        public string fuel_type { get; set; }
        public string vin_no { get; set; }
        public string mileage { get; set; }
        public string transmission_type { get; set; }
        public string seller_note { get; set; }
        public string colour_interior { get; set; }
        public string colour_exterior { get; set; }
        public string ac_condition { get; set; }
        public string suspension { get; set; }
        public string battery { get; set; }
        public string brakes { get; set; }
        public string tyers_condition { get; set; }
        public string seating { get; set; }
        public string interior { get; set; }
        public string exterior { get; set; }
        public string engine { get; set; }
        public string overall { get; set; }
        public string electricals_condition { get; set; }
        public List<AvailableBranch> available_branches { get; set; }
        public string car_image { get; set; }
    }
}
