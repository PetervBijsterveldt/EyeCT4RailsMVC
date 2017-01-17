using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using EyeCT4RailzMVC.Models;

namespace EyeCT4RailzMVC.Controllers
{

    public class ReparatieController : Controller
    {
        TramRepository tramRepository = new TramRepository(new MssqlTramLogic());
        // GET: Reparatie

        
        public ActionResult Index()
        {
            return View();
        }

#if !DEBUG
        [Authorize(Roles = "Technicus, Beheerder")]
#endif
        public ActionResult Reparatieoverzicht()
        {
            if (User.IsInRole("Technicus"))
            {
                return RedirectToAction("Taken");
            }
            else
            {
                List<ReparatieBeurt> reparatieBeurten = tramRepository.ListReparatiebeurten(0);
                return View(reparatieBeurten);
            }
        }

        public ActionResult Taken()
        {
            string[] namen = User.Identity.Name.Split('\\');
            string naam = namen[1];
            List<ReparatieBeurt> reparatieBeurten = new List<ReparatieBeurt>();
            foreach (var item in tramRepository.ListReparatiebeurten(0))
            {
                if (item.Medewerkernaam == naam)
                {
                    reparatieBeurten.Add(item);
                }
            }
            return View(reparatieBeurten);
        }

        public ActionResult HistoryReparatieoverzicht()
        {
            List<ReparatieBeurt> reparatieBeurten = tramRepository.ListReparatiebeurten(1);
            return View(reparatieBeurten);
        }

        [HttpGet]
        public ActionResult AddReparatie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddReparatie(FormCollection form)
        {
            ReparatieBeurt beurt = new ReparatieBeurt();
            beurt.Medewerkernaam = form["Medewerker"];
            beurt.TramId = Convert.ToInt32(form["TramId"]);
            beurt.EindDatum = Convert.ToDateTime(form["EindDatum"]);
            beurt.ReparatiebeurtType = (ReparatiebeurtType)Enum.Parse(typeof(ReparatiebeurtType), form["ReparatiebeurtType"]);

            MailMessage mail = new MailMessage("beheer@EyeCT4Railz.local", beurt.Medewerkernaam + "@eyect4railz.local");
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            client.Host = "localhost";
            mail.Subject = "Nieuwe werkzaamheden zijn beschikbaar";
            mail.Body = "Kijk op de website voor nieuwe taken";
            client.Send(mail);

            tramRepository.AddReparatiebeurt(beurt);
            return RedirectToAction("Reparatieoverzicht");
        }

        public ActionResult BeëindigReparatie(int reparatieid)
        {
            tramRepository.EditOnderhoud(reparatieid);

            return RedirectToAction("Reparatieoverzicht");
        }
    }
}