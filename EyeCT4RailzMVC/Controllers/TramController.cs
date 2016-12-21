﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeCT4RailzMVC.Models;

namespace EyeCT4RailzMVC.Controllers
{
    public class TramController : Controller
    {
        // GET: Tram
        public ActionResult Overzicht()
        {
            TramRepository tramRepository = new TramRepository(new MssqlTramLogic());
            List<Tram> trams = tramRepository.ListTrams();
            return View(trams);
        }
    }
}