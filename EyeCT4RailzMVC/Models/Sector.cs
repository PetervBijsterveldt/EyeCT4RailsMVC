using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace EyeCT4RailzMVC.Models
{
    public class Sector
    {
        public int ID { get; set; }
        public int SectorNr { get; set; }
        public int RailsID { get; set; }
        public int TramID { get; set; }
        public bool Beschikbaar { get; set; }
        public List<TramType> GeblokkeerdVoor { get; set; }

        public Sector()
        {
            
        }

        public Sector(int id, int nr, int railsid, int tramid, bool beschikbaar)
        {
            ID = id;
            SectorNr = nr;
            RailsID = railsid;
            TramID = tramid;
            Beschikbaar = beschikbaar;
        }

        public Sector(int id, int nr, int railsid)
        {
            ID = id;
            SectorNr = nr;
            RailsID = railsid;
        }

        public void VeranderBlokkerenVoorTram(Tram tram)
        {
            
        }

        public void TramToevoegenAanSector(Tram tram)
        {
            
        }
    }
}