using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeCT4RailzMVC.Models;
using System.Text.RegularExpressions;

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
        [Authorize(Roles = @"Beheerders")]
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
        public ActionResult EditUser(int id)
        {
            //User rol kunnen kiezen nog toevoegen

            User user = userRepository.ListUsers().Single(User => User.ID == id);
            return View(user);
        }


        [HttpPost]
        public ActionResult EditUser(User user)
        {
            //User rol kunnen kiezen nog toevoegen
            
            userRepository.EditUser(user);
            return RedirectToAction("UserLijst");
        }



        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }
    }

}