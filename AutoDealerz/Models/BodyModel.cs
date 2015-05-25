using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class BodyType
    {
        public string body_type_id { get; set; }
        public string body_type { get; set; }
    }

    public class BodyObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<BodyType> body_type { get; set; }
    }
}
