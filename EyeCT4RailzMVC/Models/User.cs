using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4RailzMVC.Models
{
    public class User
    {
        public int Personeelsnummer { get; set; }
        public string Naam { get; set; }
        public string Wachtwoord { get; set; }
        public string Email { get; set; }
        public string Postcode { get; set; }
        public string Woonplaats { get; set; }
        public string Rfid { get; set; }
        public UserType Rol { get; set; }
        //test
        public User(int nr, string naam, string ww)
        {
            Personeelsnummer = nr;
            Naam = naam;
            Wachtwoord = ww;
        }

        public User(string naam, string ww)
        {
            Naam = naam;
            Wachtwoord = ww;
        }

        public User(int nr, string naam, string ww, string email, string postcode, string woonplaats, string rfid,
            UserType rol)
        {
            Personeelsnummer = nr;
            Naam = naam;
            Wachtwoord = ww;
            Email = email;
            Postcode = postcode;
            Woonplaats = woonplaats;
            Rfid = rfid;
            Rol = rol;
        }

        public void ChangeRol(UserType rol)
        {
            Rol = rol;
        }
    }
}