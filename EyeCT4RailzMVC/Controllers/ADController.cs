using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActiveDirectoryTests.Controllers
{
    public class AdController : Controller
    {
        // GET: AD
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string naam = form["naam"];
            string email = form["email"];
            string password = form["password"];

            using (var pc = new PrincipalContext(ContextType.Domain))
            {
                using (var up = new UserPrincipal(pc))
                {
                    up.SamAccountName = naam;
                    up.EmailAddress = email;
                    up.SetPassword(password);
                    up.Enabled = true;
                    up.ExpirePasswordNow();
                    up.Save();
                }
            }

            return RedirectToAction("Index");
        }
    }
}