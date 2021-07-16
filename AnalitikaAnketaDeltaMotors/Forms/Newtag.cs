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
    public partial class Newtag : Form
    {
        int id;

        public Newtag(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        public Newtag()
        {
            InitializeComponent();
        }
        private void AddRecord(Tag newTag)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.Tags.Add(newTag);
                db.SaveChanges();
            }
        }

       
            Tag newTag = new Tag();
          

        private void button1_Click(object sender, EventArgs e)
        {  if (textBox1.Text.Trim().Length > 1)
            {
                newTag.Name = textBox1.Text;

                newTag.GroupId = id;
                AddRecord(newTag);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("unesite neki tekst");
            }
        
        }
    }
           

    }


