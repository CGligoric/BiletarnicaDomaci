using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biletarnica.Moduli
{
    class Rezervacija
    {
        public int Id { get; set; }
        public string Datum { get; set; }

        public Rezervacija()
        {

        }

        public Rezervacija(int id, string dtm)
        {
            Id = id;
            Datum = dtm;
        }

        public override string ToString()
        {
            return String.Format("ID: {0} | Datum: {1}", Id, Datum);   
        }
    }
}
