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
        private TramRepository tramRepository = new TramRepository(new MssqlTramLogic());
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
                spoorRepository.AddSectoren(oudeSpoor, hoeveelheid);
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
            List<Spoor> sporen = spoorRepository.ListSporen();

            try
            {
                Spoor spoor = sporen.Find(x => x.Nummer == int.Parse(form["nummer"]));
                Spoor oudespoor = tramRepository.CheckForTramOnSpoor(tram);

                if (spoor.Lengte - tram.Lengte > 0)
                {
                    tramRepository.Uitrijden(oudespoor, tram);
                    tramRepository.Inrijden(spoor, tram);
                }
                else
                {
                    TempData["InrijError"] = "Er is geen plek op deze spoor!";
                }
            }
            catch (Exception)
            {
                TempData["InrijError"] = "Dit spoor bestaat niet!";
            }


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
            Spoor addedSpoor = spoorRepository.ListSporen().Find(x => x.Nummer == spoor.Nummer);

            spoorRepository.SpoorSectoren(spoor, addedSpoor.ID);
            return RedirectToAction("Index");
        }
    }
}