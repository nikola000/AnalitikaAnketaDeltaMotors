
using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using UnitOfWorkExample.UnitOfWork;
namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class Tagovi : Form
    {
     
        DatabaseContext context = new DatabaseContext();
        public Tagovi()
        {
            InitializeComponent();
        }

        private void grupaTagova_Load(object sender, EventArgs e)
        {
            context.Tags.Include(x => x.Group).Load();
            SetDataGrid();
        }
        private void SetDataGrid()
        {
            dataGridView1.DataSource = context.Tags.Local.ToBindingList();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {   
            dataGridView1.Columns["GroupId"].Visible =false;
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Name"].HeaderText = "Naziv";
            dataGridView1.Columns["Group"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Group"].ReadOnly = true;
            dataGridView1.Columns["ImportDatas"].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in context.ChangeTracker.Entries())
            {
                if (item.State == EntityState.Deleted)
                {
                    int id = (item.Entity as Tag).Id;
                    if (context.ImportDatas.Where(x => x.Tags.Where(c => c.Id == id).ToList().Count > 0).ToList().Count > 0) 
                    {
                        item.State = EntityState.Unchanged;
                    }
                }
            }
             
            int result = context.SaveChanges();
            dataGridView1.Sort(dataGridView1.Columns["Id"],System.ComponentModel.ListSortDirection.Ascending);
            if (result > 0)
            {
                MessageBox.Show("Izmene su uspesno sacuvane");
            }
        }

        private void bAddTag_Click(object sender, EventArgs e)
        {
            AddingTag addingTag = new AddingTag();
            addingTag.ShowDialog();
            context.Tags.Include(x => x.Group).Load();
            SetDataGrid();
        }
    }
}
