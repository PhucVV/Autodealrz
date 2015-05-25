using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class Brand
    {
        public string brand_id { get; set; }
        public string brand_name { get; set; }
    }

    public class BrandObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<Brand> brands { get; set; }
    }
}
