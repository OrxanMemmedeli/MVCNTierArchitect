using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Admin.Controllers
{
    public class IstatistikController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult TotalCategory()
        {
            CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
            ViewBag.Total = categoryManager.GetAll().Count();
            return PartialView();
        }

        public PartialViewResult TotalHeadingByProgramming()
        {
            HeadingManager headingManager = new HeadingManager(new EFHeadingRepository());
            ViewBag.TotalByProgramming = headingManager.GetAll(x => x.CategorID == 1).Count();
            return PartialView();
        }

        public PartialViewResult FindASimvolInWriter()
        {
            WriterManager writerManager = new WriterManager(new EFWriterRepository());
            ViewBag.Writers = writerManager.GetAll(x => x.Name.Contains("a")).Count();
            return PartialView();
        }

        public PartialViewResult CategoryMostTitle()
        {
            CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
            var categories = categoryManager.GetAllWithHeading().OrderByDescending(x => x.Headings.Count());
            ViewBag.Category = categories.First().Name;
            return PartialView();
        }

        public PartialViewResult Difference()
        {
            CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
            var categories = categoryManager.GetAll();
            int statusTrue = categories.Where(x => x.Status == true).Count();
            int statusFalse = categories.Where(x => x.Status == false).Count();
            int fark = statusTrue - statusFalse;
            ViewBag.Difference = "Status True sayı: " + statusTrue + " Status False sayı: " + statusFalse + " aradakı fark: " + fark;
            return PartialView();
        }
    }
}