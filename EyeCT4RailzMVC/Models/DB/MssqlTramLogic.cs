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
                            cmd.CommandText = "select t.ID, Nummer, omschrijving, lengte, status, ConducteurGeschikt from tram t " +
                                              "left join tramtype tt on t.Tramtype_ID = tt.ID WHERE ID = @id";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@id", tramId);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                int id = reader.GetInt32(0);
                                int tramnr = reader.GetInt32(1);
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
                                int lengte = reader.GetInt32(3);
                                TramStatus status = (TramStatus)Enum.Parse(typeof(TramStatus), reader.GetString(4));
                                bool conducteurgeschikt = reader.GetBoolean(5);

                                List<SchoonmaakBeurt> schoonmaakBeurten = ListSchoonmaakbeurtenPerTram(id);
                                List<ReparatieBeurt> reparatieBeurten = ListReparatiebeurten(id);

                                return new Tram(id, tramnr, lengte, type, status, conducteurgeschikt, schoonmaakBeurten, reparatieBeurten);
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
                                "UPDATE Tram SET Nummer = @tramnr, TypeTram = @type, Lengte = @lengte, [Status] = @status WHERE ID = @tramid";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@tramnr", tram.TramNr);

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
                            cmd.Parameters.AddWithValue("@status", tram.Status.ToString());
                            cmd.Parameters.AddWithValue("@tramid", tram.TramID);

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
                                "INSERT INTO Tram (Nummer, TypeTram, Lengte, Status, ConducteurGeschikt) VALUES (@nr, @type, @lengte, @status, @conducteur)";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@nr", tram.TramNr);
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
                            cmd.Parameters.AddWithValue("@status", tram.Status.ToString());
                            cmd.Parameters.AddWithValue("@conducteur", tram.ConducteurGeschikt);

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

                            cmd.Parameters.AddWithValue("@tramid", tram.TramID);

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
            throw new NotImplementedException();
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
                            cmd.CommandText = "select t.ID, Nummer, omschrijving, lengte, status, ConducteurGeschikt from tram t " +
                                              "left join tramtype tt on t.Tramtype_ID = tt.ID";
                            cmd.Connection = conn;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int tramId = reader.GetInt32(0);
                                    int tramnr = reader.GetInt32(1);
                                    TramType type;
                                    if (reader.GetString(2) == "11G")
                                    {
                                        type = TramType._11G;
                                    }
                                    else if (reader.GetString(2) == "12G")
                                    {
                                        type = TramType._12G;
                                    }
                                    else if (reader.GetString(2) == "9G")
                                    {
                                        type = TramType._9G;
                                    }
                                    else if (reader.GetString(2) == "10G")
                                    {
                                        type = TramType._10G;
                                    }
                                    else
                                    {
                                        type = (TramType)Enum.Parse(typeof(TramType), reader.GetString(2).Replace(" ", ""));
                                    }
                                    bool conducteurgeschikt = reader.GetBoolean(5);
                                    int lengte = reader.GetInt32(3);

                                    TramStatus status = (TramStatus)Enum.Parse(typeof(TramStatus), reader.GetString(4));
                                    //List<SchoonmaakBeurt> schoonmaakBeurten = ListSchoonmaakbeurtenPerTram(tramId);
                                    //List<ReparatieBeurt> reparatieBeurten = ListReparatiebeurten(tramId);

                                    //trams.Add(new Tram(tramId, tramnr, lengte, type, status, conducteurgeschikt, schoonmaakBeurten, reparatieBeurten));
                                    trams.Add(new Tram(tramId, tramnr, lengte, type, status, conducteurgeschikt, new List<SchoonmaakBeurt>(), new List<ReparatieBeurt>()));
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
                                "INSERT INTO TRAM_ONDERHOUD (medewerker_id, tram_id, datumtijdstip, beschikbaardatum, typeonderhoud)" +
                                "VALUES (@medewerkerid, @tramid, @begin, @eind, @onderhoud)";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@tramid", reparatieBeurt.TramId);
                            cmd.Parameters.AddWithValue("@begin", reparatieBeurt.StartDatumEnTijd);
                            cmd.Parameters.AddWithValue("@medewerkerid", reparatieBeurt.MedewerkerId);
                            cmd.Parameters.AddWithValue("@eind", reparatieBeurt.EindDatum);
                            cmd.Parameters.AddWithValue("@onderhoud", reparatieBeurt.ReparatiebeurtType);

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
    }
}
