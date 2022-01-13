using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Writer.Controllers
{
    [RouteArea("Writer")]
    public class WriterController : Controller
    {
        // GET: Writer/Writer
        public ActionResult Index()
        {
            return View();
        }
    }
}