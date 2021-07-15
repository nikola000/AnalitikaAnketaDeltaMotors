using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using UnitOfWorkExample.UnitOfWork;
using UnitOfWorkExample.UnitOfWork.Models;
namespace AnalitikaAnketaDeltaMotors.Forms
{

    public partial class Tagovi : Form
    {
        Newtag forma1;

        DatabaseContext context = new DatabaseContext();
        public Tagovi()
        {
            InitializeComponent();
        }

        private void grupaTagova_Load(object sender, EventArgs e)
        {
            context.Tags.Include(x => x.Group).Load();
            context.Tags.Include(x => x.Group).Load();
            FillComboBox();
        }

        private void FillComboBox()
        {
            List<Group> groups = context.Groups.Where(x => x.Id > 0).ToList();
            foreach (Group i in groups)
            {
                comboBox1.Items.Add(i);
            }
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int result = context.SaveChanges();

            if (result > 0)
            {
                MessageBox.Show("Izmene su uspesno sacuvane");
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            Group item = (Group)comboBox1.SelectedItem;
            dataGridView1.DataSource = context.Tags.Where(x => x.GroupId == item.Id).ToList();
            InitializeDataGridView();
      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                forma1 = new Newtag(((Group)comboBox1.SelectedItem).Id);
                forma1.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Izaberite nesto u comboboxu");
            }
            dataGridView1.Refresh();
        }
        public void AddTag(Tag newTag)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
              
                db.Tags.Add(newTag);
                List<Group> groups = context.Groups.Where(x => x.Id > 0).ToList();
                db.SaveChanges();

                
              


            }

        }
    }
}
