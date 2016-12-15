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
        // GET: Beheer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserLijst()
        {
            UserRepository UserRepository = new UserRepository(new MssqlUserLogic());
            List<User> UserList = UserRepository.ListUsers();
            return View(UserList);
        }
    }

}