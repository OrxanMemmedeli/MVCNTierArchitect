using MVCNTierArchitect.Infrastrucrure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCNTierArchitect
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //GlobalFilters.Filters.Add(new AuthorizeAttribute()); //All controller same time authorize
            GlobalFilters.Filters.Add(new AuthorizeAttribute() { Roles = "{action}" }); //All controller same time authorize

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory()); // NİNJECT FACTORY CONTROLLER
        }
    }
}
