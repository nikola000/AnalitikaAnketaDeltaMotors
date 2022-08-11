using AnalitikaAnketaDeltaMotors.Classes;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading;
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
            (bool res, string message) = ConfigHelper.CheckConnection(textBox1.Text);
            MessageBox.Show(message);
        }

    }
}
