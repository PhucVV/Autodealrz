using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
 
    public class NewCarDetailObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string brand_name { get; set; }
        public string model_name { get; set; }
        public string price { get; set; }
        public string variant { get; set; }
        public string upto_engine_cc { get; set; }
        public string fuel_type { get; set; }
        public string subvariant { get; set; }
        public string month_year { get; set; }
        public string max_power { get; set; }
        public string max_torque { get; set; }
        public string transmission { get; set; }
        public string seating { get; set; }
        public string vin_no { get; set; }
        public string mileage_city { get; set; }
        public string mileage_highway { get; set; }
        public string fuel_tank_capacity { get; set; }
        public string engine_description { get; set; }
        public string colour_interior { get; set; }
        public string colour_exterior { get; set; }
        public string car_image { get; set; }
        public string std_features { get; set; }
        public string antilock_braking_system { get; set; }
        public string brake_assist { get; set; }
        public string central_locking { get; set; }
        public string power_door { get; set; }
        public string child_lock { get; set; }
        public string passenger_air_bag { get; set; }
        public string halogen_head_lamps { get; set; }
        public string door_ajar_warning { get; set; }
        public string adjustable_seats { get; set; }
        public string engine_immobilizer { get; set; }
        public string rear_seat_belts { get; set; }
        public string crash_senser { get; set; }
        public string ebd { get; set; }
        public List<AvailableBranch> available_branches { get; set; }
    }
}
