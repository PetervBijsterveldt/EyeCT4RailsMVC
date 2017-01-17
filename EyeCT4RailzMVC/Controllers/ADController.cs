using System;
using System.DirectoryServices.AccountManagement;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;

namespace EyeCT4RailzMVC.Controllers
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
            string memberof = form["email"];
            string password = form["password"];


            using (var pc = new PrincipalContext(ContextType.Domain))
            {
                using (var up = new UserPrincipal(pc))
                {
                    up.SamAccountName = naam;
                    up.SetPassword(password);
                    up.Enabled = true;
            
                    up.ExpirePasswordNow();
                    up.Save();
                }
            }


            PrincipalContext principalContext = new PrincipalContext(ContextType.Domain);
            UserPrincipal userPrincipal = new UserPrincipal(principalContext);
            GroupPrincipal groupPrincipal = new GroupPrincipal(principalContext);
            userPrincipal.Name = naam;
            groupPrincipal.Members.Add(userPrincipal);
            

            return RedirectToAction("Index");
        }

       /* public ActionResult LogOut()
        {
            HttpCookie cookie = Request.Cookies["TSWA-Last-User"];

            if (User.Identity.IsAuthenticated == false || cookie == null || StringComparer.OrdinalIgnoreCase.Equals(User.Identity.Name, cookie.Value))
            {
                string name = string.Empty;

                if (Request.IsAuthenticated)
                {
                    name = User.Identity.Name;
                }

                cookie = new HttpCookie("TSWA-Last-User", name);
                Response.Cookies.Set(cookie);

                Response.AppendHeader("Connection", "close");
                Response.StatusCode = 401; // Unauthorized;
                Response.Clear();
                //should probably do a redirect here to the unauthorized/failed login page
                //if you know how to do this, please tap it on the comments below
                Response.Write("Unauthorized. Reload the page to try again...");
                Response.End();

                return RedirectToAction("Index");
            }

            cookie = new HttpCookie("TSWA-Last-User", string.Empty)
            {
                Expires = DateTime.Now.AddYears(-5)
            };

            Response.Cookies.Set(cookie);

            return RedirectToAction("Index");

        }*/
    }
}