using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeCT4RailzMVC.Models;

namespace EyeCT4RailzMVC.Controllers
{
    public class SpoorController : Controller
    {
        // GET: Spoor
        [HttpGet]
#if !DEBUG
        [Authorize(Roles = "Beheerder, Wagenparkbeheerder")]
#endif
        public ActionResult Index()
        {
            SpoorRepository spoorRepository = new SpoorRepository(new MssqlSpoorLogic());

            return View(spoorRepository.ListSporen());
        }

        
        [HttpPost]
        public ActionResult Index(Spoor spoor)
        {
            SpoorRepository spoorRepository = new SpoorRepository(new MssqlSpoorLogic());
            spoorRepository.EditSpoor(spoor);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Spoor spoor)
        {
            return View(spoor);
        }

        [HttpGet]
        public ActionResult TramVerplaatsen(int id)
        {
            TramRepository tramRepository = new TramRepository(new MssqlTramLogic());
            Tram tram = tramRepository.CheckForTramId(id);
            return View(tram);
        }

        [HttpPost]
        public ActionResult TramVerplaatsen(Tram tram, FormCollection form)
        {
            SpoorRepository spoorRepository = new SpoorRepository(new MssqlSpoorLogic());
            Spoor spoor = spoorRepository.CheckForSpoorId(Convert.ToInt32(form["spoorid"]));
            return RedirectToAction("Index");
        }
    }
}