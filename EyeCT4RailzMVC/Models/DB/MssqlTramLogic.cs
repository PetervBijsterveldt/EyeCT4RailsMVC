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
        private readonly string connectie =
            "Server=mssql.fhict.local;Database=dbi344475;User Id=dbi344475;Password=Rails1";

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
                                    type = (TramType) Enum.Parse(typeof(TramType), reader.GetString(2).Replace(" ", ""));
                                }
                                int lengte = reader.GetInt32(3);
                                TramStatus status = (TramStatus) Enum.Parse(typeof(TramStatus), reader.GetString(4));
                                bool conducteurgeschikt = reader.GetBoolean(5);

                                List<SchoonmaakBeurt> schoonmaakBeurten = ListSchoonmaakbeurten(id);
                                List<ReparatieBeurt> reparatieBeurten = ListReparatiebeurten(id);

                                return new Tram(id, tramnr, lengte, type, status,conducteurgeschikt, schoonmaakBeurten, reparatieBeurten);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException();
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
                        throw new Exceptions.DataException();
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
                        throw new Exceptions.DataException();
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
                        throw new Exceptions.DataException();
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
                                              "left join tramtype tt on t.Tramtype_ID = tt.ID WHERE ID = @id";
                            cmd.Connection = conn;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int tramId = reader.GetInt32(0);
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
                                        type =
                                            (TramType)
                                            Enum.Parse(typeof(TramType), reader.GetString(2).Replace(" ", ""));
                                    }
                                    bool conducteurgeschikt = reader.GetBoolean(5);
                                    int lengte = reader.GetInt32(3);

                                    TramStatus status = (TramStatus) Enum.Parse(typeof(TramStatus), reader.GetString(4));
                                    List<SchoonmaakBeurt> schoonmaakBeurten = ListSchoonmaakbeurten(tramId);
                                    List<ReparatieBeurt> reparatieBeurten = ListReparatiebeurten(tramId);

                                    trams.Add(new Tram(tramId, tramnr, lengte, type, status, conducteurgeschikt, schoonmaakBeurten, reparatieBeurten));
                                }

                                return trams;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                        throw new Exceptions.DataException();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }

        public List<SchoonmaakBeurt> ListSchoonmaakbeurten(int tramnr)
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
                                "SELECT s.*, naam FROM Schoonmaakbeurt s, Medewerker m WHERE TramID = @id AND s.MedewerkerID = m.MedewerkerID";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@id", tramnr);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int schoonmaakId = reader.GetInt32(0);
                                    int tramId = reader.GetInt32(1);
                                    string beschrijvijng = reader.GetString(2).TrimEnd(' ');
                                    DateTime beginDatumEntijd = reader.GetDateTime(3);
                                    int medewerkerId = reader.GetInt32(5);
                                    bool isGroteSchoonmaak = reader.GetBoolean(6);
                                    string naam = reader.GetString(7).TrimEnd(' ');

                                    if (!reader.IsDBNull(4))
                                    {
                                        DateTime eindDatumEnTijd = reader.GetDateTime(4);

                                        if (isGroteSchoonmaak)
                                        {
                                            schoonmaakBeurten.Add(new SchoonmaakBeurt(schoonmaakId, medewerkerId, tramId,
                                                beschrijvijng, beginDatumEntijd, eindDatumEnTijd, SchoonmaakType.Groot,
                                                naam));
                                        }
                                        else
                                        {
                                            schoonmaakBeurten.Add(new SchoonmaakBeurt(schoonmaakId, medewerkerId, tramId,
                                                beschrijvijng, beginDatumEntijd, eindDatumEnTijd, SchoonmaakType.Klein,
                                                naam));
                                        }
                                    }
                                    else
                                    {
                                        if (isGroteSchoonmaak)
                                        {
                                            schoonmaakBeurten.Add(new SchoonmaakBeurt(schoonmaakId, medewerkerId, tramId,
                                                beschrijvijng, beginDatumEntijd, SchoonmaakType.Groot, naam));
                                        }
                                        else
                                        {
                                            schoonmaakBeurten.Add(new SchoonmaakBeurt(schoonmaakId, medewerkerId, tramId,
                                                beschrijvijng, beginDatumEntijd, SchoonmaakType.Klein, naam));
                                        }
                                    }
                                }

                                return schoonmaakBeurten;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                        throw new Exceptions.DataException();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }

        public List<SchoonmaakBeurt> ListSchoonmaakbeurten(int tramnr, DateTime date)
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
                                "SELECT s.*, naam FROM Schoonmaakbeurt s, Medewerker m WHERE TramID = @id AND s.MedewerkerID = m.MedewerkerID AND BeginDatumEnTijd BETWEEN @date AND @date + 1;";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@id", tramnr);
                            cmd.Parameters.AddWithValue("@date", date.ToShortDateString());

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int schoonmaakId = reader.GetInt32(0);
                                    int tramId = reader.GetInt32(1);
                                    string beschrijvijng = reader.GetString(2).TrimEnd(' ');
                                    DateTime beginDatumEntijd = reader.GetDateTime(3);
                                    int medewerkerId = reader.GetInt32(5);
                                    bool isGroteSchoonmaak = reader.GetBoolean(6);
                                    string naam = reader.GetString(7).TrimEnd(' ');

                                    if (!reader.IsDBNull(4))
                                    {
                                        DateTime eindDatumEnTijd = reader.GetDateTime(4);

                                        if (isGroteSchoonmaak)
                                        {
                                            schoonmaakBeurten.Add(new SchoonmaakBeurt(schoonmaakId, medewerkerId, tramId,
                                                beschrijvijng, beginDatumEntijd, eindDatumEnTijd, SchoonmaakType.Groot,
                                                naam));
                                        }
                                        else
                                        {
                                            schoonmaakBeurten.Add(new SchoonmaakBeurt(schoonmaakId, medewerkerId, tramId,
                                                beschrijvijng, beginDatumEntijd, eindDatumEnTijd, SchoonmaakType.Klein,
                                                naam));
                                        }
                                    }
                                    else
                                    {
                                        if (isGroteSchoonmaak)
                                        {
                                            schoonmaakBeurten.Add(new SchoonmaakBeurt(schoonmaakId, medewerkerId, tramId,
                                                beschrijvijng, beginDatumEntijd, SchoonmaakType.Groot, naam));
                                        }
                                        else
                                        {
                                            schoonmaakBeurten.Add(new SchoonmaakBeurt(schoonmaakId, medewerkerId, tramId,
                                                beschrijvijng, beginDatumEntijd, SchoonmaakType.Klein, naam));
                                        }
                                    }
                                }

                                return schoonmaakBeurten;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException();
                        return null;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }

        public List<ReparatieBeurt> ListReparatiebeurten(int tramnr)
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
                            cmd.CommandText = "SELECT r.*, Naam FROM Reparatie r, Medewerker m WHERE TramID = @id AND r.MedewerkerID = m.MedewerkerID;";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@id", tramnr);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int reparatieId = reader.GetInt32(0);
                                    int tramId = reader.GetInt32(1);
                                    string beschrijvijng = reader.GetString(2).TrimEnd(' ');
                                    bool isGroteSchoonmaak = reader.GetBoolean(3);
                                    DateTime beginDatumEntijd = reader.GetDateTime(4);
                                    DateTime verwachteEindDatumEnTijd = reader.GetDateTime(6);
                                    int medewerkerId = reader.GetInt32(7);
                                    string naam = reader.GetString(8).TrimEnd(' ');

                                    if (!reader.IsDBNull(5))
                                    {
                                        DateTime eindDatumEnTijd = reader.GetDateTime(5);

                                        if (isGroteSchoonmaak)
                                        {
                                            reparatieBeurten.Add(new ReparatieBeurt(reparatieId, tramId, medewerkerId,
                                                beschrijvijng, beginDatumEntijd, eindDatumEnTijd, verwachteEindDatumEnTijd, ReparatiebeurtType.Groot, naam));
                                        }
                                        else
                                        {
                                            reparatieBeurten.Add(new ReparatieBeurt(reparatieId, tramId, medewerkerId,
                                                beschrijvijng, beginDatumEntijd, eindDatumEnTijd, verwachteEindDatumEnTijd, ReparatiebeurtType.Klein, naam));
                                        }
                                    }
                                    else
                                    {
                                        if (isGroteSchoonmaak)
                                        {
                                            reparatieBeurten.Add(new ReparatieBeurt(reparatieId, tramId, medewerkerId,
                                                beschrijvijng, beginDatumEntijd, verwachteEindDatumEnTijd, ReparatiebeurtType.Groot, naam));
                                        }
                                        else
                                        {
                                            reparatieBeurten.Add(new ReparatieBeurt(reparatieId, tramId, medewerkerId,
                                                beschrijvijng, beginDatumEntijd, verwachteEindDatumEnTijd, ReparatiebeurtType.Klein, naam));
                                        }
                                    }
                                }

                                return reparatieBeurten;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        conn.Close();
                        return null;
                    }
                }
            }
            return null;
        }

        public List<ReparatieBeurt> ListReparatiebeurten(int tramnr, DateTime date)
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
                                "SELECT r.*, Naam FROM Reparatie r, Medewerker m WHERE TramID = @id AND r.MedewerkerID = m.MedewerkerID;";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@id", tramnr);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int reparatieId = reader.GetInt32(0);
                                    int tramId = reader.GetInt32(1);
                                    string beschrijvijng = reader.GetString(2).TrimEnd(' ');
                                    bool isGroteSchoonmaak = reader.GetBoolean(3);
                                    DateTime beginDatumEntijd = reader.GetDateTime(4);
                                    DateTime verwachteEindDatumEnTijd = reader.GetDateTime(6);
                                    int medewerkerId = reader.GetInt32(7);
                                    string naam = reader.GetString(8).TrimEnd(' ');

                                    if (!reader.IsDBNull(5))
                                    {
                                        DateTime eindDatumEnTijd = reader.GetDateTime(5);

                                        if (isGroteSchoonmaak)
                                        {
                                            reparatieBeurten.Add(new ReparatieBeurt(reparatieId, tramId, medewerkerId,
                                                beschrijvijng, beginDatumEntijd, eindDatumEnTijd,
                                                verwachteEindDatumEnTijd, ReparatiebeurtType.Groot, naam));
                                        }
                                        else
                                        {
                                            reparatieBeurten.Add(new ReparatieBeurt(reparatieId, tramId, medewerkerId,
                                                beschrijvijng, beginDatumEntijd, eindDatumEnTijd,
                                                verwachteEindDatumEnTijd, ReparatiebeurtType.Klein, naam));
                                        }
                                    }
                                    else
                                    {
                                        if (isGroteSchoonmaak)
                                        {
                                            reparatieBeurten.Add(new ReparatieBeurt(reparatieId, tramId, medewerkerId,
                                                beschrijvijng, beginDatumEntijd, verwachteEindDatumEnTijd,
                                                ReparatiebeurtType.Groot, naam));
                                        }
                                        else
                                        {
                                            reparatieBeurten.Add(new ReparatieBeurt(reparatieId, tramId, medewerkerId,
                                                beschrijvijng, beginDatumEntijd, verwachteEindDatumEnTijd,
                                                ReparatiebeurtType.Klein, naam));
                                        }
                                    }
                                }

                                return reparatieBeurten;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException();
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
                                "INSERT INTO TRAM_ONDERHOUD (medewerker_id, tram_id, datumtijdstip, beschikbaardatum, typeonderhoud)" +
                                "VALUES (@medewerkerid, @tramid, @begin, @eind, @onderhoud)";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@tramid", schoonmaakBeurt.TramId);
                            cmd.Parameters.AddWithValue("@begin", schoonmaakBeurt.StartDatum);
                            cmd.Parameters.AddWithValue("@eind", schoonmaakBeurt.EindDatum);
                            cmd.Parameters.AddWithValue("@medewerkerid", schoonmaakBeurt.MedewerkerId);
                            cmd.Parameters.AddWithValue("@onderhoud", schoonmaakBeurt.Type);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException();
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
                            cmd.Parameters.AddWithValue("@eind", reparatieBeurt.EindDatumEnTijd);
                            cmd.Parameters.AddWithValue("@onderhoud", reparatieBeurt.ReparatiebeurtType);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void EditSchoonmaakbeurt(SchoonmaakBeurt schoonmaakBeurt, DateTime time)
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
                                "UPDATE TRAM_ONDERHOUD SET BeschikbaarDatum = @eind WHERE ID = @schoonmaakid";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@schoonmaakid", schoonmaakBeurt.Id);
                            cmd.Parameters.AddWithValue("@eind", time);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void EditReparatiebeurt(ReparatieBeurt reparatieBeurt, DateTime time)
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
                                "UPDATE TRAM_ONDERHOUD SET BeschikbaarDatum = @eind WHERE ID = @reparatieid";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@reparatieid", reparatieBeurt.Id);
                            cmd.Parameters.AddWithValue("@eind", time);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException();
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
