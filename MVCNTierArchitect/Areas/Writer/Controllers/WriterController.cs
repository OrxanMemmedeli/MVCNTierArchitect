using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using FluentValidation.Results;
using MVCNTierArchitect.Infrastrucrure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        [CustomWriterAuthorizeAttribute]
        public ActionResult Index()
        {
            int writeriId = _sessionControl.GetWriterID();
            if (writeriId == 0)
                return RedirectToAction("Login", "Account");
            var writer = _writerService.GetByID(x => x.ID == writeriId);
            ViewBag.Email = _ancryptionAndDecryption.DecodeData(writer.Email);

            ViewBag.Headings = _headingService.GetAll(x => x.Status == true && x.WriterID == writeriId).Count();
            ViewBag.Contents = _contentService.GetAll(x => x.Status == true && x.WriterID == writeriId).Count();
            return View(writer);
        }

        public ActionResult LastActivity()
        {
            int writeriId = _sessionControl.GetWriterID();
            if (writeriId == 0)
                return RedirectToAction("Login", "Account"); 
            var lastActivity = _contentService.GetAllByHeading(x => x.Status == true && x.WriterID == writeriId).OrderByDescending(x => x.CreatedDate).Take(5);
            return PartialView(lastActivity);
        }

        public ActionResult LastPosts()
        {
            int writeriId = _sessionControl.GetWriterID();
            if (writeriId == 0)
                return RedirectToAction("Login", "Account");
            var lastPosts = _contentService.GetAllByHeading(x => x.Status == true).OrderByDescending(x => x.CreatedDate).Take(5);
            return PartialView(lastPosts);
        }

        [HttpPost]
        [CustomWriterAuthorizeAttribute]
        public ActionResult Edit(EntityLayer.Concrete.Writer writer)
        {
            writer.Password = _ancryptionAndDecryption.DecodeData(writer.Password);
            writer.ConfirmPassword = writer.Password;
            writer.Email = _ancryptionAndDecryption.EncodeData(writer.Email);
            if (writer.imageFile != null)
            {
                UploadImage(writer);
            }
            List<string> errors = new List<string>();

            ValidationResult results = _validator.Validate(writer);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    errors.Add(item.ErrorMessage);
                }
                return Json(JsonConvert.SerializeObject(errors));
            }
            else if (!_ancryptionAndDecryption.DecodeData(writer.Email).Contains("@"))
            {
                errors.Add("Email adresi düzgün daxil edilmeyib.");
                writer.Email = _ancryptionAndDecryption.DecodeData(writer.Email);
                return Json(JsonConvert.SerializeObject(errors));
            }
            else if (!_writerService.IsEmailUnique(writer.Email, writer.ID))
            {
                errors.Add("Email ünvanı istifadə edilib. Fərqli ünvan istifadə edin.");
                writer.Email = _ancryptionAndDecryption.DecodeData(writer.Email);
                return Json(JsonConvert.SerializeObject(errors));
            }
            writer.Password = _ancryptionAndDecryption.EncodeData(writer.Password);
            _writerService.Update(writer, writer.ID);
            return Json(200);
        }


        [HttpPost]
        [CustomWriterAuthorizeAttribute]
        public ActionResult EditPassword(EntityLayer.Concrete.Writer writer)
        {
            writer.Email = _ancryptionAndDecryption.EncodeData(writer.Email);

            List<string> errors = new List<string>();
            var originalData = _writerService.GetByID(x => x.ID == writer.ID);
            if (originalData.Password != _ancryptionAndDecryption.EncodeData(writer.OldPassword))
            {
                errors.Add("Cari şifrə səhv daxil edilib.");
                return Json(JsonConvert.SerializeObject(errors));
            }

            ValidationResult results = _validator.Validate(writer);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    errors.Add(item.ErrorMessage);
                }
                return Json(JsonConvert.SerializeObject(errors));
            }
            else if (!_ancryptionAndDecryption.DecodeData(writer.Email).Contains("@"))
            {
                errors.Add("Email adresi düzgün daxil edilmeyib.");
                writer.Email = _ancryptionAndDecryption.DecodeData(writer.Email);
                return Json(JsonConvert.SerializeObject(errors));
            }
            else if (!_writerService.IsEmailUnique(writer.Email, writer.ID))
            {
                errors.Add("Email ünvanı istifadə edilib. Fərqli ünvan istifadə edin.");
                writer.Email = _ancryptionAndDecryption.DecodeData(writer.Email);
                return Json(JsonConvert.SerializeObject(errors));
            }
            writer.Password = _ancryptionAndDecryption.EncodeData(writer.Password);
            _writerService.Update(writer, writer.ID);
            return Json(200);
        }


        private void UploadImage(EntityLayer.Concrete.Writer writer)
        {
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(writer.imageFile.FileName);
            string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName + extension);
            writer.imageFile.SaveAs(path);
            writer.ImageURL = "/UploadedFiles/" + fileName + extension;
        }

    }
}