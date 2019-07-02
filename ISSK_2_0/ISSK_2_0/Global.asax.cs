using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using ISSK_2_0.Models;
using Newtonsoft.Json;

namespace ISSK_2_0
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Request.Cookies["Cookie1"];
            if (authCookie == null) return;
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            var serializeModel = JsonConvert.DeserializeObject<AccountView>(authTicket.UserData);
            var principal = new CustomPrincipal(authTicket.Name)
            {
                ConductorId = serializeModel.ConductorId,
                Email = serializeModel.Email,
                Roles = serializeModel.RoleName.ToArray<string>()
            };
            HttpContext.Current.User = principal;
        }
    }
}
