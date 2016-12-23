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

        [HttpGet]
        public ActionResult Schoonmaakoverzicht()
        {
            List<SchoonmaakBeurt> schoonmaakBeurten = tramRepository.ListSchoonmaakbeurten(0); 
            return View(schoonmaakBeurten);
        }

        public ActionResult HistorySchoonmaakoverzicht()
        {
            List<SchoonmaakBeurt> schoonmaakBeurten = tramRepository.ListSchoonmaakbeurten(1);
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
            SchoonmaakBeurt beurt = new SchoonmaakBeurt();
            beurt.Medewerkernaam = form["Medewerker"];
            beurt.TramId = Convert.ToInt32(form["TramId"]);
            beurt.EindDatum = Convert.ToDateTime(form["EindDatum"]);
            beurt.Type = (SchoonmaakType) Enum.Parse(typeof(SchoonmaakType), form["Type"]);
            
            tramRepository.AddSchoonmaakbeurt(beurt);
            return RedirectToAction("Schoonmaakoverzicht");
        }

        
        public ActionResult BeëindigSchoonmaak(int schoonmaakid)
        {
            tramRepository.EditOnderhoud(schoonmaakid);

            return RedirectToAction("Schoonmaakoverzicht");
        }
    }
}