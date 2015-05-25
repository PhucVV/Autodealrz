using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class RequestData
    {
        public string c_s_r_id { get; set; }
        public string vehical { get; set; }
        public string scheduled_datetime { get; set; }
        public string user_suggested_datetime { get; set; }
        public string service_status { get; set; }
        public string branch_name { get; set; }
        public string service_description { get; set; }
        public string other_service { get; set; }
        public string branch_address { get; set; }
    }

    public class MyRequestObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<RequestData> services { get; set; }
    }

    public class DeleteMyRequestObject
    {
        public string status { get; set; }
        public string msg { get; set; }
    }

    public class SimpleMyRequestObject
    {
        public string status { get; set; }
        public string msg { get; set; }
    }
}
