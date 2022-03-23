using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using FluentValidation.Results;
using MVCNTierArchitect.Infrastrucrure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tools.Abstract;

namespace MVCNTierArchitect.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [CustomAdminAuthorizeAttribute]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IAncryptionAndDecryption _ancryptionAndDecryption;
        private readonly AdminValidator _validator;
        private readonly IRoleService _roleService;

        public AdminController(IAdminService adminService, IAncryptionAndDecryption ancryptionAndDecryption, AdminValidator validator, IRoleService roleService)
        {
            _adminService = adminService;
            _ancryptionAndDecryption = ancryptionAndDecryption;
            _validator = validator;
            _roleService = roleService;
        }

        // GET: Admin/Admin
        public ActionResult Index()
        {
            var admins = _adminService.GetAll();
            return View(admins);
        }

        public ActionResult Create()
        {
            ViewBag.RoleID = GetRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EntityLayer.Concrete.Admin admin)
        {
            ValidationResult results = _validator.Validate(admin);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                ViewBag.RoleID = GetRoles(); 
                return View(admin);
            }
            if (!_adminService.IsUserNameUnique(_ancryptionAndDecryption.EncodeData(admin.UserName)))
            {
                ViewBag.Unique =  "User adı təkrar edilə bilməz.";
                ViewBag.RoleID = GetRoles(); 
                return View(admin);
            }
            admin.Password = _ancryptionAndDecryption.EncodeData(admin.Password);
            admin.UserName = _ancryptionAndDecryption.EncodeData(admin.UserName);
            _adminService.Add(admin);
            return RedirectToAction("Index");

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


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var admin = _adminService.GetByID(x => x.ID == id);
            if (admin == null)
            {
                return new HttpNotFoundResult();
            }
            ViewBag.RoleID = GetRoles();
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EntityLayer.Concrete.Admin admin)
        {
            ValidationResult results = _validator.Validate(admin);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                ViewBag.RoleID = GetRoles();
                admin.Password = _ancryptionAndDecryption.EncodeData(admin.Password);
                admin.UserName = _ancryptionAndDecryption.EncodeData(admin.UserName);
                return View(admin);
            }

            admin.Password = _ancryptionAndDecryption.EncodeData(admin.Password);
            admin.UserName = _ancryptionAndDecryption.EncodeData(admin.UserName);

            if (!_adminService.IsUserNameUnique(admin.UserName, admin.ID))
            {
                ViewBag.Unique = "User adı təkrar edilə bilməz.";
                ViewBag.RoleID = GetRoles(); 
                return View(admin);
            }
            TempData["EditAdmin"] = "Admin yeniləndi.";
            _adminService.Update(admin, admin.ID);
            return RedirectToAction("Index");
        }

        [CustomAdminAuthorizeAttribute]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            return Redirect("/admin/Admin/DeleteConfirm/" + id);
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            var admin = _adminService.GetByID(x => x.ID == id);
            if (admin == null)
            {
                return new HttpNotFoundResult();
            }

            _adminService.Delete(admin);
            TempData["DeleteAdmin"] = "Admin silindi.";
            return RedirectToAction("Index");
        }
    }
}