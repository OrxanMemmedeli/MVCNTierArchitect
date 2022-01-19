using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tools.Abstract;

namespace MVCNTierArchitect.Areas.Writer.Controllers
{
    [RouteArea("Writer")]
    public class ContentController : Controller
    {
        private readonly IContentService _contentService;
        private readonly IHeadingService _headingService;
        private readonly ContentValidator _validator;
        private readonly IWriterService _writerService;
        private readonly IAncryptionAndDecryption _ancryptionAndDecryption;


        public ContentController(IContentService contentService, IHeadingService headingService, ContentValidator validator, IWriterService writerService, IAncryptionAndDecryption ancryptionAndDecryption)
        {
            _writerService = writerService;
            _contentService = contentService;
            _headingService = headingService;
            _validator = validator;
            _ancryptionAndDecryption = ancryptionAndDecryption;
        }

        // GET: Writer/Content
        public ActionResult Index()
        {
            var writer = _writerService.Get(x => x.Email == _ancryptionAndDecryption.EncodeData(Session["WriterEmail"].ToString()));

            var contents = _contentService.GetAllByHeading(x => x.WriterID == writer.ID && x.Status == true);
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

            ViewBag.Headings = headings;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Content content)
        {
            var writer = _writerService.Get(x => x.Email == _ancryptionAndDecryption.EncodeData(Session["WriterEmail"].ToString()));

            content.CreatedDate = DateTime.Now;
            content.Status = true;
            content.WriterID = writer.ID;
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
            TempData["WDeleteContent"] = "Məzmun silindi.";
            return Redirect("/Writer/Content");
        }

    }
}