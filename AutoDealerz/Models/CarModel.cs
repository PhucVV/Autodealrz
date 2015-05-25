using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class Car
    {
        public string car_id { get; set; }
        public string car_image { get; set; }
        public string car_price { get; set; }
        public string brand_name { get; set; }
        public string model_name { get; set; }
    }

    public class CarObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<Car> cars { get; set; }
    }
}
