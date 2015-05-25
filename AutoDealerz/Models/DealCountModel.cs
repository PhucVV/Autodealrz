using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class DealCountObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public int services_count { get; set; }
        public int sales_count { get; set; }
        public int parts_count { get; set; }
    }
}
