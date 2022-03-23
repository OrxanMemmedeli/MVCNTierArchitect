using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using MVCNTierArchitect.Infrastrucrure;
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
    public class ContentController : Controller
    {
        private readonly IContentService _contentService;
        private readonly IHeadingService _headingService;
        private readonly ContentValidator _validator;
        private readonly ISessionControl _sessionControl;

        public ContentController(IContentService contentService, IHeadingService headingService, ContentValidator validator, ISessionControl sessionControl)
        {
            _contentService = contentService;
            _headingService = headingService;
            _validator = validator;
            _sessionControl = sessionControl;
        }

        // GET: Writer/Content
        public ActionResult Index()
        {
            int writeriId = _sessionControl.GetWriterID(); 
            if (writeriId == 0)
                return RedirectToAction("Login", "Account");
            var contents = _contentService.GetAllByHeading(x => x.WriterID == writeriId && x.Status == true);
            return View(contents);
        }

        public ActionResult ContentByWriter(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var contents = _contentService.GetAll(x => x.WriterID == id).OrderByDescending(x => x.CreatedDate);
            ViewBag.User = contents.First().Writer.Name + " " + contents.First().Writer.Surname;
            return View(contents);
        }

        public ActionResult Create(int? id)
        {
            Content content = new Content();
            List<SelectListItem> headings = (from c in _headingService.GetAll()
                                             select new SelectListItem
                                             {
                                                 Text = c.Name,
                                                 Value = c.ID.ToString()
                                             }).ToList();
            if (id != null)
            {
                content.HeadingID = (int)id;
            }
            
            ViewBag.Headings = headings;
            return View(content);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Content content)
        {
            content.CreatedDate = DateTime.Now;
            content.Status = true;
            content.WriterID = _sessionControl.GetWriterID();
            if (content.WriterID == 0)
                return RedirectToAction("Login", "Account");
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
                ViewBag.Headings = headings;
                return View(content);
            }

            _contentService.Add(content);
            return Redirect("/Writer/Content");
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
            ViewBag.Headings = headings;
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
                ViewBag.Headings = headings;
                return View(content);
            }

            _contentService.Update(content, content.ID);
            TempData["WEditContent"] = "Məzmun yeniləndi.";
            return Redirect("/Writer/Content");
        }

        [CustomWriterAuthorizeAttribute]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            return Redirect("/Writer/Content/DeleteConfirm/" + id);
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            var content = _contentService.GetByID(x => x.ID == id);
            if (content == null)
            {
                return new HttpNotFoundResult();
            }
            content.Status = false;
            _contentService.Update(content, content.ID);
            TempData["WDeleteContent"] = "Məzmun silindi.";
            return Redirect("/Writer/Content");
        }

    }
}