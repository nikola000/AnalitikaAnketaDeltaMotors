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
using System.Windows.Forms.DataVisualization.Charting;

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
        public CtrlAnswer CntrlAnswer  { get; set; }
        public List<Tag> FilterTags { get; set; }
        public List<Subtopic> FilterSubtopics { get; set; }
        public IEnumerable<Entry> SearchedEntries { get; set; }
        public int SelectedIndex { get; set; }

        public AnalitikaAnketaDeltaMotors(IUserService userService,IEntryService entryService)
        {
            this._userService = userService;
            this._entryService = entryService;
            InitializeComponent();

            // filteri
            InitFilters();

            InitializeComboBox();

            // dashborad
            SetupDashboard();
        }

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
            using (DatabaseContext db = new DatabaseContext())
            {
                comboBox1.Items.AddRange(db.Topics.Include(x => x.Subtopics).ToArray());
            }
            comboBox1.SelectedItem = comboBox1.Items[0];
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
                        .Where(x => x.Score == Classes.Utils.Score.Low && x.Subtopic.Topic.Id == topic.Id).Count());
                    chartRezultati.Series["medium"].Points.AddXY(topic.Name, SearchedEntries
                        .SelectMany(x => x.EntryScores)
                        .Where(x => x.Score == Classes.Utils.Score.Medium && x.Subtopic.Topic.Id == topic.Id).Count());
                    chartRezultati.Series["high"].Points.AddXY(topic.Name, SearchedEntries
                        .SelectMany(x => x.EntryScores)
                        .Where(x => x.Score == Classes.Utils.Score.High && x.Subtopic.Topic.Id == topic.Id).Count());

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

                foreach (var ocena in ocene)
                {
                    chartOcene.Series["Ocene"].Points.AddXY(ocena.ToString(), SearchedEntries
                            .Where(x => x.Ocena == ocena).Count());
                }
            }
        }
        private void SetChartSubtopics()
        {
            chartSubtopics.Series["low"].Points.Clear();
            chartSubtopics.Series["medium"].Points.Clear();
            chartSubtopics.Series["high"].Points.Clear();

            if (SearchedEntries != null)
            {
                foreach (var item in ((Topic)comboBox1.SelectedItem).Subtopics)
                {
                    chartSubtopics.Series["low"].Points.AddXY(item.Name, SearchedEntries
                        .SelectMany(x => x.EntryScores)
                        .Where(x => x.Score == Classes.Utils.Score.Low && x.SubtopicId==item.Id).Count());
                    chartSubtopics.Series["medium"].Points.AddXY(item.Name, SearchedEntries
                        .SelectMany(x => x.EntryScores)
                        .Where(x => x.Score == Classes.Utils.Score.Medium && x.SubtopicId == item.Id).Count());
                    chartSubtopics.Series["high"].Points.AddXY(item.Name, SearchedEntries
                        .SelectMany(x => x.EntryScores)
                        .Where(x => x.Score == Classes.Utils.Score.High && x.SubtopicId == item.Id).Count());
                }
            }
        }

        private void SetChartOverallNPS()
        {
            chartOverallNPS.Series["low"].Points.Clear();
            chartOverallNPS.Series["medium"].Points.Clear();
            chartOverallNPS.Series["high"].Points.Clear();

            if (SearchedEntries != null && SearchedEntries.Count() > 0)
            {
                chartOverallNPS.Series["low"].Points.AddXY("", 100 / SearchedEntries.Count() * SearchedEntries.Where(x => x.Ocena < 7).Count());
                chartOverallNPS.Series["medium"].Points.AddXY("", 100 / SearchedEntries.Count() * SearchedEntries.Where(x => x.Ocena == 7 || x.Ocena == 8).Count());
                chartOverallNPS.Series["high"].Points.AddXY("", 100 / SearchedEntries.Count() * SearchedEntries.Where(x => x.Ocena > 8).Count());
                chartOverallNPS.Series["low"].Label = 100 / SearchedEntries.Count() * SearchedEntries.Where(x => x.Ocena < 7).Count() + " %";
                chartOverallNPS.Series["medium"].Label = 100 / SearchedEntries.Count() * SearchedEntries.Where(x => x.Ocena == 7 || x.Ocena == 8).Count() + " %";
                chartOverallNPS.Series["high"].Label = 100 / SearchedEntries.Count() * SearchedEntries.Where(x => x.Ocena > 8).Count() + " %";

                chartOverallNPS.Legends.Clear();

                chartOverallNPS.Legends.Add(new Legend("Legend1"));
                chartOverallNPS.Legends["Legend1"].Docking = Docking.Bottom;
                chartOverallNPS.Series["low"].Legend = "Legend1";
                chartOverallNPS.Series["low"].LegendText = "detraktori";
                chartOverallNPS.Series["low"].IsVisibleInLegend = true;

                chartOverallNPS.Legends.Add(new Legend("Legend2"));
                chartOverallNPS.Legends["Legend2"].Docking = Docking.Bottom;
                chartOverallNPS.Series["medium"].Legend = "Legend2";
                chartOverallNPS.Series["medium"].LegendText = "neutralni";
                chartOverallNPS.Series["medium"].IsVisibleInLegend = true;

                chartOverallNPS.Legends.Add(new Legend("Legend3"));
                chartOverallNPS.Legends["Legend3"].Docking = Docking.Bottom;
                chartOverallNPS.Series["high"].Legend = "Legend3";
                chartOverallNPS.Series["high"].LegendText = "promoteri";
                chartOverallNPS.Series["high"].IsVisibleInLegend = true;
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
        }

        private void subtopicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSubtopic = new Subtopics();
            frmSubtopic.ShowDialog();
        }

        private void bUcitaj_Click(object sender, EventArgs e)
        {
            dataGridViewRezultatiAnkete.AutoGenerateColumns = true;

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

            intializeDataGrid(dataGridViewRezultatiAnkete);

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
            e.Cancel=true;
        }

        private void intializeDataGrid(DataGridView dataGrid) 
        {
            dataGridViewRezultatiAnkete.Columns["CreatedAt"].HeaderText = "Datum";
            dataGridViewRezultatiAnkete.Columns["Odgovor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewRezultatiAnkete.Columns["Ocena"].Visible = true;
            dataGridViewRezultatiAnkete.Columns["PredlogPoboljsanja"].Visible = true;
            dataGridViewRezultatiAnkete.Columns["Kontakt"].Visible = true;
            dataGridViewRezultatiAnkete.Columns["ImportDataId"].Visible = false;
            dataGridViewRezultatiAnkete.Columns["ImportData"].Visible = false;
            dataGridViewRezultatiAnkete.Columns["EntryScores"].Visible = false;

            dataGridViewRezultatiAnkete.DataError += DataGrid_DataError;
            dataGridViewRezultatiAnkete.Paint += DataGridViewRezultatiAnkete_Paint;
            dataGridViewRezultatiAnkete.DoubleClick += DataGrid_DoubleClick;
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
            labelFilterTopic.Text= "( " + FilterSubtopics.Count() + " )";
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
    }
}
