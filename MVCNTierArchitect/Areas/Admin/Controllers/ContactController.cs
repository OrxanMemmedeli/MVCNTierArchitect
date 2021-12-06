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

        public PartialViewResult MailLeftMenu()
        {
            var NewMessageCount = _contactManager.GetAll(x => x.IsResponded == false).Count();
            ViewBag.NewMessageCount = NewMessageCount;
            return PartialView();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var message = _contactManager.GetByID(x => x.ID == id);
            if (message == null)
            {
                return new HttpNotFoundResult();
            }
            if (!message.IsReaded)
            {
                message.IsReaded = true;
                _contactManager.Update(message);
            }
            return View(message);
        }
    }
}