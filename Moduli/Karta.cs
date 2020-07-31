using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biletarnica.Moduli
{
    class Karta
    {
        public int Id { get; set; }
        public string NazDog { get; set; }
        public string Tip { get; set; }
        public int BrojReda { get; set; }
        public int BrojSed { get; set; }
        public string Sektor { get; set; }
        public int Cena { get; set; }

        public Karta()
        {

        }

        public Karta(int id, string ng, string tip, int br, int bs, string skt, int cena)
        {
            Id = id;
            NazDog = ng;
            Tip = tip;
            BrojReda = br;
            BrojSed = bs;
            Sektor = skt;
            Cena = cena;
        }

        public override string ToString()
        {
            return String.Format("ID: {0} | Naziv dogadjaja: {1} | Tip karte: {2} | Red: {3} | Sediste: {4} | Sektor: {5} | Cena: {6}",
                Id, NazDog, Tip, BrojReda, BrojSed, Sektor, Cena);

        }
    }
}
