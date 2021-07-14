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
            if (e.ClickedItem.Name == toolStripMenuItem1.Name)
            {
                if (user != null)
                {
                    MessageBox.Show("Vec ste ulogovani");
                }
                else
                {
                    frm2 = new LogOn(user, _userService);
                    frm2.ShowDialog();
                    user = frm2.getUser();
                    if (user != null)
                    {
                        toolStripStatusLabelLogin.Text = "Ulogovan: " + user.Name;

                        if (user.IsAdministrator == true)
                        {
                            tsmiAdministrator.Visible = true;
                        }
                    }
                }
            }
            if (e.ClickedItem.Name == ToolStripMenuItem2.Name)
            {
                frm3 = new Import();
                frm3.ShowDialog();
            }
            if (e.ClickedItem.Name == tsmiAdministrator.Name)
            {
                frmAdmin = new Administration();
                frmAdmin.ShowDialog();
            }
            if (e.ClickedItem.Name == tsmiGroupOfTags.Name)
            {
                frmGroup = new GroupOfTags();
                frmGroup.ShowDialog();
            }
            if (e.ClickedItem.Name == tagsToolStripMenuItem.Name)
            {
                frmtag = new Tagovi();
                frmtag.ShowDialog();
            }
            if (e.ClickedItem.Name == podesavanjaToolStripMenuItem.Name)
            {
                frmPodesavanja = new Podesavanja();
                frmPodesavanja.ShowDialog();
            }
            if (e.ClickedItem.Name == topicsToolStripMenuItem.Name)
            {
                frmTopic = new Topics();
                frmTopic.ShowDialog();
            }
            if (e.ClickedItem.Name == subtopicsToolStripMenuItem.Name)
            {
                frmSubtopic = new Subtopics();
                frmSubtopic.ShowDialog();
            }
        }
    }
}
