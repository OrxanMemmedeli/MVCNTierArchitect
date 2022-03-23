using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using MVCNTierArchitect.Infrastrucrure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tools.Abstract;

namespace MVCNTierArchitect.Areas.Writer.Controllers
{
    [RouteArea("Writer")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly MessageValidator _validator;
        private readonly IAncryptionAndDecryption _ancryptionAndDecryption;

        public MessageController(IMessageService messageService, IAncryptionAndDecryption ancryptionAndDecryption)
        {
            _messageService = messageService;
            _validator = new MessageValidator();
            _ancryptionAndDecryption = ancryptionAndDecryption;
        }

        public PartialViewResult MailLeftMenu()
        {
            var writer = _ancryptionAndDecryption.EncodeData(Session["WriterEmail"] == null ? "" : Session["WriterEmail"].ToString());

            var messages = _messageService.GetAll();

            var newSystemMessageCount = messages.Where(x => x.IsResponded == false && x.IsDeleted == false && x.IsDraft == false && x.ReceiverEmail == writer).Count();
           
            var readedMessageCount = messages.Where(x => x.IsReaded == true && x.IsDeleted == false && x.IsResponded == false && x.IsDraft == false && x.ReceiverEmail == writer).Count();

            var draftMessages = messages.Where(x => x.IsResponded == false && x.IsDeleted == false && x.IsDraft == true);
            var draftMessageCount = draftMessages.Where(x => x.SenderEmail == writer && x.ReceiverEmail != writer).Count() +
                draftMessages.Where(x => x.SenderEmail != writer && x.ReceiverEmail == writer).Count();

            var deletedMessages = messages.Where(x => x.IsDeleted == false);
            var deletedMessageCount = deletedMessages.Where(x => x.SenderEmail == writer && x.ReceiverEmail != writer).Count() +
                deletedMessages.Where(x => x.SenderEmail != writer && x.ReceiverEmail == writer).Count();

            var sentMessageCount = messages.Where(x => x.SenderEmail == writer && x.IsResponded == true && x.IsDraft == false && x.IsDeleted == false).Count();

            ViewData["WNewSystemMessageCount"] = newSystemMessageCount;
            ViewData["WSentMessageCount"] = sentMessageCount;
            ViewData["WDraftMessageCount"] = draftMessageCount;
            ViewData["WReadedMessageCount"] = readedMessageCount;
            ViewData["WDeletedMessageCount"] = deletedMessageCount;
            return PartialView();
        }

        // GET: Writer/Message
        [CustomWriterAuthorizeAttribute]
        public ActionResult Index()
        {
            var writer = _ancryptionAndDecryption.EncodeData(Session["WriterEmail"] == null ? "" : Session["WriterEmail"].ToString());
            if (string.IsNullOrEmpty(writer))
                return RedirectToAction("Login", "Account");
            var messages = _messageService.GetAll(x => x.IsDeleted == false && x.IsDraft == false && x.IsResponded == false && x.ReceiverEmail == writer).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.IsResponded);
            return View(messages);
        }
        [CustomWriterAuthorizeAttribute]
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
        [CustomWriterAuthorizeAttribute]
        public ActionResult Deleteds()
        {
            var writer = _ancryptionAndDecryption.EncodeData(Session["WriterEmail"] == null ? "" : Session["WriterEmail"].ToString());
            if (string.IsNullOrEmpty(writer))
                return RedirectToAction("Login", "Account");
            var messages = _messageService.GetAll(x => x.IsDeleted == true).OrderByDescending(x => x.CreatedDate);
            var writerMessages = messages.Where(x => x.ReceiverEmail == writer || x.SenderEmail == writer).OrderByDescending(x => x.CreatedDate);
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
        [CustomWriterAuthorizeAttribute]
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
        [CustomWriterAuthorizeAttribute]
        public ActionResult Create(Message message)
        {
            message.SenderEmail = _ancryptionAndDecryption.EncodeData(Session["WriterEmail"] == null ? "" : Session["WriterEmail"].ToString());
            if (string.IsNullOrEmpty(message.SenderEmail))
                return RedirectToAction("Login", "Account");
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
            return RedirectToAction("Sent", "Message", "Writer");
        }
        [CustomWriterAuthorizeAttribute]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            return Redirect("/Writer/Message/DeleteConfirm/" + id);
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
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

        [CustomWriterAuthorizeAttribute]
        public ActionResult Drafts()
        {
            var writer = _ancryptionAndDecryption.EncodeData(Session["WriterEmail"] == null ? "" : Session["WriterEmail"].ToString());
            if (string.IsNullOrEmpty(writer))
                return RedirectToAction("Login", "Account");

            var drafts = _messageService.GetAll(x => x.IsDraft == true && x.IsDeleted == false).OrderByDescending(x => x.CreatedDate);
            var writerDrafts = drafts.Where(x => x.SenderEmail == writer || x.ReceiverEmail == writer);
            return View(writerDrafts);
        }
        [CustomWriterAuthorizeAttribute]
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
                message.IsResponded = false;
                _messageService.Update(message, message.ID);
            }
            return RedirectToAction("Index");
        }
        [CustomWriterAuthorizeAttribute]
        public ActionResult Sent()
        {
            var writer = _ancryptionAndDecryption.EncodeData(Session["WriterEmail"] == null ? "" : Session["WriterEmail"].ToString());
            if (string.IsNullOrEmpty(writer))
                return RedirectToAction("Login", "Account");

            var sents = _messageService.GetAll(x => x.IsDraft == false && x.IsDeleted == false && x.IsResponded == true && x.SenderEmail == writer).OrderByDescending(x => x.CreatedDate);
            return View(sents);
        }
        [CustomWriterAuthorizeAttribute]
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
        [CustomWriterAuthorizeAttribute]
        public ActionResult Reply(Message message)
        {
            message.SenderEmail = _ancryptionAndDecryption.EncodeData(Session["WriterEmail"] == null ? "" : Session["WriterEmail"].ToString());
            if (string.IsNullOrEmpty(message.SenderEmail))
                return RedirectToAction("Login", "Account");
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

            return RedirectToAction("Sent", "Message", "Writer");
        }

    }


}