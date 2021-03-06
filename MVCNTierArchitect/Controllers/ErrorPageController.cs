using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Controllers
{
    [AllowAnonymous]
    public class ErrorPageController : Controller
    {
        // GET: ErrorPage
        public ActionResult Responce403()
        {
            Response.StatusCode = 403;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        public ActionResult Responce404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}