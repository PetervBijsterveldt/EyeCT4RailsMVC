using System;

namespace EyeCT4RailzMVC.Models
{
    public class ReparatieBeurt
    {
        public int Id { get; set; }
        public int TramId { get; set; }
        public int MedewerkerId { get; set; }
        //public string Naam { get; set; }
        //public string Beschrijving { get; set; }
        public DateTime StartDatumEnTijd { get; set; }
        public DateTime EindDatumEnTijd { get; set; }
        //public DateTime VerwachteDatumEnTijd { get; set; }
        public ReparatiebeurtType ReparatiebeurtType { get; set; }
       
        public ReparatieBeurt(int id, int tramId, int medewerkerId, DateTime startDatumEnTijd, DateTime eindDatumEnTijd, ReparatiebeurtType reparatiebeurtType)
        {
            Id = id;
            TramId = tramId;
            MedewerkerId = medewerkerId;
            //this.Naam = Naam;
            //this.Beschrijving = Beschrijving;
            StartDatumEnTijd = startDatumEnTijd;
            EindDatumEnTijd = eindDatumEnTijd;
            ReparatiebeurtType = reparatiebeurtType;
        }
        public ReparatieBeurt(int id, int tramId, int medewerkerId, DateTime startDatumEnTijd, ReparatiebeurtType reparatiebeurtType)
        {
            Id = id;
            TramId = tramId;
            MedewerkerId = medewerkerId;
            //this.Naam = Naam;
            //this.Beschrijving = Beschrijving;
            StartDatumEnTijd = startDatumEnTijd;
            ReparatiebeurtType = reparatiebeurtType;
        }

        public ReparatieBeurt(int id, int tramId, int medewerkerId, string naam, string beschrijving, DateTime startDatumEnTijd, DateTime verwachteDatumEnTijd, ReparatiebeurtType reparatiebeurtType)
        {
            Id = id;
            TramId = tramId;
            MedewerkerId = medewerkerId;
            //this.Naam = Naam;
            //this.Beschrijving = Beschrijving;
            StartDatumEnTijd = startDatumEnTijd;
            ReparatiebeurtType = reparatiebeurtType;
        }

        public ReparatieBeurt( int tramId, int medewerkerId, string naam, string beschrijving, DateTime startDatumEnTijd, DateTime verwachteDatumEnTijd, ReparatiebeurtType reparatiebeurtType)
        {
            
            TramId = tramId;
            MedewerkerId = medewerkerId;
            //this.Naam = Naam;
            //this.Beschrijving = Beschrijving;
            StartDatumEnTijd = startDatumEnTijd;
            ReparatiebeurtType = reparatiebeurtType;
        }

        public ReparatieBeurt( int tramId, int medewerkerId, string naam, string beschrijving, DateTime startDatumEnTijd, DateTime eindDatumEnTijd, DateTime verwachteDatumEnTijd, ReparatiebeurtType reparatiebeurtType)
        {
            TramId = tramId;
            MedewerkerId = medewerkerId;
            //this.Naam = Naam;
            //this.Beschrijving = Beschrijving;
            StartDatumEnTijd = startDatumEnTijd;
            EindDatumEnTijd = eindDatumEnTijd;
            ReparatiebeurtType = reparatiebeurtType;
        }
    }
}