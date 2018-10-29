using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using reCAPTCHA.MVC;
using Umbraco.Web.Mvc;

namespace FdBogucin.Controllers
{
    public class MailController : SurfaceController
    {
        [HttpPost]
        [CaptchaValidator]
        public ActionResult SendEmail(EmailDto model)
        {
            if (ModelState.IsValid)
            {
                SendMessage(model);
                return RedirectToCurrentUmbracoPage();
            }
            return CurrentUmbracoPage();
        }
        public static void SendMessage(EmailDto model)
        {
            MailMessage msg = new MailMessage(new MailAddress("aplikacja@fdbogucin.pl", "Aplikacja", Encoding.Default),
                new MailAddress("fdbogucin@fdbogucin.pl"));
            msg.Bcc.Add(new MailAddress("sada1337@gmail.com"));
            msg.IsBodyHtml = true;
            msg.Body = $"Wiadomość od: {model.Name}, email: {model.Email}<br/><br/>{model.Message}";
            msg.BodyEncoding = Encoding.UTF8;
            msg.Subject = "Zapytanie ze strony: " + model.Subject;
            msg.SubjectEncoding = Encoding.UTF8;
            SmtpClient Client = new SmtpClient("194.88.154.132");
            Client.Credentials = new System.Net.NetworkCredential("sada@fdbogucin.hostingasp.pl", "1990.18,m");
            Client.Send(msg);
        }
        public static void SendConfirmationMessage(EmailDto model)
        {
            MailMessage msg = new MailMessage(new MailAddress("aplikacja@fdbogucin.pl", "Aplikacja", Encoding.Default),
                new MailAddress(model.Email));
            msg.Subject = "Dziękujemy za wysłanie zapytania";
            msg.SubjectEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Body = "Dziękujemy za wysłanie zapytania '" + model.Subject + "' do Fabryki Domów 'Bogucin' <br /><br />Z poważaniem,<br />Fabryka Domów 'Bogucin'";
            msg.BodyEncoding = Encoding.UTF8;
            SmtpClient Client = new SmtpClient("194.88.154.132");
            Client.Credentials = new System.Net.NetworkCredential("sada@fdbogucin.hostingasp.pl", "1990.18,m");
            Client.Send(msg);
        }
    }

    public class EmailDto
    {
        [MinLength(1)]
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [MinLength(1)]
        [Required]
        public string Subject { get; set; }

        [MinLength(1)]
        [Required]
        public string Message { get; set; }
    }
}