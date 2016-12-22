using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4RailzMVC.Models
{
    public class SchoonmaakBeurt
    {
        public int Id { get; set; } 
        public string Medewerkernaam { get; set; }
        public int MedewerkerId { get; set; }
        public int TramId { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public SchoonmaakType Type { get; set; }

        public SchoonmaakBeurt(int id, string medewerkernaam, int tramid, DateTime startdatum, DateTime einddatum, SchoonmaakType type, int medewerkerid)
        {
            Id = id;
            Medewerkernaam = medewerkernaam;
            TramId = tramid;
            StartDatum = startdatum;
            EindDatum = einddatum;
            Type = type;
            MedewerkerId = medewerkerid;
        }
        public SchoonmaakBeurt(int medewerkerid, int tramid, DateTime startdatum, DateTime einddatum, SchoonmaakType type)
        {
            MedewerkerId = medewerkerid;
            TramId = tramid;
            StartDatum = startdatum;
            EindDatum = einddatum;
            Type = type;
        }

        public SchoonmaakBeurt(int id, string medewerkernaam, int tramid, DateTime startdatum, SchoonmaakType type, int medewerkerid)
        {
            Id = id;
            Medewerkernaam = medewerkernaam;
            TramId = tramid;
            StartDatum = startdatum;
            Type = type;
            MedewerkerId = medewerkerid;
        }
        public SchoonmaakBeurt(string medewerkernaam, DateTime startdatum, DateTime einddatum, SchoonmaakType type)
        {
            Medewerkernaam = medewerkernaam;
            StartDatum = startdatum;
            EindDatum = einddatum;
            Type = type;
        }
        public SchoonmaakBeurt(string medewerkernaam, DateTime startdatum, SchoonmaakType type)
        {
            Medewerkernaam = medewerkernaam;
            StartDatum = startdatum;
            Type = type;
        }
        public SchoonmaakBeurt(int id, string medewerkernaam, DateTime startdatum, SchoonmaakType type)
        {
            Id = id;
            Medewerkernaam = medewerkernaam;
            StartDatum = startdatum;
            Type = type;
        }

        public SchoonmaakBeurt()
        {
            StartDatum = DateTime.Now;
        }
    }
}