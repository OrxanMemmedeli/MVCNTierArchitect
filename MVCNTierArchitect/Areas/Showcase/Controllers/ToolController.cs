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
    public class ToolController : Controller
    {
        private readonly IAdressService _adressService;

        public ToolController(IAdressService adressService)
        {
            _adressService = adressService;
        }


        public async Task<ActionResult> Index()
        {
            var url = _adressService.GetLast();

            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync(url.URL + "api/Tool");
            var jsonstring = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Tool>(jsonstring);

            return View(values);
        }

        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Create(Tool tool)
        {
            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var jsonTool = JsonConvert.SerializeObject(tool);
            StringContent content = new StringContent(jsonTool, Encoding.UTF8, "application/json");
            var responseMessage = await httpclient.PostAsync(url.URL + "api/Tool", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(tool);

        }
    }
}