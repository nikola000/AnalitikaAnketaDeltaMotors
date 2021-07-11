using System;
using System.Windows.Forms;
using UnitOfWorkExample.UnitOfWork.Models;
using UnitOfWorkExample.Services;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class LogOn : Form
    {
        private readonly IUserService _userService;
        User _user;

        public LogOn()
        {
            InitializeComponent();
        }

        public LogOn(User user, IUserService userService)
        {
            this._user = user;
            this._userService = userService;
            
            InitializeComponent();
        }

        public User getUser()
        {
            return _user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogIn();
        }

        private void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                LogIn();
            }
        }

        private void LogIn()
        {
            _user = _userService.CheckUser(tbUser.Text, tbPassword.Text);
            if (_user == null)
            {
                MessageBox.Show("Unet je pogresan username ili password", "Neuspelo logovanje", MessageBoxButtons.OK);
            }
            else
            {
                this.Dispose();
            }
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
