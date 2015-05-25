using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class EmployeeService
    {
        public string branch_emp_id { get; set; }
        public string employee_name { get; set; }
        public string employee_designation { get; set; }
        public string contact_no { get; set; }
        public string email_id { get; set; }
    }

    public class EmployeeObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<EmployeeService> services { get; set; }
    }
}
