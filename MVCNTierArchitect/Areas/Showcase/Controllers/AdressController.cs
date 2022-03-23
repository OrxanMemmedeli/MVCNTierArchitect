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

namespace MVCNTierArchitect.Areas.Showcase.Controllers
{
    [Route("Showcase")]
    [CustomAdminAuthorizeAttribute]
    public class AdressController : Controller
    {
        private readonly IAdressService _adressService;
        private readonly AdressValidator _validator;

        public AdressController(IAdressService adressService)
        {
            _adressService = adressService;
            _validator = new AdressValidator();

        }

        public ActionResult Index()
        {
            var adresses = _adressService.GetAll();
            return View(adresses);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Adress adress)
        {
            ValidationResult results = _validator.Validate(adress);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(adress);
            }

            _adressService.Add(adress);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var adress = _adressService.GetByID(x => x.ID == id);
            if (adress == null)
            {
                return new HttpNotFoundResult();
            }

            return View(adress);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Adress adress)
        {
            ValidationResult results = _validator.Validate(adress);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(adress);
            }

            _adressService.Update(adress, adress.ID);
            TempData["EditAdress"] = "Ünvan yeniləndi.";
            return RedirectToAction("Index");
        }


        [CustomAdminAuthorizeAttribute]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            return Redirect("/Showcase/Adress/DeleteConfirm/" + id);
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            var adress = _adressService.GetByID(x => x.ID == id);
            if (adress == null)
            {
                return new HttpNotFoundResult();
            }

            _adressService.Delete(adress);
            TempData["DeleteAdress"] = "Ünvan silindi.";
            return RedirectToAction("Index");
        }
    }
}