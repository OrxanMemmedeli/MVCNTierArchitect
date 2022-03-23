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
    [CustomAdminAuthorizeAttribute]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly CategoryValidator _validator;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _validator = new CategoryValidator();
        }

        public ActionResult Index()
        {
            var categories = _categoryService.GetAll();
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

            _categoryService.Add(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var category = _categoryService.GetByID(x => x.ID == id);
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

            _categoryService.Update(category, category.ID);
            TempData["EditCategory"] = "Kateqoriya yeniləndi.";
            return RedirectToAction("Index");
        }


        [CustomAdminAuthorizeAttribute]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            return Redirect("/admin/Category/DeleteConfirm/" + id);
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            var category = _categoryService.GetByID(x => x.ID == id);
            if (category == null)
            {
                return new HttpNotFoundResult();
            }

            _categoryService.Delete(category);
            TempData["DeleteCategory"] = "Kateqoriya silindi.";
            return RedirectToAction("Index");
        }
    }
}