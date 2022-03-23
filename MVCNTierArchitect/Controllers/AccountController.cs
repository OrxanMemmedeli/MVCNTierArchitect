using BusinessLayer.Abstract;
using FluentValidation.Results;
using MVCNTierArchitect.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tools.Abstract;

namespace MVCNTierArchitect.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAncryptionAndDecryption _ancryptionAndDecryption;
        private readonly IAdminService _adminService;
        private readonly IWriterService _writerService;
        private readonly LoginViewModelValidator _validator;
        private readonly WriterLoginViewModelValidator _writerValidator;


        public AccountController(IAncryptionAndDecryption ancryptionAndDecryption, IAdminService adminService, IWriterService writerService)
        {
            _ancryptionAndDecryption = ancryptionAndDecryption;
            _adminService = adminService;
            _writerService = writerService;
            _validator = new LoginViewModelValidator();
            _writerValidator = new WriterLoginViewModelValidator();
        }

        // GET: Account
        public ActionResult AdminLogin(string returnURL)
        {
            ViewBag.ReturnURL = returnURL;
            //if (User.Identity.IsAuthenticated)
            //{
            //    if (Url.IsLocalUrl(returnURL))
            //    {
            //        return Redirect(returnURL);
            //    }
            //    else
            //    {
            //        return Redirect("/Admin");
            //    }
            //}
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(LoginViewModel model, string returnURL)
        {
            ValidationResult results = _validator.Validate(model);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(model);
            }

            var password = _ancryptionAndDecryption.EncodeData(model.Password);
            var userName = _ancryptionAndDecryption.EncodeData(model.UserName);

            var admin = _adminService.Get(x => x.UserName == userName && x.Password == password);

            if (admin != null)
            {
                FormsAuthentication.SetAuthCookie(admin.UserName, false);
                Session["AdminUserName"] = _ancryptionAndDecryption.DecodeData(admin.UserName);
                Session["WriterEmail"] = null;
                if (!string.IsNullOrEmpty(returnURL))
                {
                    return Redirect(returnURL);
                }
                else
                {
                    return Redirect("/Admin");
                }
            }
            ViewBag.LoginMessage = "Uğursuz əməliyat";
            return View(model);

        }


        public ActionResult Login(string returnURL)
        {
            ViewBag.ReturnURL = returnURL;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(WriterLoginViewModel model, string returnURL)
        {
            ValidationResult results = _writerValidator.Validate(model);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(model);
            }

            var password = _ancryptionAndDecryption.EncodeData(model.Password);
            var email = _ancryptionAndDecryption.EncodeData(model.Email);

            var writer = _writerService.Get(x => x.Email == email && x.Password == password);

            if (writer != null)
            {
                FormsAuthentication.SetAuthCookie(writer.Email, false);
                Session["WriterEmail"] = _ancryptionAndDecryption.DecodeData(writer.Email);
                Session["WriterImage"] = writer.ImageURL;
                Session["AdminUserName"] = null;
                if (!string.IsNullOrEmpty(returnURL))
                {
                    return Redirect(returnURL);
                }
                else
                {
                    return Redirect("/Writer/Writer");
                }
            }
            ViewBag.WriterLoginMessage = "Uğursuz əməliyat";
            return View(model);

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }

        public ActionResult Denied()
        {
            return View();
        }

    }
}