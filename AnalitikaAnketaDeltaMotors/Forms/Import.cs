﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AnalitikaAnketaDeltaMotors.Classes;
using UnitOfWorkExample.UnitOfWork;
using UnitOfWorkExample.UnitOfWork.Models;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class Import : Form
    {
        ExcelHelper helper;
        DataTable dt = new DataTable();
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
            if (txtIzborFajla.Text.Trim() == "")
            {
                MessageBox.Show("MORATE imati putanju excel file-a kako bi ucitali podatke");
            }
            else
            {
                helper.FilePath = txtIzborFajla.Text;
                dt = helper.ImportData();
                dataGridView1.DataSource = dt;
                DataGridFormat();
            }           
        }
        private void DataGridFormat()
        { 
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Visible = false;           
            for (int i = 0; i < dataGridView1.Columns.Count - 2;i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ImportData newImportData = new ImportData();
            newImportData.ImportDate = DateTime.Now;
            newImportData.Description = "";
            List<Entry> entries = new List<Entry>();
            foreach (DataRow row in dt.Rows)
            {
                Entry newEntry = new Entry();
                newEntry.CreatedAt = DateTime.Parse(row[0].ToString());
                newEntry.Ocena = int.Parse(row[1].ToString());
                newEntry.Odgovor = row[2].ToString();
                newEntry.PredlogPoboljsanja = row[3].ToString();
                newEntry.Kontakt = row[4].ToString();
                if (DoesRecordExist(newEntry))
                {
                    entries.Add(newEntry);                  
                }
            }
            newImportData.Entries = entries;
            AddRecord(newImportData);
            MessageBox.Show("Podaci su sacuvani");
        }
        public void AddRecord(ImportData newImportData)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.ImportDatas.Add(newImportData);
                db.SaveChanges();
            }
        }
        public bool DoesRecordExist(Entry newEntry)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                if (db.Entries.Any(c => c.CreatedAt == newEntry.CreatedAt && c.Ocena == newEntry.Ocena && c.Kontakt == newEntry.Kontakt))
                    return false;
                else
                    return true;
            }            
        }
    }
}

