using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeCT4RailzMVC.Models
{
    public class SpoorRepository
    {
        private ISpoorServices spoorLogic;

        public SpoorRepository(ISpoorServices spoorLogic)
        {
            this.spoorLogic = spoorLogic;
        }

        public Spoor CheckForSpoorId(int spoorId)
        {
            return spoorLogic.CheckForSpoorId(spoorId);
        }

        public void AddSpoor(Spoor spoor)
        {
            spoorLogic.AddSpoor(spoor);
        }

        public void RemoveSpoor(Spoor spoor)
        {
            spoorLogic.RemoveSpoor(spoor);
        }

        public List<Spoor> ListSporen()
        {
            return spoorLogic.ListSporen();
        }

        public Spoor CheckForSpoor(Spoor spoor)
        {
            return spoorLogic.CheckForSpoor(spoor);
        }

        public void SpoorStatusVeranderen(Spoor spoor)
        {
            spoorLogic.SpoorStatusVeranderen(spoor);
        }
    }
}
