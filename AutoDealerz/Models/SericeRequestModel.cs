using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class ServiceRequestObject
    {
        public string status { get; set; }
        public int c_s_r_id { get; set; }
        public string service_status { get; set; }
        public string msg { get; set; }
    }



    public class CompleteService
    {
        public string c_v_id { get; set; }
        public string model_name { get; set; }
        public string brand_name { get; set; }
        public string branch_id { get; set; }
    }

    public class CompleteServiceObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<CompleteService> services { get; set; }
    }
}
