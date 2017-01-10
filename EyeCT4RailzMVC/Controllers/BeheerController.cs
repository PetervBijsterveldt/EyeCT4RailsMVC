using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
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

        public ActionResult RemoveUser(string naam)
        {
            userRepository.RemoveUser(naam);
            return RedirectToAction("UserLijst");
        }


        [HttpPost]
        public ActionResult EditUser(User original, User edit)
        {
            //User rol kunnen kiezen nog toevoegen
            // PrincipalContext insPrincipalContext = new PrincipalContext(ContextType.Domain, "EyeCT4Railz",
            //  "DC=EyeCT4Railz dc=local");
            // PrincipalSearcher insPrincipalSearcher = new PrincipalSearcher();
            // UserPrincipal up = new UserPrincipal(insPrincipalContext);
            // up.Name = original.Naam;
            //insPrincipalSearcher.QueryFilter = up;
            // Principal p = insPrincipalSearcher.FindOne();
            // UserPrincipal userPrincipal =(UserPrincipal) p;

            // userPrincipal.Name = edit.Naam;

            return RedirectToAction("UserLijst");
        }



        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }
    }

}