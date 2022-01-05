using BusinessLayer.Abstract;
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
        private readonly IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        public ActionResult Index()
        {
            var contents = _contentService.GetAll();
            return View(contents);
        }

        public ActionResult ContentByHeading(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var contents = _contentService.GetAll(x => x.HeadingID == id).OrderByDescending(x => x.CreatedDate);
            return View(contents);
        }

        public ActionResult ContentByWriter(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var contents = _contentService.GetAll(x => x.WriterID == id).OrderByDescending(x => x.CreatedDate);
            return View(contents);
        }
    }
}