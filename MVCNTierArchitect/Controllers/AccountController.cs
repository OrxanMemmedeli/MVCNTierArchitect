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
    public class AccountController : Controller
    {
        private readonly IAncryptionAndDecryption _ancryptionAndDecryption;
        private readonly IAdminService _adminService;
        private readonly LoginViewModelValidator _validator;

        public AccountController(IAncryptionAndDecryption ancryptionAndDecryption, IAdminService adminService)
        {
            _ancryptionAndDecryption = ancryptionAndDecryption;
            _adminService = adminService;
            _validator = new LoginViewModelValidator();
        }

        // GET: Account
        public ActionResult AdminLogin(string returnURL)
        {
            ViewBag.ReturnURL = returnURL;
            if (User.Identity.IsAuthenticated)
            {
                if (Url.IsLocalUrl(returnURL))
                {
                    return Redirect(returnURL);
                }
                else
                {
                    return Redirect("/Admin");
                }
            }
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

            var admin = _adminService.Get(x => x.UserName == model.UserName && x.Password == password);

            if (admin != null)
            {
                FormsAuthentication.SetAuthCookie(admin.UserName, false);
                Session["AdminUserName"] = admin.UserName;
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
    }
}