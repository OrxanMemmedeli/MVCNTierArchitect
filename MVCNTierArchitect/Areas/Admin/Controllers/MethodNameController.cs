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
    public class MethodNameController : Controller
    {
        private readonly IMethodNameService _methodNameService;
        private readonly IRoleService _roleService;
        private readonly IRoleMethodService _roleMethodService;
        private readonly MethodNameValidator _validator;

        public MethodNameController(IMethodNameService methodNameService, IRoleService roleService, IRoleMethodService roleMethodService, MethodNameValidator validator)
        {
            _methodNameService = methodNameService;
            _roleService = roleService;
            _roleMethodService = roleMethodService;
            _validator = validator;
        }



        // GET: Admin/MethodName
        public ActionResult Index()
        {
            var methodName = _methodNameService.GetAll();
            return View(methodName);
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


        [HttpGet]
        public ActionResult Relation(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            RelationViewModel model = new RelationViewModel();
            var roles = _roleService.GetAll();
            var roleMethods = _roleMethodService.GetAll();
            if (roles == null)
            {
                return new HttpNotFoundResult();
            }
            model.Roles = roles;
            model.RoleMethods = roleMethods;
            ViewBag.MethodNameID = id;
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Relation(int methodNameID, int[] RoleID)
        {
            List<RoleMethod> ListAdd = new List<RoleMethod>();
            var roleMethods = _roleMethodService.GetAll(x => x.MethodNameID == methodNameID);
            List<RoleMethod> ListDelete = roleMethods;

            if (RoleID != null)
            {
                foreach (var item in RoleID)
                {
                    var control = roleMethods.FirstOrDefault(x => x.RoleID == item);
                    if (control == null)
                    {
                        ListAdd.Add(new RoleMethod()
                        {
                            MethodNameID = methodNameID,
                            RoleID = item
                        });
                    }

                    ListDelete = ListDelete.Where(x => x.RoleID != item).ToList();
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