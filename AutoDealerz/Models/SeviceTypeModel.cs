using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class ServiceType
    {
        public string service_type_id { get; set; }
        public string service_type_name { get; set; }
    }

    public class ServiceTypeObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<ServiceType> service_type { get; set; }
    }
}
