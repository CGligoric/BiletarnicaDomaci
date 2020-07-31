using Biletarnica.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biletarnica
{
    class Program
    {
        public static SqlConnection conn;
        private static void LoadConnection()
        {
            try
            {
                string connectionStringZaPoKuci = "Data Source=.\\SQLEXPRESS;Initial Catalog=Biletarnica;Integrated Security=True;MultipleActiveResultSets=True";

                // Parametar "MultipleActiveResultSets=True" je neophodan kada zelimo da imamo istovremeno
                // otvorena dva data readera ka bazi podataka. Zasto je u ovom programu to neophodno?
                conn = new SqlConnection(connectionStringZaPoKuci);
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        static void Main(string[] args)
        {
            LoadConnection();

            KROUI.IspisiKompletMeni();
        }
    }
}
