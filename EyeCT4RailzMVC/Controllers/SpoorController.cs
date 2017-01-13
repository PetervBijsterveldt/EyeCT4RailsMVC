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
        private SpoorRepository spoorRepository = new SpoorRepository(new MssqlSpoorLogic());
        // GET: Spoor
        [HttpGet]
#if !DEBUG
        [Authorize(Roles = "Beheerder, Wagenparkbeheerder")]
#endif
        public ActionResult Index()
        {
            return View(spoorRepository.ListSporen());
        }

        
        [HttpPost]
        public ActionResult Index(Spoor spoor)
        {
            Spoor oudeSpoor = spoorRepository.CheckForSpoor(spoor);
            int difference = spoor.Lengte - oudeSpoor.Lengte;
            int hoeveelheid = Math.Abs(difference);
            if (difference > 0)
            {
                //sectoren komen erbij
                spoorRepository.AddSectoren(spoor, hoeveelheid);
            }
            else if (difference < 0)
            {
                //sectoren gaan eraf
                spoorRepository.RemoveSectoren(oudeSpoor, hoeveelheid);
            }
            spoorRepository.EditSpoor(spoor);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Spoor spoor)
        {
            return View(spoor);
        }

        public ActionResult Remove(Spoor spoor)
        {
            spoorRepository.RemoveAllSectoren(spoor);
            spoorRepository.RemoveSpoor(spoor);
            return RedirectToAction("Index");
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
            Spoor spoor = spoorRepository.CheckForSpoorId(Convert.ToInt32(form["spoorid"]));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddSpoor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSpoor(Spoor spoor)
        {
            spoorRepository.AddSpoor(spoor);
            Spoor AddedSpoor = spoorRepository.ListSporen().Find(x => x.Nummer == spoor.Nummer);
            
            spoorRepository.SpoorSectoren(spoor, AddedSpoor.ID);
            return RedirectToAction("Index");
        }
    }
}