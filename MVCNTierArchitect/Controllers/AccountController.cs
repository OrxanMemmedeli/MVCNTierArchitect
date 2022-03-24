using BusinessLayer.Abstract;
using FluentValidation.Results;
using MVCNTierArchitect.Models;
using MVCNTierArchitect.Models.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly GoogleConfigModel _googleConfigModel;

        public AccountController(IAncryptionAndDecryption ancryptionAndDecryption, IAdminService adminService, IWriterService writerService, GoogleConfigModel googleConfigModel)
        {
            _ancryptionAndDecryption = ancryptionAndDecryption;
            _adminService = adminService;
            _writerService = writerService;
            _validator = new LoginViewModelValidator();
            _writerValidator = new WriterLoginViewModelValidator();
            _googleConfigModel = googleConfigModel;
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
            ViewBag.Key = _googleConfigModel.Key;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(LoginViewModel model, string returnURL)
        {
            ViewBag.ReturnURL = returnURL;
            ViewBag.Key = _googleConfigModel.Key;

            var status = GoogleReCaptchaControl(model);
            if (!status)
            {
                ViewBag.LoginMessage = "Google reCaptcha doğrulaması uğursuz oldu";
                return View(model);
            }

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
            ViewBag.Key = _googleConfigModel.Key;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(WriterLoginViewModel model, string returnURL)
        {
            ViewBag.ReturnURL = returnURL;
            ViewBag.Key = _googleConfigModel.Key;

            var status = GoogleReCaptchaControl(model);
            if (!status)
            {
                ViewBag.WriterLoginMessage = "Google reCaptcha doğrulaması uğursuz oldu";
                return View(model);
            }

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

        private bool GoogleReCaptchaControl(WriterLoginViewModel model)
        {
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", _googleConfigModel.Secret, model.Captcha));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            return status;
        }
        private bool GoogleReCaptchaControl(LoginViewModel model)
        {
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", _googleConfigModel.Secret, model.Captcha));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            return status;
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