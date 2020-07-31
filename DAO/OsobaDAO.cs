using Biletarnica.Moduli;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biletarnica.DAO
{
    class OsobaDAO
    {
        public static Osoba GetOsobaById(SqlConnection conn, int id)
        {
            Osoba osoba = null;

            try
            {
                string query = "SELECT ime, prezime FROM osoba WHERE id = " + id;
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    string ime = (string)r["ime"];
                    string prezime = (string)r["prezime"];

                    osoba = new Osoba(id, ime, prezime);
                }
                r.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return osoba;
        }

        public static List<Osoba> GetAll(SqlConnection conn)
        {
            List<Osoba> sveOosobe = new List<Osoba>();

            try
            {
                string query = "SELECT id,ime, prezime FROM osoba;";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    int id = (int)r["id"];
                    string ime = (string)r["ime"];
                    string prezime = (string)r["prezime"];

                    sveOosobe.Add(new Osoba(id, ime, prezime));
                }
                r.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return sveOosobe;
        }

        public static bool Add(SqlConnection conn, Osoba osoba)
        {
            bool retVal = false;

            try
            {
                string query = "INSERT INTO osoba VALUES (@id,@ime,@prezime)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", osoba.Id);
                cmd.Parameters.AddWithValue("@ime", osoba.Ime);
                cmd.Parameters.AddWithValue("@prezime", osoba.Prezime);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    retVal = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public static bool Update(SqlConnection conn, Osoba osoba)
        {
            bool retVal = false;

            try
            {
                string query = "UPDATE osoba SET ime=@ime,prezime=@prezime WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", osoba.Id);
                cmd.Parameters.AddWithValue("@ime", osoba.Ime);
                cmd.Parameters.AddWithValue("@prezime", osoba.Prezime);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    retVal = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public static bool Delete(SqlConnection conn, int id)
        {
            bool retVal = false;

            try
            {
                string query = "DELETE FROM osoba WHERE id = " + id;
                SqlCommand cmd = new SqlCommand(query, conn);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    retVal = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }
    }
}
