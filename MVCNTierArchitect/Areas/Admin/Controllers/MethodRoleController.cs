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
    public class MethodRoleController : Controller
    {
        private readonly IRoleMethodService _roleMethodService;
        private readonly RoleMethodValidator _validator;

        public MethodRoleController(IRoleMethodService roleMethodService, RoleMethodValidator validator)
        {
            _roleMethodService = roleMethodService;
            _validator = validator;
        }

        // GET: Admin/MethodRole
        public ActionResult Index()
        {
            var categories = _roleMethodService.GetAll();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleMethod roleMethod)
        {
            ValidationResult results = _validator.Validate(roleMethod);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(roleMethod);
            }

            _roleMethodService.Add(roleMethod);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var roleMethod = _roleMethodService.GetByID(x => x.ID == id);
            if (roleMethod == null)
            {
                return new HttpNotFoundResult();
            }

            return View(roleMethod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleMethod roleMethod)
        {
            ValidationResult results = _validator.Validate(roleMethod);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(roleMethod);
            }

            _roleMethodService.Update(roleMethod, roleMethod.ID);
            TempData["EditRoleMethod"] = "Məlumat yeniləndi.";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var roleMethod = _roleMethodService.GetByID(x => x.ID == id);
            if (roleMethod == null)
            {
                return new HttpNotFoundResult();
            }

            _roleMethodService.Delete(roleMethod);
            TempData["DeleteRoleMethod"] = "Məlumat silindi.";
            return RedirectToAction("Index");
        }
    }
}