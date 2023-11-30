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

        [HttpPost]
        public IActionResult ProcessForm(BuyNowModel bn, ContactUsModel cu, LogicModel l)
        {
            if (ModelState.IsValid)
            {
                string body;
                if (!string.IsNullOrEmpty(bn.fname)) // Check if formData contains fields specific to the first method
                {
                    body = ConstructBodyFromBuyNowModel(bn);
                    l.Subject = "Buy Now";
                }
                else if (!string.IsNullOrEmpty(cu.FirstName)) // Check if mailModel contains fields specific to the second method
                {
                    body = ConstructBodyFromContactUsModel(cu);
                    l.Subject = "Contact Us";
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
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(l.Login, l.Password);
                    var message = new MailMessage();
                    message.From = new MailAddress(l.FromEmail, l.FromName); // Your email address
                    string[] toEmails = l.ToEmail.Split(',');
                    foreach (string email in toEmails)
                    {
                        message.To.Add(email.Trim());
                    }
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

        private string ConstructBodyFromBuyNowModel(BuyNowModel bn)
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

        private string ConstructBodyFromContactUsModel(ContactUsModel cu)
        {
            return $"First Name: {cu.FirstName}<br>" +
                              $"Last Name: {cu.LastName}<br>" +
                              $"Phone: {cu.Phone}<br>" +
                              $"Email: {cu.Email}<br>" +
                              $"Request: {cu.Request}<br>";
        }

    }
}