﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNTierArchitect.Areas.Showcase.Controllers
{
    public class GaleryController : Controller
    {
        // GET: Showcase/Galery
        public ActionResult Index()
        {
            return View();
        }
    }
}