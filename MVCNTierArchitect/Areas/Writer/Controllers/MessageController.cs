using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Writer.Controllers
{
    [RouteArea("Writer")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly MessageValidator _validator;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
            _validator = new MessageValidator();
        }

        public PartialViewResult MailLeftMenu()
        {
            var messages = _messageService.GetAll();

            var newSystemMessageCount = messages.Where(x => x.IsResponded == false && x.IsDeleted == false && x.IsDraft == false && x.ReceiverEmail == "memmedeli.orxan.om@gmail.com").Count();
           
            var readedMessageCount = messages.Where(x => x.IsReaded == true && x.IsDeleted == false && x.IsResponded == false && x.IsDraft == false && x.ReceiverEmail == "memmedeli.orxan.om@gmail.com").Count();

            var draftMessages = messages.Where(x => x.IsResponded == false && x.IsDeleted == false && x.IsDraft == true);
            var draftMessageCount = draftMessages.Where(x => x.SenderEmail == "memmedeli.orxan.om@gmail.com" && x.ReceiverEmail != "memmedeli.orxan.om@gmail.com").Count() +
                draftMessages.Where(x => x.SenderEmail != "memmedeli.orxan.om@gmail.com" && x.ReceiverEmail == "memmedeli.orxan.om@gmail.com").Count();

            var deletedMessages = messages.Where(x => x.IsReaded == true && x.IsDeleted == false && x.IsResponded == false && x.IsDraft == false);
            var deletedMessageCount = deletedMessages.Where(x => x.SenderEmail == "memmedeli.orxan.om@gmail.com" && x.ReceiverEmail != "memmedeli.orxan.om@gmail.com").Count() +
                deletedMessages.Where(x => x.SenderEmail != "memmedeli.orxan.om@gmail.com" && x.ReceiverEmail == "memmedeli.orxan.om@gmail.com").Count();

            //*******************************************************************
            var sentMessageCount = messages.Where(x => x.SenderEmail == "memmedeli.orxan.om@gmail.com" && x.IsResponded == true && x.IsDraft == false && x.IsDeleted == false).Count();

            ViewData["WNewSystemMessageCount"] = newSystemMessageCount;
            ViewData["WSentMessageCount"] = sentMessageCount;
            ViewData["WDraftMessageCount"] = draftMessageCount;
            ViewData["WReadedMessageCount"] = readedMessageCount;
            ViewData["WDeletedMessageCount"] = deletedMessageCount;
            return PartialView();
        }

        // GET: Writer/Message
        public ActionResult Index()
        {
            var messages = _messageService.GetAll(x => x.IsDeleted == false && x.IsDraft == false && x.IsResponded == false && x.ReceiverEmail == "memmedeli.orxan.om@gmail.com").OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.IsResponded);
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
            var writerMessages = messages.Where(x => x.ReceiverEmail == "memmedeli.orxan.om@gmail.com" || x.SenderEmail == "memmedeli.orxan.om@gmail.com").OrderByDescending(x => x.CreatedDate);
            DeleteOldMessage(writerMessages);

            return View(writerMessages);
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
            TempData["WMessageSent"] = "Mesaj Göndərildi.";

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
                TempData["WMailDeleted"] = "Mesaj SİLİNMİŞLƏR qovluğuna daxil ediləcək və 30 gündən sonra həmişəlik silinəcəkdir.";

                _messageService.Update(message, message.ID);
            }
            return RedirectToAction("Index");
        }


        public ActionResult Drafts()
        {
            var drafts = _messageService.GetAll(x => x.IsDraft == true && x.IsDeleted == false).OrderByDescending(x => x.CreatedDate);
            var writerDrafts = drafts.Where(x => x.SenderEmail == "memmedeli.orxan.om@gmail.com" || x.ReceiverEmail == "memmedeli.orxan.om@gmail.com");
            return View(writerDrafts);
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
                TempData["WMailDrafted"] = "Mesaj QARALAMALAR qovluğuna daxil edildi.";
                message.IsDraft = true;
                _messageService.Update(message, message.ID);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Sent()
        {
            var sents = _messageService.GetAll(x => x.IsDraft == false && x.IsDeleted == false && x.IsResponded == true && x.SenderEmail == "memmedeli.orxan.om@gmail.com").OrderByDescending(x => x.CreatedDate);
            return View(sents);
        }

        public ActionResult Reply(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var message = _messageService.GetByID(x => x.ID == id);
            message.IsResponded = true;
            message.MessageText = message.MessageText == null ? "Təşəkkür edirəm." : message.MessageText;
            message.Subject = message.Subject == null ? "Yazardan cavab" : message.Subject;

            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            TempData["WMessageReply"] = "Mesaj Göndərildi.";
            _messageService.Update(message, message.ID);

            return RedirectToAction("Sent", "Message", "Admin");
        }

    }


}