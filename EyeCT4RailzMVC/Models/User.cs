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
        public UserType Rol { get; set; }
        
        public User(int id, string naam, UserType rol)
        {
            ID = id;
            Naam = naam;
            Rol = rol;
        }
        public User(string naam, UserType rol)
        {
            Naam = naam;
            Rol = rol;
        }

        public User()
        {
            
        }

        public void ChangeRol(UserType rol)
        {
            Rol = rol;
        }
    }
}