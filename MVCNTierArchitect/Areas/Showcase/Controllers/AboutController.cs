using BusinessLayer.Abstract;
using MVCNTierArchitect.Infrastrucrure;
using Newtonsoft.Json;
using ShowcaseAPI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Showcase.Controllers
{
    [Route("Showcase")]
    [CustomAdminAuthorizeAttribute]
    public class AboutController : Controller
    {
        private readonly IAdressService _adressService;

        public AboutController(IAdressService adressService)
        {
            _adressService = adressService;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<About> abouts = null;
            var url = _adressService.GetLast();

            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync(url.URL + "api/About");
            var jsonstring = await responseMessage.Content.ReadAsStringAsync();
            if (jsonstring != "[]")
            {
                var values = JsonConvert.DeserializeObject<List<About>>(jsonstring);
                return View(values);

            }
            return View(abouts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(About about)
        {
            var url = _adressService.GetLast();
            var httpclient = new HttpClient();

            var jsonAbout = JsonConvert.SerializeObject(about);
            StringContent content = new StringContent(jsonAbout, Encoding.UTF8, "application/json");
            var responseMessage = await httpclient.PostAsync(url.URL + "api/About", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(about);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync(url.URL + "api/About/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonAbout = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<About>(jsonAbout);
                return View(value);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, About about)
        {
            if (id != about.ID)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Uyğunsuz məlumat");
            }
            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var jsonAbout = JsonConvert.SerializeObject(about);
            StringContent content = new StringContent(jsonAbout, Encoding.UTF8, "application/json");
            var responseMessage = await httpclient.PutAsync(url.URL + "api/About/" + about.ID, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["EditAbout"] = "Məlumat yeniləndi.";
                return RedirectToAction("Index");
            }

            return View(about);
        }


        [CustomAdminAuthorizeAttribute]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            return Redirect("/Showcase/About/DeleteConfirm/" + id);
        }

        public async Task<ActionResult> DeleteConfirm(int id)
        {
            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.DeleteAsync(url.URL + "api/About/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["DeleteAbout"] = "Məlumat silindi.";
                return RedirectToAction("Index");
            }

            return View();
        }



    }
}