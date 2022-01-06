using BusinessLayer.Abstract;
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
        private readonly IContactService _contactService;
        private readonly IMessageService _messageService;
        private readonly MessageValidator _validator;

        public ContactController(IContactService contactService, IMessageService messageService)
        {
            _contactService = contactService;
            _messageService = messageService;            
            _validator = new MessageValidator();
        }

        public ActionResult Index()
        {
            var messages = _contactService.GetAll(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.IsResponded);
            return View(messages);
        }

        public PartialViewResult MailLeftMenu()
        {
            var newMessageCount = _contactService.GetAll(x => x.IsResponded == false && x.IsDeleted == false).Count();
            var newSystemMessageCount = _messageService.GetAll(x => x.IsResponded == false && x.IsDeleted == false && x.IsDraft == false).Count();
            var draftMessageCount = _messageService.GetAll(x => x.IsResponded == false && x.IsDeleted == false && x.IsDraft == true).Count();
            //*******************************************************************
            var sentMessageCount = _messageService.GetAll(x => x.SenderEmail == "memmedeli.orxan.om@gmail.com" && x.IsResponded == true && x.IsDraft == false).Count();

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
            var message = _contactService.GetByID(x => x.ID == id);
            if (message == null)
            {
                return new HttpNotFoundResult();
            }
            if (!message.IsReaded)
            {
                message.IsReaded = true;
                _contactService.Update(message, message.ID);
            }
            return View(message);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var message = _contactService.GetByID(x => x.ID == id);
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
                _contactService.Delete(message);
                _messageService.Add(model);
            }
            return RedirectToAction("Index");
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
                _messageService.Add(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var contactMessage = _contactService.GetByID(x => x.ID == id);
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
            message.IsResponded = false;

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

            _messageService.Add(message);

            var contact = _contactService.GetByID(x => x.ID == contactMessage.ID);
            contact.IsResponded = true;
            _contactService.Update(contact, contact.ID);

            return RedirectToAction("Sent", "Message", "Admin");
        }
    }
}