using DbManager.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using szh.dao;
using szh.measurement;

namespace szh.cultivation.notifications {
    public class Notification : Entity {

        #region -----------------DbValues-----------------
        public int id;
        public Tunnel tunnel;
        public string condition;
        public string measurement_type;
        public float value;
        public int repeat_after;
        public string receivers;
        public bool isActive;
        #endregion

        public static List<Notification> GetNotifications() =>
            GetNotifications($"select * from measurement.notifications");

        public static bool AddNotification(NotificationAddModel notification) {
            if (AddNotification(Tunnel.GetTunnel(notification.tunnel), notification.condition, notification.measurement_type, notification.value,
                notification.repeat_after, notification.receivers, notification.isActive))
                return true;
            return false;
        }

        public static bool AddNotification(Tunnel tunnel, string condition, string measurement_type, float value, int repeat_after, string receivers,
            bool isActive) {

            try {
                string sql = $"insert into measurement.notifications (tunnel,condition,measurement_type,value,repeat_after," +
                    $"receivers,isActive) " +
                $"values ({tunnel.id},'{condition}',{measurement_type},{value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}," +
                $"{repeat_after},'{receivers}',{isActive})";

                pgSqlSingleManager.ExecuteSQL(sql);
                return true;
            } catch { }
            return false;
        }

        private static List<Notification> GetNotifications(string query) {
            List<Notification> notifications = new List<Notification>();

            foreach (var notification in pgSqlSingleManager.ExecuteSQL(query)) {
                Notification newNotification = new Notification() {
                    id = Int32.Parse(notification["id"]),
                    condition = notification["condition"],
                    receivers = notification["receivers"],
                    repeat_after = Int32.Parse(notification["repeat_after"]),
                    value = float.Parse(notification["value"]),
                    isActive = bool.Parse(notification["isActive"])
                };

                newNotification.tunnel = Tunnel.GetTunnel(Int32.Parse(notification["tunnel"]));
                newNotification.measurement_type = MeasurementType.GetNameOfMeasurementType(Int32.Parse(notification["measurement_type"]));

                notifications.Add(newNotification);
            }
            return notifications;
        }
    }
}
