using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EyeCT4RailzMVC.Models;

namespace EyeCT4RailzMVC.Controllers
{
    public class UserViewController : Controller
    {
        // GET: User
        public ActionResult UserOverview()
        {
            UserRepository userRepository = new UserRepository(new MssqlUserLogic());
            List<User> users = userRepository.ListUsers();
            return View(users);
        }
    }
}