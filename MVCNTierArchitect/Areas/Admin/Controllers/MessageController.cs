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
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly MessageValidator _validator;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
            _validator = new MessageValidator();
        }

        public ActionResult Index()
        {
            var messages = _messageService.GetAll(x => x.IsDeleted == false && x.IsDraft == false && x.IsResponded == false).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.IsResponded);
            return View(messages);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var message = _messageService.GetByID(x => x.ID == id);
            if (message == null)
            {
                return new HttpNotFoundResult();
            }
            if (!message.IsReaded)
            {
                message.IsReaded = true;
                _messageService.Update(message, message.ID);
            }
            return View(message);
        }

        public ActionResult Deleteds()
        {
            var messages = _messageService.GetAll(x => x.IsDeleted == true).OrderByDescending(x => x.CreatedDate);

            DeleteOldMessage(messages);

            return View(messages);
        }

        private void DeleteOldMessage(IOrderedEnumerable<Message> messages)
        {
            List<Message> models = new List<Message>();

            foreach (var item in messages)
            {
                if (item.DeletedDate.AddDays(30) < DateTime.Now)
                {
                    models.Add(item);
                }
            }
            if (models.Count() != 0)
            {
                _messageService.DeleteAll(models);
            }
        }

        public ActionResult Create(int? id)
        {
            Message message = new Message(); 
            if (id == null)
            {
                return View(message);
            }
            else
            {
                message = _messageService.GetByID(x => x.ID == id);
                if (message == null)
                {
                    return new HttpNotFoundResult();
                }
                return View(message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Message message)
        {
            //*******************************************************************
            message.SenderEmail = "memmedeli.orxan.om@gmail.com";
            message.CreatedDate = DateTime.Now;
            message.IsResponded = true;

            ValidationResult results = _validator.Validate(message);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(message);
            }
            TempData["MessageSent"] = "Mesaj Göndərildi.";

            _messageService.Add(message);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var message = _messageService.GetByID(x => x.ID == id);
            if (message == null)
            {
                return new HttpNotFoundResult();
            }
            if (!message.IsDeleted)
            {
                message.DeletedDate = DateTime.Now;
                message.IsDeleted = true;
                TempData["MailDeleted"] = "Mesaj SİLİNMİŞLƏR qovluğuna daxil ediləcək və 30 gündən sonra həmişəlik silinəcəkdir.";

                _messageService.Update(message, message.ID);
            }
            return RedirectToAction("Index");
        }


        public ActionResult Drafts()
        {
            var drafts = _messageService.GetAll(x => x.IsDraft == true && x.IsDeleted == false).OrderByDescending(x => x.CreatedDate);
            return View(drafts);
        }

        public ActionResult Draft(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var message = _messageService.GetByID(x => x.ID == id);
            if (message == null)
            {
                return new HttpNotFoundResult();
            }
            if (!message.IsDeleted)
            {
                TempData["MailDrafted"] = "Mesaj QARALAMALAR qovluğuna daxil edildi.";
                message.IsDraft = true;
                _messageService.Update(message, message.ID);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Sent()
        {
            var drafts = _messageService.GetAll(x => x.IsDraft == false && x.IsDeleted == false && x.IsResponded == true).OrderByDescending(x => x.CreatedDate);
            return View(drafts);
        }

        public ActionResult Reply(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var contactMessage = _messageService.GetByID(x => x.ID == id);
            contactMessage.IsResponded = true;
            contactMessage.MessageText = "Müraciətiniz üçün təşəkkür edirik";
            contactMessage.Subject = "Admindən cavab";

            return View(contactMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply(Message message)
        {
            //*******************************************************************
            message.SenderEmail = "memmedeli.orxan.om@gmail.com";

            ValidationResult results = _validator.Validate(message);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(message);
            }
            TempData["MessageReply"] = "Mesaj Göndərildi.";
            _messageService.Update(message, message.ID);

            return RedirectToAction("Sent", "Message", "Admin");
        }
    }
}