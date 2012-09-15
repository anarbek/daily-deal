using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace aGrouponClasses.Utils {
    public class MailSender {
        public static void SendMail(string FromName, string Subject, string Body, MailAddressCollection addresses) {
            System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();
            for (int i = 0; i < addresses.Count; i++) {
                email.To.Add(addresses[i]);
            }


            email.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], FromName);
            email.Subject = Subject;

            email.Body = Body;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;
            email.BodyEncoding = System.Text.Encoding.UTF8;
            SmtpClient mailClient = new System.Net.Mail.SmtpClient();
            mailClient.Timeout = 400000;
            mailClient.EnableSsl = false;

            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;


            NetworkCredential basicAuthenticationInfo =
                new NetworkCredential("workflow@dbhsoft.com", "147258");
            mailClient.Host = "mail.dbhsoft.com";
            mailClient.Credentials = basicAuthenticationInfo;
            mailClient.Port = 587;

            try {
                mailClient.Send(email);
            } catch (Exception) {
                throw;
            }
        }
    }
}
