﻿using System;
using System.Windows.Forms;
using AnalitikaAnketaDeltaMotors.Classes;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class Import : Form
    {
        ExcelHelper helper;
        public Import()
        {
            InitializeComponent();
            helper = new ExcelHelper();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFD.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (openFD.ShowDialog() == DialogResult.OK)
            {
                txtIzborFajla.Text = openFD.FileName;
            }
        }

        private void bUcitaj_Click(object sender, EventArgs e)
        {
            helper.FilePath = txtIzborFajla.Text;
            dataGridView1.DataSource = helper.ImportData();
            DataGridFormat();
            }
        private void DataGridFormat()
        { 
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Visible = false;           
            for (int i = 0; i < dataGridView1.Columns.Count - 2;i++)
            {
                double kolone = 1.0 / (dataGridView1.Columns.Count - 1);
                double n1= (dataGridView1.Width * kolone);
                int n= (int)(dataGridView1.Width * (1.0 / (dataGridView1.Columns.Count - 1)));

                dataGridView1.Columns[i].Width = (int)(dataGridView1.Width * (1.0 / (dataGridView1.Columns.Count-1)));
            }
            dataGridView1.Columns[dataGridView1.Columns.Count - 2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        
        }
    }
}

