using System;
using System.Data.Entity;
using System.Linq;
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
            dataGridView1.Columns["Tags"].Visible = false;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Id=int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            context.Tags.Load();
            var filteredData = context.Tags.Local.ToBindingList().Where(x => x.GroupId == Id);
            dataGridView2.DataSource = filteredData.Count() > 0 ?
                filteredData : filteredData.ToArray();
            dataGridView2.DataSource = context.Tags.Local.ToBindingList();

            //dataGridView2.DataSource = context.Tags.Local.ToBindingList().Where(x => x.GroupId == Id).ToList();
            //dataGridView2.Columns["Name"].HeaderText = "Naziv";
            //dataGridView2.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridView2.Columns["Id"].Visible = false;
            //dataGridView2.Columns["GroupId"].Visible = false;
            //dataGridView2.Columns["Group"].Visible = false;
            //dataGridView2.Columns["ImportDatas"].Visible = false;
        }
    }
}
