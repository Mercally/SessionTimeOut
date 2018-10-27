using SessionTimeOut.Models;
using SessionTimeOut.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SessionTimeOut.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            SessionTime.CreateSession();
            return RedirectToActionPermanent("Index");
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult InSession()
        {
            return Json(SessionTime.Current, JsonRequestBehavior.DenyGet);
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult CloseSession()
        {
            SessionTime.CloseSession();
            return RedirectToActionPermanent("Login");
        }
    }
}