using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ISSK_2_0.Models;

namespace ISSK_2_0.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [Authorize]
        public ActionResult Index()
        {
            var user = (CustomMembershipUser) Membership.GetUser(HttpContext.User.Identity.Name, true);
            if (user == null) return RedirectToAction("Login");
            var userModel = new AccountView()
            {
                Code = user.Code,
                Email = user.Email,
                FirstName = user.ConductorData.FirstName,
                LastName = user.ConductorData.LastName,
                Avatar = user.ConductorData.Avatar,
                RoleName = user.Roles.Select(r => r.Name).ToList()
            };
            return View(userModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //[Authorize]
        [HttpGet]
        public ActionResult List()
        {
            using (var db = new IsskDb())
            {
                var users = (db.Conductors.Include("ConductorData").Select(c => c)).ToList();
                return users.Count > 0 ? View(users) : View();
            }            
        }
    }
}