using Biletarnica.Moduli;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biletarnica.DAO
{
    class KartaDAO
    {
        public static Karta GetById(SqlConnection conn, int id)
        {
            Karta karta = null;

            try
            {
                string query = "SELECT naz_dog,tip,b_reda,b_sedista,sektor,cena FROM karta WHERE id = " + id;
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    string naz_dog = (string)r["naz_dog"];
                    string tip = (string)r["tip"];
                    int br = (int)r["b_reda"];
                    int bs = (int)r["b_sedista"];
                    string skt = (string)r["sektor"];
                    int cena = (int)r["cena"];

                    karta = new Karta(id, naz_dog, tip, br, bs, skt, cena);
                }
                r.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return karta;
        }

        public static List<Karta> GetAll(SqlConnection conn)
        {
            List<Karta> sveKarte = new List<Karta>();

            try
            {
                string query = "SELECT * FROM karta";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    int id = (int)r["id"];
                    string naz_dog = (string)r["naz_dog"];
                    string tip = (string)r["tip"];
                    int br = (int)r["b_reda"];
                    int bs = (int)r["b_sedista"];
                    string skt = (string)r["sektor"];
                    int cena = (int)r["cena"];

                    sveKarte.Add(new Karta(id, naz_dog, tip, br, bs, skt, cena));
                }
                r.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return sveKarte;
        }

        public static bool Add(SqlConnection conn, Karta karta)
        {
            bool retVal = false;

            try
            {
                string query = "INSERT INTO karta VALUES (@id,@naz_dog,@tip,@b_reda,@b_sedista,@sektor,@cena)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", karta.Id);
                cmd.Parameters.AddWithValue("@naz_dog", karta.NazDog);
                cmd.Parameters.AddWithValue("@tip", karta.Tip);
                cmd.Parameters.AddWithValue("@b_reda", karta.BrojReda);
                cmd.Parameters.AddWithValue("@b_sedista", karta.BrojSed);
                cmd.Parameters.AddWithValue("@sektor", karta.Sektor);
                cmd.Parameters.AddWithValue("@cena", karta.Cena);


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

        public static bool Update(SqlConnection conn, Karta karta)
        {
            bool retVal = false;

            try
            {
                string query = "UPDATE karta SET @naz_dog,@tip,@b_reda,@b_sedista,@sektor,@cena WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", karta.Id);
                cmd.Parameters.AddWithValue("@naz_dog", karta.NazDog);
                cmd.Parameters.AddWithValue("@tip", karta.Tip);
                cmd.Parameters.AddWithValue("@b_reda", karta.BrojReda);
                cmd.Parameters.AddWithValue("@b_sedista", karta.BrojSed);
                cmd.Parameters.AddWithValue("@sektor", karta.Sektor);
                cmd.Parameters.AddWithValue("@cena", karta.Cena);

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
                string query = "DELETE FROM karta WHERE id = " + id;
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

        public static List<Karta> GetKarteByNaziv(SqlConnection conn, string unos)
        {
            List<Karta> sveKarte = new List<Karta>();

            try
            {
                string query = "SELECT * FROM karta WHERE naz_dog LIKE '%" + unos + "%'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    int id = (int)r["id"];
                    string naz_dog = (string)r["naz_dog"];
                    string tip = (string)r["tip"];
                    int br = (int)r["b_reda"];
                    int bs = (int)r["b_sedista"];
                    string skt = (string)r["sektor"];
                    int cena = (int)r["cena"];

                    sveKarte.Add(new Karta(id, naz_dog, tip, br, bs, skt, cena));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return sveKarte;
        }
    }
}
