﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4RailzMVC.Models
{
    public class Tram
    {
        public int ID { get; set; }
        public int RemiseID { get; set; }
        public TramType Type { get; set; }
        public int TramNr { get; set; }
        public int Lengte { get; set; }
        public TramStatus Status { get; set; }
        public bool Vervuild { get; set; }
        public bool Defect { get; set; }
        public bool ConducteurGeschikt { get; set; }
        public bool Beschikbaar { get; set; }
        public List<SchoonmaakBeurt> SchoonmaakBeurten { get; set; }
        public List<ReparatieBeurt> ReparatieBeurten { get; set; }
        

        //constructors
        public Tram()
        {
            //lege tram
        }
        public Tram(int id, int nr, int lengte, TramType type)
        {
            ID = id;
            TramNr = nr;
            Lengte = lengte;
            Type = type;
        }
        public Tram(int nr, int lengte, TramType type)
        {
            TramNr = nr;
            Lengte = lengte;
            Type = type;
        }
        public Tram(int id, int Rid, TramType type, int nr, int lengte, TramStatus status, bool vervuild, bool defect, bool geschikt, bool beschikbaar)
        {
            ID = id;
            RemiseID = Rid;
            Type = type;
            TramNr = nr;
            Lengte = lengte;
            Status = status;
            Vervuild = vervuild;
            Defect = defect;
            ConducteurGeschikt = geschikt;
            Beschikbaar = beschikbaar;
            SchoonmaakBeurten = new List<SchoonmaakBeurt>();
            ReparatieBeurten = new List<ReparatieBeurt>();
        }
        public Tram(int id, int nr, int lengte, TramType type, TramStatus status, bool conducteurgeschikt, List<SchoonmaakBeurt> schoonmaakbeurten, List<ReparatieBeurt> reparatiebeurten)
        {
            ID = id;
            TramNr = nr;
            Lengte = lengte;
            Type = type;
            Status = status;
            SchoonmaakBeurten = schoonmaakbeurten;
            ReparatieBeurten = reparatiebeurten;

        }

        //methodes
        public void Schoonmaak(SchoonmaakBeurt schoonmaakBeurt)
        {
            SchoonmaakBeurten.Add(schoonmaakBeurt);
        }
        public void Reparatie(ReparatieBeurt reparatieBeurt)
        {
            ReparatieBeurten.Add(reparatieBeurt);
        }
        public void StatusVeranderen(TramStatus status)
        {
            Status = status;
        }
        public void EditTram(int nr, int lengte, TramStatus status, TramType type)
        {
            TramNr = nr;
            Lengte = lengte;
            Status = status;
            Type = type;
        }
        public override string ToString()
        {
            if (Type == TramType.DubbelKopCombino)
            {
                return $"DKC {Environment.NewLine} { TramNr }";
            }
            else if (Type == TramType.Opleidingtram)
            {
                return $"Edu {Environment.NewLine} { TramNr }";
            }
            return $"{Type.ToString()} {Environment.NewLine}{TramNr}";
        }
    }
}