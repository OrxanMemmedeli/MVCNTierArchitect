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

namespace MVCNTierArchitect.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        private readonly IAdressService _adressService;

        public DefaultController(IAdressService adressService)
        {
            _adressService = adressService;
        }
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Tools()
        {
            var url = _adressService.GetLast();

            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync(url.URL + "api/Tool");
            var jsonstring = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.SerializeObject(jsonstring);
            return Json(values);

        }
    }
}