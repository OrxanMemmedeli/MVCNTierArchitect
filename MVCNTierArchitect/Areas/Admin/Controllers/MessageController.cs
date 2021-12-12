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
    public class MessageController : Controller
    {
        private readonly MessageManager _messageManager;
        private readonly MessageValidator _validator;
        public MessageController()
        {
            _messageManager = new MessageManager(new EFMessageRepository());
            _validator = new MessageValidator();
        }

        public ActionResult Index()
        {
            var messages = _messageManager.GetAll(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.IsResponded);
            return View(messages);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var message = _messageManager.GetByID(x => x.ID == id);
            if (message == null)
            {
                return new HttpNotFoundResult();
            }
            if (!message.IsReaded)
            {
                message.IsReaded = true;
                _messageManager.Update(message);
            }
            return View(message);
        }

        public ActionResult Deleteds()
        {
            var messages = _messageManager.GetAll(x => x.IsDeleted == true).OrderByDescending(x => x.CreatedDate);

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
                _messageManager.DeleteAll(models);
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
                message = _messageManager.GetByID(x => x.ID == id);
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
            ValidationResult results = _validator.Validate(message);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(message);
            }

            _messageManager.Add(message);
            return RedirectToAction("Index");
        }
    }
}