using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeCT4RailzMVC.Models.Exceptions;

namespace EyeCT4RailzMVC.Models
{
    public class MssqlTramLogic : ITramServices
    {
#if !DEBUG
        private readonly string connectie = "Server=RailzDB;Database=dbi344475; Database=dbi344475; Trusted_Connection=Yes;";
#else
        private readonly string connectie =
            "Server=mssql.fhict.local;Database=dbi344475;User Id=dbi344475;Password=Rails1;";
#endif

        public Tram CheckForTramId(int tramId)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = "SELECT * FROM Tram T " +
                                              "LEFT JOIN TramType TT ON TT.ID = T.TramType_ID " +
                                              "LEFT JOIN Remise R ON R.ID = T.Remise_ID_Standplaats " +
                                              "WHERE T.ID = @ID";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@ID", tramId);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                reader.Read();
                                return ReturnTram(reader);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }

        public void EditTram(Tram tram)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText =
                                "UPDATE Tram SET Nummer = @tramnr, TramType_ID = @type, Lengte = @lengte, Status = @status, Vervuild = @vervuild, Defect = @defect, ConducteurGeschikt = @geschikt, Beschikbaar = @beschikbaar WHERE ID = @tramid";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@tramid", tram.ID);
                            

                            if (tram.Type == TramType._11G)
                            {
                                cmd.Parameters.AddWithValue("@type", "2");
                            }
                            else if (tram.Type == TramType._12G)
                            {
                                cmd.Parameters.AddWithValue("@type", "4");
                            }
                            else if (tram.Type == TramType.DubbelKopCombino)
                            {
                                cmd.Parameters.AddWithValue("@type", "3");
                            }
                            else if (tram.Type == TramType.Combino)
                            {
                                cmd.Parameters.AddWithValue("@type", "1");
                            }
                            else if (tram.Type == TramType.Opleidingtram)
                            {
                                cmd.Parameters.AddWithValue("@type", "5");
                            }
                            else if (tram.Type == TramType._9G)
                            {
                                cmd.Parameters.AddWithValue("@type", "6");
                            }
                            else if (tram.Type == TramType._10G)
                            {
                                cmd.Parameters.AddWithValue("@type", "7");
                            }

                            cmd.Parameters.AddWithValue("@lengte", tram.Lengte);
                            if (tram.Status == TramStatus.Dienst)
                            {
                                cmd.Parameters.AddWithValue("@status", "Dienst");

                                cmd.Parameters.AddWithValue("@vervuild", false);
                                cmd.Parameters.AddWithValue("@defect", false);
                                cmd.Parameters.AddWithValue("@geschikt", true);
                                cmd.Parameters.AddWithValue("@beschikbaar", false);
                            }
                            else if (tram.Status == TramStatus.Onderhoud)
                            {
                                cmd.Parameters.AddWithValue("@status", "Onderhoud");

                                cmd.Parameters.AddWithValue("@vervuild", false);
                                cmd.Parameters.AddWithValue("@defect", true);
                                cmd.Parameters.AddWithValue("@geschikt", false);
                                cmd.Parameters.AddWithValue("@beschikbaar", false);
                            }
                            else if (tram.Status == TramStatus.Remise)
                            {
                                cmd.Parameters.AddWithValue("@status", "Remise");

                                cmd.Parameters.AddWithValue("@vervuild", false);
                                cmd.Parameters.AddWithValue("@defect", false);
                                cmd.Parameters.AddWithValue("@geschikt", true);
                                cmd.Parameters.AddWithValue("@beschikbaar", true);
                            }
                            else if (tram.Status == TramStatus.Schoonmaak)
                            {
                                cmd.Parameters.AddWithValue("@status", "Schoonmaak");

                                cmd.Parameters.AddWithValue("@vervuild", true);
                                cmd.Parameters.AddWithValue("@defect", false);
                                cmd.Parameters.AddWithValue("@geschikt", false);
                                cmd.Parameters.AddWithValue("@beschikbaar", false);
                            }
                            else if (tram.Status == TramStatus.SchoonmaakNodig)
                            {
                                cmd.Parameters.AddWithValue("@status", "SchoonmaakNodig");

                                cmd.Parameters.AddWithValue("@vervuild", true);
                                cmd.Parameters.AddWithValue("@defect", false);
                                cmd.Parameters.AddWithValue("@geschikt", true);
                                cmd.Parameters.AddWithValue("@beschikbaar", true);
                            }
                            else if (tram.Status == TramStatus.ServiceNodig)
                            {
                                cmd.Parameters.AddWithValue("@status", "ServiceNodig");

                                cmd.Parameters.AddWithValue("@vervuild", false);
                                cmd.Parameters.AddWithValue("@defect", true);
                                cmd.Parameters.AddWithValue("@geschikt", true);
                                cmd.Parameters.AddWithValue("@beschikbaar", true);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@status", "Remise");

                                cmd.Parameters.AddWithValue("@vervuild", false);
                                cmd.Parameters.AddWithValue("@defect", false);
                                cmd.Parameters.AddWithValue("@geschikt", true);
                                cmd.Parameters.AddWithValue("@beschikbaar", true);
                            }

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public Spoor CheckForTramOnSpoor(Tram tram)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open) conn.Open();

                try
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SELECT Spoor_ID FROM SECTOR WHERE Tram_ID = @tramid";
                        cmd.Connection = conn;

                        cmd.Parameters.AddWithValue("@tramid", tram.ID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();

                            if (reader.HasRows)
                            {
                                int spoorid = reader.GetInt32(0);

                                SpoorRepository spoorRepository = new SpoorRepository(new MssqlSpoorLogic());

                                return spoorRepository.CheckForSpoorId(spoorid);
                            }
                            else
                            {
                                return new Spoor();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exceptions.DataException(ex.Message);
                }
            }
        }

        public void Inrijden(Spoor spoor, Tram tram)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open) conn.Open();

                try
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UPDATE SECTOR SET Tram_ID = @tramid WHERE Spoor_ID = @spoorid AND Nummer >= @start AND NUMMER <= @eind";
                        cmd.Connection = conn;

                        //Bijv spoor met lengte 10 en tram met lengte 4
                        //op het spoor staat een tram met lengte 3 achteraan
                        //dus de al ingereden tram staat op sectoren 10-9-8
                        //de inrijdende tram moet dus op sectoren 7-6-5-4 komen
                        int eindSectorNummer = spoor.Sectoren.Find(trm => trm.ID != 0).SectorNr - 1;
                        if (eindSectorNummer == 0) eindSectorNummer = spoor.Sectoren.Count;
                        int startSectorNummer = eindSectorNummer - tram.Lengte;

                        cmd.Parameters.AddWithValue("tramid", tram.ID);
                        cmd.Parameters.AddWithValue("spoorID", spoor.ID);
                        cmd.Parameters.AddWithValue("start", startSectorNummer);
                        cmd.Parameters.AddWithValue("eind", eindSectorNummer);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exceptions.DataException(ex.Message);
                }
            }
        }

        public void Uitrijden(Spoor spoor, Tram tram)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open) conn.Open();

                try
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UPDATE SECTOR SET Tram_ID = 0 WHERE Spoor_ID = @spoorid AND Tram_ID = @tramid";
                        cmd.Connection = conn;

                        cmd.Parameters.AddWithValue("@spoorid", spoor.ID);
                        cmd.Parameters.AddWithValue("@tramid", tram.ID);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exceptions.DataException(ex.Message);
                }
            }
        }

        public void AddTram(Tram tram)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText =
                                //Typetram is nr uit ander tabel
                                "INSERT INTO Tram (Tramtype_ID, Nummer, Lengte, Status, Vervuild, Defect, ConducteurGeschikt, Beschikbaar) VALUES (@type, @nr, @lengte, @status, @vervuild, @defect, @conducteur, @beschikbaar)";
                            cmd.Connection = conn;

                            if (tram.Type == TramType._11G)
                            {
                                cmd.Parameters.AddWithValue("@type", "2");
                            }
                            else if (tram.Type == TramType._12G)
                            {
                                cmd.Parameters.AddWithValue("@type", "4");
                            }
                            else if (tram.Type == TramType.DubbelKopCombino)
                            {
                                cmd.Parameters.AddWithValue("@type", "3");
                            }
                            else if (tram.Type == TramType.Combino)
                            {
                                cmd.Parameters.AddWithValue("@type", "1");
                            }
                            else if (tram.Type == TramType.Opleidingtram)
                            {
                                cmd.Parameters.AddWithValue("@type", "5");
                            }
                            else if (tram.Type == TramType._9G)
                            {
                                cmd.Parameters.AddWithValue("@type", "6");
                            }
                            else if (tram.Type == TramType._10G)
                            {
                                cmd.Parameters.AddWithValue("@type", "7");
                            }
                            cmd.Parameters.AddWithValue("@nr", tram.TramNr);
                            cmd.Parameters.AddWithValue("@lengte", tram.Lengte);
                            cmd.Parameters.AddWithValue("@status", tram.Status.ToString());
                            cmd.Parameters.AddWithValue("@vervuild", tram.Vervuild);
                            cmd.Parameters.AddWithValue("@defect", tram.Defect);
                            cmd.Parameters.AddWithValue("@conducteur", tram.ConducteurGeschikt);
                            cmd.Parameters.AddWithValue("@beschikbaar", tram.Beschikbaar);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void RemoveTram(int _ID)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = "DELETE FROM Tram WHERE ID = @tramid";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@tramid", _ID);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void TramVerplaatsen(Tram tram, Spoor spoor)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            TramInrijden(conn, cmd, spoor, tram);
                            TramUitrijden(conn, cmd, spoor, tram);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException(ex.Message);
                    }
                }
            }
        }

        public List<Tram> ListTrams()
        {
            List<Tram> trams = new List<Tram>();
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = "SELECT T.ID, T.Remise_ID_Standplaats, T.Tramtype_ID, T.Nummer, T.Lengte, T.Status, T.Vervuild, T.Defect, T.ConducteurGeschikt, T.Beschikbaar FROM Tram T " +
                                              "LEFT JOIN  TramType TT on T.Tramtype_ID = TT.ID";
                            cmd.Connection = conn;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    trams.Add(ReturnTram(reader));
                                }

                                return trams;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }

        public List<SchoonmaakBeurt> ListSchoonmaakbeurtenPerTram(int tramnr)
        {
            List<SchoonmaakBeurt> schoonmaakBeurten = new List<SchoonmaakBeurt>();

            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText =
                                "SELECT t.id, naam, tram_id, DatumTijdstip, BeschikbaarDatum, TypeOnderhoud, medewerker_id FROM tram_onderhoud t " +
                                "left join medewerker m ON t.Medewerker_ID = m.ID  WHERE TypeOnderhoud IN('SchoonmaakGroot', 'SchoonmaakKlein')";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@id", tramnr);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int schoonmaakId = reader.GetInt32(0);
                                    int tramId = reader.GetInt32(2);
                                    DateTime beginDatumEntijd = reader.GetDateTime(3);
                                    string medewerkernaam = reader.GetString(1);
                                    SchoonmaakType type = (SchoonmaakType)Enum.Parse(typeof(SchoonmaakType), reader.GetString(5));
                                    int medewerkerid = reader.GetInt32(6);
                                    if (!reader.IsDBNull(4))
                                    {
                                        DateTime einddatum = reader.GetDateTime(4);
                                        schoonmaakBeurten.Add(new SchoonmaakBeurt(schoonmaakId, medewerkernaam, tramId, beginDatumEntijd, einddatum, type, medewerkerid));
                                    }
                                    else
                                    {
                                        schoonmaakBeurten.Add(new SchoonmaakBeurt(schoonmaakId, medewerkernaam, tramId, beginDatumEntijd, type, medewerkerid));
                                    }
                                }
                                return schoonmaakBeurten;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }

        public List<SchoonmaakBeurt> ListSchoonmaakbeurten(int afgerond)
        {
            List<SchoonmaakBeurt> schoonmaakBeurten = new List<SchoonmaakBeurt>();

            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText =
                                "SELECT t.id, naam, tram_id, DatumTijdstip, BeschikbaarDatum, TypeOnderhoud, medewerker_id FROM tram_onderhoud t " +
                                "left join medewerker m ON t.Medewerker_ID = m.ID WHERE TypeOnderhoud IN('SchoonmaakGroot', 'SchoonmaakKlein') AND Afgerond = @afgerond";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@afgerond", afgerond);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int schoonmaakId = reader.GetInt32(0);
                                    int tramId = reader.GetInt32(2);
                                    DateTime beginDatumEntijd = reader.GetDateTime(3);
                                    string medewerkernaam = reader.GetString(1);
                                    SchoonmaakType type = (SchoonmaakType)Enum.Parse(typeof(SchoonmaakType), reader.GetString(5));
                                    int medewerkerid = reader.GetInt32(6);
                                    if (!reader.IsDBNull(4))
                                    {
                                        DateTime einddatum = reader.GetDateTime(4);
                                        schoonmaakBeurten.Add(new SchoonmaakBeurt(schoonmaakId, medewerkernaam, tramId, beginDatumEntijd, einddatum, type, medewerkerid));
                                    }
                                    else
                                    {
                                        schoonmaakBeurten.Add(new SchoonmaakBeurt(schoonmaakId, medewerkernaam, tramId, beginDatumEntijd, type, medewerkerid));
                                    }
                                }
                                return schoonmaakBeurten;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }

        public List<ReparatieBeurt> ListReparatiebeurtenPerTram(int tramnr)
        {
            List<ReparatieBeurt> reparatieBeurten = new List<ReparatieBeurt>();

            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText =
                                "SELECT * FROM TRAM_ONDERHOUD WHERE Tram_ID = @id AND TypeOnderhoud IN('ReparatieGroot', 'ReparatieKlein')";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@id", tramnr);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int reparatieId = reader.GetInt32(0);
                                    int tramId = reader.GetInt32(2);
                                    DateTime beginDatumEntijd = reader.GetDateTime(3);
                                    int medewerkerId = reader.GetInt32(1);
                                    ReparatiebeurtType type = (ReparatiebeurtType)Enum.Parse(typeof(ReparatiebeurtType), reader.GetString(5));
                                    if (!reader.IsDBNull(4))
                                    {
                                        DateTime einddatum = reader.GetDateTime(4);
                                    }
                                    else
                                    {
                                    }
                                }
                                return reparatieBeurten;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }

        public List<ReparatieBeurt> ListReparatiebeurten(int afgerond)
        {
            List<ReparatieBeurt> reparatiekBeurten = new List<ReparatieBeurt>();

            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText =
                                "SELECT t.id, naam, tram_id, DatumTijdstip, BeschikbaarDatum, TypeOnderhoud, medewerker_id FROM tram_onderhoud t " +
                                "left join medewerker m ON t.Medewerker_ID = m.ID WHERE TypeOnderhoud IN('ReparatieGroot', 'ReparatieKlein') AND Afgerond = @afgerond";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@afgerond", afgerond);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int reparatieid = reader.GetInt32(0);
                                    int tramId = reader.GetInt32(2);
                                    DateTime beginDatumEntijd = reader.GetDateTime(3);
                                    string medewerkernaam = reader.GetString(1);
                                    ReparatiebeurtType type = (ReparatiebeurtType)Enum.Parse(typeof(ReparatiebeurtType), reader.GetString(5));
                                    int medewerkerid = reader.GetInt32(6);
                                    if (!reader.IsDBNull(4))
                                    {
                                        DateTime einddatum = reader.GetDateTime(4);
                                        reparatiekBeurten.Add(new ReparatieBeurt(reparatieid, medewerkernaam, tramId, beginDatumEntijd, einddatum, type, medewerkerid));
                                    }
                                    else
                                    {
                                        reparatiekBeurten.Add(new ReparatieBeurt(reparatieid, medewerkernaam, tramId, beginDatumEntijd, type, medewerkerid));
                                    }
                                }
                                return reparatiekBeurten;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }

        public void AddSchoonmaakbeurt(SchoonmaakBeurt schoonmaakBeurt)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText =
                                "EXECUTE InsertSchoonmaakbeurtMetNaam @medewerkernaam, @tramid, @begin, @eind, @onderhoud";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@tramid", schoonmaakBeurt.TramId);
                            cmd.Parameters.AddWithValue("@begin", schoonmaakBeurt.StartDatum);
                            cmd.Parameters.AddWithValue("@eind", schoonmaakBeurt.EindDatum);
                            cmd.Parameters.AddWithValue("@medewerkernaam", schoonmaakBeurt.Medewerkernaam);
                            cmd.Parameters.AddWithValue("@onderhoud", schoonmaakBeurt.Type.ToString());

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void AddReparatiebeurt(ReparatieBeurt reparatieBeurt)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText =
                                "EXECUTE InsertSchoonmaakbeurtMetNaam @medewerkernaam, @tramid, @begin, @eind, @onderhoud";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@tramid", reparatieBeurt.TramId);
                            cmd.Parameters.AddWithValue("@begin", reparatieBeurt.StartDatumEnTijd);
                            cmd.Parameters.AddWithValue("@eind", reparatieBeurt.EindDatum);
                            cmd.Parameters.AddWithValue("@medewerkernaam", reparatieBeurt.Medewerkernaam);
                            cmd.Parameters.AddWithValue("@onderhoud", reparatieBeurt.ReparatiebeurtType.ToString());

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void EditOnderhoud(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText =
                                "UPDATE TRAM_ONDERHOUD SET BeschikbaarDatum = @eind, Afgerond = 1 WHERE ID = @onderhoudid";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@onderhoudid", id);
                            cmd.Parameters.AddWithValue("@eind", DateTime.Now);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
        //een methode om een tram uit de database te halen
        //deze is als methode geschreven omdat hij vaker dan 1x gebruikt wordt.
        private Tram ReturnTram(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            int Rid;
            if (!reader.IsDBNull(1))
            {
                Rid = reader.GetInt32(1);
            }
            else
            {
                Rid = -1;
            }
            TramType type;

            switch (reader.GetInt32(2))
            {
                case 1:
                    type = TramType.Combino;
                    break;

                case 2:
                    type = TramType._11G;
                    break;

                case 3:
                    type = TramType.DubbelKopCombino;
                    break;

                case 4:
                    type = TramType._12G;
                    break;

                case 5:
                    type = TramType.Opleidingtram;
                    break;

                case 6:
                    type = TramType._9G;
                    break;

                case 7:
                    type = TramType._10G;
                    break;

                default:
                    type = TramType.Combino;
                    break;
            }

            int nr = reader.GetInt32(3);
            int lengte = reader.GetInt32(4);
            TramStatus status;
            if (!reader.IsDBNull(5))
            {
                status = (TramStatus)Enum.Parse(typeof(TramStatus), reader.GetString(5));
            }
            else
            {
                status = TramStatus.Remise;
            }
            bool vervuild = reader.GetBoolean(6);
            bool defect = reader.GetBoolean(7);
            bool conducteur = reader.GetBoolean(8);
            /* if (reader.GetInt32(8) == 0)
             {
                 conducteur = false; //bestuurder mag niet rijden
             }
             else if (reader.GetInt32(8) == 1)
             {
                 conducteur = true; //bestuurder mag wel rijden
             }
             else
             {
                 conducteur = false;
             }*/
            bool beschikbaar = reader.GetBoolean(9);

            return new Tram(id, Rid, type, nr, lengte, status, vervuild, defect, conducteur, beschikbaar);
        }


        private void TramInrijden(SqlConnection conn, SqlCommand cmd, Spoor spoor, Tram tram)
        {
            foreach (Sector S in spoor.Sectoren)
            {
                cmd.CommandText = "UDPATE Sector SET Tram_ID = @tramid WHERE ID = @sectorid;";

                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("@tramid", tram.ID);
                cmd.Parameters.AddWithValue("@sectorid", S.ID);

                cmd.ExecuteNonQuery();
            }
        }
        private void TramUitrijden(SqlConnection conn, SqlCommand cmd, Spoor spoor, Tram tram)
        {
            foreach (Sector S in spoor.Sectoren)
            {
                cmd.CommandText = "UPDATE Sector SET Tram_ID = 0 WHERE Tram_ID = @tramid AND ID = @sectorid";

                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("@tramid", tram.ID);
                cmd.Parameters.AddWithValue("@sectorid", S.ID);

                cmd.ExecuteNonQuery();
            }
        }


    }
}
