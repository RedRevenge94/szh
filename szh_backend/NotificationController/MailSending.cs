using System;
using   System.Net.Mail;
using szh.configuraiton;

namespace NotificationController {
    public class MailSending {
        MailMessage mail;
        SmtpClient smtp;
        private string mailAddresFrom = "---";
        private string mailAddresTo = "---";
        private string password = "---";
        private string hostAddres = "---";
        private int hostPort = 587;

        public MailSending(string receivers) {
            mail = new MailMessage();
            mailAddresTo = receivers;

            LoadConfiguration();

            foreach (var address in mailAddresTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)) {
                mail.To.Add(address);
            }
            mail.From = new MailAddress(mailAddresFrom);
            mail.Subject = $"SZH - Backend Information";
            mail.IsBodyHtml = true;
            smtp = new SmtpClient
            {
                Port = hostPort,
                Host = hostAddres,
                Credentials = new System.Net.NetworkCredential(mailAddresFrom, password),
                EnableSsl = true
            };
        }

        public void SendMessage(string title, string tekst) {
            mail.Subject = title;
            SendMessage(tekst);
        }

        public void SendMessage(string tekst) {
            try {
                //Attachment att = new Attachment($"{pathRaport}");
                mail.Body = tekst;
                //mail.Attachments.Add(att);
                smtp.Send(mail);
                Console.WriteLine("Mail wysłany");
            } catch (Exception e) {
                Console.WriteLine("Nie udało sie wysłać maila");
                Console.WriteLine(e);
            }
        }

        private void LoadConfiguration() {
            mailAddresFrom = NotificationSettings.GetSenderEmailAddress();
            password = NotificationSettings.GetSenderEmailPassword();
            hostAddres = NotificationSettings.GetSenderEmailHost();
            Int32.TryParse(NotificationSettings.GetSenderEmailHostPort(), out hostPort);
        }
    }

}
