using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using MVCNTierArchitect.Infrastrucrure;
using MVCNTierArchitect.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [CustomAdminAuthorizeAttribute]
    public class ControllerNameController : Controller
    {
        private readonly IControllerNameService _controllerNameService;
        private readonly IRoleService _roleService;
        private readonly IRoleControllerNameService _roleControllerNameService;
        private readonly ControllerNameValidator _validator;

        public ControllerNameController(IControllerNameService controllerNameService, IRoleService roleService, IRoleControllerNameService roleControllerNameService, ControllerNameValidator validator)
        {
            _controllerNameService = controllerNameService;
            _roleService = roleService;
            _roleControllerNameService = roleControllerNameService;
            _validator = validator;
        }

        // GET: Admin/ControllerName
        // GET: Admin/MethodName
        public ActionResult Index()
        {
            var controllerNames = _controllerNameService.GetAll().OrderBy(x => x.Name);
            return View(controllerNames);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ControllerName controllerName)
        {
            ValidationResult results = _validator.Validate(controllerName);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(controllerName);
            }

            _controllerNameService.Add(controllerName);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var controllerName = _controllerNameService.GetByID(x => x.ID == id);
            if (controllerName == null)
            {
                return new HttpNotFoundResult();
            }

            return View(controllerName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ControllerName controllerName)
        {
            ValidationResult results = _validator.Validate(controllerName);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(controllerName);
            }

            _controllerNameService.Update(controllerName, controllerName.ID);
            TempData["EditControllerName"] = "Controller yeniləndi.";
            return RedirectToAction("Index");
        }


        [CustomAdminAuthorizeAttribute]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            return Redirect("/admin/ControllerName/DeleteConfirm/" + id);
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            var controllerName = _controllerNameService.GetByID(x => x.ID == id);
            if (controllerName == null)
            {
                return new HttpNotFoundResult();
            }
            var controllerNames = _roleControllerNameService.GetAll(x => x.ControllerNameID == controllerName.ID);

            _controllerNameService.Delete(controllerName);
            _roleControllerNameService.DeleteRange(controllerNames);

            TempData["DeleteControllerName"] = "Controller silindi.";
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
            var roleControllerNames = _roleControllerNameService.GetAll();
            if (roles == null)
            {
                return new HttpNotFoundResult();
            }
            model.Roles = roles;
            model.RoleControllerNames = roleControllerNames;
            ViewBag.ControllerNameID = id;
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Relation(int controllerNameID, int[] RoleID)
        {
            List<RoleControllerName> ListAdd = new List<RoleControllerName>();
            var roleControllerNames = _roleControllerNameService.GetAll(x => x.ControllerNameID == controllerNameID);
            List<RoleControllerName> ListDelete = roleControllerNames;

            if (RoleID != null)
            {
                foreach (var item in RoleID)
                {
                    var control = roleControllerNames.FirstOrDefault(x => x.RoleID == item);
                    if (control == null)
                    {
                        ListAdd.Add(new RoleControllerName()
                        {
                            ControllerNameID = controllerNameID,
                            RoleID = item
                        });
                    }

                    ListDelete = ListDelete.Where(x => x.RoleID != item).ToList();
                }
            }

            if (ListAdd.Count() != 0)
            {
                _roleControllerNameService.AddRange(ListAdd);
            }
            if (ListDelete.Count() != 0)
            {
                _roleControllerNameService.DeleteRange(ListDelete);
            }
            return RedirectToAction("Index");
        }
    }
}