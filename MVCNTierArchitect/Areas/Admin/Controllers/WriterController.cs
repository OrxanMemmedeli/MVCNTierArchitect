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
    public class WriterController : Controller
    {
        private readonly WriterManager _writerManager;
        private readonly WriterValidator _validator;

        public WriterController()
        {
            _writerManager = new WriterManager(new EFWriterRepository());
            _validator = new WriterValidator();
        }

        public ActionResult Index()
        {
            var writers = _writerManager.GetAll();
            return View(writers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Writer writer)
        {
            ValidationResult results = _validator.Validate(writer);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(writer);
            }
            _writerManager.Add(writer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var Writer = _writerManager.GetByID(x => x.ID == id);
            if (Writer == null)
            {
                return new HttpNotFoundResult();
            }

            return View(Writer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Writer writer)
        {
            if ((writer.Password == "Test123456!!" || string.IsNullOrEmpty(writer.Password)) && (writer.ConfirmPassword == "Test123456!!" || string.IsNullOrEmpty(writer.ConfirmPassword)))
            {
                writer.Password = writer.OldPassword;
                writer.ConfirmPassword = writer.OldPassword;
            }

            ValidationResult results = _validator.Validate(writer);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(writer);
            }

            _writerManager.Update(writer);
            return RedirectToAction("Index");
        }
    }
}