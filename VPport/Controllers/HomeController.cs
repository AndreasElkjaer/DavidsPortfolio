using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace VPport.Controllers
{
    public class HomeController : Controller
    {
        private dbPortEntities db = new dbPortEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        // GET: Projects
        public ActionResult Projects()
        {
            return View();
        }
        public ActionResult ListProjects()
        {
            //var projects = db.Projects.OrderByDescending(o => o.Id).Take(8);
           return View(db.Projects.ToList());
            //return View();
        }
        // GET: About
        public ActionResult About()
        {
            var about = db.Abouts.OrderByDescending(o => o.Id).Take(1);
            return View(about.ToList());
        }
        // GET: Login
        public ActionResult Login()
        {
            Models.LoginViewModel UserLogin = new Models.LoginViewModel();
            return View(UserLogin);
        }
        // HttpPost: Login
        [HttpPost]
        public ActionResult Login(Models.LoginViewModel UserLogin)
        {

            User MyUser = (from U in db.Users where U.UserName == UserLogin.User.UserName select U).FirstOrDefault();

            if (MyUser != null)
            {
                string MyPassword = Mycrypt.HashPassword(UserLogin.User.UserPassword, MyUser.UserSalt);

                User MyLogIn = (from U in db.Users where U.UserName == UserLogin.User.UserName && U.UserPassword == MyPassword select U).FirstOrDefault();

                if (MyLogIn != null)
                {

                    Session["User"] = UserLogin.User.UserName;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    UserLogin.Errmsg = "Wrong username or password!";
                    return View(UserLogin);
                }
            }

            UserLogin.Errmsg = "Wrong username or password!";
            return View(UserLogin);


        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(VPport.Models.MailViewModel myMail)
        {
            if (ModelState.IsValid)
            {
                ContactSendMail(myMail.MailEmail, myMail.MailMessage, myMail.MailName, myMail.MailPhone);
                return RedirectToAction("Contact", "Home");
            }
            else
            {
                return View();
            }
        }
        public void ContactSendMail(string mailEmail, string mailMessage, string mailName, string mailPhone)
        {
            // Mail
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(mailEmail);
            mail.To.Add("andreaselkj@gmail.com");
            mail.ReplyToList.Add(mailEmail);
            mail.Subject = "fra: " + mailName + " Telefon: " + mailPhone;
            mail.Body = mailMessage;
            mail.IsBodyHtml = true;
            // SMTP
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("andreaselkj@gmail.com", "lol123456"),
                EnableSsl = true
            };
            smtp.Send(mail);

        }
    }
}