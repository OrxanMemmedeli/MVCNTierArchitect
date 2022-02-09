using BusinessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IHeadingService _headingService;
        private readonly IContentService _contentService;

        public HomeController(IHeadingService headingService, IContentService contentService)
        {
            _headingService = headingService;
            _contentService = contentService;
        }

        public ActionResult Index()
        {
            var fiveDaysAgo = DateTime.Now.AddDays(-5);
            var contents = _contentService.GetAll(x => x.CreatedDate >= fiveDaysAgo && x.Status == true).OrderByDescending(x => x.CreatedDate);
            return View(contents);
        }

        public ActionResult HeadigsBar()
        {
            var headings = _headingService.GetAllWithContentAndWriter(x => x.Status == true).OrderByDescending(x => x.Contents.Count());
            var selectedHeadings = headings.Take(20);
            return PartialView(selectedHeadings);
        }


        public ActionResult ContentByHeading(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var contents = _contentService.GetAll(x => x.HeadingID == id && x.Status == true).OrderByDescending(x => x.CreatedDate);
            return View(contents);
        }

    }
}