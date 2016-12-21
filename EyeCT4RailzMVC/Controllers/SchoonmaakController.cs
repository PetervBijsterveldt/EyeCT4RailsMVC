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
        // GET: Schoonmaak
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Overzicht()
        {
            List<SchoonmaakBeurt> schoonmaakBeurten = tramRepository.ListSchoonmaakbeurten(); 
            return View(schoonmaakBeurten);
        }
    }
}