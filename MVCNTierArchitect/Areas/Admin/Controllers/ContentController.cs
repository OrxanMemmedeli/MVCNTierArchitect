using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class ContentController : Controller
    {
        private readonly ContentManager _contentManager;
        public ContentController()
        {
            _contentManager = new ContentManager(new EFContentRepository());
        }

        public ActionResult Index()
        {
            var contents = _contentManager.GetAll();
            return View(contents);
        }

        public ActionResult ContentByHeading(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var contents = _contentManager.GetAll(x => x.HeadingID == id).OrderByDescending(x => x.CreatedDate);
            return View(contents);
        }

        public ActionResult ContentByWriter(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var contents = _contentManager.GetAll(x => x.WriterID == id).OrderByDescending(x => x.CreatedDate);
            return View(contents);
        }
    }
}