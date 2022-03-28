using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnitOfWorkExample.UnitOfWork;
using System.Data.Entity;
using UnitOfWorkExample.UnitOfWork.Models;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class IzmenaAnketa : Form
    {
        DatabaseContext dbContext;
        public IzmenaAnketa()
        {
            InitializeComponent();
        }
        private void intializeDataGrid(DataGridView dataGrid)
        {
            dataGridView1.AutoGenerateColumns = true;

            dataGridView1.Columns["CreatedAt"].HeaderText = "Datum";
            dataGridView1.Columns["Odgovor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Ocena"].Visible = true;
            dataGridView1.Columns["PredlogPoboljsanja"].Visible = true;
            dataGridView1.Columns["Kontakt"].Visible = true;
            dataGridView1.Columns["ImportDataId"].Visible = false;
            dataGridView1.Columns["ImportData"].Visible = false;
            dataGridView1.Columns["EntryScores"].Visible = false;
            dataGridView1.Columns["Id"].ReadOnly = true;

            dataGridView1.DataError += DataGrid_DataError;
            dataGridView1.Paint += DataGridView1_Paint; ;
            //dataGridView1.DoubleClick += DataGrid_DoubleClick;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightGreen;
        }

        private void DataGridView1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void DataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void bUcitaj_Click(object sender, EventArgs e)
        {
            
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
            dbContext = new DatabaseContext();

            dataGridView1.AutoGenerateColumns = true;

            var temp = dbContext.Entries
                .Include(x => x.ImportData.Tags.Select(y => y.Group))
                .Include(x => x.EntryScores
                    .Select(y => y.Subtopic)
                    .Select(z => z.Topic))
                .Where(x => x.CreatedAt >= dateTimePicker1.Value &&
                            x.CreatedAt <= dateTimePicker2.Value).ToList();

            dataGridView1.DataSource = temp;

            intializeDataGrid(dataGridView1);
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            if (dbContext.ChangeTracker.HasChanges())
            {
                if (MessageBox.Show("Da li ste sigurni?", "Izmena anketa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dbContext.SaveChanges();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cell.Style.BackColor = Color.White;
                        }
                    }
                }
            }
        }

        private void IzmenaAnketa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dbContext != null && dbContext.ChangeTracker.HasChanges())
            {
                if (MessageBox.Show("Postoje izmene. Da li zelite da ih sacuvate?", "Izmena anketa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dbContext.SaveChanges();
                }
                else
                {
                    dbContext.Dispose();
                }
            }
        }
    }
}
