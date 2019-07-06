using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISSK_2_0.Models;

namespace ISSK_2_0.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize(Roles = "Conductor")]
        public ActionResult Index()
        {
            return View();
        }
    }
}