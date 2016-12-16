using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4RailzMVC.Models
{
    public class Spoor
    {
        public int RemiseID { get; set; }
        public int ID { get; set; }
        public int Nummer { get; set; }
        public List<Sector> Sectoren { get; set; }
        public int Lengte { get; set; }
        public int RestererendeLengte { get; set; }
        public int InUitRijSpoor { get; set; }
        public int Beschikbaar { get; set; }

        public Spoor(int nr, int lengte)
        {
            ID = nr;
            Lengte = lengte;
        }

        public Spoor(int nr, int lengte, int remiseid)
        {
            ID = nr;
            Lengte = lengte;
            RemiseID = remiseid;
        }

        //constructor voor uit de database
        public Spoor(int id, int remiseid, int nummer, int lengte, int beschikbaar, int inuitrijspoor, List<Sector> sectoren)
        {
            ID = id;
            RemiseID = remiseid;
            Nummer = nummer;
            Lengte = lengte;
            Beschikbaar = beschikbaar;
            InUitRijSpoor = inuitrijspoor;
            Sectoren = sectoren;
        }

        //constructor voor in de database, maar dan zonder ID (auto increment)
        public Spoor(int remiseid, int nummer, int lengte, int beschikbaar, int inuitrijspoor, List<Sector> sectoren)
        {
            RemiseID = remiseid;
            Nummer = nummer;
            Lengte = lengte;
            Beschikbaar = beschikbaar;
            InUitRijSpoor = inuitrijspoor;
            Sectoren = sectoren;
        }

        public void BlokkeerSpoor()
        {
            
        }

        public void DeblokkeerSpoor()
        {
            
        }

        public void SectorenToevoegen(int lengte)
        {
            
        }

        public void SectorenVerwijderen(int lengte)
        {
            
        }
    }
}