using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EyeCT4RailzMVC.Controllers
{
    public class BestuurderController : Controller
    {
        // GET: Bestuurder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BestuurdersOverzicht()
        {
            return View();
        }
    }
}