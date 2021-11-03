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
    public class CategoryController : Controller
    {
        private readonly CategoryManager _categoryManager;
        private readonly CategoryValidator _validator;
        public CategoryController()
        {
            _categoryManager = new CategoryManager(new EFCategoryRepository());
            _validator = new CategoryValidator();
        }

        public ActionResult Index()
        {
            var categories = _categoryManager.GetAll();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            ValidationResult results = _validator.Validate(category);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(category);
            }

            _categoryManager.Add(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var category = _categoryManager.GetByID(x => x.ID == id);
            if (category == null)
            {
                return new HttpNotFoundResult();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            ValidationResult results = _validator.Validate(category);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(category);
            }

            _categoryManager.Update(category);
            TempData["EditCategory"] = "Kateqoriya yeniləndi.";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var category = _categoryManager.GetByID(x => x.ID == id);
            if (category == null)
            {
                return new HttpNotFoundResult();
            }

            _categoryManager.Delete(category);
            TempData["DeleteCategory"] = "Kateqoriya silindi.";
            return RedirectToAction("Index");
        }
    }
}