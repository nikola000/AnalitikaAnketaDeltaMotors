using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnitOfWorkExample.UnitOfWork;
using UnitOfWorkExample.UnitOfWork.Models;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class Subtopics : Form
    {
        DatabaseContext context = new DatabaseContext();
        public Subtopics()
        {
            InitializeComponent();
        }

        private void Subtopics_Load(object sender, EventArgs e)
        {
            context.Subtopics.Include(x => x.Topic).Load();
            SetDataGrid();
        }
        private void SetDataGrid()
        {
            dataGridView1.DataSource = context.Subtopics.Local.ToBindingList();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.Columns["Name"].HeaderText = "Naziv";
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["TopicId"].Visible = false;
            dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Topic"].ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in context.ChangeTracker.Entries())
            {
                if (item.State == EntityState.Deleted)
                {
                    int id = (item.Entity as Subtopic).Id;
                    if (context.EntryScores.Where(x => x.SubtopicId == id).ToList().Count > 0)
                    {
                        item.State = EntityState.Unchanged;
                    }
                }
            }

            int result = context.SaveChanges();
            dataGridView1.Sort(dataGridView1.Columns["Id"], System.ComponentModel.ListSortDirection.Ascending);
            if (result > 0)
            {
                MessageBox.Show("Izmene su uspesno sacuvane");
            }
        }

        private void bAddSubtopic_Click(object sender, EventArgs e)
        {
            AddingSubtopic addingSubtopic = new AddingSubtopic();
            addingSubtopic.ShowDialog();
            context.Subtopics.Include(x => x.Topic).Load();
            SetDataGrid();
        }
    }
}
