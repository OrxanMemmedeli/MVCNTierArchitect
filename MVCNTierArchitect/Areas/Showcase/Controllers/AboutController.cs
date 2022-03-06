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
    public class AboutController : Controller
    {
        private readonly IAdressService _adressService;

        public AboutController(IAdressService adressService)
        {
            _adressService = adressService;
        }

        public async Task<ActionResult> Index()
        {
            var url = _adressService.GetLast();

            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync(url.URL + "api/About");
            var jsonstring = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<About>(jsonstring);

            return View(values);
        }

        public ActionResult Create()
        {
            return View();
        }

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
    }
}