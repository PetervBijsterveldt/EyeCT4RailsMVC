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
        //private readonly string connectie = "Server=RailzDB;Database=dbi344475; Database=dbi344475; Trusted_Connection=Yes;";
        private readonly string connectie = "Server=mssql.fhict.local;Database=dbi344475;User Id=dbi344475;Password=Rails1;";
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
                                "INSERT INTO Spoor (ID, Remise_ID, Nummer, Lengte, Beschikbaar, InUitRijSpoor) VALUES (@id, @remiseid, @nummer, @lengte, @beschikbaar, @inuitrijspoor)";
                            cmd.Connection = conn;

                            //er moet dus nog wat shit worden toegevoegd aan spoor, anders werkt het allemaal niet
                            cmd.Parameters.AddWithValue("@id", spoor.ID);
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
                            cmd.CommandText = "DELETE FROM Rails WHERE RemiseID = @remiseid AND RailsNR = @ID";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@spoornr", spoor.ID);
                            cmd.Parameters.AddWithValue("@remiseid", spoor.RemiseID);

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
                                "SELECT ID, Remise_ID, Nummer, Lengte, Beschikbaar, InUitRijspoor FROM Rails WHERE ID = @id AND RemiseID = @remiseid";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@id", spoor.ID);
                            cmd.Parameters.AddWithValue("@remiseid", spoor.RemiseID);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
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
                                "UPDATE Rails SET Beschikbaar = @beschikbaar WHERE ID = @id AND Remise_ID = @remiseid";
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

        public void AddSectoren(Spoor spoor)
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
                            cmd.CommandText = "INSERT INTO Sector (Spoor_ID) VALUES (@id)";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@id", spoor.ID);

                            foreach (var sector in spoor.Sectoren)
                            {
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

        public void RemoveSectoren(Spoor spoor)
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

                            foreach (var sector in spoor.Sectoren)
                            {
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
    }
}
