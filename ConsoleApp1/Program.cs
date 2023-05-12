using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {




        static void Main(string[] args)
        {

            List<string[]> autok = FajlBeolvasasa("autok.csv");
            List<string[]> igenyek = FajlBeolvasasa("igenyek.csv");


            HirdetokSzama(autok, "2. Feladat");

            FerohelyekSzama(autok, "3. Feladat");

            LegtobbFerohely(autok, "4. Feladat");

            IgenyTalalat(igenyek, autok, "5. Feladat");

            // 6. feladat az 5. feladatban metódusként van meghivva


            Console.ReadKey();

        }



        public static List<string[]> FajlBeolvasasa(string path)
        {
            StreamReader reader = new StreamReader(path, Encoding.Default);
            string line;
            List<string[]> list = new List<string[]>();
            while ((line = reader.ReadLine()) != null)
            {
                string[] splitter = line.Split(';');
                list.Add(splitter);
            }

            reader.Close();
            return list;
        }




        public static void HirdetokSzama(List<string[]> lista, string feladat)
        {
            List<string> hirdetok = new List<string>();

            Console.WriteLine(feladat);
            for (int i = 1; i < lista.Count; i++)
            {
                string telefon = lista.ElementAt(i)[3];

                if (!hirdetok.Contains(telefon))
                {
                    hirdetok.Add(telefon);
                }
            }
            Console.WriteLine("{0} autós hirdet fuvart", hirdetok.Count);
            Console.WriteLine("");
        }



        public static void FerohelyekSzama(List<string[]> lista, string feladat)
        {
            Console.WriteLine(feladat);
            int ferohelyek = 0;
            for (int i = 1; i < lista.Count; i++)
            {
                if(lista.ElementAt(i)[0] == "Budapest" && lista.ElementAt(i)[1] == "Miskolc")
                {
                    ferohelyek += int.Parse(lista.ElementAt(i)[4]);
                }
            }
            Console.WriteLine("Összesen {0} férőhelyet hirdettek az autósok Budapestről Miskolcra", ferohelyek);
            Console.WriteLine("");
        }



        public static void LegtobbFerohely(List<string[]> lista, string feladat)
        {
            Console.WriteLine(feladat);

            int maxHelyek = 0;
            string indulo = "";
            string cel = "";

            for (int i = 1; i < lista.Count; i++)
            {
                
                if(int.Parse(lista.ElementAt(i)[4]) > maxHelyek)
                {
                    maxHelyek = int.Parse(lista.ElementAt(i)[4]);
                    indulo = lista.ElementAt(i)[0];
                    cel = lista.ElementAt(i)[1];
                }
            }
            Console.WriteLine("A legtöbb férőhelyet ({0}-et) a {1}-{2} útvonalon ajánlották fel a hirdetők", maxHelyek, indulo, cel);
            Console.WriteLine("");
        }



        public static void IgenyTalalat(List<string[]> igenyek, List<string[]> autok, string feladat)
        {
            Console.WriteLine(feladat);

            for (int i = 1; i < igenyek.Count; i++)
            {
                string indulo = igenyek.ElementAt(i)[1];
                string cel = igenyek.ElementAt(i)[2];
                int szemelyek = int.Parse(igenyek.ElementAt(i)[3]);
                bool talalat = false;

                for(int j = 0; j < autok.Count; j++)
                {
                    string start = autok.ElementAt(j)[0];
                    string end = autok.ElementAt(j)[1];
                    int place = int.Parse(autok.ElementAt(i)[4]);

                    if(indulo == start && end == cel && place >= szemelyek)
                    {
                        talalat = true;
                        Console.WriteLine("{0} => {1} ", igenyek.ElementAt(i)[0], autok.ElementAt(j)[2]);
                        UtasUzenet(igenyek.ElementAt(i)[0] + ": Rendszám: " + autok.ElementAt(j)[2] + ", Telefonszám: " + autok.ElementAt(j)[3]);
                        break;
                    }
                }

                if (!talalat)
                {
                    UtasUzenet(igenyek.ElementAt(i)[0] + ": Sajnos nem sikerült autót találni");
                }
            }
            Console.WriteLine("");
        }


        public static void UtasUzenet(string uzenet)
        {
            if (!File.Exists("utasuzenet.txt"))
            {
                StreamWriter sw = File.CreateText("utasuzenet.txt");
                sw.WriteLine(uzenet);
                sw.Close();
            } else
            {
                StreamWriter sw = File.AppendText("utasuzenet.txt");
                sw.WriteLine(uzenet);
                sw.Close();
            }
        }

    }
}
