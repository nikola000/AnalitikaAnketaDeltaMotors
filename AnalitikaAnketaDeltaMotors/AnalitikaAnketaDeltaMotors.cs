using AnalitikaAnketaDeltaMotors.Classes;
using AnalitikaAnketaDeltaMotors.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnitOfWorkExample.Services;
using UnitOfWorkExample.UnitOfWork.Models;

namespace AnalitikaAnketaDeltaMotors
{
    public partial class AnalitikaAnketaDeltaMotors : Form
    {
        private readonly IUserService _userService;
        LogOn frm2;
        User user;
        Import frm3;
        Administration frmAdmin;
        Tagovi frmtag;
        GroupOfTags frmGroup;
        Podesavanja frmPodesavanja;
        Topics frmTopic;
        Subtopics frmSubtopic;

        public AnalitikaAnketaDeltaMotors(IUserService userService)
        {
            this._userService = userService;
            InitializeComponent();
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
            if (e.ClickedItem.Name == ToolStripMenuItem2.Name)
            {
                frm3 = new Import();
                frm3.ShowDialog();
            }
        }

        private void izlazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // check data for save
            this.Close();
        }

        private void korisniciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdmin = new Administration();
            frmAdmin.ShowDialog();
        }

        private void grupeOznakaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGroup = new GroupOfTags();
            frmGroup.ShowDialog();
        }

        private void oznakeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmtag = new Tagovi();
            frmtag.ShowDialog();
        }

        private void bazaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPodesavanja = new Podesavanja();
            frmPodesavanja.ShowDialog();
        }

        private void prijavaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (user != null)
            {
                var result = MessageBox.Show("Da li ste sigurni da zelite da se odjavite", "Odjava", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    user = null;
                    toolStripStatusLabelLogin.Text = "";
                    prijavaToolStripMenuItem.Text = "Prijava";
                }
            }
            else
            {
                frm2 = new LogOn(user, _userService);
                frm2.ShowDialog();
                user = frm2.getUser();
                if (user != null)
                {
                    toolStripStatusLabelLogin.Text = "Ulogovan/a: " + user.Name;
                    korisniciToolStripMenuItem.Visible = user.IsAdministrator;
                    prijavaToolStripMenuItem.Text = "Odjava";
                }
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            var g = e.Graphics;
            var text = this.tabControl1.TabPages[e.Index].Text;
            var sizeText = g.MeasureString(text, this.tabControl1.Font);

            var x = e.Bounds.Left + 3;
            var y = e.Bounds.Top + (e.Bounds.Height - sizeText.Height) / 2;

            g.DrawString(text, this.tabControl1.Font, Brushes.Black, x, y);
        }

        private void topicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTopic = new Topics();
            frmTopic.ShowDialog();
        }

        private void subtopicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSubtopic = new Subtopics();
            frmSubtopic.ShowDialog();
        }

        private void bUcitaj_Click(object sender, EventArgs e)
        {

        }
    }
}
