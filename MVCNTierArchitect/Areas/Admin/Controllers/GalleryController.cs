using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using MVCNTierArchitect.Infrastrucrure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [CustomAdminAuthorizeAttribute]
    public class GalleryController : Controller
    {
        private readonly IImageFileService _imageFileService;
        private readonly ImageFileValidator _validator;
        public GalleryController(IImageFileService imageFileService)
        {
            _imageFileService = imageFileService;
            _validator = new ImageFileValidator();
        }
        // GET: Admin/Gallery
        public ActionResult Index()
        {
            var files = _imageFileService.GetAll();
            return View(files);
        }

        public ActionResult GetAll()
        {
            var files = _imageFileService.GetAll();
            return View(files);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var image = _imageFileService.GetByID(x => x.ID == id);
            if (image == null)
            {
                return new HttpNotFoundResult();
            }
            _imageFileService.Delete(image);
            if (image.URL != null)
            {
                string fullPath = Request.MapPath(image.URL);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            TempData["DeleteImage"] = "Şəkil baza və serverdən silindi.";
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<HttpPostedFileBase> Images)
        {
            if (Images == null)
            {
                TempData["ErrorImage"] = "Şəkil və ya şəkillər seçilməyib.";
                return View();
            }
            List<ImageFile> files = UploadFiles(Images);
            _imageFileService.Addrange(files);
            return RedirectToAction("GetAll");
        }

        private List<ImageFile> UploadFiles(List<HttpPostedFileBase> Images)
        {
            List<ImageFile> files = new List<ImageFile>();
            int counter = 0;
            foreach (var image in Images)
            {
                if (image.ContentLength > 0)
                {
                    ImageFile file = new ImageFile();
                    string _FileName = Guid.NewGuid().ToString();
                    string _extension = Path.GetExtension(image.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName + _extension);
                    image.SaveAs(_path);
                    file.Title = "Title (" + counter + ")";
                    file.URL = "/UploadedFiles/" + _FileName + _extension;
                    files.Add(file);
                }
                counter++;
            }

            return files;
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var image = _imageFileService.GetByID(x => x.ID == id);
            if (image == null)
            {
                return new HttpNotFoundResult();
            }

            return View(image);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ImageFile imageFile)
        {
            if (imageFile.Image != null)
            {
                if (imageFile.URL != null)
                {
                    string fullPath = Request.MapPath(imageFile.URL);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                string _FileName = Guid.NewGuid().ToString();
                string _extension = Path.GetExtension(imageFile.Image.FileName);
                string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName + _extension);
                imageFile.Image.SaveAs(_path);
                imageFile.URL = "/UploadedFiles/" + _FileName + _extension;
            }
            ValidationResult results = _validator.Validate(imageFile);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(imageFile);
            }

            _imageFileService.Update(imageFile, imageFile.ID);
            TempData["EditImage"] = "Şəkil məlumatları yeniləndi.";
            return RedirectToAction("GetAll");
        }

    }
}