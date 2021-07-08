using System;
using System.Windows.Forms;
using UnitOfWorkExample.UnitOfWork.Models;
using UnitOfWorkExample.UnitOfWork;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class AddingUser : Form
    {
        public AddingUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User newUser = new User();
            if (tbName.Text.Trim().Length > 1 && tbUsername.Text.Trim().Length > 1
                && tbUsername.Text.Trim().Length > 1 && tbPassword.Text.Trim().Length > 1)
            {
                newUser.Name = tbName.Text;
                newUser.IsAdministrator = cbAdmin.Checked;
                newUser.Username = tbUsername.Text;
                newUser.Mail = tbMail.Text;
                newUser.Password = tbPassword.Text;
                AddRecord(newUser);
                this.Dispose();
            }
            else 
            {
                MessageBox.Show("Niste uneli sve podatke","Registracija",MessageBoxButtons.OK);
            }
            
        }
        public void AddRecord(User newUser)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }
        }
    }
}
