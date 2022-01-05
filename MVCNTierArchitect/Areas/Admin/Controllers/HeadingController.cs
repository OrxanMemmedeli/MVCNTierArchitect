using BusinessLayer.Abstract;
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
    public class HeadingController : Controller
    {
        private readonly IHeadingService _headingService;
        private readonly HeadingValidator _validator;
        private readonly ICategoryService _categoryService;
        private readonly IWriterService _writerService;

        public HeadingController(IHeadingService headingService, ICategoryService categoryService, IWriterService writerService)
        {
            _headingService = headingService;
            _categoryService = categoryService;
            _writerService = writerService;                    
            _validator = new HeadingValidator();
        }

        public ActionResult Index()
        {
            var headings = _headingService.GetAllWithContent();
            return View(headings);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> categories = (from c in _categoryService.GetAll()
                                               select new SelectListItem
                                               {
                                                   Text = c.Name,
                                                   Value = c.ID.ToString()
                                               }).ToList();

            List<SelectListItem> writers = (from w in _writerService.GetAll()
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
                List<SelectListItem> categories = (from c in _categoryService.GetAll()
                                                   select new SelectListItem
                                                   {
                                                       Text = c.Name,
                                                       Value = c.ID.ToString()
                                                   }).ToList();

                List<SelectListItem> writers = (from w in _writerService.GetAll()
                                                select new SelectListItem
                                                {
                                                    Text = w.Name + " " + w.Surname,
                                                    Value = w.ID.ToString()
                                                }).ToList();
                ViewBag.Categories = categories;
                ViewBag.Writers = writers;
                return View(heading);
            }

            _headingService.Add(heading);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            List<SelectListItem> categories = (from c in _categoryService.GetAll()
                                               select new SelectListItem
                                               {
                                                   Text = c.Name,
                                                   Value = c.ID.ToString()
                                               }).ToList();
            ViewBag.Categories = categories;
            var heading = _headingService.GetByID(x => x.ID == id);
            return View(heading);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Heading heading)
        {
            heading.CreatedDate = DateTime.Now;
            ValidationResult results = _validator.Validate(heading);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                List<SelectListItem> categories = (from c in _categoryService.GetAll()
                                                   select new SelectListItem
                                                   {
                                                       Text = c.Name,
                                                       Value = c.ID.ToString()
                                                   }).ToList();
                ViewBag.Categories = categories;
                return View(heading);
            }

            _headingService.Update(heading);
            TempData["EditHeading"] = "Başlıq yeniləndi.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var heading = _headingService.GetByID(x => x.ID == id);
            if (heading == null)
            {
                return new HttpNotFoundResult();
            }
            heading.Status = false;
            _headingService.Update(heading);
            TempData["DeleteHeading"] = "Başlıq silindi.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Restore(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var heading = _headingService.GetByID(x => x.ID == id);
            if (heading == null)
            {
                return new HttpNotFoundResult();
            }
            heading.Status = true;
            _headingService.Update(heading);
            TempData["RestoreHeading"] = "Başlıq bərpa edildi.";
            return RedirectToAction("Index");
        }
    }
}