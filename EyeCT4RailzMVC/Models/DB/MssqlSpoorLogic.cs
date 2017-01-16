using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EyeCT4RailzMVC.Models
{
    public class MssqlSpoorLogic : ISpoorServices
    {
#if !DEBUG
        private readonly string connectie = "Server=RailzDB;Database=dbi344475; Database=dbi344475; Trusted_Connection=Yes;";
#else
        private readonly string connectie =
            "Server=mssql.fhict.local;Database=dbi344475;User Id=dbi344475;Password=Rails1;";
#endif
        //checked
        public Spoor CheckForSpoorId(int spoorId)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText = "SELECT * FROM Spoor WHERE ID = @id";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@id", spoorId);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                reader.Read();
                                int id = reader.GetInt32(0);
                                int remiseid = reader.GetInt32(1);
                                int nummer = reader.GetInt32(2);
                                int lengte = reader.GetInt32(3);
                                bool beschikbaar = reader.GetBoolean(4);
                                bool inuitrijspoor = reader.GetBoolean(5);
                                List<Sector> sectoren = getSectoren(id);

                                return new Spoor(id, remiseid, nummer, lengte, beschikbaar, inuitrijspoor, sectoren);
                            }
                        }
                        catch (Exception ex)
                        {
                            conn.Close();
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
        }

        //checked
        public void AddSpoor(Spoor spoor)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText =
                                "INSERT INTO Spoor (Remise_ID, Nummer, Lengte, Beschikbaar, InUitRijSpoor) VALUES (@remiseid, @nummer, @lengte, @beschikbaar, @inuitrijspoor)";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@remiseid", spoor.RemiseID);
                            cmd.Parameters.AddWithValue("@nummer", spoor.Nummer);
                            cmd.Parameters.AddWithValue("lengte", spoor.Lengte);
                            cmd.Parameters.AddWithValue("@beschikbaar", spoor.Beschikbaar);
                            cmd.Parameters.AddWithValue("@inuitrijspoor", spoor.InUitRijSpoor);

                            //Zorgt ervoor dat de query wordt uitgevoerd op de database
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new DataException(ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }


        //checked
        public void RemoveSpoor(Spoor spoor)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText = "DELETE FROM Spoor WHERE ID = @spoorId";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@spoorId", spoor.ID);

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new DataException(ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        public void RemoveSpoor(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText = "DELETE FROM Spoor WHERE ID = @spoorId";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@spoornr", id);


                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new DataException(ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        public void SpoorSectoren(Spoor spoor, int spoorid)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    try
                    {
                        for (int i = 0; i < spoor.Lengte; i++)
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {

                                cmd.CommandText = "INSERT INTO Sector (Spoor_ID, tram_ID, nummer, Beschikbaar, blokkade) VALUES (@spoor_ID, 0, @nummer, @beschikbaar, @blokkade)";
                                cmd.Connection = conn;

                                cmd.Parameters.AddWithValue("@spoor_ID", spoorid);
                                cmd.Parameters.AddWithValue("@nummer", i + 1);
                                cmd.Parameters.AddWithValue("@beschikbaar", "False");
                                cmd.Parameters.AddWithValue("@blokkade", "False");

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public List<Spoor> ListSporen()
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                List<Spoor> sporen = new List<Spoor>();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText = "SELECT * FROM SPOOR";
                            cmd.Connection = conn;

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                int remiseid = reader.GetInt32(1);
                                int nummer = reader.GetInt32(2);
                                int lengte = reader.GetInt32(3);
                                bool beschikbaar = reader.GetBoolean(4);
                                bool inuitrijspoor = reader.GetBoolean(5);
                                List<Sector> sectoren = getSectoren(id);

                                sporen.Add(new Spoor(id, remiseid, nummer, lengte, beschikbaar, inuitrijspoor, sectoren));
                            }
                            return sporen;
                        }
                        catch (Exception exc)
                        {
                            throw new Exceptions.DataException(exc.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
                return null;
            }
        }

        public Spoor CheckForSpoor(Spoor spoor)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText =
                                "SELECT * FROM Spoor WHERE ID = @id";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@id", spoor.ID);

                            SqlDataReader reader = cmd.ExecuteReader();

                            reader.Read();

                            int id = reader.GetInt32(0);
                            int remiseid = reader.GetInt32(1);
                            int nummer = reader.GetInt32(2);
                            int lengte = reader.GetInt32(3);
                            bool beschikbaar = reader.GetBoolean(4);
                            bool inuitrijspoor = reader.GetBoolean(5);
                            List<Sector> sectoren = getSectoren(spoor.ID);

                            return new Spoor(id, remiseid, nummer, lengte, beschikbaar, inuitrijspoor, sectoren);
                        }
                        catch (Exception ex)
                        {
                            throw new DataException(ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
                return null;
            }
        }

        public void SpoorStatusVeranderen(Spoor spoor)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText =
                                "UPDATE Spoor SET Beschikbaar = @beschikbaar WHERE ID = @id";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@id", spoor.ID);
                            cmd.Parameters.AddWithValue("@remiseid", spoor.RemiseID);
                            cmd.Parameters.AddWithValue("@beschikbaar", spoor.Beschikbaar);

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new DataException(ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        public void AddSectoren(Spoor spoor, int hoeveelheid)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    try
                    {
                        int sectorcount = spoor.Lengte;
                        for (int i = 0; i < hoeveelheid; i++)
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.CommandText = "INSERT INTO Sector (Spoor_ID, tram_ID, nummer, Beschikbaar, blokkade) VALUES (@id, 0, @nummer, @beschikbaar, @blokkade)";
                                cmd.Connection = conn;

                                cmd.Parameters.AddWithValue("@id", spoor.ID);
                                cmd.Parameters.AddWithValue("@beschikbaar", false);
                                cmd.Parameters.AddWithValue("@blokkade", false);

                                cmd.Parameters.AddWithValue("@nummer", sectorcount + 1);
                                cmd.ExecuteNonQuery();
                                sectorcount++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void RemoveSectoren(Spoor spoor, int hoeveelheid)
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
                            cmd.CommandText = "DELETE FROM Sector WHERE Spoor_ID = @spoorid AND nummer > @min AND nummer <= @max";
                            cmd.Connection = conn;
                            cmd.Parameters.AddWithValue("@spoorid", spoor.ID);
                            cmd.Parameters.AddWithValue("@min", spoor.Lengte - hoeveelheid);
                            cmd.Parameters.AddWithValue("@max", spoor.Lengte);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void RemoveAllSectoren(Spoor spoor)
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
                            cmd.CommandText = "DELETE FROM Sector WHERE Spoor_ID = @spoorid";
                            cmd.Connection = conn;
                            cmd.Parameters.AddWithValue("@spoorid", spoor.ID);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new DataException(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public List<Sector> getSectoren(int spoorId)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText = "SELECT * FROM Sector WHERE Spoor_ID = @id";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@id", spoorId);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                List<Sector> sectoren = new List<Sector>();
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    int spoorid = spoorId;
                                    int tramid = reader.GetInt32(2);
                                    int nummer = reader.GetInt32(3);
                                    bool beschikbaar = reader.GetBoolean(4);
                                    bool blokkade = reader.GetBoolean(5);

                                    sectoren.Add(new Sector(id, nummer, spoorid, tramid, beschikbaar));
                                }
                                return sectoren;
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
            return null;
        }


        public void EditSpoor(Spoor spoor)
        {
            using (SqlConnection connection = new SqlConnection(connectie))
            {
                if (connection.State != ConnectionState.Open) connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.CommandText = "UPDATE Spoor " +
                                              "SET Remise_ID = @remiseId, Nummer = @nummer, Lengte = @lengte, Beschikbaar = @beschikbaar, InUitRijspoor = @inuitrijSpoor " +
                                              "WHERE ID = @id";
                        command.Connection = connection;

                        command.Parameters.AddWithValue("@remiseId", spoor.RemiseID);
                        command.Parameters.AddWithValue("@nummer", spoor.Nummer);
                        command.Parameters.AddWithValue("@lengte", spoor.Lengte);
                        command.Parameters.AddWithValue("@beschikbaar", spoor.Beschikbaar);
                        command.Parameters.AddWithValue("@inuitrijSpoor", spoor.InUitRijSpoor);
                        command.Parameters.AddWithValue("@id", spoor.ID);

                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exceptions.DataException(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
