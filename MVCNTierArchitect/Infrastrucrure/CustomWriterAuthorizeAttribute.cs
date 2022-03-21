using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tools.Concrete;

namespace MVCNTierArchitect.Infrastrucrure
{
    public class CustomWriterAuthorizeAttribute : ActionFilterAttribute
    {

        private readonly WriterManager writerManager = new WriterManager(new EFWriterRepository());
        private readonly RoleMethodManager roleMethodManager = new RoleMethodManager(new EFRoleMethodRepository());
        private readonly AncryptionAndDecryption ancryptionAndDecryption = new AncryptionAndDecryption();
        private readonly RoleControllerNameManager roleControllerNameManager = new RoleControllerNameManager(new EFRoleControllerNameRepository());

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var cookieWriter = HttpContext.Current.Session["WriterEmail"] == null ? "" : HttpContext.Current.Session["WriterEmail"];

            if (string.IsNullOrEmpty(cookieWriter.ToString()))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "area", "" }, { "controller", "Account" }, { "action", "Login" } });
            }
            else
            {
                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var actionName = filterContext.RouteData.Values["action"].ToString();

                var ancrypt = ancryptionAndDecryption.EncodeData(cookieWriter.ToString());
                var writer = writerManager.Get(x => x.Email == ancrypt);


                var actions = roleMethodManager.GetRoleMethodNames((int)writer.RoleID);
                var controllers = roleControllerNameManager.GetRoleControllerNames((int)writer.RoleID);

                if (!actions.Contains(actionName) || !controllers.Contains(controllerName))
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "area", "" }, { "controller", "Account" }, { "action", "Denied" } });

            }

            base.OnActionExecuted(filterContext);
        }
    }
}