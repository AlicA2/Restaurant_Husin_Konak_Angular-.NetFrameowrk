using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.Helpers
{
    public class EmailSender
    {
        public static void Posalji(string to, string messageSubject, string messageBody, bool isBodyHtml = false)
        {
            if (to == "")
                return;
            String SendMailFrom = "darkic951@gmail.com";

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage email = new MailMessage();

            email.From = new MailAddress(SendMailFrom);
            email.To.Add(to);
            email.CC.Add(SendMailFrom);
            email.Subject = messageSubject;
            email.Body = messageBody;
            email.IsBodyHtml = isBodyHtml;

            SmtpServer.Timeout = 5000;
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential(SendMailFrom, "bgyxkruismdmjbwr");
            SmtpServer.Send(email);
        }
    }
}
