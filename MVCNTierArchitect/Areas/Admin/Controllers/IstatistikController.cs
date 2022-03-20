using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using MVCNTierArchitect.Infrastrucrure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class IstatistikController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IHeadingService _headingService;
        private readonly IWriterService _writerService;

        public IstatistikController(ICategoryService categoryService, IHeadingService headingService, IWriterService writerService)
        {
            _categoryService = categoryService;
            _headingService = headingService;
            _writerService = writerService;
        }
        [CustomAdminAuthorizeAttribute]
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult TotalCategory()
        {
            ViewBag.Total = _categoryService.GetAll().Count();
            return PartialView();
        }

        public PartialViewResult TotalHeadingByProgramming()
        {
            ViewBag.TotalByProgramming = _headingService.GetAll(x => x.CategoryID == 1).Count();
            return PartialView();
        }

        public PartialViewResult FindASimvolInWriter()
        {
            ViewBag.Writers = _writerService.GetAll(x => x.Name.Contains("a")).Count();
            return PartialView();
        }

        public PartialViewResult CategoryMostTitle()
        {
            var categories = _categoryService.GetAllWithHeading().OrderByDescending(x => x.Headings.Count());
            ViewBag.Category = categories.First().Name;
            return PartialView();
        }

        public PartialViewResult Difference()
        {
            var categories = _categoryService.GetAll();
            int statusTrue = categories.Where(x => x.Status == true).Count();
            int statusFalse = categories.Where(x => x.Status == false).Count();
            int fark = statusTrue - statusFalse;
            ViewBag.Difference = "Status True sayı: " + statusTrue + " Status False sayı: " + statusFalse + " aradakı fark: " + fark;
            return PartialView();
        }
    }
}