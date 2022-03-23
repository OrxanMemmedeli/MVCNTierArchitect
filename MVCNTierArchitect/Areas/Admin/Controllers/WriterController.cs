using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using MVCNTierArchitect.Infrastrucrure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tools.Abstract;

namespace MVCNTierArchitect.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [CustomAdminAuthorizeAttribute]
    public class WriterController : Controller
    {
        private readonly IAncryptionAndDecryption _ancryptionAndDecryption;
        private readonly IWriterService _writerService;
        private readonly WriterValidator _validator;
        private readonly IRoleService _roleService;
        public WriterController(IAncryptionAndDecryption ancryptionAndDecryption, IWriterService writerService, IRoleService roleService)
        {
            _ancryptionAndDecryption = ancryptionAndDecryption;
            _writerService = writerService;
            _validator = new WriterValidator(_writerService);
            _roleService = roleService;
        }

        public ActionResult Index()
        {
            var writers = _writerService.GetAll();
            return View(writers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.RoleID = GetRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EntityLayer.Concrete.Writer writer)
        {
            writer.Status = true;
            writer.Email = _ancryptionAndDecryption.EncodeData(writer.Email);
            if (writer.imageFile != null)
            {
                UploadImage(writer);
            }
            ValidationResult results = _validator.Validate(writer);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                ViewBag.RoleID = GetRoles();
                return View(writer);
            }
            else if (!_ancryptionAndDecryption.DecodeData(writer.Email).Contains("@"))
            {
                ModelState.AddModelError("Email", "Email adresi düzgün daxil edilmeyib.");
                writer.Email = _ancryptionAndDecryption.DecodeData(writer.Email);
                ViewBag.RoleID = GetRoles();
                return View(writer);
            }
            else if (!_writerService.IsEmailUnique(writer.Email, null))
            {
                ModelState.AddModelError("Email", "Email ünvanı istifadə edilib. Fərqli ünvan istifadə edin.");
                writer.Email = _ancryptionAndDecryption.DecodeData(writer.Email);
                ViewBag.RoleID = GetRoles();
                return View(writer);
            }
            writer.Password = _ancryptionAndDecryption.EncodeData(writer.Password);

            _writerService.Add(writer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var Writer = _writerService.GetByID(x => x.ID == id);
            if (Writer == null)
            {
                return new HttpNotFoundResult();
            }
            ViewBag.RoleID = GetRoles();
            return View(Writer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EntityLayer.Concrete.Writer writer)
        {
            if ((writer.Password == "Test123456!!" || string.IsNullOrEmpty(writer.Password)) && (writer.ConfirmPassword == "Test123456!!" || string.IsNullOrEmpty(writer.ConfirmPassword)))
            {
                writer.Password = writer.OldPassword;
                writer.ConfirmPassword = writer.OldPassword;

                writer.Password = _ancryptionAndDecryption.DecodeData(writer.Password);
                writer.ConfirmPassword = writer.Password;
            }


            writer.Email = _ancryptionAndDecryption.EncodeData(writer.Email);

            if (writer.imageFile != null)
            {
                UploadImage(writer);
            }

            ValidationResult results = _validator.Validate(writer);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                ViewBag.RoleID = GetRoles();
                return View(writer);
            }
            else if (!_ancryptionAndDecryption.DecodeData(writer.Email).Contains("@"))
            {
                ModelState.AddModelError("Email", "Email adresi düzgün daxil edilmeyib.");
                writer.Email = _ancryptionAndDecryption.DecodeData(writer.Email);
                ViewBag.RoleID = GetRoles();
                return View(writer);
            }
            else if (!_writerService.IsEmailUnique(writer.Email, writer.ID))
            {
                ModelState.AddModelError("Email", "Email ünvanı istifadə edilib. Fərqli ünvan istifadə edin.");
                writer.Email = _ancryptionAndDecryption.DecodeData(writer.Email);
                ViewBag.RoleID = GetRoles();
                return View(writer);
            }

            writer.Password = _ancryptionAndDecryption.EncodeData(writer.Password);
            _writerService.Update(writer, writer.ID);
            TempData["EditWriter"] = "Admin yeniləndi.";
            return RedirectToAction("Index");
        }

        private void UploadImage(EntityLayer.Concrete.Writer writer)
        {
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(writer.imageFile.FileName);
            string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName + extension);
            writer.imageFile.SaveAs(path);
            writer.ImageURL = "/UploadedFiles/" + fileName + extension;
        }

        private List<SelectListItem> GetRoles()
        {
            return (from c in _roleService.GetAll()
                    select new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.ID.ToString()
                    }).ToList();
        }



        [CustomAdminAuthorizeAttribute]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            return Redirect("/admin/Writer/DeleteConfirm/" + id);
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            var writer = _writerService.GetByID(x => x.ID == id);
            if (writer == null)
            {
                return new HttpNotFoundResult();
            }

            _writerService.Delete(writer);
            TempData["DeleteWriter"] = "Yazar silindi.";
            return RedirectToAction("Index");
        }
    }

}
