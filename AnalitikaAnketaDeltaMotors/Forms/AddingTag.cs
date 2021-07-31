using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
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
using UnitOfWorkExample.UnitOfWork.Models;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class AddingTag : Form
    {
        public AddingTag()
        {
            InitializeComponent();
        }

        private void AddingTag_Load(object sender, EventArgs e)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                comboBox1.Items.AddRange(db.Groups.ToArray());
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedItem = comboBox1.Items[0];
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Tag newTag = new Tag();
            if (tbName.Text.Trim().Length > 1)
            {
                newTag.Name = tbName.Text;
                if (!(comboBox1.SelectedItem is Group))
                {
                    MessageBox.Show("Mora biti izabrana grupa da bi se dodao tag", "Dodavanje tag-a", MessageBoxButtons.OK);
                    return;
                }
                newTag.GroupId = (comboBox1.SelectedItem as Group).Id;
                AddRecord(newTag);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Niste uneli naziv tag-a", "Dodavanje tag-a", MessageBoxButtons.OK);
            }
        }
        private void AddRecord(Tag newTag)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.Tags.Add(newTag);
                db.SaveChanges();
            }
        }
    }
}
