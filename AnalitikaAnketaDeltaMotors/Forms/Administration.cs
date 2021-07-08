using System;
using System.Data.Entity;
using System.Windows.Forms;
using UnitOfWorkExample.UnitOfWork;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class Administration : Form
    {
        AddingUser AddNewUser;
        DatabaseContext context = new DatabaseContext();

        public Administration()
        {
            InitializeComponent();
        }

        private void Administration_Load(object sender, EventArgs e)
        {
            SetDataGrid();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddNewUser = new AddingUser();
            AddNewUser.ShowDialog();
            SetDataGrid();
        }


        private void SetDataGrid()
        {
            context.Users.Load();
            dataGridView1.DataSource = context.Users.Local.ToBindingList();
            InitializeDataGridView();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int result = context.SaveChanges();

            if (result > 0)
            {
                MessageBox.Show("Izmene su uspesno sacuvane");
            }
        }
        private void InitializeDataGridView() 
        {
            dataGridView1.Columns["Username"].HeaderText = "Korisnicko ime";
            dataGridView1.Columns["Name"].HeaderText = "Ime";
            dataGridView1.Columns["IsAdministrator"].HeaderText = "Administrator";
            dataGridView1.Columns["Mail"].HeaderText = "E-mail";
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Password"].Visible = false;
            dataGridView1.Columns["Username"].Width = (int)(dataGridView1.Width * 0.25);
            dataGridView1.Columns["Name"].Width = (int)(dataGridView1.Width * 0.25);
            dataGridView1.Columns["IsAdministrator"].Width = 100;
            dataGridView1.Columns["Mail"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
