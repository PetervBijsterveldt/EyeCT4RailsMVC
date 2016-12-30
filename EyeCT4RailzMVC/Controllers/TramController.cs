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
        private TramRepository tramRepository = new TramRepository(new MssqlTramLogic());

        // GET: Tram
        [HttpGet]
        public ActionResult Overzicht()
        {
            List<Tram> trams = tramRepository.ListTrams();
            return View(trams);
        }

        [HttpGet]
        public ActionResult Trammerino(Tram tram)
        {
            return View(tram);
        }

        // Select Tram
        [HttpPost]
        public ActionResult Overzicht(Tram tram)
        {
            return RedirectToAction("Trammerino", tram);
        }

        [HttpPost]
        public ActionResult Edit(Tram tram)
        {
            tramRepository.EditTram(tram);
            return RedirectToAction("Overzicht");
        }
    }
}