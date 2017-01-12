using System;
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
#if !DEBUG
        [Authorize(Roles = "Beheerder, Wagenparkbeheerder")]
#endif
        [HttpGet]
        public ActionResult Overzicht()
        {
            try
            {
                List<Tram> trams = tramRepository.ListTrams();
                return View(trams);
            }
            catch (Exception)
            {
                return View("Index", "Error");
            }
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

        [HttpGet]
        public ActionResult AddTram()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTram(Tram tram)
        {
            tramRepository.AddTram(tram);
            return RedirectToAction("Overzicht");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;

            var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");

            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(model)
            };
        }
    }
}