using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class DealerObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string dealer_id { get; set; }
    }

    public class AllBranch
    {
        public string branch_id { get; set; }
        public string branch_name { get; set; }
        public string branch_address { get; set; }
        public string branch_working_hours { get; set; }
        public string branch_email_id { get; set; }
        public string branch_contact_no { get; set; }
    }

    public class DealerInfoObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string d_id { get; set; }
        public string d_service_name { get; set; }
        public string d_full_name { get; set; }
        public string d_phone_number { get; set; }
        public string d_email { get; set; }
        public string application_package_name { get; set; }
        public string d_address { get; set; }
        public string d_image { get; set; }
        public List<AllBranch> all_branches { get; set; }
    }
}
