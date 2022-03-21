using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
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
    public class RoleControllerNameController : Controller
    {
        private readonly IRoleControllerNameService _roleControllerNameService;

        public RoleControllerNameController(IRoleControllerNameService roleControllerNameService)
        {
            _roleControllerNameService = roleControllerNameService;
        }

        // GET: Admin/RoleControllerName
        public ActionResult Index()
        {
            var roleControllerNames = _roleControllerNameService.GetAll();
            return View(roleControllerNames);
        }
    }
}