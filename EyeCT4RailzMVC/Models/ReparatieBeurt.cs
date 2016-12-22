using System;

namespace EyeCT4RailzMVC.Models
{
    public class ReparatieBeurt
    {
        public int Id { get; set; }
        public int TramId { get; set; }
        public int MedewerkerId { get; set; }
        public string Medewerkernaam { get; set; }
        public DateTime StartDatumEnTijd { get; set; }
        public DateTime EindDatumEnTijd { get; set; }
        public ReparatiebeurtType ReparatiebeurtType { get; set; }
       
        public ReparatieBeurt(int id, string medewerkernaam, int tramId, DateTime startDatumEnTijd, DateTime eindDatumEnTijd, ReparatiebeurtType reparatiebeurtType, int medewerkerid)
        {
            Id = id;
            TramId = tramId;
            Medewerkernaam = medewerkernaam;
            MedewerkerId = medewerkerid;
            StartDatumEnTijd = startDatumEnTijd;
            EindDatumEnTijd = eindDatumEnTijd;
            ReparatiebeurtType = reparatiebeurtType;
        }
        public ReparatieBeurt(int id, string medewerkernaam, int tramId, DateTime startDatumEnTijd, ReparatiebeurtType reparatiebeurtType, int medewerkerid)
        {
            Id = id;
            TramId = tramId;
            Medewerkernaam = medewerkernaam;
            MedewerkerId = medewerkerid;
            StartDatumEnTijd = startDatumEnTijd;
            ReparatiebeurtType = reparatiebeurtType;
        }
    }
}