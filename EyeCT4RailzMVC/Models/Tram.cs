using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4RailzMVC.Models
{
    public class Tram
    {
        public int TramID { get; set; }
        public string Naam { get; set; }
        public Spoor HuidigSpoor { get; set; }
        public User Bestuurder { get; set; }
        public List<SchoonmaakBeurt> SchoonmaakBeurten { get; set; }
        public List<ReparatieBeurt> ReparatieBeurten { get; set; }
        public TramType Type { get; set; }
        public TramStatus Status { get; set; }
        public int Lengte { get; set; }
        public int TramNr { get; set; }
        public bool ConducteurGeschikt { get; set; }

        //constructors
        public Tram(int id, int nr, int lengte, TramType type)
        {
            TramID = id;
            TramNr = nr;
            Lengte = lengte;
            Type = type;
        }
        public Tram(int nr, int lengte, TramType type)
        {
            TramNr = nr;
            Lengte = lengte;
            Type = type;
        }
        public Tram(int id, int nr, int lengte, TramType type, TramStatus status, bool conducteurgeschikt, List<SchoonmaakBeurt> schoonmaakbeurten, List<ReparatieBeurt> reparatiebeurten)
        {
            TramID = id;
            TramNr = nr;
            Lengte = lengte;
            Type = type;
            Status = status;
            SchoonmaakBeurten = schoonmaakbeurten;
            ReparatieBeurten = reparatiebeurten;

        }

        //methodes
        public void Schoonmaak(SchoonmaakBeurt schoonmaakBeurt)
        {
            SchoonmaakBeurten.Add(schoonmaakBeurt);
        }
        public void Reparatie(ReparatieBeurt reparatieBeurt)
        {
            ReparatieBeurten.Add(reparatieBeurt);
        }
        public void StatusVeranderen(TramStatus status)
        {
            Status = status;
        }
        public void EditTram(int nr, int lengte, TramStatus status, TramType type)
        {
            TramNr = nr;
            Lengte = lengte;
            Status = status;
            Type = type;
        }
    }
}