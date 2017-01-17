using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Util;
using EyeCT4RailzMVC.Models;

namespace EyeCT4RailzMVC.Controllers
{
#if !DEBUG
        [Authorize(Roles = "Bestuurders")]
#endif
    public class BestuurderController : Controller
    {
        TramRepository tramRepository = new TramRepository(new MssqlTramLogic());
        SpoorRepository spoorRepository = new SpoorRepository(new MssqlSpoorLogic());

        [HttpGet]
        public ActionResult BestuurdersOverzicht()
        {
            Tram tram = tramRepository.CheckForTramId(3);

            Spoor spoor = tramRepository.CheckForTramOnSpoor(tram);

            Tuple<Tram, Spoor> data = new Tuple<Tram, Spoor>(tram, spoor);
            return View(data);
        }

        [HttpGet]
        public ActionResult Uitrijden(int spoorid, int tramid)
        {
            Tram tram = tramRepository.CheckForTramId(tramid);
            Spoor spoor = spoorRepository.CheckForSpoorId(spoorid);
            tramRepository.Uitrijden(spoor, tram);
            return RedirectToAction("BestuurdersOverzicht");
        }

        [HttpGet]
        public ActionResult Inrijden(Tram tram)
        {
            Spoor spoor = KiesBesteSpoor(tram);

            if (spoor.ID != 0)
            {
                tram.Status = TramStatus.Remise;
                tramRepository.EditTram(tram);
                tramRepository.Inrijden(spoor, tram);
            }
            else
            {
                TempData["InrijError"] = "Er is geen plek voor deze tram op de remise!";
            }

            return RedirectToAction("BestuurdersOverzicht");

        }

        private Spoor KiesBesteSpoor(Tram tram)
        {
            List<Spoor> sporen = spoorRepository.ListSporen();

            sporen.Sort((spoorA, spoorB) => (spoorA.Lengte - tram.Lengte).CompareTo(spoorB.Lengte - tram.Lengte));

            sporen.RemoveAll(spoor => spoor.RestererendeLengte - tram.Lengte < 0);

            if (sporen[0] == null)
            {
                return new Spoor();
            }

            return sporen[0];
        }
    }
}