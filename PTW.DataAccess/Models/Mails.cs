using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PTW.DataAccess.Models
{
   public class Mails
    {

        public static void funSendEnquiryMail(NewUsers user,string HtmlBody, System.Net.Mail.Attachment inlineLogo)
        {
            try
            {

                string sendFromEmail = "donotreply@ptw.com";
                string sendFromPassword = "P@ssw0rd123";

                HtmlBody = HtmlBody.Replace("$FIRSTNAME$", user.FirstName);
                HtmlBody = HtmlBody.Replace("$LASTNAME$", user.LastName);
                HtmlBody = HtmlBody.Replace("$EMAIL$", user.Email);
                HtmlBody = HtmlBody.Replace("$CONTACTNUMBER$", user.ContactNumber);
                HtmlBody = HtmlBody.Replace("$COMPANYNAME$", user.CompanyName);
                HtmlBody = HtmlBody.Replace("$AREAOFINTEREST$", user.AreaOfInterest);
                HtmlBody = HtmlBody.Replace("$HEARABOUT$", user.HearAbout);
                HtmlBody = HtmlBody.Replace("$CONTACTMESSAGE$", user.ContactMessage);


                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.Attachments.Add(inlineLogo);

                string contentID = "PTW";
                inlineLogo.ContentId = contentID;

                //To make the image display as inline and not as attachment
                inlineLogo.ContentDisposition.Inline = true;
                inlineLogo.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;

                HtmlBody = HtmlBody.Replace("$IMAGE$", contentID);

                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(HtmlBody, System.Text.Encoding.UTF8, "text/html");
                mail.IsBodyHtml = true;
                mail.AlternateViews.Add(htmlView);
                mail.From = new System.Net.Mail.MailAddress(sendFromEmail);

                var toAddresses = "kondaiah.gattupalli@ptw.com";

                foreach (var address in toAddresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.To.Add(address);
                }

                mail.Subject = "PTW Enquiry";


                using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.office365.com", 587))
                {
                    client.Credentials = new System.Net.NetworkCredential(sendFromEmail, sendFromPassword);
                    client.EnableSsl = true;
                    client.TargetName = "STARTTLS/smtp.office365.com";
                    client.Send(mail);
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static bool ValidateEmailID(string EmailID)
        {
            try
            {
                bool falsevalue = false;
                if (Regex.Match(EmailID, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*").Length > 0)
                    falsevalue = true;

                return falsevalue;
            }
            catch { throw; }

        }
    }
}
