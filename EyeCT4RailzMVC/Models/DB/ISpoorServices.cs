using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EyeCT4RailzMVC.Models
{
    public interface ISpoorServices
    {
        Spoor CheckForSpoorId(int spoorId);
        void AddSpoor(Spoor spoor);
        void RemoveSpoor(Spoor spoor);
        List<Spoor> ListSporen();
        Spoor CheckForSpoor(Spoor spoor);
        void SpoorStatusVeranderen(Spoor spoor);
        void AddSectoren(Spoor spoor);
        void RemoveSectoren(Spoor spoor);
        List<Sector> ListSectoren(Spoor spoor);
    }
}
