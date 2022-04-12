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

namespace Zeiterfassung
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string AufgabenName_Create = "";
        private void button1_Click(object sender, EventArgs e)
        {
            Hinzufügen h = new Hinzufügen(this);
            if (h.ShowDialog() == DialogResult.OK)
            {
                DB.Aufgaben aufgaben = new DB.Aufgaben();
                aufgaben.Aufgabe = AufgabenName_Create;
                aufgaben.Teilaufgaben = new List<int>();
                aufgaben.ComboBox = new List<string>();

                var col = Program.DB.GetCollection<DB.Aufgaben>();
                col.Insert(aufgaben);
                MessageBox.Show("Die Aufgabe wurde erfolgreich hinzugefügt","Erfolg",MessageBoxButtons.OK,MessageBoxIcon.Information);
                refresh_ListView();
            }
        }

        private void refresh_ListView()
        {
            listView1.Items.Clear();
            var col = Program.DB.GetCollection<DB.Aufgaben>();
            DB.Aufgaben[] aufgaben = col.FindAll().ToArray();
            foreach(DB.Aufgaben aufgabe in aufgaben)
            {
                ListViewItem lvi = listView1.Items.Add(aufgabe.ID.ToString());
                lvi.SubItems.Add(aufgabe.Aufgabe);
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            refresh_ListView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].Text);
                var col = Program.DB.GetCollection<DB.Aufgaben>();
                DB.Aufgaben aufgabe = col.FindById(id);
                if(MessageBox.Show($"Wollen sie die Aufgabe \"{aufgabe.Aufgabe}\" wirklich löschen","Löschen?",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    col.Delete(id);
                    MessageBox.Show("Die Aufgabe wurde erfolgreich gelöscht.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refresh_ListView();
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].Text);
                var col = Program.DB.GetCollection<DB.Aufgaben>();
                DB.Aufgaben aufgabe = col.FindById(id);
                Verwaltung verwaltung = new Verwaltung(aufgabe);
                verwaltung.Show();
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].Text);
                var col = Program.DB.GetCollection<DB.Aufgaben>();
                DB.Aufgaben aufgabe = col.FindById(id);

                Excel.Application app = new Excel.Application();
                app.Visible = true;
                Excel.Workbook workbook = app.Workbooks.Open($"{System.IO.Directory.GetCurrentDirectory()}\\Forlage.xlsx");
                Excel.Worksheet sheet = workbook.Sheets[1];
                sheet.Name = aufgabe.Aufgabe;
                int excelcounter = 5;
                float gesamtpreis = 0f;
                sheet.Range["B1"].Value = aufgabe.Aufgabe;
                foreach (int aid in aufgabe.Teilaufgaben)
                {
                    var tcol = Program.DB.GetCollection<DB.Teilaufgaben>();
                    DB.Teilaufgaben taufgabe = tcol.FindById(aid);
                    TimeSpan time = new TimeSpan(0);
                    foreach (DateTime dt in taufgabe.Zeitspanne.Keys)
                    {
                        if (!(taufgabe.Zeitspanne[dt] == (new DateTime(0))))
                        {
                            TimeSpan buff = taufgabe.Zeitspanne[dt].Subtract(dt);
                            time = time + buff;
                        }
                    }

                    float Preis = (float)Convert.ToDouble(sheet.Range["D2"].Value);
                    float preisoffset = Preis / 60 / 60;
                    float PreisZeit = preisoffset * (float)time.TotalSeconds;
                    gesamtpreis = gesamtpreis + PreisZeit;
                    sheet.Range[$"A{excelcounter}"].Value = taufgabe.ID;
                    sheet.Range[$"B{excelcounter}"].Value = taufgabe.Name;
                    sheet.Range[$"C{excelcounter}"].Value = $"{string.Format("{0:0}", time.TotalHours) }:{time.Minutes}:{time.Seconds}";
                    sheet.Range[$"D{excelcounter}"].Value = $"{string.Format("{0:0.00}", PreisZeit)}€";
                    excelcounter++;
                }
                sheet.Range[$"C{excelcounter}"].Value = "Gesamt:";
                sheet.Range[$"D{excelcounter}"].Value = $"{string.Format("{0:0.00}", gesamtpreis)}€";
            }
            catch { }

        }
    }
}
