using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.DirectoryServices.AccountManagement;

namespace EyeCT4RailzMVC.Models
{
    public class MssqlUserLogic : IUserServices
    {
        //connectiestring met de database
        //private readonly string connectie = "Server=RailzDB;Database=dbi344475; Database=dbi344475; Trusted_Connection=Yes;";
        private readonly string connectie = "Server=mssql.fhict.local;Database=dbi344475;User Id=dbi344475;Password=Railz1;";

        //check of user bestaat door RFID-tag
        public User CheckForUserId(string rfid)
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                //als de connectie nog niet open is, wordt hij open gezet
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    //nieuw command
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            //query
                            cmd.CommandText = "SELECT * FROM Medewerker WHERE RFID = @rfid";
                            cmd.Connection = conn;

                            //parameter meegeven aan de query
                            cmd.Parameters.AddWithValue("@rfid", rfid);

                            //nieuwe reader aanmaken
                            SqlDataReader reader = cmd.ExecuteReader();
                            reader.Read();

                            //voor iedere kolom die hij leest, geeft hij de waarde van die kolom aan de volgende int en strings
                            int id = reader.GetInt32(0);
                            string naam = reader.GetString(1);
                            UserType rol = (UserType) Enum.Parse(typeof(UserType), reader.GetString(2));

                            conn.Close();
                            return new User(id, naam, rol);
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

        //check of de user bestaat
        public User CheckForUser(User user)
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
                                "SELECT m.ID, m.Naam, f.Naam FROM medewerker m left join functie f ON m.Functie_ID = f.ID WHERE m.ID = @ID AND m.naam = @gebrn";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@ID", user.ID);
                            cmd.Parameters.AddWithValue("@gebrn", user.Naam);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                reader.Read();

                                int id = reader.GetInt32(0);
                                string naam = reader.GetString(1);
                                UserType rol = (UserType) Enum.Parse(typeof(UserType), reader.GetString(2));

                                conn.Close();
                                return new User(id, naam, rol);
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

        //voeg een user toe aan de database
        public void AddUser(User user)
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
                                "INSERT INTO Medewerker (Functie_ID, Naam) VALUES (@rol, @naam)";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@naam", user.Naam);
                           
                            if (user.Rol == UserType.Beheerder)
                            {
                                cmd.Parameters.AddWithValue("@rol", 1);
                            }
                            if (user.Rol == UserType.Wagenparkbeheerder)
                            {
                                cmd.Parameters.AddWithValue("@rol", 2);
                            }
                            if (user.Rol == UserType.Bestuurder)
                            {
                                cmd.Parameters.AddWithValue("@rol", 3);
                            }
                            if (user.Rol == UserType.Technicus)
                            {
                                cmd.Parameters.AddWithValue("@rol", 4);
                            }
                            if (user.Rol == UserType.Schoonmaker)
                            {
                                cmd.Parameters.AddWithValue("@rol", 5);
                            }

                            cmd.ExecuteNonQuery();
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

        //verwijder een user uit de database
        public void RemoveUser(User user)
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
                            cmd.CommandText = "DELETE FROM Medewerker WHERE Naam = @naam";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@naam", user.Naam);

                            cmd.ExecuteNonQuery();
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

        //wijzig een user in de database
        public void EditUser(User user)
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
                                "UPDATE Medewerker SET Naam = @naam, Functie_ID = @functie WHERE ID = @mID";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@mID", user.ID);
                            cmd.Parameters.AddWithValue("@naam", user.Naam);
                            if (user.Rol == UserType.Beheerder)
                            {
                                cmd.Parameters.AddWithValue("@functie", 1);
                            }
                            if (user.Rol == UserType.Wagenparkbeheerder)
                            {
                                cmd.Parameters.AddWithValue("@functie", 2);
                            }
                            if (user.Rol == UserType.Bestuurder)
                            {
                                cmd.Parameters.AddWithValue("@functie", 3);
                            }
                            if (user.Rol == UserType.Technicus)
                            {
                                cmd.Parameters.AddWithValue("@functie", 4);
                            }
                            if (user.Rol == UserType.Schoonmaker)
                            {
                                cmd.Parameters.AddWithValue("@functie", 5);
                            }

                            cmd.ExecuteNonQuery();
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

        //haal alle users op uit de database
        public List<User> ListUsers()
        {
            using (SqlConnection conn = new SqlConnection(connectie))
            {
                List<User> users = new List<User>();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText = "SELECT m.ID, m.Naam, f.Naam FROM medewerker m left join functie f ON m.Functie_ID = f.ID";
                            cmd.Connection = conn;

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string naam = reader.GetString(1);
                                UserType functie = (UserType) Enum.Parse(typeof(UserType), reader.GetString(2));
                                users.Add(new User(id, naam, functie));
                            }
                            return users;
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
