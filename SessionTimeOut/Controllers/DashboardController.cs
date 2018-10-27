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
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Pag1()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Pag2()
        {
            return View();
        }
    }
}