using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCTForRailsMVC.Models
{
    public class Spoor
    {
        public int RemiseID { get; set; }
        public int SpoorNR { get; set; }
        public List<Sector> Sectoren { get; set; }
        public int Lengte { get; set; }
        public int RestererendeLengte { get; set; }
        public bool Geblokkeerd { get; set; }

        public Spoor(int nr, int lengte)
        {
            SpoorNR = nr;
            Lengte = lengte;
        }

        public Spoor(int nr, int lengte, int remiseid, bool geblokkeerd)
        {
            SpoorNR = nr;
            Lengte = lengte;
            RemiseID = remiseid;
            Geblokkeerd = geblokkeerd;
        }

        public Spoor(int nr, int lengte, int remiseid, bool geblokkeerd, List<Sector> sectoren)
        {
            SpoorNR = nr;
            Lengte = lengte;
            RemiseID = remiseid;
            Geblokkeerd = geblokkeerd;
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