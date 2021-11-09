using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Admin.Controllers
{
    public class WriterController : Controller
    {
        private readonly WriterManager _writerManager;

        public WriterController()
        {
            _writerManager = new WriterManager(new EFWriterRepository());
        }

        public ActionResult Index()
        {
            var writers = _writerManager.GetAll();
            return View(writers);
        }
    }
}