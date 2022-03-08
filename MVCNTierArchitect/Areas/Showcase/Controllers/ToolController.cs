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
    public class ToolController : Controller
    {
        private readonly IAdressService _adressService;

        public ToolController(IAdressService adressService)
        {
            _adressService = adressService;
        }


        public async Task<ActionResult> Index()
        {
            IEnumerable<Tool> tools = null;
            var url = _adressService.GetLast();

            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync(url.URL + "api/Tool");
            var jsonstring = await responseMessage.Content.ReadAsStringAsync();

            if (jsonstring != "[]")
            {
                var values = JsonConvert.DeserializeObject<List<Tool>>(jsonstring);
                return View(values);

            }

            return View(tools);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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


        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.GetAsync(url.URL + "api/Tool/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonstring = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Tool>(jsonstring);

                return View(values);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Tool tool)
        {
            if (id != tool.ID)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Uyğunsuz məlumat");
            }

            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var jsonTool = JsonConvert.SerializeObject(tool);
            StringContent content = new StringContent(jsonTool, Encoding.UTF8, "application/json");
            var responseMessage = await httpclient.PutAsync(url.URL + "api/Tool/" + tool.ID, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["EditTool"] = "Alət yeniləndi.";
                return RedirectToAction("Index");
            }

            return View(tool);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var url = _adressService.GetLast();
            var httpclient = new HttpClient();
            var responseMessage = await httpclient.DeleteAsync(url.URL + "api/Tool/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["DeleteTool"] = "Alət silindi.";
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}