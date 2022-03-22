using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tools.Concrete;

namespace MVCNTierArchitect.Infrastrucrure
{
    public class CustomAdminAuthorizeAttribute : ActionFilterAttribute
    {

        private readonly AdminManager adminManager = new AdminManager(new EFAdminRepository());
        private readonly RoleMethodManager roleMethodManager = new RoleMethodManager(new EFRoleMethodRepository());
        private readonly AncryptionAndDecryption ancryptionAndDecryption = new AncryptionAndDecryption();
        private readonly RoleControllerNameManager roleControllerNameManager = new RoleControllerNameManager(new EFRoleControllerNameRepository());
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var cookieAdmin = HttpContext.Current.Session["AdminUserName"] == null ? "" : HttpContext.Current.Session["AdminUserName"];

            if (string.IsNullOrEmpty(cookieAdmin.ToString()))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "area", "" }, { "controller", "Account" }, { "action", "AdminLogin" } });
            }
            else
            {
                var controllerName = filterContext.RouteData.Values["controller"].ToString().ToLower();
                var actionName = filterContext.RouteData.Values["action"].ToString().ToLower();

                var ancrypt = ancryptionAndDecryption.EncodeData(cookieAdmin.ToString());
                var admin = adminManager.Get(x => x.UserName == ancrypt);

                var actions = roleMethodManager.GetRoleMethodNames((int)admin.RoleID);
                var controllers = roleControllerNameManager.GetRoleControllerNames((int)admin.RoleID);
                if (!actions.Contains(actionName) || !controllers.Contains(controllerName))
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "area", "" }, { "controller", "Account" }, { "action", "Denied" } });

            }

            //base.OnActionExecuted(filterContext);
        }
    }
}