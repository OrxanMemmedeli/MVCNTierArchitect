using BusinessLayer.Abstract;
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

namespace MVCNTierArchitect.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryServive;

        public CategoryController(ICategoryService categoryServive)
        {
            _categoryServive = categoryServive;
        }

        // GET: Category
        public ActionResult Index()
        {
            return View(_categoryServive.GetAll());
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
            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult validationResult = categoryValidator.Validate(category);    // using FluentValidation.Results

            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(category);
            }

            _categoryServive.Add(category);
            return RedirectToAction("Index");
        }
    }
}