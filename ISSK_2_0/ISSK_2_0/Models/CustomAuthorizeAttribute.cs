using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ISSK_2_0.Models
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual CustomPrincipal CurrentUser => HttpContext.Current.User as CustomPrincipal;

        protected override bool AuthorizeCore(HttpContextBase httpContext) =>
            (CurrentUser == null || CurrentUser.IsInRole(Roles)) && CurrentUser != null;

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult routeData;
            if (CurrentUser == null)
            {
                routeData = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Account",
                    action = "Login"
                }));
            }
            else
            {
                routeData = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Error",
                    action = "AccessDenied"
                }));
            }

            filterContext.Result = routeData;
        }
    }
}