using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biletarnica.Moduli
{
    class KRO
    {
        public Karta Karta { get; set; }
        public Osoba Osoba { get; set; }
        public Rezervacija Rezervacija { get; set; }

        public KRO()
        {

        }

        public KRO(Karta karta, Osoba osoba, Rezervacija rez)
        {
            Karta = karta;
            Osoba = osoba;
            Rezervacija = rez;
        }
    }
}
