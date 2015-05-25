using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class State
    {
        public string state_id { get; set; }
        public string state_name { get; set; }
        public string country_code { get; set; }
    }

    public class StateObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<State> state { get; set; }
    }
}
