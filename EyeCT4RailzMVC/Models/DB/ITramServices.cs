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
        List<SchoonmaakBeurt> ListSchoonmaakbeurten(int tramId);
        void AddSchoonmaakbeurt(SchoonmaakBeurt schoonmaakBeurt);
        void AddReparatiebeurt(ReparatieBeurt reparatieBeurt);
        void EditSchoonmaakbeurt(SchoonmaakBeurt schoonmaakBeurt, DateTime time);
        void EditReparatiebeurt(ReparatieBeurt reparatieBeurt, DateTime time);
        void EditTram(Tram tram);
    }
}
