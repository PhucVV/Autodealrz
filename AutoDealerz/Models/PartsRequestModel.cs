using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class PartsRequestData
    {
        public string c_p_r_id { get; set; }
        public string parts_name { get; set; }
        public string parts_description { get; set; }
        public string parts_image { get; set; }
        public string vehicle_name { get; set; }
        public string city { get; set; }
        public string branch_name { get; set; }
        public string branch_address { get; set; }
        public string mystring { get; set; }
    }

    public class PartsRequestObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<PartsRequestData> services { get; set; }
    }
}
