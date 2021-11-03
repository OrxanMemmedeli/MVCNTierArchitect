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
    }
}