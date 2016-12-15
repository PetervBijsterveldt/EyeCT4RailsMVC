using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace EyeCT4RailzMVC.Models
{
    public class Sector
    {
        public int SectorID { get; set; }
        public int SectorNr { get; set; }
        public int RailsID { get; set; }
        public int TramID { get; set; }
        public int Beschikbaar { get; set; }
        public List<TramType> GeblokkeerdVoor { get; set; }

        public Sector(int nr)
        {
            SectorNr = nr;
        }

        public Sector(int id, int nr, int railsid, int tramid, int beschikbaar)
        {
            SectorID = id;
            SectorNr = nr;
            RailsID = railsid;
            TramID = tramid;
            Beschikbaar = beschikbaar;
        }

        public Sector(int id, int nr, int railsid)
        {
            SectorID = id;
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