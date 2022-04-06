using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace TestFormsProject
{
    public partial class Form1 : Form
    {
        LiteDB.LiteDatabase DB;
        public Form1()
        {
            InitializeComponent();
        }


        public void Form1_Load(object sender, EventArgs e)
        {
            DB = new LiteDB.LiteDatabase(@"lite.db");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Name = textBox1.Text;
            string Nachname = textBox2.Text;
            int Alter = (int)numericUpDown1.Value;
            var col = DB.GetCollection<KundenDaten>();

            col.Insert(new KundenDaten(Name, Nachname, Alter));

            button1_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            var col = DB.GetCollection<KundenDaten>();

            KundenDaten[] datas = col.FindAll().ToArray();

            foreach(KundenDaten data in datas)
            {
                ListViewItem lvi = listView1.Items.Add(data.ID.ToString());
                lvi.SubItems.Add(data.Name);
                lvi.SubItems.Add(data.Nachname);
                lvi.SubItems.Add(data.Alter.ToString());
                lvi.SubItems.Add(data.U30());
            }
        }

        public void button3_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook workbook = app.Workbooks.Add();
            Excel.Worksheet sheet = workbook.Sheets.Add();
            sheet.Name = "Export";


            //////Export Header//////////
            for(int i = 0;i<listView1.Columns.Count;i++)
            {
                char Buchstabe = (char)(65 + i);
                sheet.Range[$"{Buchstabe.ToString()}1"].Value = listView1.Columns[i].Text;
            }


            ////////Export Datas////////
            int counter = 2;
            foreach(ListViewItem lvi in listView1.Items)
            {                
                int countercolum = 65;
                foreach(ListViewItem.ListViewSubItem slvi in lvi.SubItems)
                {
                    sheet.Range[$"{((char)countercolum).ToString()}{counter}"].Value = slvi.Text;
                    countercolum++;
                }

                counter++;
            }
        }
    }

    public class KundenDaten
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Nachname { get; set; }
        public int Alter { get; set; }

        public KundenDaten(string Name,string Nachname,int Alter)
        {
            this.Name = Name;
            this.Nachname = Nachname;
            this.Alter = Alter;
        }

        public string U30()
        {
            if(Alter>=30)
            {
                return "Ja";
            }
            return "Nein";
        }
    }
}
