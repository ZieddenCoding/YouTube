using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test = System;
using IO = System.IO;

namespace YoutubeProjekt1
{
    public class Program
    {
        static List<string> AllDirs = new List<string>();
        static List<string> AllFiles = new List<string>();
        static void Main(string[] args)
        {

            string path = @"I:\ZieddenCodding\Test\";
            string[] rootDirs = IO.Directory.GetDirectories(path);
            string[] rootFiles = IO.Directory.GetFiles(path, "*.txt");
            AllDirs = rootDirs.ToList<string>();
            AllFiles = rootFiles.ToList<string>();

            foreach(string dir in rootDirs)
            {
                FindDirs(dir);
            }            

            foreach(string adirs in AllDirs)
            {
                string[] tempFiles = IO.Directory.GetFiles(adirs, "*.txt");
                foreach(string tfile in tempFiles)
                {
                    AllFiles.Add(tfile);
                }
            }

            Console.Write("Geben sie ihr Suchword ein: ");
            string Keyword = Console.ReadLine();

            foreach(string afile in AllFiles)
            {
                string FileText = IO.File.ReadAllText(afile);
                string lowerFileText = FileText.ToLower();
                string lowerKeyword = Keyword.ToLower();
                if (lowerFileText.Contains(lowerKeyword))
                {
                    Console.WriteLine(afile);
                }
            }

            Console.WriteLine("Finish!");
            Console.ReadKey();
        }

        static void FindDirs(string path)
        {
            string[] tempDir = IO.Directory.GetDirectories(path);
            foreach(string tdir in tempDir)
            {
                AllDirs.Add(tdir);
                HandleFindDirs(tdir);
            }
        }

        static void HandleFindDirs(string path)
        {
            FindDirs(path);
        }

    }

}
