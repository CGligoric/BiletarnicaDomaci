using Biletarnica.DAO;
using Biletarnica.Moduli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biletarnica.UI
{
    class KROUI
    {
        static List<KRO> sveKRO = KRODAO.GetAll(Program.conn);
        static List<Karta> sveKarte = KartaDAO.GetAll(Program.conn);
        static List<Rezervacija> sveRez = RezervacijaDAO.GetAll(Program.conn);
        static List<Osoba> sveOsobe = OsobaDAO.GetAll(Program.conn);

        private static void IspisiOsnovniMeni()
        {
            Console.WriteLine("1 - ispis svih karata");
            Console.WriteLine("2 - ispis odredjene rezervacije sa podacima o rezervisanim kartama");
            Console.WriteLine("3 - ispis svih podataka za svaki dogadjaj");
            Console.WriteLine("4 - dodajte novu rezervaciju");
        }

        public static void IspisiKompletMeni()
        {
            bool nastavak = false;

            do
            {
                IspisiOsnovniMeni();
                int izbor = Convert.ToInt32(Console.ReadLine());
                switch (izbor)
                {
                    case 1:
                        IspisiSveKarte();
                        break;
                    case 2:
                        IspisiRezervacijuSaKartomPoId();
                        break;
                    case 3:
                        IspisiPodatkeZaSvakiDogadjaj();
                        break;
                    case 4:
                        NovaRezervacija();
                        break;
                    default:
                        Console.WriteLine("Nevazeca komanda! Probajte ponovo");
                        nastavak = true;
                        break;
                }
                Console.WriteLine("Da li zelite da nastavite s radom? d/n");
                string odg = Console.ReadLine();
                if (odg == "d")
                {
                    nastavak = true;
                }
                else
                {
                    break;
                }
                Console.Clear();
            } while (nastavak);
        }

        public static void NovaRezervacija()
        {
            Console.WriteLine("Unesite svoje ime, a zatim prezime: ");
            string ime = Console.ReadLine();
            string prez = Console.ReadLine();

            Osoba osoba = new Osoba(sveOsobe.Count + 1, ime, prez);
            OsobaDAO.Add(Program.conn, osoba);

            Rezervacija rez = new Rezervacija(sveRez.Count + 1, DateTime.Now.Date.ToString());
            RezervacijaDAO.Add(Program.conn, rez);

            Console.WriteLine("Unesite naziv dogadjaja za koji zelite kartu:");
            string naziv = Console.ReadLine();

            List<Karta> karteZaNaziv = KartaDAO.GetKarteByNaziv(Program.conn, naziv);

            bool opet = true;
            int red = 0;

            Console.WriteLine("Unesite broj karata koji cete rezervisati");
            int brojKarata = Convert.ToInt32(Console.ReadLine());

            do
            {
                Console.WriteLine("Odaberite svoje karte, tako sto cete upisati red u koji zelite da Vas smestimo.");
                red = Convert.ToInt32(Console.ReadLine());

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < karteZaNaziv.Count; i++)
                {
                    sb.Append(karteZaNaziv[i].BrojReda);
                }

                if (sb.ToString().Contains(red.ToString()))
                {
                    int ukupnaCenaRez = 0;

                    for (int i = 0; i < brojKarata; i++)
                    {
                        if (red == karteZaNaziv[i].BrojReda)
                        {
                            ukupnaCenaRez += karteZaNaziv[i].Cena;

                            KRO kro = new KRO(karteZaNaziv[i], osoba, rez);

                            KRODAO.Add(Program.conn, kro);
                        }
                    }
                    Console.WriteLine($"Cena te rezervacije ce biti {ukupnaCenaRez:C}");
                    opet = false;
                }
                else
                {
                    Console.WriteLine("Karte s tim redom su rasprodate. Probajte neki drugi red.");
                }
            } while (opet);

        }
        
        public static void IspisiSveKarte()
        {
            foreach (Karta k in sveKarte)
            {
                Console.WriteLine(k.ToString());
            }
        }

        public static void IspisiRezervacijuSaKartomPoId()
        {
            Console.WriteLine("Upisite ID rezervacije: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Rezervacija rez = RezervacijaDAO.GetById(Program.conn, id);

            Console.WriteLine(rez.ToString());
            Console.WriteLine("Rezervisane karte: ");

            List<Karta> rezervKarte = new List<Karta>();

            foreach (KRO k in sveKRO)
            {
                if (id == k.Rezervacija.Id)
                {
                    rezervKarte.Add(KartaDAO.GetById(Program.conn, k.Karta.Id));
                }
            }
            for (int i = 0; i < rezervKarte.Count; i++)
            {
                Console.WriteLine("\t" + rezervKarte[i].ToString());
            }
        }

        public static void IspisiPodatkeZaSvakiDogadjaj()
        {
            StringBuilder sb = new StringBuilder();
            int brojRezerv = 0;
            int ukBrRezerv = 0;

            for (int i = 0; i < sveKarte.Count; i++)
            {

                if (sb.ToString().Contains(sveKarte[i].NazDog) == false)
                {
                    int brojKarata = 0;
                    int ukupanKes = 0;

                    sb.Append(sveKarte[i].NazDog);
                    Console.WriteLine("\nNAZIV DOGADJAJA - " + sveKarte[i].NazDog);

                    for (int j = 0; j < sveKRO.Count; j++)
                    {
                        if (sveKarte[i].NazDog == sveKarte[j].NazDog)
                        {
                            brojKarata++;
                            ukupanKes += sveKarte[j].Cena;
                        }
                        if (sveKRO[ukBrRezerv].Rezervacija.Id == sveKRO[j].Rezervacija.Id)
                        {
                            brojRezerv = 0;
                            brojRezerv++;
                        }
                    }
                    ukBrRezerv += brojRezerv;
                    Console.WriteLine(" - Ukupan broj karata: " + brojKarata);
                    Console.WriteLine(" - Ukupan broj rezervacija: " + brojRezerv);
                    Console.WriteLine($" - Ukupna zarada: {ukupanKes:c}");
                }
                else
                {
                    sb.Append(sveKarte[i].NazDog);
                }
            }
        }
    }
}
