using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class CheckSettingObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string notifcation_onoff_status { get; set; }
        public string email_onoff_status { get; set; }
    }


    public class ChangeSettingObject
    {
        public string status { get; set; }
        public string msg { get; set; }
    }
}
