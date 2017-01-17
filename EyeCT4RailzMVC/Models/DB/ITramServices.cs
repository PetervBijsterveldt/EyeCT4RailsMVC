using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeCT4RailzMVC.Models
{
    public interface ITramServices
    {
        Tram CheckForTramId(int tramId);
        void AddTram(Tram tram);
        void RemoveTram(int _id);
        void TramVerplaatsen(Tram tram, Spoor spoor);
        List<Tram> ListTrams();
        List<SchoonmaakBeurt> ListSchoonmaakbeurtenPerTram(int tramId);
        List<SchoonmaakBeurt> ListSchoonmaakbeurten(int afgerond);
        List<ReparatieBeurt> ListReparatiebeurtenPerTram(int tramnr);
        List<ReparatieBeurt> ListReparatiebeurten(int afgerond);
        void AddSchoonmaakbeurt(SchoonmaakBeurt schoonmaakBeurt);
        void AddReparatiebeurt(ReparatieBeurt reparatieBeurt);
        void EditOnderhoud(int id);
        void EditTram(Tram tram);
        Spoor CheckForTramOnSpoor(Tram tram);
        void Inrijden(Spoor spoor, Tram tram);
        void Uitrijden(Spoor spoor, Tram tram);
    }
}
