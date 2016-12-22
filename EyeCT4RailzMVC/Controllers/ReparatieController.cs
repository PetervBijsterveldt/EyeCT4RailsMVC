using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeCT4RailzMVC.Models;

namespace EyeCT4RailzMVC.Controllers
{
    public class ReparatieController : Controller
    {
        TramRepository tramRepository = new TramRepository(new MssqlTramLogic());
        // GET: Reparatie
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reparatieoverzicht()
        {
            List<ReparatieBeurt> reparatieBeurten = tramRepository.ListReparatiebeurten(0);
            return View(reparatieBeurten);
        }

        public ActionResult HistoryReparatieoverzicht()
        {
            List<ReparatieBeurt> reparatieBeurten = tramRepository.ListReparatiebeurten(1);
            return View(reparatieBeurten);
        }

        [HttpGet]
        public ActionResult AddReparatie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddReparatie(FormCollection form)
        {
            ReparatieBeurt beurt = new ReparatieBeurt();
            beurt.Medewerkernaam = form["Medewerkernaam"];
            beurt.TramId = Convert.ToInt32(form["TramId"]);
            beurt.EindDatum = Convert.ToDateTime(form["EindDatum"]);
            beurt.ReparatiebeurtType = (ReparatiebeurtType)Enum.Parse(typeof(ReparatiebeurtType), form["ReparatiebeurtType"]);

            tramRepository.AddReparatiebeurt(beurt);
            return RedirectToAction("Reparatieoverzicht");
        }

        public ActionResult BeëindigReparatie(int reparatieid)
        {
            return RedirectToAction("Reparatieoverzicht");
        }
    }
}