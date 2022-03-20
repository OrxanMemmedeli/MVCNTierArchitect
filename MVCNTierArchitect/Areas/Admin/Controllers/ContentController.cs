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
    public class ContentController : Controller
    {
        private readonly IContentService _contentService;
        private readonly IHeadingService _headingService;
        private readonly IWriterService _writerService;
        private readonly ContentValidator _validator;

        public ContentController(IContentService contentService, IHeadingService headingService, IWriterService writerService)
        {
            _contentService = contentService;
            _headingService = headingService;
            _writerService = writerService;
            _validator = new ContentValidator();
        }

        public ActionResult Index()
        {
            var contents = _contentService.GetAll();
            return View(contents);
        }

        public ActionResult ContentByHeading(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var contents = _contentService.GetAll(x => x.HeadingID == id).OrderByDescending(x => x.CreatedDate);
            return View(contents);
        }

        public ActionResult ContentByWriter(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var contents = _contentService.GetAll(x => x.WriterID == id).OrderByDescending(x => x.CreatedDate);
            return View(contents);
        }

        public ActionResult Create()
        {
            List<SelectListItem> headings = (from c in _headingService.GetAll()
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
            ViewBag.Headings = headings;
            ViewBag.Writers = writers;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Content content)
        {
            content.CreatedDate = DateTime.Now;
            content.Status = true;
            ValidationResult results = _validator.Validate(content);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                List<SelectListItem> headings = (from c in _headingService.GetAll()
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
                ViewBag.Headings = headings;
                ViewBag.Writers = writers;
                return View(content);
            }

            _contentService.Add(content);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            List<SelectListItem> headings = (from c in _headingService.GetAll()
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
            ViewBag.Headings = headings;
            ViewBag.Writers = writers;

            var content = _contentService.GetByID(x => x.ID == id);
            return View(content);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Content content)
        {
            ValidationResult results = _validator.Validate(content);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                List<SelectListItem> headings = (from c in _headingService.GetAll()
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
                ViewBag.Headings = headings;
                ViewBag.Writers = writers;
                return View(content);
            }

            _contentService.Update(content, content.ID);
            TempData["EditContent"] = "Məzmun yeniləndi.";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var content = _contentService.GetByID(x => x.ID == id);
            if (content == null)
            {
                return new HttpNotFoundResult();
            }
            content.Status = false;
            _contentService.Update(content, content.ID);
            TempData["DeleteContent"] = "Məzmun silindi.";
            return RedirectToAction("Index");
        }

        public ActionResult Restore(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var content = _contentService.GetByID(x => x.ID == id);
            if (content == null)
            {
                return new HttpNotFoundResult();
            }
            content.Status = true;
            _contentService.Update(content, content.ID);
            TempData["RestoreContent"] = "Məzmun bərpa edildi.";
            return RedirectToAction("Index");
        }
    }
}