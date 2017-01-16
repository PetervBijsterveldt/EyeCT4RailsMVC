using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeCT4RailzMVC.Models
{
    public class TramRepository
    {
        private ITramServices tramLogic;

        public TramRepository(ITramServices tramLogic)
        {
            this.tramLogic = tramLogic;
        }

        public Tram CheckForTramId(int tramId)
        {
            return tramLogic.CheckForTramId(tramId);
        }

        public void EditTram(Tram tram)
        {
            tramLogic.EditTram(tram);
        }

        public void AddTram(Tram tram)
        {
            tramLogic.AddTram(tram);
        }

        public void RemoveTram(Tram tram)
        {
            tramLogic.RemoveTram(tram);
        }

        public void TramInrijden(Tram tram, Spoor spoor)
        {
            tramLogic.TramVerplaatsen(tram, spoor);
        }

        public List<Tram> ListTrams()
        {
            return tramLogic.ListTrams();
        }

        public List<SchoonmaakBeurt> ListSchoonmaakBeurtenPerTram(int tramId)
        {
            return tramLogic.ListSchoonmaakbeurtenPerTram(tramId);
        }

        public List<SchoonmaakBeurt> ListSchoonmaakbeurten(int afgerond)
        {
            return tramLogic.ListSchoonmaakbeurten(afgerond);
        }

        public List<ReparatieBeurt> ListReparatiebeurtenPerTram(int tramnr)
        {
            return tramLogic.ListReparatiebeurtenPerTram(tramnr);
        }

        public List<ReparatieBeurt> ListReparatiebeurten(int afgerond)
        {
            return tramLogic.ListReparatiebeurten(afgerond);
        }

        public void AddSchoonmaakbeurt(SchoonmaakBeurt schoonmaakBeurt)
        {
            tramLogic.AddSchoonmaakbeurt(schoonmaakBeurt);
        }

        public void AddReparatiebeurt(ReparatieBeurt reparatieBeurt)
        {
            tramLogic.AddReparatiebeurt(reparatieBeurt);
        }

        public void EditOnderhoud(int id)
        {
            tramLogic.EditOnderhoud(id);
        }

        public Spoor CheckForTramOnSpoor(Tram tram)
        {
            return tramLogic.CheckForTramOnSpoor(tram);
        }

        public void Inrijden(Spoor spoor, Tram tram)
        {
            tramLogic.Inrijden(spoor, tram);
        }

        public void Uitrijden(Spoor spoor, Tram tram)
        {
            tramLogic.Uitrijden(spoor, tram);
        }
    }
}
