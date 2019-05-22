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
            var user = (CustomMembershipUser)Membership.GetUser(HttpContext.User.Identity.Name, true);
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

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateView createView)
        {
            if (ModelState.IsValid)
            {
                var email = Membership.GetUser(createView.Email)?.Email;
                if (!string.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError("Warning Email", @"Sorry: Email jest już używany!");
                    const string messageCreation = "Email jest już używany!";
                    ViewBag.Message = messageCreation;
                    return View(createView);
                }

                using (var db = new IsskDb())
                {
                    var activationCode = RandomString(10);
                    var conductor = new Conductor()
                    {
                        Email = createView.Email,
                        Password = null,
                        IsActive = false,
                        ActivationCode = activationCode,
                        Code = createView.Code,
                        ConductorData = new ConductorData()
                        {
                            BirthDate = Convert.ToDateTime(createView.BirthDate),
                            FirstName = createView.FirstName,
                            MiddleName = null,
                            LastName = createView.LastName,
                            City = createView.City
                        }
                    };
                    db.Conductors.Add(conductor);
                    db.SaveChanges();
                    ViewBag.Message = "Konduktor utworzony poprawnie!";
                    TempData["Status"] = true;
                    return RedirectToAction("CreateResult", "Account", new { id = conductor.ConductorId});
                }

            }

            TempData["Status"] = false;
            return RedirectToAction("CreateResult", "Account");
        }

        public ActionResult CreateResult(int? id)
        {
            ViewBag.Status = TempData["Status"];
            using (var db = new IsskDb())
            {
                var conductor =
                    (db.Conductors.Include("ConductorData").Where(c => c.ConductorId == id)).FirstOrDefault();
                return View(conductor);
            }
        }

        [NonAction]
        public static string RandomString(int length)
        {
            var random = new Random();
            const string chars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%-+=/*\|";
            return new string(Enumerable.Repeat(chars,length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}