using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class Branch
    {
        public string branch_id { get; set; }
        public string branch_name { get; set; }
        public string branch_address { get; set; }
        public string branch_email_id { get; set; }
        public string branch_contact_no { get; set; }
        public string branch_working_hours { get; set; }
    }

    public class BranchObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<Branch> branches { get; set; }
    }
}
