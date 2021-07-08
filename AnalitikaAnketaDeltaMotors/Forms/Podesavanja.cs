﻿using AnalitikaAnketaDeltaMotors.Classes;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class Podesavanja : Form
    {
        public Podesavanja()
        {
            InitializeComponent();
            UcitajFajl();
        }

        private void UcitajFajl()
        {
            textBox1.Text = ConfigHelper.ReadConfigFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigHelper.WriteConfigFile(textBox1.Text);
            Configuration.GetInstance().ResetConnectionString();
        }
        
    }
}
