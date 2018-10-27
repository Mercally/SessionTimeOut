using SessionTimeOut.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SessionTimeOut.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            SessionTime.CreateSession();
            return RedirectToActionPermanent("index", "dashboard");
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult InSession()
        {
            return Json(SessionTime.Current, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult CloseSession()
        {
            SessionTime.CloseSession();
            return RedirectToActionPermanent("login");
        }
    }
}