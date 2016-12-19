using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeCT4RailzMVC.Models;

namespace EyeCT4RailzMVC.Controllers
{
    public class BeheerController : Controller
    {
        UserRepository userRepository = new UserRepository(new MssqlUserLogic());
        // GET: Beheer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserLijst()
        {
            UserRepository userRepository = new UserRepository(new MssqlUserLogic());
            List<User> userList = userRepository.ListUsers();
            return View(userList);
        }

        [HttpPost]
        public ActionResult CreateUser(FormCollection form)
        {
            //User rol kunnen kiezen nog toevoegen
            string naam = form["Naam"];
            userRepository.AddUser(new User(naam, UserType.Beheerder));
            return RedirectToAction("UserLijst");
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }
    }

}