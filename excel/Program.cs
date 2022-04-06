using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace YoutubeProjekt1
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook workbook = app.Workbooks.Add();
            Excel.Worksheet sheet = workbook.Worksheets.Add();
            sheet.Name = "Test";
            for (int i = 1; i < 11; i++)
            {
                sheet.Range[$"A{i}"].Value = "Test";
            }*/
            Console.WriteLine(((char)65).ToString());
            Console.ReadKey();
            //app.Quit();
        }

    }



}
