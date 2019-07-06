using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISSK_2_0.Models;

namespace ISSK_2_0.Controllers
{
    public class DutyController : Controller
    {
        // GET: Duty
        public ActionResult Index()
        {
            using (var db = new IsskDb())
            {
                var duties = db.Brigades.Select(d => d).ToList();
                return duties.Count > 0 ? View(duties) : View();
            }
        }
    }
}