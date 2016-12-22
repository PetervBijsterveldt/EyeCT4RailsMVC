using System.DirectoryServices.AccountManagement;
using System.Web.Mvc;

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
            string email = form["email"];
            string password = form["password"];

            using (var pc = new PrincipalContext(ContextType.Machine))
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

            return RedirectToAction("Index");
        }
    }
}