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
        void RemoveTram(Tram tram);
        void TramInrijden(Tram tram, Spoor spoor);
        List<Tram> ListTrams();
        List<SchoonmaakBeurt> ListSchoonmaakbeurtenPerTram(int tramId);
        List<SchoonmaakBeurt> ListSchoonmaakbeurten();
        void AddSchoonmaakbeurt(SchoonmaakBeurt schoonmaakBeurt);
        void AddReparatiebeurt(ReparatieBeurt reparatieBeurt);
        void EditOnderhoud(int id);
        void EditTram(Tram tram);
    }
}
