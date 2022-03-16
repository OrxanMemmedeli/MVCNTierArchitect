using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Infrastrucrure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _methodName;
        public CustomAuthorizeAttribute(string methodName)
        {
            _methodName = methodName;
        }

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    bool authorize = false;
        //    var userId = Convert.ToString(httpContext.Session["UserId"]);
        //    if (!string.IsNullOrEmpty(userId))


        //        using (var context = new SqlDbContext())
        //        {
        //            var userRole = (from u in context.Users
        //                            join r in context.Roles on u.RoleId equals r.Id
        //                            where u.UserId == userId
        //                            select new
        //                            {
        //                                r.Name
        //                            }).FirstOrDefault();
        //            foreach (var role in allowedroles)
        //            {
        //                if (role == userRole.Name) return true;
        //            }
        //        }


        //    return authorize;
        //}

        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    filterContext.Result = new RedirectToRouteResult(
        //       new RouteValueDictionary
        //       {
        //            { "controller", "Home" },
        //            { "action", "UnAuthorized" }
        //       });
        //}
    }  
}