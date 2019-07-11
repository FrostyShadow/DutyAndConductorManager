using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISSK_2_0.Models;

namespace ISSK_2_0.Controllers
{
    public class SetController : Controller
    {
        [CustomAuthorize(Roles = "Moderator, Administrator")]
        // GET: Set
        public ActionResult Index()
        {
            using (var db = new IsskDb())
            {
                var sets = db.Sets.Select(s => s).ToList();
                return View(sets);
            }
        }
    }
}