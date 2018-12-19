using DbManager.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace szh.configuraiton {
    public class NotificationSettings : Entity {

        #region DataBase

        public int id;
        public string name;
        public string value;
        public string description;
        public string datatype;

        #endregion

        #region GetValues

        public static string GetSenderEmailAddress() {
            return GetNotificationSetting("SENDER_EMAIL_ADDRESS");
        }

        public static string GetSenderEmailPassword() {
            return GetNotificationSetting("SENDER_EMAIL_PASSWORD");
        }

        public static string GetSenderEmailHost() {
            return GetNotificationSetting("SENDER_EMAIL_HOST");
        }

        public static string GetSenderEmailHostPort() {
            return GetNotificationSetting("SENDER_EMAIL_HOST_PORT");
        }

        private static string GetNotificationSetting(string name) {

            string query = $"select value from configuration.notification_settings where name = '{name}'";

            return pgSqlSingleManager.ExecuteSQL(query)[0]["value"];
        }

        #endregion
    }
}
