using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4RailzMVC.Models
{
    public class Remise
    {
        public int RemiseID { get; set; }
        public List<Spoor> Sporen { get; set; }

        public Remise(int id)
        {
            RemiseID = id;
        }
    }
}