using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using MVCNTierArchitect.Infrastrucrure;
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
        private readonly IHeadingService _headingService;
        private readonly IContentService _contentService;
        private readonly IContactService _contactService;
        private readonly IWriterService _writerService;
        private readonly ContactValidator _validator;

        public DefaultController(IAdressService adressService, IHeadingService headingService, IContentService contentService, IContactService contactService, IWriterService writerService)
        {
            _adressService = adressService;
            _headingService = headingService;
            _contentService = contentService;
            _contactService = contactService;
            _writerService = writerService;
            _validator = new ContactValidator();
        }


        // GET: Default    
        public ActionResult Index()
        {
            ViewBag.Url = _adressService.GetLast().URL;
            return View();
        }


        public PartialViewResult ToolsPartial()
        {

            var jsonstring = Task.Run(async () => await CallAPI("api/Tool"));

            if (jsonstring.Result != "[]")
            {
                var values = JsonConvert.DeserializeObject<List<Tool>>(jsonstring.Result);
                return PartialView(values);
            }
            return PartialView();
        }

        public PartialViewResult DevelopmentPartial()
        {

            var jsonstring = Task.Run(async () => await CallAPI("api/Development"));
            
            if (jsonstring.Result != "[]")
            {
                var values = JsonConvert.DeserializeObject<List<Development>>(jsonstring.Result);
                return PartialView(values);
            }
            return PartialView();
        }

        public PartialViewResult GaleryPartial()
        {

            var jsonstring = Task.Run(async () => await CallAPI("api/Image"));

            if (jsonstring.Result != "[]")
            {
                var values = JsonConvert.DeserializeObject<List<Image>>(jsonstring.Result);
                return PartialView(values);
            }
            return PartialView();
        }

        public PartialViewResult CounterPartial()
        {
            ViewBag.Headings = _headingService.GetAll(x => x.Status == true).Count();
            ViewBag.Contents = _contentService.GetAll(x => x.Status == true).Count();
            ViewBag.Contacts = _contactService.GetAll(x => x.IsDeleted == false).Count();
            ViewBag.Writers = _writerService.GetAll(x => x.Status == true).Count();
            return PartialView();
        }

        public PartialViewResult NotificationPartial()
        {

            var jsonstring = Task.Run(async () => await CallAPI("api/Notification"));

            if (jsonstring.Result != "[]")
            {
                var values = JsonConvert.DeserializeObject<List<Notification>>(jsonstring.Result);
                return PartialView(values);
            }
            return PartialView();
        }

        public PartialViewResult MessagePartial()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Message(Contact contact)
        {
            contact.CreatedDate = DateTime.Now;
            List<string> errors = new List<string>();
            ValidationResult results = _validator.Validate(contact);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    errors.Add(item.ErrorMessage);
                }

                return Json(new { alert = JsonConvert.SerializeObject(errors) });
            }

            _contactService.Add(contact);
            //return Json(new { alert = "XƏTA. Mesaj göndərilmədi" });            
            return Json(new { alert = JsonConvert.SerializeObject("Mesaj göndərildi") });
        }

        public PartialViewResult ContactPartial()
        {

            var jsonstring = Task.Run(async () => await CallAPI("api/About"));

            if (jsonstring.Result != "[]")
            {
                var values = JsonConvert.DeserializeObject<List<ShowcaseAPI.Models.Entity.About>>(jsonstring.Result);
                return PartialView(values);
            }
            return PartialView();
        }

        public PartialViewResult SosialPartial()
        {

            var jsonstring = Task.Run(async () => await CallAPI("api/SosialPage"));

            if (jsonstring.Result != "[]")
            {
                var values = JsonConvert.DeserializeObject<List<SosialPage>>(jsonstring.Result);
                return PartialView(values);
            }
            return PartialView();
        }

        private async Task<string> CallAPI(string adress)
        {
            var url = _adressService.GetLast();

            var httpClient = new HttpClient();
            HttpResponseMessage responseMessage = await httpClient.GetAsync(url.URL + adress);
            string jsonstring = await responseMessage.Content.ReadAsStringAsync();
            return jsonstring;
        }
    }
}