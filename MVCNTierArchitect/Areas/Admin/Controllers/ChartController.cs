using BusinessLayer.Abstract;
using MVCNTierArchitect.Infrastrucrure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [CustomAdminAuthorizeAttribute]
    public class ChartController : Controller
    {
        private readonly ICategoryService _categoryService;

        public ChartController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Admin/Chart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCategoryChart()
        {
            var list = _categoryService.GetCountHeading();
            return Json(list,JsonRequestBehavior.AllowGet);
        }
    }
}