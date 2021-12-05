using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class AboutController : Controller
    {
        private readonly AboutManager _aboutManager;
        private readonly AboutValidator _validator;
        public AboutController()
        {
            _aboutManager = new AboutManager(new EFAboutRepository());
            _validator = new AboutValidator();
        }
        public ActionResult Index()
        {
            var abouts = _aboutManager.GetAll();
            return View(abouts);
        }

        [HttpGet]
        public PartialViewResult CreatePartial()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(About about)
        {
            ValidationResult results = _validator.Validate(about);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(about);
            }

            _aboutManager.Add(about);
            return RedirectToAction("Index");
        }

    }
}