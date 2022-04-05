using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace AufgabeJson
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Geben sie \"L\" zum lesen oder \"S\" zum schreiben ein.");
                Console.Write("Eingabe: ");
                string eingabe = Console.ReadLine();
                if (eingabe.ToLower().Equals("L".ToLower()))
                {
                    if (Lesen())
                    {
                        break;
                    }
                }
                else if (eingabe.ToLower().Equals("S".ToLower()))
                {
                    Schreiben();
                    break;
                }
                else
                {
                    Console.WriteLine("Ihre eingabe war nicht korret!");
                }
            }
            Console.WriteLine("Finish");
            Console.Read();
        }

        static bool Lesen()
        {
            if (File.Exists(@"datas.json"))
            {

                List<string> Datas;
                string jdata = File.ReadAllText(@"datas.json");
                Datas = JsonConvert.DeserializeObject<List<string>>(jdata);
                foreach(string data in Datas)
                {
                    Console.WriteLine("-> {0}",data);
                }
                return true;
            }
            else
            {
                Console.WriteLine("Die Datei ist nicht vorhanden! Sie müssen erst etwas eintragen!");
                return false;
            }
        }

        static void Schreiben()
        {
            Console.Write("->: ");
            string eingabe = Console.ReadLine();
            List<string> Datas;

            if(File.Exists(@"datas.json"))
            {
                string jdata = File.ReadAllText(@"datas.json");
                Datas = JsonConvert.DeserializeObject<List<string>>(jdata);
            }
            else
            {
                Datas = new List<string>(); 
            }

            Datas.Add(eingabe);

            File.WriteAllText(@"datas.json", JsonConvert.SerializeObject(Datas));
        }
    }
}
