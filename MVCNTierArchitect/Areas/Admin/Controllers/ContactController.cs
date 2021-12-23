using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
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
        private readonly MessageValidator _validator;

        public ContactController()
        {
            _contactManager = new ContactManager(new EFContactRepository());
            _messageManager = new MessageManager(new EFMessageRepository());
            _validator = new MessageValidator();

        }
        public ActionResult Index()
        {
            var messages = _contactManager.GetAll(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.IsResponded);
            return View(messages);
        }

        public PartialViewResult MailLeftMenu()
        {
            var newMessageCount = _contactManager.GetAll(x => x.IsResponded == false && x.IsDeleted == false).Count();
            var newSystemMessageCount = _messageManager.GetAll(x => x.IsResponded == false && x.IsDeleted == false && x.IsDraft == false).Count();
            var draftMessageCount = _messageManager.GetAll(x => x.IsResponded == false && x.IsDeleted == false && x.IsDraft == true).Count();
            //*******************************************************************
            var sentMessageCount = _messageManager.GetAll(x => x.SenderEmail == "memmedeli.orxan.om@gmail.com" && x.IsResponded == true).Count();

            ViewData["NewSystemMessageCount"] = newSystemMessageCount;
            ViewData["NewMessageCount"] = newMessageCount;
            ViewData["SentMessageCount"] = sentMessageCount;
            ViewData["DraftMessageCount"] = draftMessageCount;
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
                message.DeletedDate = DateTime.Now;
                message.IsDeleted = true;
                TempData["MailDeleted"] = "Mesaj SİLİNMİŞLƏR qovluğuna daxil ediləcək və 30 gündən sonra həmişəlik silinəcəkdir.";

                Message model = message;
                //*******************************************************************
                model.SenderEmail = "memmedeli.orxan.om@gmail.com";
                _contactManager.Delete(message);
                _messageManager.Add(model);
            }
            return RedirectToAction("Index");
        }

        public PartialViewResult Draft(int id, Contact contact)
        {
            return PartialView(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Draft(Contact contact)
        {
            if (contact == null)
            {
                return new HttpNotFoundResult();
            }

            if (!contact.IsDeleted)
            {
                TempData["ContactDrafted"] = "Mesaj QARALAMALAR qovluğuna daxil edildi.";
                Message model = contact;

                //*******************************************************************
                model.SenderEmail = "memmedeli.orxan.om@gmail.com";
                model.IsDraft = true;
                _messageManager.Add(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var contactMessage = _contactManager.GetByID(x => x.ID == id);
            contactMessage.IsResponded = true;
            contactMessage.Message = "Müraciətiniz üçün təşəkkür edirik";
            contactMessage.Subject = "Admindən cavab";
            return View(contactMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact contactMessage)
        {
            Message message = new Message();

            message = contactMessage;
            //*******************************************************************
            message.SenderEmail = "memmedeli.orxan.om@gmail.com";
            message.CreatedDate = DateTime.Now;
            message.ContactID = contactMessage.ID;

            ValidationResult results = _validator.Validate(message);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(contactMessage);
            }
            TempData["ContactSent"] = "Mesaj Göndərildi.";

            _messageManager.Add(message);
            return RedirectToAction("Sent", "Message", "Admin");
        }
    }
}