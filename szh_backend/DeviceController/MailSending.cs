using System;
using System.Net.Mail;

namespace DeviceDeviceController {
    public class MailSending {
        MailMessage mail;
        SmtpClient smtp;
        private string mailAddresFrom = "---";
        private string mailAddresTo = "---";
        private string password = "---";
        private string hostAddres = "---";
        private int hostPort = 587;

        public MailSending() {
            mail = new MailMessage();
            foreach (var address in mailAddresTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)) {
                mail.To.Add(address);
            }
            mail.From = new MailAddress(mailAddresFrom);
            mail.Subject = $"SZH - Backend Information";
            mail.IsBodyHtml = true;
            smtp = new SmtpClient();
            smtp.Port = hostPort;
            smtp.UseDefaultCredentials = true;
            smtp.Host = hostAddres;
            smtp.Credentials = new System.Net.NetworkCredential(mailAddresFrom, password);
        }
        public void sendMessage(string tekst) {
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
    }

}
