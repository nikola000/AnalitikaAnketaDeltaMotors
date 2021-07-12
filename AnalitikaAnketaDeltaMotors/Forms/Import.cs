using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AnalitikaAnketaDeltaMotors.Classes;
using AnalitikaAnketaDeltaMotors.Controls;
using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using UnitOfWorkExample.UnitOfWork;
using UnitOfWorkExample.UnitOfWork.Models;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class Import : Form
    {
        ExcelHelper helper;
        DataTable dt = new DataTable();
        TagBookmarks tagBookmarks;
        public List<Tag> Tags { get; set; } = new List<Tag>();
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

        int _maximum = 0;
        private void bUcitaj_Click(object sender, EventArgs e)
        {
            if (txtIzborFajla.Text.Trim() == "")
            {
                MessageBox.Show("Morate izbrati excel file", "Upozorenje", MessageBoxButtons.OK);
            }
            else
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = 0;
                helper.FilePath = txtIzborFajla.Text;
                dt = helper.ImportData(ref _maximum, UpdateProgressBar);
                dataGridView1.DataSource = dt;
            }
        }

        private void UpdateProgressBar(int value)
        {
            if (progressBar1.Maximum == 0)
            {
                progressBar1.Maximum = _maximum;
            }
            progressBar1.Value = value;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (DatabaseContext db = new DatabaseContext())
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

                    if (db.Entries.Any(c => c.CreatedAt == newEntry.CreatedAt && c.Ocena == newEntry.Ocena && c.Kontakt == newEntry.Kontakt))
                    {
                        entries.Add(newEntry);
                    }
                }
                newImportData.Entries = entries;
                newImportData.Tags = Tags;
                db.ImportDatas.Add(newImportData);
                db.SaveChanges();
            }
            MessageBox.Show("Podaci su sacuvani");
        }

        private void bAddTag_Click(object sender, EventArgs e)
        {
            tagBookmarks = new TagBookmarks(Tags);
            var result=tagBookmarks.ShowDialog();
            if (result==DialogResult.Cancel)
            {
                AddTags();
            }
        }
        public void AddTags()
        {
            panelTags.Controls.Clear();
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.AutoSize = true;
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            Tags = tagBookmarks.Tags;
            foreach (var item in Tags)
            {
                Button button = new Button();
                button.Name = item.Id.ToString();
                button.Text = item.Name;
                flowLayoutPanel.Controls.Add(button);
            }
            panelTags.Controls.Add(flowLayoutPanel);
        }
    }
}

