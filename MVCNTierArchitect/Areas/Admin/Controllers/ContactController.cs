using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class ContactController : Controller
    {
        private readonly ContactManager _contactManager;
        public ContactController()
        {
            _contactManager = new ContactManager(new EFContactRepository());
        }
        public ActionResult Index()
        {
            var messages = _contactManager.GetAll(x => x.IsResponded == false).OrderByDescending(x => x.CreatedDate);
            return View(messages);
        }
    }
}