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
        private readonly MessageManager _messageManager;
        public ContactController()
        {
            _contactManager = new ContactManager(new EFContactRepository());
            _messageManager = new MessageManager(new EFMessageRepository());
        }
        public ActionResult Index()
        {
            var messages = _contactManager.GetAll().OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.IsResponded);
            return View(messages);
        }

        public PartialViewResult MailLeftMenu()
        {
            var newMessageCount = _contactManager.GetAll(x => x.IsResponded == false).Count();
            var newSystemMessageCount = _messageManager.GetAll(x => x.IsResponded == false).Count();
            //*******************************************************************
            var sentMessageCount = _messageManager.GetAll(x => x.SenderEmail == "memmedeli.orxan.om@gmail.com").Count();

            ViewData["NewSystemMessageCount"] = newSystemMessageCount;
            ViewData["NewMessageCount"] = newMessageCount;
            ViewData["SentMessageCount"] = sentMessageCount;
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