using BusinessLayer.Abstract;
using Newtonsoft.Json;
using ShowcaseAPI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}