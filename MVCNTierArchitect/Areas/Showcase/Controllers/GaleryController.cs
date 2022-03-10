using BusinessLayer.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ShowcaseAPI.Models.Entity;
using System.Text;
using System.Net;
using System.IO;

namespace MVCNTierArchitect.Areas.Showcase.Controllers
{
    [Route("Showcase")]
    public class GaleryController : Controller
    {
        private readonly IAdressService _adressService;

        public GaleryController(IAdressService adressService)
        {
            _adressService = adressService;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<Image> images = null;

            var url = _adressService.GetLast();

            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync(url.URL + "api/Image");
            var jsonstring = await responseMessage.Content.ReadAsStringAsync();
            if (jsonstring != "[]")
            {
                var values = JsonConvert.DeserializeObject<List<Image>>(jsonstring);
                return View(values);
            }
            return View(images);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Image image, HttpPostedFileBase imageFile)
        {
            if (imageFile == null)
            {
                TempData["ErrorImageShowcase"] = "Şəkil seçilməyib.";
                return View(image);
            }

            image = UploadFiles(image, imageFile);

            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var jsonImage = JsonConvert.SerializeObject(image);
            StringContent content = new StringContent(jsonImage, Encoding.UTF8, "application/json");
            var responseMessage = await httpclient.PostAsync(url.URL + "api/Image", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(image);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync(url.URL + "api/Image/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonImage = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<Image>(jsonImage);
                return View(value);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Image image, HttpPostedFileBase imageFile)
        {
            if (id != image.ID)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Uyğunsuz məlumat");
            }

            if (imageFile != null)
            {
                image = UploadFiles(image, imageFile);
            }

            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var jsonImage = JsonConvert.SerializeObject(image);
            StringContent content = new StringContent(jsonImage, Encoding.UTF8, "application/json");
            var responseMessage = await httpclient.PutAsync(url.URL + "api/Image/" + image.ID, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["EditImage"] = "Məlumat yeniləndi.";
                return RedirectToAction("Index");
            }

            return View(image);
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.DeleteAsync(url.URL + "api/Image/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["DeleteImage"] = "Məlumat silindi.";
                return RedirectToAction("Index");
            }

            return View();
        }

        private Image UploadFiles(Image image, HttpPostedFileBase imageFile)
        {
            string _FileName = Guid.NewGuid().ToString();
            string _extension = Path.GetExtension(imageFile.FileName);
            string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName + _extension);
            imageFile.SaveAs(_path);

            image.URL = "/UploadedFiles/" + _FileName + _extension;

            return image;
        }
    }
}