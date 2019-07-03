﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ISSK_2_0.Models;
using Newtonsoft.Json;

namespace ISSK_2_0.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [CustomAuthorize(Roles = "Conductor, Manager, Moderator, Administrator")]
        public ActionResult Index()
        {
            var user = (CustomMembershipUser) Membership.GetUser(HttpContext.User.Identity.Name, true);
            if (user == null) return RedirectToAction("Login");
            var userModel = new AccountView
            {
                Code = user.Code,
                Email = user.Email,
                FirstName = user.ConductorData.FirstName,
                LastName = user.ConductorData.LastName,
                Avatar = user.ConductorData.Avatar,
                RoleName = user.Roles.Select(r => r.DisplayName).ToList()
            };
            return View(userModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
                return LogOut();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginView loginView, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginView.Email, loginView.Password))
                {
                    var user = (CustomMembershipUser) Membership.GetUser(loginView.Email, false);
                    if (user != null)
                    {
                        var userModel = new AccountView
                        {
                            ConductorId = user.ConductorId,
                            Code = user.Code,
                            Email = user.Email,
                            FirstName = user.ConductorData.FirstName,
                            MiddleName = user.ConductorData.MiddleName,
                            LastName = user.ConductorData.LastName,
                            Avatar = user.ConductorData.Avatar,
                            RoleName = user.Roles.Select(r => r.DisplayName).ToList()
                        };

                        var userData = JsonConvert.SerializeObject(userModel);
                        var authTicket = new FormsAuthenticationTicket(1, loginView.Email, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData);
                        var enTicket = FormsAuthentication.Encrypt(authTicket);
                        var faCookie = new HttpCookie("Cookie1", enTicket);
                        Response.Cookies.Add(faCookie);

                        using (var db = new IsskDb())
                        {
                            var conductor = db.Conductors.FirstOrDefault(u =>
                                string.Compare(loginView.Email, u.Email, StringComparison.OrdinalIgnoreCase) == 0);
                            if (conductor != null)
                            {
                                conductor.LastActiveDateTime = DateTime.Now;
                                db.SaveChanges();
                            }
                        }
                    }

                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", @"Something Wrong : Adres Email lub Hasło nieprawidłowe");
            return View(loginView);
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterView registerView)
        {
            using (var db = new IsskDb())
            {
                var email = db.Conductors.FirstOrDefault(u => string.Compare(registerView.Email, u.Email, StringComparison.OrdinalIgnoreCase) == 0);
                if (email == null)
                {
                    ModelState.AddModelError("", "Email nie został znaleziony!");
                    return View(registerView);
                }
                if (string.Compare(email.ActivationCode, registerView.ActivationCode, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    ModelState.AddModelError("", "Nieprawidłowy kod aktywacyjny!");
                    return View(registerView);
                }
                if (email.IsActive)
                {
                    ModelState.AddModelError("", "Konto jest już aktywne!");
                    return View(registerView);
                }

                email.IsActive = true;
                email.LastActiveDateTime = DateTime.Now;
                db.SaveChanges();

                var userModel = new AccountView
                {
                    ConductorId = email.ConductorId,
                    Code = email.Code,
                    Email = email.Email,
                    FirstName = email.ConductorData.FirstName,
                    MiddleName = email.ConductorData.MiddleName,
                    LastName = email.ConductorData.LastName,
                    Avatar = email.ConductorData.Avatar,
                    RoleName = email.Roles.Select(r => r.DisplayName).ToList(),
                };

                var userData = JsonConvert.SerializeObject(userModel);
                var authTicket = new FormsAuthenticationTicket(1, registerView.Email, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData);
                var enTicket = FormsAuthentication.Encrypt(authTicket);
                var faCookie = new HttpCookie("Cookie1", enTicket);
                Response.Cookies.Add(faCookie);
                return RedirectToAction("ChangePassword", "Account");
            }
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult ChangePassword(string returnUrl = "")
        {
            return View();
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult ChangePassword(SetPasswordView setPasswordView, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                var pbkdf2 = new Rfc2898DeriveBytes(setPasswordView.Password, salt, 10000);
                var hash = pbkdf2.GetBytes(20);
                var hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                var savedPasswordHash = Convert.ToBase64String(hashBytes);

                using (var db = new IsskDb())
                {
                    var user = db.Conductors.FirstOrDefault(u =>
                        string.Compare(u.Email, HttpContext.User.Identity.Name, StringComparison.OrdinalIgnoreCase) ==
                        0);
                    if (user != null)
                    {
                        if (string.Compare(user.Password, savedPasswordHash, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            ModelState.AddModelError("", "Nowe hasło nie może być takie samo jak stare.");
                            return View(setPasswordView);
                        }

                        user.Password = savedPasswordHash;
                        user.Roles.Add(db.Roles.FirstOrDefault(r => r.Id == 1));
                        db.SaveChanges();
                    }
                }

            }

            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index");
        }

        [CustomAuthorize(Roles = "Conductor, Manager, Moderator, Administrator")]
        [HttpGet]
        public ActionResult Edit()
        {
            using (var db = new IsskDb())
            {
                var user = db.Conductors.Include("ConductorData").FirstOrDefault(u => string.Compare(u.Email, HttpContext.User.Identity.Name, StringComparison.OrdinalIgnoreCase) == 0);
                if (user == null) return RedirectToAction("Login");
                return View(user);
            }
        }

        [CustomAuthorize(Roles = "Conductor, Manager, Moderator, Administrator")]
        [HttpGet]
        public ActionResult List()
        {
            using (var db = new IsskDb())
            {
                var users = db.Conductors.Include("ConductorData").Include("Roles").Select(c => c).ToList();
                return users.Count > 0 ? View(users) : View();
            }
        }

        [CustomAuthorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Administrator")]
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
                    var conductor = new Conductor
                    {
                        Email = createView.Email,
                        Password = null,
                        IsActive = false,
                        ActivationCode = activationCode,
                        Code = createView.Code,
                        ConductorData = new ConductorData
                        {
                            BirthDate = createView.BirthDate.Date,
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

        [CustomAuthorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult CreateResult(int? id)
        {
            ViewBag.Status = TempData["Status"] ?? false;

            using (var db = new IsskDb())
            {
                var conductor =
                    db.Conductors.Include("ConductorData").FirstOrDefault(c => c.ConductorId == id);
                return View(conductor);
            }
        }

        [CustomAuthorize(Roles = "Conductor, Manager, Moderator, Administrator")]
        [HttpGet]
        public ActionResult LogOut()
        {
            var cookie = new HttpCookie("Cookie1", "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            Response.Cookies.Add(cookie);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
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