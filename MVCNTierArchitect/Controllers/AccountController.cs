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

        public AccountController(IAncryptionAndDecryption ancryptionAndDecryption)
        {
            _ancryptionAndDecryption = ancryptionAndDecryption;
        }

        // GET: Account
        public ActionResult Login(string returnURL)
        {
            ViewBag.ReturnURL = returnURL;
            if (Url.IsLocalUrl(returnURL))
            {
                return Redirect(returnURL);
            }
            else
            {
                return Redirect("/Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnURL)
        {
            var password = _ancryptionAndDecryption.EncodeData(model.Password);
            return Redirect("/Admin");

        }
    }
}