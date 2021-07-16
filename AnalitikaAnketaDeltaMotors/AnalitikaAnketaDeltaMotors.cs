using AnalitikaAnketaDeltaMotors.Controls;
using AnalitikaAnketaDeltaMotors.Forms;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UnitOfWorkExample.Services;
using UnitOfWorkExample.UnitOfWork;
using UnitOfWorkExample.UnitOfWork.Models;
using System.Data.Entity;

namespace AnalitikaAnketaDeltaMotors
{
    public partial class AnalitikaAnketaDeltaMotors : Form
    {
        private readonly IUserService _userService;
        private readonly IEntryService _entryService;
        DatabaseContext dbContext = new DatabaseContext();
        LogOn frm2;
        User user;
        Import frm3;
        Administration frmAdmin;
        Tagovi frmtag;
        GroupOfTags frmGroup;
        Podesavanja frmPodesavanja;
        Topics frmTopic;
        Subtopics frmSubtopic;

        public AnalitikaAnketaDeltaMotors(IUserService userService,IEntryService entryService)
        {
            this._userService = userService;
            this._entryService = entryService;
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
            dataGridViewRezultatiAnkete.AutoGenerateColumns = true;

            dataGridViewRezultatiAnkete.DataSource = dbContext.Entries
                .Include(x => x.EntryScores
                    .Select(y => y.Subtopic)
                    .Select(z => z.Topic))
                .Where(x => x.CreatedAt >= dateTimePicker1.Value &&
                            x.CreatedAt <= dateTimePicker2.Value).ToList();

            intializeDataGrid(dataGridViewRezultatiAnkete);
        }

        private void DataGridViewRezultatiAnkete_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewRezultatiAnkete.Rows)
                if (row.DataBoundItem != null && ((Entry)row.DataBoundItem).EntryScores.Count > 0)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;

                }
        }

        private void DataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel=true;
        }

        private void intializeDataGrid(DataGridView dataGrid) 
        {
            dataGridViewRezultatiAnkete.Columns["CreatedAt"].HeaderText = "Datum";
            dataGridViewRezultatiAnkete.Columns["Odgovor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewRezultatiAnkete.Columns["Ocena"].Visible = false;
            dataGridViewRezultatiAnkete.Columns["PredlogPoboljsanja"].Visible = false;
            dataGridViewRezultatiAnkete.Columns["Kontakt"].Visible = false;
            dataGridViewRezultatiAnkete.Columns["ImportDataId"].Visible = false;
            dataGridViewRezultatiAnkete.Columns["ImportData"].Visible = false;
            dataGridViewRezultatiAnkete.Columns["EntryScores"].Visible = false;

            dataGridViewRezultatiAnkete.DataError += DataGrid_DataError;
            dataGridViewRezultatiAnkete.Paint += DataGridViewRezultatiAnkete_Paint;
            dataGridViewRezultatiAnkete.DoubleClick += DataGrid_DoubleClick;
        }

        private void DataGrid_DoubleClick(object sender, EventArgs e)
        {
            Entry entry = (Entry)(sender as DataGridView).CurrentRow.DataBoundItem;
            tabPage3.Controls.Clear();
            CtrlAnswer answer = new CtrlAnswer(entry, dbContext);
            tabPage3.Controls.Add(answer);
            tabControl1.SelectTab(tabPage3);
        }


    }
}
