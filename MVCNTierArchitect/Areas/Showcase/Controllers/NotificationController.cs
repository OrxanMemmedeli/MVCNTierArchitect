using BusinessLayer.Abstract;
using Newtonsoft.Json;
using ShowcaseAPI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Showcase.Controllers
{
    [RouteArea("Admin")]
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
            var url = _adressService.GetAll().OrderByDescending(x => x.ID).First();

            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync(url + "api/Notification");
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Notification>>(jsonString);

            return View(values);
        }
    }
}