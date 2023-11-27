using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using VegasVacationVip.Models;

namespace VegasVacationVip.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Package()
        {
            return View();
        }
        public IActionResult Hotels()
        {
            return View();
        }

        public IActionResult Gifts()
        {
            return View();
        }
        public IActionResult WhatsTheCatch()
        {
            return View();
        }
        public IActionResult Concierge()
        {
            return View();
        }
        public IActionResult TravelShow()
        {
            return View();
        }
        public IActionResult BuyNow()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        //[HttpPost]
        //public IActionResult ProcessForm(FormData formData)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Construct your email message
        //        string body = $"First Name: {formData.fname}<br>" +
        //                      $"Last Name: {formData.lname}<br>" +
        //                      $"Spouse/Partner First Name: {formData.sfname}<br>" +
        //                      $"Spouse/Partner Last Name: {formData.slname}<br>" +
        //                      $"Address: {formData.address}<br>" +
        //                      $"City: {formData.city}<br>" +
        //                      $"State/Province: {formData.state}<br>" +
        //                      $"ZIP/Postal Code: {formData.zipcode}<br>" +
        //                      $"Country: {formData.country}<br>" +
        //                      $"Phone: {formData.phone}<br>" +
        //                      $"Email: {formData.email}<br>" +
        //                      $"Card Holder Name: {formData.chname}<br>" +
        //                      $"Card Type: {formData.ctype}<br>" +
        //                      $"Card Number: {formData.cnumber}<br>" +
        //                      $"Expiration Date: {formData.xmonth} / {formData.xyear}<br>" +
        //                      $"CVV: {formData.ccvv}<br>";

        //        // Configure the SMTP client
        //        using (var client = new SmtpClient())
        //        {

        //            client.Host = formData.Smtp; // Your SMTP server (e.g., Gmail)
        //            client.Port = formData.Port; // SMTP Port (Gmail uses 587)
        //            client.EnableSsl = true;
        //            client.UseDefaultCredentials = true;
        //            client.Credentials = new NetworkCredential(formData.Login, formData.Password);

        //            // Construct and send the email
        //            var message = new MailMessage();
        //            message.From = new MailAddress(formData.FromEmail, formData.FromName); // Your email address
        //            message.To.Add(new MailAddress(formData.ToEmail));
        //            message.Subject = formData.Subject; // Email subject
        //            message.IsBodyHtml = true;
        //            message.Body = body;

        //            client.Send(message);
        //        }

        //        return RedirectToAction("Success"); // Redirect to success page after sending email
        //    }
        //    else
        //    {
        //        var errors = ModelState.Values.SelectMany(v => v.Errors);
        //        // Log or debug the errors to understand which fields are failing validation
        //    }

        //    return View("Index", formData);
        //}

        //[HttpPost]
        //public static string SendMessage(MailModel m)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Construct your email message
        //        string body = $"First Name: {m.FirstName}<br>" +
        //                      $"Last Name: {m.LastName}<br>" +
        //                      $"Phone: {m.Phone}<br>" +
        //                      $"Email: {m.Email}<br>" +
        //                      $"Request: {m.Request}<br>";

        //        // Configure the SMTP client
        //        using (var client = new SmtpClient())
        //        {

        //            client.Host = m.Smtp; // Your SMTP server (e.g., Gmail)
        //            client.Port = m.Port; // SMTP Port (Gmail uses 587)
        //            client.EnableSsl = true;
        //            client.UseDefaultCredentials = true;
        //            client.Credentials = new NetworkCredential(m.Login, m.Password);

        //            // Construct and send the email
        //            var message = new MailMessage();
        //            message.From = new MailAddress(m.FromEmail, m.FromName); // Your email address
        //            message.To.Add(new MailAddress(m.ToEmail));
        //            message.Subject = m.Subject; // Email subject
        //            message.IsBodyHtml = true;
        //            message.Body = body;

        //            client.Send(message);
        //        }

        //        return RedirectToAction("Success"); // Redirect to success page after sending email
        //    }
        //    else
        //    {
        //        var errors = ModelState.Values.SelectMany(v => v.Errors);
        //        // Log or debug the errors to understand which fields are failing validation
        //    }

        //    return View("Index", m);
        //}

        [HttpPost]
        public IActionResult ProcessForm(BuyNowModel bn, ContactUsModel cu, LogicModel l)
        {
            if (ModelState.IsValid)
            {
                string body;
                if (!string.IsNullOrEmpty(bn.fname)) // Check if formData contains fields specific to the first method
                {
                    body = ConstructBodyFromFormData(bn);
                }
                else if (!string.IsNullOrEmpty(cu.FirstName)) // Check if mailModel contains fields specific to the second method
                {
                    body = ConstructBodyFromMailModel(cu);
                }
                else
                {
                    // Handle the case where neither set of data is present
                    return BadRequest("Invalid form data");
                }

                using (var client = new SmtpClient())
                {

                    client.Host = l.Smtp; // Your SMTP server (e.g., Gmail)
                    client.Port = l.Port; // SMTP Port (Gmail uses 587)
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = true;
                    client.Credentials = new NetworkCredential(l.Login, l.Password);

                    // Construct and send the email
                    var message = new MailMessage();
                    message.From = new MailAddress(l.FromEmail, l.FromName); // Your email address
                    message.To.Add(new MailAddress(l.ToEmail));
                    message.Subject = l.Subject; // Email subject
                    message.IsBodyHtml = true;
                    message.Body = body;

                    client.Send(message);
                }
                return RedirectToAction("Index");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }

            return View("Index");
        }

        private string ConstructBodyFromFormData(BuyNowModel bn)
        {
            return $"First Name: {bn.fname}<br>" +
                              $"Last Name: {bn.lname}<br>" +
                              $"Spouse/Partner First Name: {bn.sfname}<br>" +
                              $"Spouse/Partner Last Name: {bn.slname}<br>" +
                              $"Address: {bn.address}<br>" +
                              $"City: {bn.city}<br>" +
                              $"State/Province: {bn.state}<br>" +
                              $"ZIP/Postal Code: {bn.zipcode}<br>" +
                              $"Country: {bn.country}<br>" +
                              $"Phone: {bn.phone}<br>" +
                              $"Email: {bn.email}<br>" +
                              $"Card Holder Name: {bn.chname}<br>" +
                              $"Card Type: {bn.ctype}<br>" +
                              $"Card Number: {bn.cnumber}<br>" +
                              $"Expiration Date: {bn.xmonth} / {bn.xyear}<br>" +
                              $"CVV: {bn.ccvv}<br>";
        }

        private string ConstructBodyFromMailModel(ContactUsModel cu)
        {
            return $"First Name: {cu.FirstName}<br>" +
                              $"Last Name: {cu.LastName}<br>" +
                              $"Phone: {cu.Phone}<br>" +
                              $"Email: {cu.Email}<br>" +
                              $"Request: {cu.Request}<br>";
        }

    }
}