using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
       
        public ReparatieBeurt(int Id, int TramId, int MedewerkerId, DateTime StartDatumEnTijd, DateTime EindDatumEnTijd, ReparatiebeurtType ReparatiebeurtType)
        {
            this.Id = Id;
            this.TramId = TramId;
            this.MedewerkerId = MedewerkerId;
            //this.Naam = Naam;
            //this.Beschrijving = Beschrijving;
            this.StartDatumEnTijd = StartDatumEnTijd;
            this.EindDatumEnTijd = EindDatumEnTijd;
            this.ReparatiebeurtType = ReparatiebeurtType;
        }

        public ReparatieBeurt(int Id, int TramId, int MedewerkerId, string Naam, string Beschrijving, DateTime StartDatumEnTijd, DateTime VerwachteDatumEnTijd, ReparatiebeurtType ReparatiebeurtType)
        {
            this.Id = Id;
            this.TramId = TramId;
            this.MedewerkerId = MedewerkerId;
            //this.Naam = Naam;
            //this.Beschrijving = Beschrijving;
            this.StartDatumEnTijd = StartDatumEnTijd;
            this.ReparatiebeurtType = ReparatiebeurtType;
        }

        public ReparatieBeurt( int TramId, int MedewerkerId, string Naam, string Beschrijving, DateTime StartDatumEnTijd, DateTime VerwachteDatumEnTijd, ReparatiebeurtType ReparatiebeurtType)
        {
            
            this.TramId = TramId;
            this.MedewerkerId = MedewerkerId;
            //this.Naam = Naam;
            //this.Beschrijving = Beschrijving;
            this.StartDatumEnTijd = StartDatumEnTijd;
            this.ReparatiebeurtType = ReparatiebeurtType;
        }

        public ReparatieBeurt( int TramId, int MedewerkerId, string Naam, string Beschrijving, DateTime StartDatumEnTijd, DateTime EindDatumEnTijd, DateTime VerwachteDatumEnTijd, ReparatiebeurtType ReparatiebeurtType)
        {
            this.TramId = TramId;
            this.MedewerkerId = MedewerkerId;
            //this.Naam = Naam;
            //this.Beschrijving = Beschrijving;
            this.StartDatumEnTijd = StartDatumEnTijd;
            this.EindDatumEnTijd = EindDatumEnTijd;
            this.ReparatiebeurtType = ReparatiebeurtType;
        }
    }
}