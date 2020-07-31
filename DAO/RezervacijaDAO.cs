using Biletarnica.Moduli;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biletarnica.DAO
{
    class RezervacijaDAO
    {
        public static Rezervacija GetById(SqlConnection conn, int id)
        {
            Rezervacija rez = null;

            try
            {
                string query = "SELECT * FROM Rezervacija WHERE id = " + id;
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    string datum = (string)r["datum"];
                    
                    rez = new Rezervacija(id, datum);
                }
                r.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return rez;
        }

        public static List<Rezervacija> GetAll(SqlConnection conn)
        {
            List<Rezervacija> sveRez = new List<Rezervacija>();

            try
            {
                string query = "SELECT * FROM Rezervacija";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    int id = (int)r["id"];
                    string datum = (string)r["datum"];

                    sveRez.Add(new Rezervacija(id, datum));
                }
                r.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return sveRez;
        }

        public static bool Add(SqlConnection conn, Rezervacija rez)
        {
            bool retVal = false;

            try
            {
                string query = "INSERT INTO Rezervacija VALUES (@id,@datum)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", rez.Id);
                cmd.Parameters.AddWithValue("@datum", rez.Datum);


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

        public static bool Update(SqlConnection conn, Rezervacija rez)
        {
            bool retVal = false;

            try
            {
                string query = "UPDATE Rezervacija SET datum=@datum WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", rez.Id);
                cmd.Parameters.AddWithValue("@datum", rez.Datum);

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
                string query = "DELETE FROM Rezervacija WHERE id = " + id;
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
