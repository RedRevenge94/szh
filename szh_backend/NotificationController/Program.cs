using System;
using System.Collections.Generic;
using System.Threading;
using szh.cultivation.notifications;
using szh.measurement;

namespace NotificationController {
    public class Program {

        private static List<Notification> notificationList;
        private static Dictionary<int, DateTime> notificationSendDate;

        public static void Main(string[] args) {

            notificationList = Notification.GetNotifications();
            InitNotificationSendDateDictionary();

            while (true) {
                foreach (Notification notification in notificationList) {

                    double currentValue = Measurement.GetCurrentValue(MeasurementType.GetMeasurementTypes()
                        .Find(x => x.name == notification.measurement_type).id, notification.tunnel.id);

                    DateTime currentDateTime = DateTime.Now;

                    if (notification.condition == "<") {
                        if (currentValue < notification.value) {

                            if ((currentDateTime - notificationSendDate[notification.id]).TotalMinutes > notification.repeat_after) {

                                string messageText = $"W tunelu {notification.tunnel.name} wartość" +
                                $" {notification.measurement_type} " +
                                $"nie może byc mniejsze od {notification.value} a jest równe = {currentValue}";

                                try {
                                    new MailSending(notification.receivers).SendMessage(messageText);
                                } catch (Exception e) {
                                    Console.WriteLine(e);
                                }


                                notificationSendDate[notification.id] = currentDateTime;
                            }
                        }
                    } else if (notification.condition == ">") {
                        if (currentValue > notification.value) {

                            if ((currentDateTime - notificationSendDate[notification.id]).TotalMinutes > notification.repeat_after) {

                                string messageText = $"W tunelu {notification.tunnel.name} wartość" +
                                $" {notification.measurement_type} " +
                                $"nie może byc większe od {notification.value} a jest równe = {currentValue}";

                                try {
                                    new MailSending(notification.receivers).SendMessage(messageText);
                                } catch (Exception e) {
                                    Console.WriteLine(e);
                                }

                                notificationSendDate[notification.id] = currentDateTime;
                            }
                        }
                    }

                }

                Thread.Sleep(30000);
            }
        }

        private static void InitNotificationSendDateDictionary() {

            DateTime InitDateTimeValue = new DateTime(1970, 1, 1);
            notificationSendDate = new Dictionary<int, DateTime>();
            foreach (Notification notification in notificationList) {
                notificationSendDate.Add(notification.id, InitDateTimeValue);
            }

        }
    }
}
