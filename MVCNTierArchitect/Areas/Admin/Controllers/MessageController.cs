using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
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
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly MessageValidator _validator;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
            _validator = new MessageValidator();
        }

        [CustomAdminAuthorizeAttribute]
        public ActionResult Index()
        {
            var messages = _messageService.GetAll(x => x.IsDeleted == false && x.IsDraft == false && x.IsResponded == false).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.IsResponded);
            return View(messages);
        }

        [CustomAdminAuthorizeAttribute]
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

        [CustomAdminAuthorizeAttribute]
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

        [CustomAdminAuthorizeAttribute]
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
        [CustomAdminAuthorizeAttribute]
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
            return RedirectToAction("Sent", "Message", "Admin");
        }

        [CustomAdminAuthorizeAttribute]
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


        [CustomAdminAuthorizeAttribute]
        public ActionResult Drafts()
        {
            var drafts = _messageService.GetAll(x => x.IsDraft == true && x.IsDeleted == false).OrderByDescending(x => x.CreatedDate);
            return View(drafts);
        }

        [CustomAdminAuthorizeAttribute]
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

        [CustomAdminAuthorizeAttribute]
        public ActionResult Sent()
        {
            var sents = _messageService.GetAll(x => x.IsDraft == false && x.IsDeleted == false && x.IsResponded == true).OrderByDescending(x => x.CreatedDate);
            return View(sents);
        }

        [CustomAdminAuthorizeAttribute]
        public ActionResult Reply(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var message = _messageService.GetByID(x => x.ID == id);
            message.IsResponded = true;
            message.MessageText = message.MessageText == null ? "Müraciətiniz üçün təşəkkür edirik" : message.MessageText;
            message.Subject = message.Subject == null ? "Admindən cavab" : message.Subject;

            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAdminAuthorizeAttribute]
        public ActionResult Reply(Message message)
        {
            //*******************************************************************
            message.SenderEmail = "memmedeli.orxan.om@gmail.com";
            message.IsDraft = false;
            message.CreatedDate = DateTime.Now;
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