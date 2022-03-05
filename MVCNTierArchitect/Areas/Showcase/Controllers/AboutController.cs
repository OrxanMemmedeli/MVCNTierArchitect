using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Showcase.Controllers
{
    public class AboutController : Controller
    {
        // GET: Showcase/About
        public ActionResult Index()
        {
            return View();
        }
    }
}