using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using MVCNTierArchitect.Models.ViewModels;
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
        private readonly IMethodNameService _methodNameService;
        private readonly IRoleMethodService _roleMethodService;
        private readonly RoleValidator _validator;

        public RoleController(IRoleService roleService, IMethodNameService methodNameService, IRoleMethodService roleMethodService, RoleValidator validator)
        {
            _roleService = roleService;
            _methodNameService = methodNameService;
            _roleMethodService = roleMethodService;
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


        [HttpGet]
        public ActionResult Relation(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            RelationViewModel model = new RelationViewModel();
            var methods = _methodNameService.GetAll();
            var roleMethods = _roleMethodService.GetAll();
            if (methods == null)
            {
                return new HttpNotFoundResult();
            }
            model.MethodNames = methods;
            model.RoleMethods = roleMethods;
            ViewBag.RoleID = id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Relation(int roleID, int[] methodID)
        {
            List<RoleMethod> ListAdd = new List<RoleMethod>();
            var roleMethods = _roleMethodService.GetAll(x => x.RoleID == roleID);
            List<RoleMethod> ListDelete = roleMethods;

            if (methodID != null)
            {
                foreach (var item in methodID)
                {
                    var control = roleMethods.FirstOrDefault(x => x.MethodNameID == item);
                    if (control == null)
                    {
                        ListAdd.Add(new RoleMethod()
                        {
                            MethodNameID = item,
                            RoleID = roleID
                        });
                    }

                    ListDelete = ListDelete.Where(x => x.MethodNameID != item).ToList();
                }
            }

            if (ListAdd.Count() != 0)
            {
                _roleMethodService.AddRange(ListAdd);
            }
            if (ListDelete.Count() != 0)
            {
                _roleMethodService.DeleteRange(ListDelete);
            }
            return RedirectToAction("Index");
        }
    }
}