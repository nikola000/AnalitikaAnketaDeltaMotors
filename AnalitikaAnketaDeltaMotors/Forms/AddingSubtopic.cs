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
    public partial class AddingSubtopic : Form
    {
        public AddingSubtopic()
        {
            InitializeComponent();
        }

        private void AddingSubtopic_Load(object sender, EventArgs e)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                comboBox1.Items.AddRange(db.Topics.ToArray());
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedItem = comboBox1.Items[0];
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Subtopic newSubtopic = new Subtopic();
            if (tbName.Text.Trim().Length > 1)
            {
                newSubtopic.Name = tbName.Text;
                if (!(comboBox1.SelectedItem is Topic))
                {
                    MessageBox.Show("Mora biti izabran topic da bi se dodao subtopic", "Dodavanje subtopic-a", MessageBoxButtons.OK);
                    return;
                }
                newSubtopic.TopicId = (comboBox1.SelectedItem as Topic).Id;
                AddRecord(newSubtopic);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Niste uneli naziv subtopic-a", "Dodavanje subtopic-a", MessageBoxButtons.OK);
            }
        }

        private void AddRecord(Subtopic newSubtopic)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.Subtopics.Add(newSubtopic);
                db.SaveChanges();
            }
        }
    }
}
