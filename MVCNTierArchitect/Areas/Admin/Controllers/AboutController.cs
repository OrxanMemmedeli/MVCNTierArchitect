using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using MVCNTierArchitect.Infrastrucrure;
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
        private readonly IAboutService _aboutService;
        private readonly AboutValidator _validator;
        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
            _validator = new AboutValidator();
        }
        [CustomAdminAuthorizeAttribute]
        public ActionResult Index()
        {
            var abouts = _aboutService.GetAll();
            return View(abouts);
        }

        [HttpGet]
        public PartialViewResult CreatePartial()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAdminAuthorizeAttribute]
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

            _aboutService.Add(about);
            return RedirectToAction("Index");
        }

        [CustomAdminAuthorizeAttribute]
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var about = _aboutService.GetByID(x => x.ID == id);

            if (about == null)
            {
                return new HttpNotFoundResult();
            }

            about.Status = !about.Status;
            _aboutService.Update(about, about.ID);
            return RedirectToAction("Index");
        }
    }
}