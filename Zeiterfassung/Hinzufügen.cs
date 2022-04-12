﻿using System;
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
    public partial class Hinzufügen : Form
    {
        public MainWindow mw;
        public Hinzufügen(MainWindow mw)
        {
            this.mw = mw;
            InitializeComponent();            
        }

        bool isSettet = false;
        private void Hinzufügen_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mw.AufgabenName_Create = textBox1.Text;
            isSettet = true;
            this.Close();
        }

        private void Hinzufügen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(isSettet)            
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {

                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
