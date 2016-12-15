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
        private readonly string connectie =
            "Server=mssql.fhict.local;Database=dbi344475;User Id=dbi344475;Password=Rails1";

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
                                int nr = reader.GetInt32(0);
                                int lengte = reader.GetInt32(3);
                                int remiseid = reader.GetInt32(1);
                                bool block = reader.GetBoolean(4);
                                List<Sector> sectoren = getSectoren(nr);

                                return new Spoor(nr, lengte, remiseid, block, sectoren);
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
                            cmd.Parameters.AddWithValue("@id", spoor.SpoorNR);
                            cmd.Parameters.AddWithValue("@remiseid", spoor.RemiseID);
                            cmd.Parameters.AddWithValue("@nummer", spoor.);
                            cmd.Parameters.AddWithValue("lengte", spoor.Lengte);
                            cmd.Parameters.AddWithValue("@beschikbaar", spoor.);
                            cmd.Parameters.AddWithValue("@inuitrijspoor", spoor.);

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
                            cmd.CommandText = "DELETE FROM Rails WHERE RemiseID = @remiseid AND RailsNR = @spoornr";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@remiseid", spoor.Remiseid);
                            cmd.Parameters.AddWithValue("@spoornr", spoor.Spoornr);

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
                            cmd.CommandText = "SELECT RailsID, RemiseID, Geblokkeerd, Lengte FROM Rails";
                            cmd.Connection = conn;

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                int spoornr = reader.GetInt32(0);
                                int remiseid = reader.GetInt32(1);
                                bool geblokkeerd = reader.GetBoolean(2);
                                int lengte = reader.GetInt32(3);
                                List<Sector> sector = ListSectoren(new Spoor(spoornr, lengte, geblokkeerd, remiseid));

                                sporen.Add(new Spoor(spoornr, lengte, geblokkeerd, remiseid, sector));
                            }
                            return sporen;
                        }
                        catch (Exception exc)
                        {
                            throw new DataException(exc.Message);
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
                                "SELECT RailsID, RemiseID, Geblokkeerd, Lengte FROM Rails WHERE RailsNR = @spoornr AND RemiseID = @remiseid";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@spoornr", spoor.Spoornr);
                            cmd.Parameters.AddWithValue("@remiseid", spoor.Remiseid);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                reader.Read();

                                int nr = reader.GetInt32(0);
                                int remiseid = reader.GetInt32(1);
                                bool geblokkeerd = reader.GetBoolean(2);
                                int lengte = reader.GetInt32(3);

                                conn.Close();
                                return new Spoor(nr, lengte, geblokkeerd, remiseid, ListSectoren(spoor));
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
                                "UPDATE Rails SET Geblokkeerd = @geblokkeerd WHERE RailsID = @spoornr AND RemiseID = @remiseid";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@spoornr", spoor.Spoornr);
                            cmd.Parameters.AddWithValue("@remiseid", spoor.Remiseid);
                            cmd.Parameters.AddWithValue("@geblokkeerd", spoor.Geblokkeerd);

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
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
                            cmd.CommandText = "INSERT INTO Sector (RailsID) VALUES (@spoorid)";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@spoorid", spoor.Spoornr);

                            foreach (var sector in spoor.sectoren)
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        conn.Close();
                    }
                }
            }
        }

        public void RemoveSectoren(Spoor spoor)
        {
            throw new NotImplementedException();
        }

        public List<Sector> ListSectoren(Spoor spoor)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();

                        List<Sector> sectoren = new List<Sector>();

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = "SELECT * FROM Sector WHERE RailsId = @railsId";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@railsId", spoor.Spoornr);

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                int sectorId = reader.GetInt32(0);
                                int sectorNr = reader.GetInt32(1);
                                int railsId = reader.GetInt32(2);

                                if (!reader.IsDBNull(3))
                                {
                                    int tramId = reader.GetInt32(3);
                                    sectoren.Add(new Sector(sectorId, sectorNr, railsId, tramId));
                                }
                                else
                                {
                                    sectoren.Add(new Sector(sectorId, sectorNr, railsId));
                                }


                            }
                        }

                        return sectoren;
                    }

                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    conn.Close();
                    return null;
                }
            }
            return null;
        }
        public List<Sector> getSectoren(int spoornr)
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

                            cmd.Parameters.AddWithValue("@id", spoornr);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                List<Sector> sectoren = new List<Sector>();
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    int spoorid = spoornr;
                                    int tramid = reader.GetInt32(2);
                                    int nummer = reader.GetInt32(3);
                                    int beschikbaar = reader.GetInt32(4);
                                    int blokkade = reader.GetInt32(5);

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
                return null;
            }
        }
    }
}
