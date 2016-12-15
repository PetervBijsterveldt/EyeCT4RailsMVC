using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4RailzMVC.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        //public string Wachtwoord { get; set; }
        //public string Email { get; set; }
        //public string Postcode { get; set; }
        //public string Woonplaats { get; set; }
        //public string Rfid { get; set; }
        public UserType Rol { get; set; }
        
        public User(int id, string naam)
        {
            ID = id;
            Naam = naam;
        }
        public User(int id, string naam, UserType rol)
        {
            ID = id;
            Naam = naam;
            Rol = rol;
        }

        public void ChangeRol(UserType rol)
        {
            Rol = rol;
        }
    }
}