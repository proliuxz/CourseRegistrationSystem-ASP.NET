using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Web;

namespace CourseRegistration.BLL
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            sendMail(message);
            return Task.FromResult(0);
        }
        private void sendMail(IdentityMessage message)
        {
            #region formatter
            string text = string.Format("Please click on this link to {0}: {1}", message.Subject, message.Body);
            string html = "Please confirm your account by clicking this link: <a href=\"" + message.Body + "\">link</a><br/>";

            html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + message.Body);
            #endregion

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(ConfigurationManager.AppSettings["mailAccount"]);
            msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            try
            {
                SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpClient"]);
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["mailAccount"], ConfigurationManager.AppSettings["mailPassword"]);
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = true;
                smtpClient.Send(msg);
            }
            catch(Exception e)
            {
                try
                {
                    SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpClient"], Convert.ToInt32(587));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["mailAccount"], ConfigurationManager.AppSettings["mailPassword"]);
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(msg);
                }
                catch (Exception ex)
                {

                }
            }

        }
    }
}
