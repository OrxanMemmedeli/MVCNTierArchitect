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
    [RouteArea("Showcase")]
    public class NotificationController : Controller
    {
        private readonly IAdressService _adressService;

        public NotificationController(IAdressService adressService)
        {
            _adressService = adressService;
        }


        // GET: Showcase/Notification
        public async Task<ActionResult> Index()
        {
            var url = _adressService.GetLast();

            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync(url.URL + "api/Notification");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Notification>>(jsonString);

            return View(values);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Notification notification)
        {
            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var jsonNotification = JsonConvert.SerializeObject(notification);
            StringContent content = new StringContent(jsonNotification, Encoding.UTF8, "application/json");
            var responseMessage = await httpclient.PostAsync(url.URL + "api/Notification", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(notification);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync(url.URL + "api/Notification/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonNotification = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<Notification>(jsonNotification);
                return View(value);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Notification notification)
        {
            if (id != notification.ID)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Uyğunsuz məlumat");
            }
            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var jsonNotification = JsonConvert.SerializeObject(notification);
            StringContent content = new StringContent(jsonNotification, Encoding.UTF8, "application/json");
            var responseMessage = await httpclient.PutAsync(url.URL + "api/Notification/" + notification.ID, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["EditNotification"] = "Bildiriş yeniləndi.";
                return RedirectToAction("Index");
            }

            return View(notification);
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult(); 
            }

            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.DeleteAsync(url.URL + "api/Notification/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {            
                TempData["DeleteNotification"] = "Bildiriş silindi.";
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
