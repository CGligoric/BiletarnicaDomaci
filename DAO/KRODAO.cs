using Biletarnica.Moduli;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biletarnica.DAO
{
    class KRODAO
    {
        public static KRO GetByKardaId(SqlConnection conn, int id)
        {
            KRO kro = null;

            try
            {
                string query = "SELECT * FROM kro WHERE id = " + id;
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    int rezId = (int)r["rezervacija_id"];
                    int osobaId = (int)r["osoba_id"];

                    Karta karta = KartaDAO.GetById(conn, id);
                    Rezervacija rez = RezervacijaDAO.GetById(conn, rezId);
                    Osoba osb = OsobaDAO.GetOsobaById(conn, osobaId);

                    kro = new KRO(karta, osb, rez);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return kro;
        }

        public static List<KRO> GetAll(SqlConnection conn)
        {
            List<KRO> sviKro = new List<KRO>();

            try
            {
                string query = "SELECT * FROM kro";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    int kid = (int)r["karta_id"];
                    int rezId = (int)r["rezervacija_id"];
                    int osobaId = (int)r["osoba_id"];

                    Karta karta = KartaDAO.GetById(conn, kid);
                    Rezervacija rez = RezervacijaDAO.GetById(conn, rezId);
                    Osoba osb = OsobaDAO.GetOsobaById(conn, osobaId);

                    sviKro.Add(new KRO(karta, osb, rez));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return sviKro;
        }

        public static bool Add(SqlConnection conn, KRO kro)
        {
            bool retVal = false;

            try
            {
                string query = "INSERT INTO kro VALUES (@karta_id,@rezervacija_id,@osoba_id)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@karta_id", kro.Karta.Id);
                cmd.Parameters.AddWithValue("@rezervacija_id", kro.Rezervacija.Id);
                cmd.Parameters.AddWithValue("@osoba_id", kro.Osoba.Id);

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

        public static bool Update(SqlConnection conn, KRO kro)
        {
            bool retVal = false;

            try
            {
                string query = "UPDATE kro SET karta_id=@karta_id,rezervacija_id=@rezervacija_id,osoba_id=@osoba_id)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@karta_id", kro.Karta.Id);
                cmd.Parameters.AddWithValue("@rezervacija_id", kro.Rezervacija.Id);
                cmd.Parameters.AddWithValue("@osoba_id", kro.Osoba.Id);

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

        public static bool Add(SqlConnection conn, int id)
        {
            bool retVal = false;

            try
            {
                string query = "DELETE FROM kro WHERE id = " + id;
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
