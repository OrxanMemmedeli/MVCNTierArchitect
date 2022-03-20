using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using MVCNTierArchitect.Infrastrucrure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [CustomAdminAuthorizeAttribute]
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

  
    }
}