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
        public int RestererendeLengte { get { return Sectoren.FindAll(x => x.TramID > 0).Count; } }
        public bool InUitRijSpoor { get; set; }
        public bool Beschikbaar { get; set; }

        public Spoor()
        {
            
        }

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
        public Spoor(int id, int remiseid, int nummer, int lengte, bool beschikbaar, bool inuitrijspoor, List<Sector> sectoren)
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
        public Spoor(int remiseid, int nummer, int lengte, bool beschikbaar, bool inuitrijspoor)
        {
            RemiseID = remiseid;
            Nummer = nummer;
            Lengte = lengte;
            Beschikbaar = beschikbaar;
            InUitRijSpoor = inuitrijspoor;
        }

        public void BlokkeerSpoor()
        {

            Beschikbaar = false;
        }

        public void DeblokkeerSpoor()
        {
            Beschikbaar = true;
        }

        public void SectorenToevoegen(int lengte)
        {
            //als i onder de lengte blijft of gelijk aan de lengte is, voegt ie steeds een nieuwe (lege) sector toe
            for (int i = 0; i <= lengte; i++)
            {
                Sectoren.Add(new Sector());
            }
        }

        public void SectorenVerwijderen(int lengte)
        {
            //idk?
        }
    }
}