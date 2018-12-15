using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace szh.dao {
    public class NotificationAddModel {

        public int tunnel;
        public string condition;
        public string measurement_type;
        public float value;
        public int repeat_after;
        public string receivers;
        public bool isActive;

    }
}
