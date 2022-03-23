using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using MVCNTierArchitect.Infrastrucrure;
using System;
using System.Collections.Generic;
using System.IO;
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
            UploadImage(about);
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
            return RedirectToAction("Index", "About");
        }
        private void UploadImage(About about)
        {
            if (about.imageFileFirst != null)
            {
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(about.imageFileFirst.FileName);
                string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName + extension);
                about.imageFileFirst.SaveAs(path);
                about.İmageFirst = "/UploadedFiles/" + fileName + extension;
            }
            if (about.imageFileSecond != null)
            {
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(about.imageFileSecond.FileName);
                string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName + extension);
                about.imageFileSecond.SaveAs(path);
                about.İmageSecond = "/UploadedFiles/" + fileName + extension;
            }

        }

        [HttpGet]
        [CustomAdminAuthorizeAttribute]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var admin = _aboutService.GetByID(x => x.ID == id);

            if (admin == null)
            {
                return new HttpNotFoundResult();
            }

            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAdminAuthorizeAttribute]
        public ActionResult Edit(About about)
        {
            UploadImage(about);
            ValidationResult results = _validator.Validate(about);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(about);
            }

            _aboutService.Update(about, about.ID);
            TempData["DeleteAbout"] = "Məlumat yeniləndi.";
            return RedirectToAction("Index", "About");
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
            TempData["EditAbout"] = "Məlumat yeniləndi.";
            return RedirectToAction("Index", "About");
        }

        [CustomAdminAuthorizeAttribute]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            return Redirect("/admin/About/DeleteConfirm/" + id);
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            var about = _aboutService.GetByID(x => x.ID == id);
            if (about == null)
            {
                return new HttpNotFoundResult();
            }

            _aboutService.Delete(about);
            TempData["DeleteAbout"] = "Məlumat silindi.";
            return RedirectToAction("Index", "About");
        }
    }
}