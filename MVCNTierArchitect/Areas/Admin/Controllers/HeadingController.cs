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
    public class HeadingController : Controller
    {
        private readonly HeadingManager _headingManager;
        private readonly HeadingValidator _validator;
        private readonly CategoryManager _categoryManager;
        private readonly WriterManager _writerManager;
        public HeadingController()
        {
            _headingManager = new HeadingManager(new EFHeadingRepository());
            _categoryManager = new CategoryManager(new EFCategoryRepository());
            _writerManager = new WriterManager(new EFWriterRepository());
            _validator = new HeadingValidator();
        }

        public ActionResult Index()
        {
            var headings = _headingManager.GetAll();
            return View(headings);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> categories = (from c in _categoryManager.GetAll()
                                               select new SelectListItem
                                               {
                                                   Text = c.Name,
                                                   Value = c.ID.ToString()
                                               }).ToList();

            List<SelectListItem> writers = (from w in _writerManager.GetAll()
                                            select new SelectListItem
                                            {
                                                Text = w.Name + " " + w.Surname,
                                                Value = w.ID.ToString()
                                            }).ToList();
            ViewBag.Categories = categories;
            ViewBag.Writers = writers;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Heading heading)
        {
            heading.CreatedDate = DateTime.Now;
            ValidationResult results = _validator.Validate(heading);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(heading);
            }

            _headingManager.Add(heading);
            return RedirectToAction("Index");
        }
    }
}