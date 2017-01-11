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
        //private readonly string connectie = "Server=RailzDB;Database=dbi344475; Database=dbi344475; Trusted_Connection=Yes;";
        private readonly string connectie = "Server=mssql.fhict.local;Database=dbi344475;User Id=dbi344475;Password=Rails1;";

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
                            cmd.CommandText = "SELECT T.ID, T.Remise_ID, T.Tramtype_ID, T.Nummer, T.Lengte, T.Status, T.Vervuild, T.Defect, T.ConducteurGeschikt, T.Beschikbaar FROM Tram T " +
                                              "LEFT JOIN TramType TT ON TT.ID = T.TramType_ID " + 
                                              "LEFT JOIN Remist R ON R.ID = T.Remise_ID" + 
                                              "WHERE T.ID = @ID";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@ID", tramId);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                ReturnTram(reader);
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
                                "UPDATE Tram SET Nummer = @tramnr, TypeTram = @type, Lengte = @lengte, Status = @status WHERE ID = @tramid";
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
                            cmd.Parameters.AddWithValue("@status", tram.Status);
                            cmd.Parameters.AddWithValue("@tramid", tram.ID);

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

        public void RemoveTram(Tram tram)
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

                            cmd.Parameters.AddWithValue("@tramid", tram.ID);

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

        public void TramInrijden(Tram tram, Spoor spoor)
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
                            foreach (Sector S in spoor.Sectoren)
                            {
                                cmd.CommandText = "INSERT INTO Sector (ID, Spoor_ID, Tram_ID, Nummer, Beschikbaar, Blokkade) VALUES (@id, @spoorid, @tramid, @nummer, @beschikbaar, @blokkade);";

                                cmd.Connection = conn;

                                cmd.Parameters.AddWithValue("@id", S.ID);
                                cmd.Parameters.AddWithValue("@spoorid", spoor.ID);
                                cmd.Parameters.AddWithValue("@tramid", tram.ID);
                                cmd.Parameters.AddWithValue("@nummer", S.SectorNr);
                                cmd.Parameters.AddWithValue("@beschikbaar", S.Beschikbaar);
                                foreach (TramType T in S.GeblokkeerdVoor)
                                {
                                    if (tram.Type == T)
                                    {
                                        cmd.Parameters.AddWithValue("@blokkade", true);
                                        break;
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@blokkade", false);
                                    }
                                }
                                cmd.ExecuteNonQuery();
                            }
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
                            cmd.CommandText = "SELECT T.ID, T.Remise_ID, T.Tramtype_ID, T.Nummer, T.Lengte, T.Status, T.Vervuild, T.Defect, T.ConducteurGeschikt, T.Beschikbaar FROM Tram T " +
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
                                        //reparatieBeurten.Add(new ReparatieBeurt(reparatieId, medewerkerId, tramId, beginDatumEntijd, einddatum, type));
                                    }
                                    else
                                    {
                                        //reparatieBeurten.Add(new ReparatieBeurt(reparatieId, medewerkerId, tramId, beginDatumEntijd, type));
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
            int Rid = reader.GetInt32(1);
            TramType type;
            if (reader.GetString(2) == "11g")
            {
                type = TramType._11G;
            }
            else if (reader.GetString(2) == "12g")
            {
                type = TramType._12G;
            }
            else if (reader.GetString(2) == "9g")
            {
                type = TramType._9G;
            }
            else if (reader.GetString(2) == "10g")
            {
                type = TramType._10G;
            }
            else
            {
                type = (TramType)Enum.Parse(typeof(TramType), reader.GetString(2).Replace(" ", ""));
            }
            int nr = reader.GetInt32(3);
            int lengte = reader.GetInt32(4);
            string status = reader.GetString(5);
            bool vervuild = reader.GetBoolean(6);
            bool defect = reader.GetBoolean(7);
            bool conducteur;
            if (reader.GetInt32(8) == 0)
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
            }
            bool beschikbaar = reader.GetBoolean(9);

            return new Tram(id, Rid, type, nr, lengte, status, vervuild, defect, conducteur, beschikbaar);
        }
    }
}
