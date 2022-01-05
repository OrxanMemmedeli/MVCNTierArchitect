using BusinessLayer.Abstract;
using MVCNTierArchitect.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tools.Abstract;

namespace MVCNTierArchitect.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAncryptionAndDecryption _ancryptionAndDecryption;
        private readonly IAdminService _adminService;

        public AccountController(IAncryptionAndDecryption ancryptionAndDecryption, IAdminService adminService)
        {
            _ancryptionAndDecryption = ancryptionAndDecryption;
            _adminService = adminService;
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
            var password = _ancryptionAndDecryption.EncodeData(model.Password);
            return Redirect("/Admin");

        }
    }
}