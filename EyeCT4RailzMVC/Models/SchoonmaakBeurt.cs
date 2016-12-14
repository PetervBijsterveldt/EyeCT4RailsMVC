using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4RailzMVC.Models
{
    public class SchoonmaakBeurt
    {
        public int Id { get; set; } 
        public int MedewerkerId { get; set; }
        //public string Naam { get; set; }
        public int TramId { get; set; }
        //public string Beschrijving { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public SchoonmaakType Type { get; set; }

        public SchoonmaakBeurt(int id, int medewerkerid, int tramid, DateTime startdatum, DateTime einddatum, SchoonmaakType type)
        {
            Id = id;
            MedewerkerId = medewerkerid;
            TramId = tramid;
            //Naam = naam;
            //Beschrijving = beschrijving;
            StartDatum = startdatum;
            EindDatum = einddatum;
            Type = type;
        }
        public SchoonmaakBeurt(int id, int medewerkerid, int tramid, DateTime startdatum, SchoonmaakType type)
        {
            Id = id;
            MedewerkerId = medewerkerid;
            TramId = tramid;
            //Naam = naam;
            //Beschrijving = beschrijving;
            StartDatum = startdatum;
            Type = type;
        }
        public SchoonmaakBeurt(int medewerkerid, string naam, int tramid, string beschrijving, DateTime startdatum, DateTime einddatum, SchoonmaakType type)
        {
            MedewerkerId = medewerkerid;
            //Naam = naam;
            //Beschrijving = beschrijving;
            StartDatum = startdatum;
            EindDatum = einddatum;
            Type = type;
        }
        public SchoonmaakBeurt(int medewerkerid, string naam, int tramid, string beschrijving, DateTime startdatum, SchoonmaakType type)
        {
            MedewerkerId = medewerkerid;
            //Naam = naam;
            //Beschrijving = beschrijving;
            StartDatum = startdatum;
            Type = type;
        }
        public SchoonmaakBeurt(int id, int medewerkerid, string naam, int tramid, string beschrijving, DateTime startdatum, SchoonmaakType type)
        {
            Id = id;
            MedewerkerId = medewerkerid;
            //Naam = naam;
            //Beschrijving = beschrijving;
            StartDatum = startdatum;
            Type = type;
        }
    }
}