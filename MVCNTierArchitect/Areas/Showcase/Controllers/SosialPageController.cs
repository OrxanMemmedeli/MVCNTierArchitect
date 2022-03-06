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
    public class SosialPageController : Controller
    {
        private readonly IAdressService _adressService;

        public SosialPageController(IAdressService adressService)
        {
            _adressService = adressService;
        }

        // GET: Showcase/SosialPage
        public async Task<ActionResult> Index()
        {
            var url = _adressService.GetLast();

            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync(url.URL + "api/SosialPage");
            var jsonstring = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<SosialPage>(jsonstring);
            return View(values);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SosialPage sosial)
        {
            var url = _adressService.GetLast();

            var httpclient = new HttpClient();
            var jsonSosialPage = JsonConvert.SerializeObject(sosial);
            StringContent content = new StringContent(jsonSosialPage, Encoding.UTF8, "application/json");
            var responseMessage = await httpclient.PostAsync(url.URL + "api/SosialPage", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(sosial);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync(url.URL + "api/SosialPage/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonSosialPage = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<Notification>(jsonSosialPage);
                return View(value);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, SosialPage sosial)
        {
            if (id != sosial.ID)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Uyğunsuz məlumat");
            }
            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var jsonSosial = JsonConvert.SerializeObject(sosial);
            StringContent content = new StringContent(jsonSosial, Encoding.UTF8, "application/json");
            var responseMessage = await httpclient.PutAsync(url.URL + "api/SosialPage/" + sosial.ID, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["EditSosialPage"] = "Məlumat yeniləndi.";
                return RedirectToAction("Index");
            }

            return View(sosial);
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.DeleteAsync(url.URL + "api/SosialPage/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["DeleteSosialPage"] = "Məlumat silindi.";
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}