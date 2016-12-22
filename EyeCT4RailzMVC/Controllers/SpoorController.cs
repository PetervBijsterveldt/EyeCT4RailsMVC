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
        public ActionResult Index()
        {
            SpoorRepository spoorRepository = new SpoorRepository(new MssqlSpoorLogic());

            return View(spoorRepository.ListSporen());
        }
    }
}