using BusinessLayer.Abstract;
using EntityLayer.ViewModels;
using MVCNTierArchitect.Infrastrucrure;
using MVCNTierArchitect.Models.ViewModels;
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

        public ActionResult Index(HeadingSearchViewModel search)
        {
            var fiveDaysAgo = DateTime.Now.AddDays(-5);
            var contents = _contentService.GetAllBySearchModel(x => x.CreatedDate >= fiveDaysAgo && x.Status == true, search).OrderByDescending(x => x.CreatedDate);
            return View(contents);
        }

        public ActionResult HeadigsBar()
        {
            var headings = _headingService.GetAllWithContentAndWriter(x => x.Status == true).OrderByDescending(x => x.Contents.Count());
            var selectedHeadings = headings.Take(20);
            return PartialView(selectedHeadings);
        }

        public PartialViewResult SearchBox()
        {
            return PartialView();
        }

        public ActionResult ContentByHeading(int? id, HeadingSearchViewModel search)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            ViewBag.ID = id;
            var contents = _contentService.GetAllBySearchModel(x => x.HeadingID == id && x.Status == true, search).OrderByDescending(x => x.CreatedDate);
            return View(contents);
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public JsonResult GetCalendarData()
        {
            List<CalendarViewModel> events = new List<CalendarViewModel>();
            Random random = new Random();
            string[] colors = new string[] { "red", "green", "blue", "yellow", "purple", "pink", "orange" };

            var contents = _contentService.GetAllByHeadingAndWriter(x => x.Status == true);

            if (contents.Count > 0)
            {
                foreach (var item in contents)
                {
                    events.Add(new CalendarViewModel
                    {
                        Subject = "Başlıq: " + item.Heading.Name + " (Yazar: " + item.Writer.Name + ")",
                        Description = "",
                        Start = item.CreatedDate.ToString("yyyy-MM-dd"),
                        Color = colors[random.Next(colors.Count())]
                    });
                }
            }

            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}