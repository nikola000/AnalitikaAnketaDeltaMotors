using AnalitikaAnketaDeltaMotors.Classes;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using UnitOfWorkExample.UnitOfWork;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class Podesavanja : Form
    {
        public Podesavanja()
        {
            InitializeComponent();
            UcitajFajl();
        }

        private void UcitajFajl()
        {
            textBox1.Text = ConfigHelper.ReadConfigFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigHelper.WriteConfigFile(textBox1.Text);
            Configuration.GetInstance().ResetConnectionString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length < 1)
            {
                MessageBox.Show("morate uneti konekcioni string");
                return;
            }


            using (DatabaseContext dbContext = new DatabaseContext())
            {
                try
                {
                    dbContext.Database.Exists();
                    MessageBox.Show("Konekcija uspesna");
                }


                catch
                {
                    MessageBox.Show("Konekcija neuspesna");
                }
            }
        }
    }
}
