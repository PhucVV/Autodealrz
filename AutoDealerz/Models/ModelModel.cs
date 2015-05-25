using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class Model
    {
        public string model_id { get; set; }
        public string model_name { get; set; }
    }

    public class ModelObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<Model> models { get; set; }
    }
}
