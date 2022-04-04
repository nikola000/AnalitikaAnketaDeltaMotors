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
using System.Collections.Generic;
using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using AnalitikaAnketaDeltaMotors.Classes;

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


        CtrlTagBookmarks _cntrlTags;
        TagBookmarks _fitlerTagDialog;
        SubtopicBookmarks _fitlerSubtopicDialog;
        public CtrlAnswer CntrlAnswer { get; set; }
        public List<Tag> FilterTags { get; set; }
        public List<Subtopic> FilterSubtopics { get; set; }
        public IEnumerable<Entry> SearchedEntries { get; set; }
        public int SelectedIndex { get; set; }

        public AnalitikaAnketaDeltaMotors(IUserService userService, IEntryService entryService)
        {
            this._userService = userService;
            this._entryService = entryService;
            InitializeComponent();

            // filteri
            InitFilters();

            InitializeComboBox();

            // dashborad
            SetupDashboard();

            InitMenuItems();

            intializeDataGrid(dataGridViewRezultatiAnkete);
        }

        private void InitMenuItems() => izmenaAnketaToolStripMenuItem.Visible = Configuration.GetInstance().CurrentUser != null ? Configuration.GetInstance().CurrentUser.IsAdministrator : false;

        private void InitFilters()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                FilterTags = db.Tags.Include(x => x.Group).ToList();
                labelFilterTags.Text = "( " + FilterTags.Count() + " )";

                FilterSubtopics = db.Subtopics.Include(x => x.Topic).ToList();
                labelFilterTopic.Text = "( " + FilterSubtopics.Count() + " )";
            }
        }
        private void InitializeComboBox()
        {
            comboBox1.Items.Clear();
            using (DatabaseContext db = new DatabaseContext())
            {
                comboBox1.Items.AddRange(db.Topics.Include(x => x.Subtopics).ToArray());
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedItem = comboBox1.Items[0];
            }
        }
        private void SetupDashboard()
        {
            SetChartRezultati();
            SetChartOdnosKodiranih();
            SetChartTags();
            SetChartGroups();
            SetChartOverallNPS();
            SetChartOcene();
            CalculateNPS();

            labelUkupnoOdgovora.Text = "Ukupno odgovora: " + ((SearchedEntries == null) ? 0 : SearchedEntries.Count());
        }

        private void CalculateNPS()
        {
            if (SearchedEntries != null && SearchedEntries.Count() > 0)
            {
                double ucescePromotera = 0;
                double ucesceDetraktora = 0;

                ucesceDetraktora = ((double)(SearchedEntries.Where(x => x.Ocena >= 0 && x.Ocena <= 6).Count()) / (double)(SearchedEntries.Count())) * 100;
                ucescePromotera = ((double)(SearchedEntries.Where(x => x.Ocena >= 9 && x.Ocena <= 10).Count()) / (double)(SearchedEntries.Count())) * 100;

                labelNPS.Text = ((int)Math.Ceiling(ucescePromotera - ucesceDetraktora)).ToString();
            }
            else
            {
                labelNPS.Text = "";
            }
        }

        private void SetChartRezultati()
        {
            chartRezultati.Series["low"].Points.Clear();
            chartRezultati.Series["medium"].Points.Clear();
            chartRezultati.Series["high"].Points.Clear();

            if (SearchedEntries != null)
            {
                var topics = SearchedEntries.SelectMany(x => x.EntryScores)
                    .Select(x => x.Subtopic)
                    .Select(x => x.Topic).ToList();

                var topicGroups = topics.GroupBy(x => x.Id);

                foreach (var item in topicGroups)
                {
                    var topic = item.FirstOrDefault();
                    chartRezultati.Series["low"].Points.AddXY(topic.Name, SearchedEntries
                        .SelectMany(x => x.EntryScores)
                        .Where(x => x.Score == Classes.Utils.Score.Low && x.Subtopic.Topic.Id == topic.Id)
                        .Where(x => cbLow.Checked == true)
                        .Count());
                    chartRezultati.Series["medium"].Points.AddXY(topic.Name, SearchedEntries
                        .SelectMany(x => x.EntryScores)
                        .Where(x => x.Score == Classes.Utils.Score.Medium && x.Subtopic.Topic.Id == topic.Id)
                        .Where(x => cbMedium.Checked == true)
                        .Count());
                    chartRezultati.Series["high"].Points.AddXY(topic.Name, SearchedEntries
                        .SelectMany(x => x.EntryScores)
                        .Where(x => x.Score == Classes.Utils.Score.High && x.Subtopic.Topic.Id == topic.Id)
                        .Where(x => cbHigh.Checked == true)
                        .Count());

                }
            }
        }

        private void SetChartOcene()
        {
            if (SearchedEntries != null)
            {

                chartOcene.Series["Ocene"].Points.Clear();

                var ocene = SearchedEntries
                            .Select(x => x.Ocena).Distinct();

                chartOcene.Series["Ocene"].Points.AddXY("Kriticari", SearchedEntries
                    //.Where(x => cbLow.Checked == true)
                    .Where(x => new int[] { 0,1,2,3,4,5,6 }.Contains(x.Ocena)).Count());

                chartOcene.Series["Ocene"].Points.AddXY("Neutralni", SearchedEntries
                    //.Where(x => cbMedium.Checked == true)
                    .Where(x => new int[] { 7, 8 }.Contains(x.Ocena)).Count());

                chartOcene.Series["Ocene"].Points.AddXY("Promoteri", SearchedEntries
                    //.Where(x => cbHigh.Checked == true)
                    .Where(x => new int[] { 9, 10 }.Contains(x.Ocena)).Count());

                //foreach (var ocena in ocene)
                //{
                //    chartOcene.Series["Ocene"].Points.AddXY(ocena.ToString(), SearchedEntries
                //            .Where(x => x.Ocena == ocena).Count());
                //}
            }
        }
        private void SetChartSubtopics()
        {
            chartSubtopics.Series["low"].Points.Clear();
            chartSubtopics.Series["medium"].Points.Clear();
            chartSubtopics.Series["high"].Points.Clear();

            if (comboBox1.Items.Count == 0)
            {
                MessageBox.Show("Molimo unesite neki Topic ili Subtopic");
                return;
            }

            if (SearchedEntries != null)
            {
                foreach (var item in ((Topic)comboBox1.SelectedItem).Subtopics)
                {
                    chartSubtopics.Series["low"].Points.AddXY(item.Name, SearchedEntries
                        .SelectMany(x => x.EntryScores)
                        .Where(x => cbLow.Checked == true)
                        .Where(x => x.Score == Classes.Utils.Score.Low && x.SubtopicId == item.Id).Count());
                    chartSubtopics.Series["medium"].Points.AddXY(item.Name, SearchedEntries
                        .SelectMany(x => x.EntryScores)
                        .Where(x => cbMedium.Checked == true)
                        .Where(x => x.Score == Classes.Utils.Score.Medium && x.SubtopicId == item.Id).Count());
                    chartSubtopics.Series["high"].Points.AddXY(item.Name, SearchedEntries
                        .SelectMany(x => x.EntryScores)
                        .Where(x => cbHigh.Checked == true)
                        .Where(x => x.Score == Classes.Utils.Score.High && x.SubtopicId == item.Id).Count());
                }
            }
        }

        private void SetChartOverallNPS()
        {
            chartOverallNPS.Series["low"].Points.Clear();
            chartOverallNPS.Series["medium"].Points.Clear();
            chartOverallNPS.Series["high"].Points.Clear();

            chartOverallNPS.ChartAreas[0].AxisY.LabelStyle.Format = "###0\\%";
            chartOverallNPS.ChartAreas[0].AxisX.LabelStyle.Enabled = false;

            if (SearchedEntries != null)
            {
                decimal totalScores = 0;

                if (cbLow.Checked == true)
                {
                    totalScores += SearchedEntries
                    .SelectMany(x => x.EntryScores)
                    .Where(x => cbLow.Checked == true && x.Score == Classes.Utils.Score.Low)
                    .Count();
                }

                if (cbMedium.Checked == true)
                {
                    totalScores += SearchedEntries
                    .SelectMany(x => x.EntryScores)
                    .Where(x => cbMedium.Checked == true && x.Score == Classes.Utils.Score.Medium)
                    .Count();
                }

                if (cbHigh.Checked == true)
                {
                    totalScores += SearchedEntries
                    .SelectMany(x => x.EntryScores)
                    .Where(x => cbHigh.Checked == true && x.Score == Classes.Utils.Score.High)
                    .Count();
                }

                if (totalScores == 0)
                {
                    chartOverallNPS.Series["low"].Points.Clear();
                    chartOverallNPS.Series["medium"].Points.Clear();
                    chartOverallNPS.Series["high"].Points.Clear();
                    return;
                }

                decimal low = Math.Round(SearchedEntries
                    .SelectMany(x => x.EntryScores)
                    .Where(x => cbLow.Checked == true)
                    .Where(x => x.Score == Classes.Utils.Score.Low).Count() / totalScores * 100, 1);

                decimal medium = Math.Round(SearchedEntries
                    .SelectMany(x => x.EntryScores)
                    .Where(x => cbMedium.Checked == true)
                    .Where(x => x.Score == Classes.Utils.Score.Medium).Count() / totalScores * 100, 1);

                decimal high = Math.Round(SearchedEntries
                    .SelectMany(x => x.EntryScores)
                    .Where(x => cbHigh.Checked == true)
                    .Where(x => x.Score == Classes.Utils.Score.High).Count() / totalScores * 100, 1);
                
                if (low != 0)
                {
                    chartOverallNPS.Series["low"].Points.AddXY("", low);
                }

                if (medium != 0)
                {
                    chartOverallNPS.Series["medium"].Points.AddXY("", medium);
                }

                if (high != 0)
                {
                    chartOverallNPS.Series["high"].Points.AddXY("", high);
                }
            }
        }

        private void SetChartGroups()
        {
            if (SearchedEntries != null)
            {
                chartGroups.Series["Groups"].Points.Clear();
                var allgroups = SearchedEntries
                    .Select(x => x.ImportData)
                    .SelectMany(x => x.Tags)
                    .Select(x => x.Group).ToList();

                var groups = allgroups.GroupBy(x => x.Id);
                foreach (var group in groups)
                {
                    if (group.FirstOrDefault() == null)
                    {
                        chartGroups.Series["Groups"].Points.AddXY("n/a", group.Count());
                    }
                    else
                    {
                        chartGroups.Series["Groups"].Points.AddXY(group.FirstOrDefault().Name, group.Count());
                    }
                }
            }
        }

        private void SetChartTags()
        {
            if (SearchedEntries != null)
            {
                chartTagovi.Series["Tagovi"].Points.Clear();
                var groups = SearchedEntries.GroupBy(x => x.ImportData.Tags).ToList();
                foreach (var group in groups)
                {
                    if (group.Key.Count == 0)
                    {
                        chartTagovi.Series["Tagovi"].Points.AddXY("n/a", group.Count());
                    }
                    else
                    {
                        chartTagovi.Series["Tagovi"].Points.AddXY(group.Key.FirstOrDefault().Name, group.Count());
                    }
                }
            }
        }

        private void SetChartOdnosKodiranih()
        {
            if (SearchedEntries != null)
            {
                chartOdnosKodiranih.Series["Kodirani"].Points.Clear();
                int kodirani = SearchedEntries.Where(x => x.EntryScores.Count() > 0).Count();
                int neKodirani = SearchedEntries.Count() - kodirani;
                chartOdnosKodiranih.Series["Kodirani"].Points.AddXY("Kodirani", kodirani);
                chartOdnosKodiranih.Series["Kodirani"].Points.AddXY("Nekodirani", neKodirani);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            
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
                    Configuration.GetInstance().CurrentUser = user;
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
                    Configuration.GetInstance().CurrentUser = user;
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
            InitializeComboBox();
        }

        private void subtopicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSubtopic = new Subtopics();
            frmSubtopic.ShowDialog();
            InitializeComboBox();
        }

        private void bUcitaj_Click(object sender, EventArgs e)
        {
            dataGridViewRezultatiAnkete.AutoGenerateColumns = true;

            dbContext = new DatabaseContext();

            var temp = dbContext.Entries
                .Include(x => x.ImportData.Tags.Select(y => y.Group))
                .Include(x => x.EntryScores
                    .Select(y => y.Subtopic)
                    .Select(z => z.Topic))
                .Where(x => x.CreatedAt >= dateTimePicker1.Value &&
                            x.CreatedAt <= dateTimePicker2.Value);

            SearchedEntries = temp.ToList()
                .Where(x => CheckPersmission(x))
                .Where(x => AcceptFilterCriteria(x))
                .Where(x => filterSentiments(x))
                .Where(x => filterOcena(x)).ToList();
            dataGridViewRezultatiAnkete.DataSource = SearchedEntries;

            dataGridViewRezultatiAnkete.Columns["CreatedAt"].HeaderText = "Datum";
            dataGridViewRezultatiAnkete.Columns["Odgovor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewRezultatiAnkete.Columns["Ocena"].Visible = true;
            dataGridViewRezultatiAnkete.Columns["PredlogPoboljsanja"].Visible = true;
            dataGridViewRezultatiAnkete.Columns["Kontakt"].Visible = true;
            dataGridViewRezultatiAnkete.Columns["ImportDataId"].Visible = false;
            dataGridViewRezultatiAnkete.Columns["ImportData"].Visible = false;
            dataGridViewRezultatiAnkete.Columns["EntryScores"].Visible = false;

            SetupDashboard();

            SetChartSubtopics();
        }

        private bool AcceptFilterCriteria(Entry entry)
        {
            bool pass = false;
            if (FilterTags.Select(x => x.Id).Intersect(entry.ImportData.Tags.Select(y => y.Id).Distinct()).Count() > 0)
            {
                pass = true;
            }
            if (FilterSubtopics.Select(x => x.Id).Intersect(entry.EntryScores.Select(x => x.Subtopic.Id).Distinct()).Count() > 0)
            {
                pass = true;
            }
            return pass;
        }

        private bool CheckPersmission(Entry entry)
        {
            if (user == null)
                return false;

            if (user.IsAdministrator)
                return true;

            if (entry.EntryScores.Count == 0)
                return true;

            return entry.EntryScores.Any(x => x.UserId == null || x.UserId == user.Id);
        }

        private void DataGridViewRezultatiAnkete_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewRezultatiAnkete.Rows)
                if (row.DataBoundItem != null && ((Entry)row.DataBoundItem).Odgovor.Contains(textBox1.Text) && textBox1.Text != "")
                {
                    row.DefaultCellStyle.BackColor = Color.Pink;
                }
                else
                {
                    if (row.DataBoundItem != null && ((Entry)row.DataBoundItem).EntryScores.Count > 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
        }

        private void DataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void intializeDataGrid(DataGridView dataGrid)
        {
            dataGridViewRezultatiAnkete.DataError += DataGrid_DataError;
            dataGridViewRezultatiAnkete.Paint += DataGridViewRezultatiAnkete_Paint;
            dataGridViewRezultatiAnkete.DoubleClick += DataGrid_DoubleClick;
            dataGridViewRezultatiAnkete.AllowUserToDeleteRows = true;
        }

        private void DataGrid_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridViewRezultatiAnkete.SelectedRows.Count < 1)
            {
                return;
            }
            SelectedIndex = dataGridViewRezultatiAnkete.SelectedRows[0].Index;
            Entry currentEntry = (Entry)(sender as DataGridView).CurrentRow.DataBoundItem;
            tabPage3.Controls.Clear();
            CntrlAnswer = new CtrlAnswer(currentEntry, dbContext, changeEntry);
            CntrlAnswer.Height = tabPage3.Height;
            CntrlAnswer.Width = tabPage3.Width;
            disableButtons();
            tabPage3.Controls.Add(CntrlAnswer);
            CntrlAnswer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.SelectTab(tabPage3);
        }
        public void changeEntry(int direction)
        {
            SelectedIndex += direction;
            dataGridViewRezultatiAnkete.ClearSelection();
            dataGridViewRezultatiAnkete.Rows[SelectedIndex].Selected = true;

            var _selected = dataGridViewRezultatiAnkete.Rows[SelectedIndex].DataBoundItem;

            if (_selected != null && _selected is Entry)
            {
                tabPage3.Controls.Clear();
                CntrlAnswer = new CtrlAnswer((Entry)_selected, dbContext, changeEntry);
                CntrlAnswer.Height = tabPage3.Height;
                CntrlAnswer.Width = tabPage3.Width;
                disableButtons();
                tabPage3.Controls.Add(CntrlAnswer);
                tabControl1.SelectTab(tabPage3);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _fitlerTagDialog = new TagBookmarks(FilterTags);
            _fitlerTagDialog.ShowDialog();
            FilterTags = _fitlerTagDialog.Tags;
            labelFilterTags.Text = "( " + FilterTags.Count() + " )";
        }

        private void buttonOpenFilterTopic_Click(object sender, EventArgs e)
        {
            _fitlerSubtopicDialog = new SubtopicBookmarks(FilterSubtopics);
            _fitlerSubtopicDialog.ShowDialog();
            FilterSubtopics = _fitlerSubtopicDialog.Subtopics;
            labelFilterTopic.Text = "( " + FilterSubtopics.Count() + " )";
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            SetChartSubtopics();
        }
        private void disableButtons()
        {
            CntrlAnswer.DisablePrevious(false);
            if (SelectedIndex == 0)
            {
                CntrlAnswer.DisablePrevious(true);
            }
            CntrlAnswer.DisableNext(false);
            if (SelectedIndex == dataGridViewRezultatiAnkete.Rows.Count - 1)
            {
                CntrlAnswer.DisableNext(true);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewRezultatiAnkete.Rows)
            {
                if (row.DataBoundItem != null && (((Entry)row.DataBoundItem).Odgovor.ToLower().Contains(textBox1.Text.ToLower()) 
                    || ((Entry)row.DataBoundItem).PredlogPoboljsanja.ToLower().Contains(textBox1.Text.ToLower()) 
                    || ((Entry)row.DataBoundItem).Kontakt.ToLower().Contains(textBox1.Text.ToLower())) && textBox1.Text != "")
                {
                    row.DefaultCellStyle.BackColor = Color.Pink;
                }
                else
                {
                    if (row.DataBoundItem != null && ((Entry)row.DataBoundItem).EntryScores.Count > 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
        }
        private bool filterSentiments(Entry entry)
        {
            List<Utils.Score> scores = new List<Utils.Score>();
            if (cbLow.Checked)
            {
                scores.Add(Utils.Score.Low);
            }
            if (cbMedium.Checked)
            {
                scores.Add(Utils.Score.Medium);
            }
            if (cbHigh.Checked)
            {
                scores.Add(Utils.Score.High);
            }
            if (scores.Count < 1)
            {
                return true;
            }
            if (scores.Select(x => x).Intersect(entry.EntryScores.Select(x => x.Score).Distinct()).Count() > 0)
            {
                return true;
            }
            return false;
        }
        private bool filterOcena(Entry entry)
        {
            List<int> ocene = new List<int>();
            if (cb0.Checked)
            {
                ocene.Add(0);
            }
            if (cb1.Checked)
            {
                ocene.Add(1);
            }
            if (cb2.Checked)
            {
                ocene.Add(2);
            }
            if (cb3.Checked)
            {
                ocene.Add(3);
            }
            if (cb4.Checked)
            {
                ocene.Add(4);
            }
            if (cb5.Checked)
            {
                ocene.Add(5);
            }
            if (cb6.Checked)
            {
                ocene.Add(6);
            }
            if (cb7.Checked)
            {
                ocene.Add(7);
            }
            if (cb8.Checked)
            {
                ocene.Add(8);
            }
            if (cb9.Checked)
            {
                ocene.Add(9);
            }
            if (cb10.Checked)
            {
                ocene.Add(10);
            }
            if (ocene.Count < 1)
            {
                return true;
            }
            if (ocene.Contains(entry.Ocena))
            {
                return true;
            }
            return false;
        }


        private void txtPosAnketa_TextChanged(object sender, EventArgs e)
        {
            double broj;
            double.TryParse(txtPosAnketa.Text, out broj);
            if (SearchedEntries == null || broj==0)
            {
                label9.Text = "";
                lblStopaOdgovora.Text = "";
                return;
            }
            label9.Text = SearchedEntries.Count().ToString();
            lblStopaOdgovora.Text = Math.Round(SearchedEntries.Count() / broj * 100, 2) + " %";
        }

        private void txtPosAnketa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cbKriticari_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = cbKriticari.Checked;
            cb0.Checked = isChecked;
            cb1.Checked = isChecked;
            cb2.Checked = isChecked;
            cb3.Checked = isChecked;
            cb4.Checked = isChecked;
            cb5.Checked = isChecked;
            cb6.Checked = isChecked;
        }

        private void cbNeutralni_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = cbNeutralni.Checked;
            cb7.Checked = isChecked;
            cb8.Checked = isChecked;
        }

        private void cbPromoteri_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = cbPromoteri.Checked;
            cb9.Checked = isChecked;
            cb10.Checked = isChecked;
        }

        private void anketeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm3 = new Import();
            frm3.ShowDialog();
        }

        private void oznakeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            IzmenaAnketa izmenaAnketa = new IzmenaAnketa();
            izmenaAnketa.ShowDialog();
        }

        private void toolStripStatusLabelLogin_TextChanged(object sender, EventArgs e)
        {
            InitMenuItems();
        }
    }
}
