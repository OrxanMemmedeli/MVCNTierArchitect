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

        public MethodRoleController(IRoleMethodService roleMethodService)
        {
            _roleMethodService = roleMethodService;
        }

        // GET: Admin/MethodRole
        public ActionResult Index()
        {
            var roleMethods = _roleMethodService.GetAll();
            return View(roleMethods);
        }

  
    }
}