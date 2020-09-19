using Intro_Dating_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.IO;

namespace Intro_Dating_Site.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //redirect to "About US" page
        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.Bcc.Add(new MailAddress("ahmed.yousseef.7@gmail.com"));  // mail which message is sent to
                message.Bcc.Add(new MailAddress("haithamessam17@gmail.com"));  // another mail message is sent to 
                // bbc is for hide mails from each other
                message.From = new MailAddress("ahmed_yousseef_7@outlook.com");  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                if (model.Upload != null && model.Upload.ContentLength > 0)
                {
                    message.Attachments.Add(new Attachment(model.Upload.InputStream, Path.GetFileName(model.Upload.FileName)));
                }

                using (var smtp = new SmtpClient())
                {
                    //var credential = new NetworkCredential
                    //{
                    //    UserName = "ahmed.yousseef.7@gmail.com",  // replace with valid value
                    //    Password = "ahmadyousseef1111994"  // replace with valid value
                    //};
                    //smtp.Credentials = credential;
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 587;
                    //smtp.EnableSsl = false;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

    }
}