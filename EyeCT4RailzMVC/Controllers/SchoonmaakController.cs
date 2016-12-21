using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeCT4RailzMVC.Models;

namespace EyeCT4RailzMVC.Controllers
{
    public class SchoonmaakController : Controller
    {
        TramRepository tramRepository = new TramRepository(new MssqlTramLogic());
        UserRepository userRepository = new UserRepository(new MssqlUserLogic());
        // GET: Schoonmaak
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Schoonmaakoverzicht()
        {
            List<SchoonmaakBeurt> schoonmaakBeurten = tramRepository.ListSchoonmaakbeurten(); 
            return View(schoonmaakBeurten);
        }

        [HttpGet]
        public ActionResult AddSchoonmaakbeurt()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSchoonmaakbeurt(FormCollection form)
        {
            string naam = form["Medewerkernaam"];
            int medewerkerid = userRepository.UserIDByName(naam);
            int tramid = Convert.ToInt32(form["TramId"]);
            DateTime startdatum = DateTime.Now;
            DateTime einddatum = Convert.ToDateTime(form["EindDatum"]);
            SchoonmaakType type = (SchoonmaakType) Enum.Parse(typeof(SchoonmaakType), form["Type"]);
            SchoonmaakBeurt beurt = new SchoonmaakBeurt(medewerkerid, tramid, startdatum, einddatum, type);
            tramRepository.AddSchoonmaakbeurt(beurt);
            return RedirectToAction("Schoonmaakoverzicht");
        }

        public ActionResult BeëindigSchoonmaak()
        {
            return RedirectToAction("Schoonmaakoverzicht");
        }
    }
}