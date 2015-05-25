using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDealerz.Models
{
    public class NotifiCountObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string unread_count { get; set; }
    }

    public class NotificationData : INotifyPropertyChanged
    {
        private bool show;
        public bool show_accept
        {
            get
            {
                return show;
            }
            set
            {
                if (show == value)
                    return;
                show = value;
                OnPropertyChanged(new PropertyChangedEventArgs("show_accept"));
            }
        }
        public string notification_id { get; set; }
        public string notification_status { get; set; }
        public string notification_datetime { get; set; }
        public string notification_msg { get; set; }
        public string notification_common_id { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var tempEvent = PropertyChanged;
            if (tempEvent != null)
            {
                tempEvent(this, e);
            }
        }
    }

    public class NotificationObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<NotificationData> notifications { get; set; }
    }
}
