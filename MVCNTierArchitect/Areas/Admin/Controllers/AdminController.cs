using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tools.Abstract;

namespace MVCNTierArchitect.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IAncryptionAndDecryption _ancryptionAndDecryption;
        private readonly AdminValidator _validator;
        public AdminController(IAdminService adminService, IAncryptionAndDecryption ancryptionAndDecryption)
        {
            _adminService = adminService;
            _ancryptionAndDecryption = ancryptionAndDecryption;
            _validator = new AdminValidator(_adminService);
        }

        // GET: Admin/Admin
        public ActionResult Index()
        {
            var admins = _adminService.GetAll();
            return View(admins);
        }

        public ActionResult Create()
        {
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
                return View(admin);
            }

            admin.Password = _ancryptionAndDecryption.EncodeData(admin.Password);
            _adminService.Add(admin);
            return RedirectToAction("Index");
        }
    }
}