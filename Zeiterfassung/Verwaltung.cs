using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zeiterfassung
{
    public partial class Verwaltung : Form
    {
        public DB.Aufgaben Aufgabe;
        public Verwaltung(DB.Aufgaben aufgabe)
        {
            Aufgabe = aufgabe;
            InitializeComponent();
        }

        Dictionary<int, bool> IsRunning = new Dictionary<int, bool>();
        private void Verwaltung_Load(object sender, EventArgs e)
        {
            this.Text = $"Verwaltung: {Aufgabe.Aufgabe}";
            refresh_List();
            try
            {
                comboBox1.Items.Clear();
                foreach(string ctext in Aufgabe.ComboBox)
                {
                    comboBox1.Items.Add(ctext);
                }
            }
            catch { Aufgabe.ComboBox = new List<string>(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var col = Program.DB.GetCollection<DB.Aufgaben>();
                string Text = comboBox1.Text;
                if(!(IsInList(Aufgabe.ComboBox,Text)))
                {
                    Aufgabe.ComboBox.Add(Text);
                    
                    

                    comboBox1.Items.Clear();
                    foreach (string ctext in Aufgabe.ComboBox)
                    {
                        comboBox1.Items.Add(ctext);
                    }
                }


                DB.Teilaufgaben teilaufgabe = new DB.Teilaufgaben();
                teilaufgabe.Name = Text;
                teilaufgabe.Zeitspanne = new Dictionary<DateTime, DateTime>();

                var colteilaufgabe = Program.DB.GetCollection<DB.Teilaufgaben>();

                int id = colteilaufgabe.Insert(teilaufgabe);
                Aufgabe.Teilaufgaben.Add(id);
                col.Update(Aufgabe);
                refresh_List();
            }
            catch { }
        }


        private void refresh_List()
        {
            listView1.Items.Clear();
            var col = Program.DB.GetCollection<DB.Teilaufgaben>();
            foreach (int id in Aufgabe.Teilaufgaben)
            {
                DB.Teilaufgaben taufgabe = col.FindById(id);
                ListViewItem lvi = listView1.Items.Add(taufgabe.ID.ToString());
                lvi.SubItems.Add(taufgabe.Name);
                TimeSpan time = new TimeSpan(0);
                bool buffisrunnig = false;
                foreach(DateTime dt in taufgabe.Zeitspanne.Keys)
                {
                    if(!(taufgabe.Zeitspanne[dt] == (new DateTime(0))))
                    {
                        TimeSpan buff = taufgabe.Zeitspanne[dt].Subtract(dt);
                        time = time + buff;
                    }
                    else
                    {
                        buffisrunnig = true;
                    }
                }
                if(buffisrunnig)
                {
                    lvi.BackColor = Color.Green;
                }
                AddToIsRunning(taufgabe.ID, buffisrunnig);
                lvi.SubItems.Add(time.ToString(@"dd\ hh\:mm\:ss"));
            }
        }

        private bool IsInList(List<string> list, string keyword)
        {
            foreach(string word in list)
            {
                if(word.ToLower().Equals(keyword.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }

        private void AddToIsRunning(int id,bool value)
        {
            bool buff;
            if(IsRunning.TryGetValue(id,out buff))
            {
                IsRunning[id] = value;
            }
            else
            {
                IsRunning.Add(id, value);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            refresh_List();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].Text);
                if(IsRunning[id])
                {
                    button2.Enabled = false;
                    button3.Enabled = true;
                }
                else
                {
                    button2.Enabled = true;
                    button3.Enabled = false;
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].Text);
                var col = Program.DB.GetCollection<DB.Teilaufgaben>();
                DB.Teilaufgaben taufgabe = col.FindById(id);
                taufgabe.Zeitspanne.Add(DateTime.Now, new DateTime(0));
                col.Update(taufgabe);
                refresh_List();
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].Text);
                var col = Program.DB.GetCollection<DB.Teilaufgaben>();
                DB.Teilaufgaben taufgabe = col.FindById(id);
                //taufgabe.Zeitspanne.Add(DateTime.Now, new DateTime(0));
                for(int i = 0;i<taufgabe.Zeitspanne.Keys.ToArray<DateTime>().Length;i++)
                {
                    DateTime key = taufgabe.Zeitspanne.Keys.ToArray<DateTime>()[i];
                    if(taufgabe.Zeitspanne[key] == (new DateTime(0)))
                    {
                        taufgabe.Zeitspanne[key] = DateTime.Now;
                    }

                }
                col.Update(taufgabe);
                refresh_List();
            }
            catch { }
        }
    }
}
