using AnalitikaAnketaDeltaMotors.Classes;
using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using UnitOfWorkExample.UnitOfWork;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class Podesavanja : Form
    {
        Settings listajOdgovore = new Settings();
        Settings automatskoCuvanje = new Settings();
        public Podesavanja()
        {
            InitializeComponent();
            UcitajFajl();

        }
        private void UcitajFajl()
        {
            textBox1.Text = ConfigHelper.ReadConfigFile();
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

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigHelper.WriteConfigFile(textBox1.Text);
            Configuration.GetInstance().ResetConnectionString();
            if ((listajOdgovore.Name == null) && (automatskoCuvanje.Name == null))
            {
                listajOdgovore.Name = "listaj_samo_nekodirane";
                automatskoCuvanje.Name = "automatsko_cuvanje";
                if (checkBox1.Checked)
                {
                    listajOdgovore.Value = 1;
                }
                else if (checkBox3.Checked)
                {
                    automatskoCuvanje.Value = 1;
                }

                MessageBox.Show("Podesavanja su dodata u program");
            }
            else
            {
                MessageBox.Show("podesavanja su azurirana u program");

            }
        }

        private void Podesavanja_FormClosing(object sender, FormClosingEventArgs e)
        {

            Properties.Settings.Default.Save();

        }
    }
}













