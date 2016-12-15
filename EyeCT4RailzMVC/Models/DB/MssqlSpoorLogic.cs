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
                //Als de connectie nog niet open is, wordt hij open gezet
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    //nieuw command
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            //query
                            cmd.CommandText = "";
                            cmd.Connection = conn;

                            //parameter meegeven aan de query

                            //nieuwe reader aanmaken
                            SqlDataReader reader = cmd.ExecuteReader();
                            reader.Read();

                            //voor iedere kolom die hij leest, geeft hij de waarde van die kolom aan de volgende integers
                            int nr = reader.GetInt32(2);
                            

                            //een user wordt gecreeerd met de waardes uit de database en deze wordt daarna gereturned
                            

                            //connectie sluiten
                            conn.Close();
                            //return je gemaakte user
                            
                        }
                        catch (Exception ex)
                        {
                            conn.Close();
                            throw new Exceptions.DataException(ex.Message);
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
                                "INSERT INTO Rails (RemiseID, RailsNR, Geblokkeerd, Lengte) VALUES (@remiseid, @spoornr, @geblokkeerd, @lengte";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@lengte", spoor.Lengte);
                            cmd.Parameters.AddWithValue("@geblokkeerd", spoor.Geblokkeerd);

                            //Zorgt ervoor dat de query wordt uitgevoerd op de database
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
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
                            System.Windows.Forms.MessageBox.Show(exc.Message);
                            conn.Close();
                            return null;
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
    }
}
