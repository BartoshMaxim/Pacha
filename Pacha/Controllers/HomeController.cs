using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pacha.Models
{
    public class HomeController : Controller
    {

        private ApplicationContext db = new ApplicationContext();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Delivery()
        {
            return View();
        }

        public ActionResult Paying()
        {
            return View();
        }

        public ActionResult Feedbacks()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }

        [Authorize]
        public ActionResult Profile()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(user);
        }
    }
}