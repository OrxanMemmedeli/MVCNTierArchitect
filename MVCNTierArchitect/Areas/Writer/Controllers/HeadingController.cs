using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using MVCNTierArchitect.Infrastrucrure;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tools.Abstract;

namespace MVCNTierArchitect.Areas.Writer.Controllers
{
    [RouteArea("Writer")]
    [CustomWriterAuthorizeAttribute]
    public class HeadingController : Controller
    {
        private readonly IHeadingService _headingService;
        private readonly HeadingValidator _validator;
        private readonly ICategoryService _categoryService;
        private readonly ISessionControl _sessionControl;

        public HeadingController(IHeadingService headingService, ICategoryService categoryService,ISessionControl sessionControl)
        {
            _headingService = headingService;
            _categoryService = categoryService;
            _validator = new HeadingValidator();
            _sessionControl = sessionControl;
        }

        // GET: Writer/Heading
        public ActionResult Index()
        {
            int writeriId = _sessionControl.GetWriterID();
            if (writeriId == 0)
                return RedirectToAction("Login", "Account");
            var headings = _headingService.GetAllWithContentAndWriter(x => x.WriterID == writeriId && x.Status == true);
            return View(headings);
        }

        public ActionResult AllHeadings(int page=1)
        {
            var headings = _headingService.GetAllWithContentAndWriter(x => x.Status == true).ToPagedList(page,10);
            return View(headings);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> categories = (from c in _categoryService.GetAll(x => x.Status == true)
                                               select new SelectListItem
                                               {
                                                   Text = c.Name,
                                                   Value = c.ID.ToString()
                                               }).ToList();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Heading heading)
        {
            heading.CreatedDate = DateTime.Now;
            heading.WriterID = _sessionControl.GetWriterID();

            ValidationResult results = _validator.Validate(heading);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                List<SelectListItem> categories = (from c in _categoryService.GetAll(x => x.Status == true)
                                                   select new SelectListItem
                                                   {
                                                       Text = c.Name,
                                                       Value = c.ID.ToString()
                                                   }).ToList();
                ViewBag.Categories = categories;
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

            List<SelectListItem> categories = (from c in _categoryService.GetAll(x => x.Status == true)
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
                List<SelectListItem> categories = (from c in _categoryService.GetAll(x => x.Status == true)
                                                   select new SelectListItem
                                                   {
                                                       Text = c.Name,
                                                       Value = c.ID.ToString()
                                                   }).ToList();
                ViewBag.Categories = categories;
                return View(heading);
            }

            _headingService.Update(heading, heading.ID);
            TempData["WEditHeading"] = "Başlıq yeniləndi.";
            return RedirectToAction("Index");
        }

        [CustomWriterAuthorizeAttribute]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            return Redirect("/Writer/Heading/DeleteConfirm/" + id);
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            var heading = _headingService.GetByID(x => x.ID == id);
            if (heading == null)
            {
                return new HttpNotFoundResult();
            }
            heading.Status = false;
            _headingService.Update(heading, heading.ID);
            TempData["WDeleteHeading"] = "Başlıq silindi.";
            return RedirectToAction("Index");
        }
    }
}