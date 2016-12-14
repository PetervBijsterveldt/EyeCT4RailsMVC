using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EyeCT4RailzMVC.Models
{
    public class MssqlUserLogic : IUserServices
    {
        //connectiestring met de database
        private readonly string connectie = "Server=mssql.fhict.local;Database=dbi344475;User Id=dbi344475;Password=Rails1";
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
                            string email = reader.GetString(3);
                            string postcode = reader.GetString(4);
                            string woonplaats = reader.GetString(5);
                            string rfidtram = reader.GetString(6);
                            string ww = reader.GetString(7);

                            conn.Close();
                            return new User(id, naam, ww, email, postcode, woonplaats, rfidtram, rol);
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
        }

        //check of de user bestaat door naam en wachtwoord
        public User CheckForUserNameAndPw(string name, string pw)
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
                            cmd.CommandText = "SELECT * FROM Medewerker WHERE Naam = @naam AND Wachtwoord = @ww";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@naam", name);
                            cmd.Parameters.AddWithValue("@ww", pw);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                reader.Read();

                                int id = reader.GetInt32(0);
                                string naam = reader.GetString(1);
                                UserType rol = (UserType)Enum.Parse(typeof(UserType), reader.GetString(2));
                                string email = reader.GetString(3);
                                string postcode = reader.GetString(4);
                                string woonplaats = reader.GetString(5);
                                string rfid = reader.GetString(6);
                                string ww = reader.GetString(7);

                                conn.Close();
                                return new User(id, naam, ww, email, postcode, woonplaats, rfid, rol);
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
            }
            return null;
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
                            cmd.CommandText = "SELECT * FROM Medewerker WHERE MedewekerID = @ID AND Gebruikersnaam = @gebrn AND Wachtwoord = @ww";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@ID", user.Personeelsnr);
                            cmd.Parameters.AddWithValue("@gebrn", user.Naam);
                            cmd.Parameters.AddWithValue("@ww", user.Wachtwoord);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                reader.Read();

                                int id = reader.GetInt32(0);
                                string naam = reader.GetString(1);
                                UserType rol = (UserType)Enum.Parse(typeof(UserType), reader.GetString(2));
                                string email = reader.GetString(3);
                                string postcode = reader.GetString(4);
                                string woonplaats = reader.GetString(5);
                                string rfid = reader.GetString(6);
                                string ww = reader.GetString(7);

                                conn.Close();
                                return new User(id, naam, ww, email, postcode, woonplaats, rfid, rol);
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
                            cmd.CommandText = "INSERT INTO Medewerker (Naam, Functie, E-Mail, Postcode, Woonplaats, RFID, Wachtwoord) VALUES (@naam, @rol, @mail, @post, @woon, @rfid, @ww";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@naam", user.Naam);
                            cmd.Parameters.AddWithValue("@rol", user.Rol);
                            cmd.Parameters.AddWithValue("@mail", user.Email);
                            cmd.Parameters.AddWithValue("@post", user.Postcode);
                            cmd.Parameters.AddWithValue("@woon", user.Woonplaats);
                            cmd.Parameters.AddWithValue("@rfid", user.Rfid);
                            cmd.Parameters.AddWithValue("@ww", user.Wachtwoord);

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
                            cmd.CommandText = "DELETE FROM Medewerker WHERE Naam = @naam AND Wachtwoord = @ww";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@naam", user.Naam);
                            cmd.Parameters.AddWithValue("@ww", user.Wachtwoord);

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
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
                            cmd.CommandText = "UPDATE Medewerker SET Naam = @naam, Functie = @functie, [E-mail] = @email, Wachtwoord = @ww WHERE MedewerkerID = @mID";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@functie", user.Rol.ToString());
                            cmd.Parameters.AddWithValue("@email", user.Email);
                            cmd.Parameters.AddWithValue("@naam", user.Naam);
                            cmd.Parameters.AddWithValue("@ww", user.Wachtwoord);
                            cmd.Parameters.AddWithValue("@mID", user.Personeelsnr);

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
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
                            cmd.CommandText = "SELECT * FROM Medewerker";
                            cmd.Connection = conn;

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string naam = reader.GetString(1);
                                UserType functie = (UserType)Enum.Parse(typeof(UserType), reader.GetString(2));
                                string email = reader.GetString(3);
                                string postcode = reader.GetString(4);
                                string woonplaats = reader.GetString(5);
                                string rfid = reader.GetString(6);
                                string wachtwoord = reader.GetString(7);
                                users.Add(new User(id, naam, wachtwoord, email, postcode, woonplaats, rfid, functie));
                            }
                            return users;
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                            conn.Close();
                            return null;
                        }
                    }
                }
                return null;
            }
        }
    }
}
