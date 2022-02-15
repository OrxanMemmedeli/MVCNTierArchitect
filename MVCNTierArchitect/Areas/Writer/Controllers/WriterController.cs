using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
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
    public class WriterController : Controller
    {
        private readonly IAncryptionAndDecryption _ancryptionAndDecryption;
        private readonly IWriterService _writerService;
        private readonly IHeadingService _headingService;
        private readonly IContentService _contentService;
        private readonly WriterValidator _validator;
        private readonly ISessionControl _sessionControl;

        public WriterController(IAncryptionAndDecryption ancryptionAndDecryption, IWriterService writerService, IHeadingService headingService, IContentService contentService, ISessionControl sessionControl)
        {
            _ancryptionAndDecryption = ancryptionAndDecryption;
            _writerService = writerService;
            _headingService = headingService;
            _contentService = contentService;
            _validator = new WriterValidator(_writerService);
            _sessionControl = sessionControl;
        }


        // GET: Writer/Writer
        public ActionResult Index()
        {
            int writeriId = _sessionControl.GetWriterID(Session["WriterEmail"].ToString());
            var writer = _writerService.GetByID(x => x.ID == writeriId);
            ViewBag.Email = _ancryptionAndDecryption.DecodeData(writer.Email);

            ViewBag.Headings = _headingService.GetAll(x => x.Status == true && x.WriterID == writeriId).Count();
            ViewBag.Contents = _contentService.GetAll(x => x.Status == true && x.WriterID == writeriId).Count();
            return View(writer);
        }

        public ActionResult LastActivity()
        {
            int writeriId = _sessionControl.GetWriterID(Session["WriterEmail"].ToString());
            var lastActivity = _contentService.GetAllByHeading(x => x.Status == true && x.WriterID == writeriId).OrderByDescending(x => x.CreatedDate).Take(5);
            return PartialView(lastActivity);
        }

        public ActionResult LastPosts()
        {
            int writeriId = _sessionControl.GetWriterID(Session["WriterEmail"].ToString());
            var lastPosts = _contentService.GetAllByHeading(x => x.Status == true).OrderByDescending(x => x.CreatedDate).Take(5);
            return PartialView(lastPosts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EntityLayer.Concrete.Writer writer)
        {
            if ((writer.Password == "Test123456!!" || string.IsNullOrEmpty(writer.Password)) && (writer.ConfirmPassword == "Test123456!!" || string.IsNullOrEmpty(writer.ConfirmPassword)))
            {
                writer.Password = writer.OldPassword;
                writer.ConfirmPassword = writer.OldPassword;
            }

            writer.Password = _ancryptionAndDecryption.DecodeData(writer.Password);
            writer.ConfirmPassword = writer.Password;
            writer.Email = _ancryptionAndDecryption.EncodeData(writer.Email);

            ValidationResult results = _validator.Validate(writer);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(writer);
            }
            else if (!_ancryptionAndDecryption.DecodeData(writer.Email).Contains("@"))
            {
                ModelState.AddModelError("Email", "Email adresi düzgün daxil edilmeyib.");
                writer.Email = _ancryptionAndDecryption.DecodeData(writer.Email);
                return View(writer);
            }
            else if (!_writerService.IsEmailUnique(writer.Email, writer.ID))
            {
                ModelState.AddModelError("Email", "Email ünvanı istifadə edilib. Fərqli ünvan istifadə edin.");
                writer.Email = _ancryptionAndDecryption.DecodeData(writer.Email);
                return View(writer);
            }

            writer.Password = _ancryptionAndDecryption.EncodeData(writer.Password);
            _writerService.Update(writer, writer.ID);
            return RedirectToAction("Index");
        }

    }
}