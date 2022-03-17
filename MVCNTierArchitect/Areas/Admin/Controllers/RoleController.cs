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
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly RoleValidator _validator;

        public RoleController(IRoleService roleService, RoleValidator validator)
        {
            _roleService = roleService;
            _validator = validator;
        }

        // GET: Admin/Role
        public ActionResult Index()
        {
            var categories = _roleService.GetAll();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role role)
        {
            ValidationResult results = _validator.Validate(role);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(role);
            }

            _roleService.Add(role);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var role = _roleService.GetByID(x => x.ID == id);
            if (role == null)
            {
                return new HttpNotFoundResult();
            }

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role role)
        {
            ValidationResult results = _validator.Validate(role);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(role);
            }

            _roleService.Update(role, role.ID);
            TempData["EditRole"] = "Rol yeniləndi.";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var role = _roleService.GetByID(x => x.ID == id);
            if (role == null)
            {
                return new HttpNotFoundResult();
            }

            _roleService.Delete(role);
            TempData["DeleteRole"] = "Rol silindi.";
            return RedirectToAction("Index");
        }
    }
}