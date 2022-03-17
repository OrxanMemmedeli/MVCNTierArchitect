using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
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
    public class MethodNameController : Controller
    {
        private readonly IMethodNameService _methodNameService;
        private readonly MethodNameValidator _validator;

        public MethodNameController(IMethodNameService methodNameService, MethodNameValidator validator)
        {
            _methodNameService = methodNameService;
            _validator = validator;
        }

        // GET: Admin/MethodName
        public ActionResult Index()
        {
            var categories = _methodNameService.GetAll();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MethodName methodName)
        {
            ValidationResult results = _validator.Validate(methodName);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(methodName);
            }

            _methodNameService.Add(methodName);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var methodName = _methodNameService.GetByID(x => x.ID == id);
            if (methodName == null)
            {
                return new HttpNotFoundResult();
            }

            return View(methodName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MethodName methodName)
        {
            ValidationResult results = _validator.Validate(methodName);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(methodName);
            }

            _methodNameService.Update(methodName, methodName.ID);
            TempData["EditMethodName"] = "Metod yeniləndi.";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var methodName = _methodNameService.GetByID(x => x.ID == id);
            if (methodName == null)
            {
                return new HttpNotFoundResult();
            }

            _methodNameService.Delete(methodName);
            TempData["DeleteMethodName"] = "Metod silindi.";
            return RedirectToAction("Index");
        }
    }
}