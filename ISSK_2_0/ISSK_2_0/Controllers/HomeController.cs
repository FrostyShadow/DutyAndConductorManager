using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISSK_2_0.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(HttpContext.User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("Login", "Account");
        }
    }
}