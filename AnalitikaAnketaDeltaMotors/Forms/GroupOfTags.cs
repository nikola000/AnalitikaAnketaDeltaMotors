using System;
using System.Data.Entity;
using System.Windows.Forms;
using UnitOfWorkExample.UnitOfWork;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class GroupOfTags : Form
    {
        DatabaseContext context = new DatabaseContext();
        public GroupOfTags()
        {
            InitializeComponent();
        }

        private void GroupOfTags_Load(object sender, EventArgs e)
        {
            context.Groups.Include(x => x.Tags).Load();
            SetDataGrid();
        }
        private void SetDataGrid()
        {
            dataGridView1.DataSource = context.Groups.Local.ToBindingList();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {

            dataGridView1.Columns["Name"].HeaderText = "Naziv";
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int result = context.SaveChanges();

            if (result > 0)
            {
                MessageBox.Show("Izmene su uspesno sacuvane");
            }
        }
    }
}
