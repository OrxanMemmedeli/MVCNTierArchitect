using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
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
            var messages = _contactManager.GetAll(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.IsResponded);
            return View(messages);
        }

        public PartialViewResult MailLeftMenu()
        {
            var newMessageCount = _contactManager.GetAll(x => x.IsResponded == false && x.IsDeleted == false).Count();
            var newSystemMessageCount = _messageManager.GetAll(x => x.IsResponded == false && x.IsDeleted == false).Count();
            //*******************************************************************
            var sentMessageCount = _messageManager.GetAll(x => x.SenderEmail == "memmedeli.orxan.om@gmail.com" && x.IsResponded == true).Count();

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

        public ActionResult Delete(int? id)
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
            if (!message.IsDeleted)
            {
                message.IsDeleted = true;
                TempData["MailDeleted"] = "Mesaj SİLİNMİŞLƏR qovluğuna daxil edildi. Mesaj 30 gündən sonra həmişəlik silinəcəkdir.";

                Message model = message;
                //*******************************************************************
                model.SenderEmail = "memmedeli.orxan.om@gmail.com";
                _contactManager.Delete(message);
                _messageManager.Add(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Draft(int? id)
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
            if (!message.IsDeleted)
            {
                TempData["MailDrafted"] = "Mesaj QARALAMALAR qovluğuna daxil edildi.";
                Message model = message;

                //*******************************************************************
                model.SenderEmail = "memmedeli.orxan.om@gmail.com";
                model.IsDraft = true;
                _messageManager.Add(model);
            }
            return RedirectToAction("Index");
        }
    }
}