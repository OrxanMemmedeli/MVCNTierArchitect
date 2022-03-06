using BusinessLayer.Abstract;
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
    public class DevelopmentController : Controller
    {
        private readonly IAdressService _adressService;

        public DevelopmentController(IAdressService adressService)
        {
            _adressService = adressService;
        }
        // GET: Showcase/Development
        public async Task<ActionResult> Index()
        {
            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync(url.URL + "api/Development");
            var jsonstring = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Development>(jsonstring);

            return View(values);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Development development)
        {
            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var jsonDevelopment = JsonConvert.SerializeObject(development);
            StringContent content = new StringContent(jsonDevelopment, Encoding.UTF8, "application/json");
            var responseMessage = await httpclient.PostAsync(url.URL + "api/Development", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(development);

        }


        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync(url.URL + "api/Development/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonstring = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Development>(jsonstring);
                return View(values);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Development development)
        {
            if (id != development.ID)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Uyğunsuz məlumat");
            }
            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var jsonDevelopment = JsonConvert.SerializeObject(development);
            StringContent content = new StringContent(jsonDevelopment, Encoding.UTF8, "application/json");
            var responseMessage = await httpclient.PutAsync(url.URL + "api/Development/" + development.ID, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["EditDevelopment"] = "Məlumat yeniləndi.";
                return RedirectToAction("Index");
            }
            return View(development);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.DeleteAsync(url.URL + "api/Development/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["DeleteDevelopment"] = "Məlumat silindi.";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}