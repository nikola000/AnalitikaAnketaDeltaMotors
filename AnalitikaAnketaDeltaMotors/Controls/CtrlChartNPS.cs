using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using UnitOfWorkExample.UnitOfWork;
using UnitOfWorkExample.UnitOfWork.Models;

namespace AnalitikaAnketaDeltaMotors.Controls
{
    public partial class CtrlChartNPS : UserControl
    {
        DatabaseContext db = new DatabaseContext();
        public IEnumerable<Entry> SearchedEntries { get; set; }
        public IEnumerable<Entry> FilteredEntries { get; set; }
        public int lastMonths=6;
        public CtrlChartNPS()
        {
            InitializeComponent();
        }
        public CtrlChartNPS(IEnumerable<Entry> SearchedEntries)
        {
            this.SearchedEntries = SearchedEntries;
            InitializeComponent();
        }
        private List<int> CompareNPS(Group group)
        {
            List<int> NPSs = new List<int>();
            if (FilteredEntries != null && FilteredEntries.Count() > 0)
            {
                DateTime dateTime = DateTime.Now.AddMonths(-lastMonths);
                for (int i = 1; i <= lastMonths; i++)
                {
                    double ucescePromotera = 0;
                    double ucesceDetraktora = 0;
                    DateTime firstDate = new DateTime(dateTime.Year, dateTime.Month, 1);
                    DateTime secondDate = firstDate.AddMonths(1);
                    dateTime = dateTime.AddMonths(1);
                    ucesceDetraktora = ((double)(FilteredEntries.Where(x => x.Ocena >= 0 && x.Ocena <= 6 && x.CreatedAt > firstDate && x.CreatedAt < secondDate).Count()) / (double)(FilteredEntries.Where(x => x.CreatedAt > firstDate && x.CreatedAt < secondDate).Count())) * 100;
                    ucescePromotera = ((double)(FilteredEntries.Where(x => x.Ocena >= 9 && x.Ocena <= 10 && x.CreatedAt > firstDate && x.CreatedAt < secondDate).Count()) / (double)(FilteredEntries.Where(x => x.CreatedAt > firstDate && x.CreatedAt < secondDate).Count())) * 100;
                    if (double.IsNaN(ucesceDetraktora))
                    {
                        ucesceDetraktora = 0;
                    }
                    if (double.IsNaN(ucescePromotera))
                    {
                        ucescePromotera = 0;
                    }
                    NPSs.Add((int)Math.Ceiling(ucescePromotera - ucesceDetraktora));
                }
            }
            return NPSs;
        }
        private void SetCompareNPSChart()
        {
            string firstCompare = (cmbCompare.SelectedItem as Group).Name;
            chartCompareNPS.Series.Clear();
            chartCompareNPS.Series.Add(firstCompare);
            chartCompareNPS.Series[firstCompare].ChartType = SeriesChartType.Line;
            chartCompareNPS.ChartAreas[0].AxisX.IsMarginVisible = false;
            DateTime dateTime = DateTime.Now.AddMonths(-lastMonths);
            foreach (var item in CompareNPS(cmbCompare.SelectedItem as Group))
            {
                chartCompareNPS.Series[firstCompare].Points.AddXY(dateTime.Month+"/"+ dateTime.Year, item);
                dateTime = dateTime.AddMonths(1);
            }
        }

        private bool CompareTags(ICollection<Tag> tagsImport, ICollection<Tag> tagsGroup)
        {
            foreach (var tagG in tagsGroup)
            {
                foreach (var tagI in tagsImport)
                {
                    if (tagG.Id == tagI.Id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CompareOneTag(ICollection<Tag> tagsImport, Tag tag)
        {          
            foreach (var tagI in tagsImport)
            {
                if (tag.Id == tagI.Id)
                {
                    return true;
                }
            }
            return false;
        }

        private void bCompare_Click(object sender, EventArgs e)
        {
            applyFilters(cmbCompare.SelectedItem as Group);
            CalculateNPS();
            SetChartOverallNPS();
            SetCompareNPSChart();
        }

        private void CtrlChartNPS_Load(object sender, EventArgs e)
        {
            if (SearchedEntries != null && SearchedEntries.Count() > 0)
            {
                cmbCompare.Items.AddRange(db.Groups.Include(x => x.Tags).ToArray());                
                if (cmbCompare.Items.Count > 0)
                {
                    cmbCompare.SelectedItem = cmbCompare.Items[0];
                }
                loadComboboxTags();
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                lastMonths = 6;
            }
            if (radioButton2.Checked)
            {
                lastMonths = 12;
            }
        }
        private void CalculateNPS()
        {
            if (FilteredEntries != null && FilteredEntries.Count() > 0)
            {
                double ucescePromotera = 0;
                double ucesceDetraktora = 0;
                ucesceDetraktora = ((double)(FilteredEntries.Where(x => x.Ocena >= 0 && x.Ocena <= 6).Count()) / (double)(FilteredEntries.Count())) * 100;
                ucescePromotera = ((double)(FilteredEntries.Where(x => x.Ocena >= 9 && x.Ocena <= 10).Count()) / (double)(FilteredEntries.Count())) * 100;

                labelNPS.Text = ((int)Math.Ceiling(ucescePromotera - ucesceDetraktora)).ToString();
            }
            else
            {
                labelNPS.Text = "";
            }
        }
        private void SetChartOverallNPS()
        {
            chartOverallNPS.Series.Clear();
            chartOverallNPS.Legends.Clear();
            if (FilteredEntries != null && FilteredEntries.Count() > 0)
            {
                chartOverallNPS.Series.Add("high");
                chartOverallNPS.Series.Add("medium");
                chartOverallNPS.Series.Add("low");
                chartOverallNPS.Series["low"].ChartType = SeriesChartType.Bar;
                chartOverallNPS.Series["medium"].ChartType = SeriesChartType.Bar;
                chartOverallNPS.Series["high"].ChartType = SeriesChartType.Bar;
                chartOverallNPS.Series["low"].Color = Color.Red;
                chartOverallNPS.Series["medium"].Color = Color.Yellow;
                chartOverallNPS.Series["high"].Color = Color.Green;
                chartOverallNPS.ChartAreas[0].AxisX.LabelStyle.Enabled = false;

                chartOverallNPS.Series["low"].Points.AddXY("", 100.0 / FilteredEntries.Count() * FilteredEntries.Where(x => x.Ocena < 7).Count());
                chartOverallNPS.Series["medium"].Points.AddXY("", 100.0 / FilteredEntries.Count() * FilteredEntries.Where(x => x.Ocena == 7 || x.Ocena == 8).Count());
                chartOverallNPS.Series["high"].Points.AddXY("", 100.0 / FilteredEntries.Count() * FilteredEntries.Where(x => x.Ocena > 8).Count());
                chartOverallNPS.Series["low"].Label = 100.0 / FilteredEntries.Count() * FilteredEntries.Where(x => x.Ocena < 7).Count() + " %";
                chartOverallNPS.Series["medium"].Label = 100.0 / FilteredEntries.Count() * FilteredEntries.Where(x => x.Ocena == 7 || x.Ocena == 8).Count() + " %";
                chartOverallNPS.Series["high"].Label = 100.0 / FilteredEntries.Count() * FilteredEntries.Where(x => x.Ocena > 8).Count() + " %";


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
        private void applyFilters(Group group)
        {
            DateTime dateTime = DateTime.Now.AddMonths(-lastMonths);
            DateTime firstDate = new DateTime(dateTime.Year, dateTime.Month, 1);
            DateTime secondDate = firstDate.AddMonths(lastMonths);
            FilteredEntries = SearchedEntries.Where(x => CompareTags(x.ImportData.Tags, group.Tags)).Where(x => x.CreatedAt > firstDate && x.CreatedAt < secondDate);
            if (cmbCompareTags.SelectedItem is Tag)
            {
                FilteredEntries = SearchedEntries.Where(x => CompareOneTag(x.ImportData.Tags, (cmbCompareTags.SelectedItem as Tag)));
            }
        }
        private void loadComboboxTags()
        {
            cmbCompareTags.Items.Clear();
            if (cmbCompare.SelectedItem is Group)
            {
                int id = (cmbCompare.SelectedItem as Group).Id;
                cmbCompareTags.Items.AddRange(db.Tags.Where(x => x.GroupId == id).ToArray());
                cmbCompareTags.Items.Insert(0, "Sve");
                if (cmbCompare.Items.Count > 0)
                {
                    cmbCompareTags.SelectedItem = cmbCompareTags.Items[0];
                }
            }            
        }

        private void cmbCompare_SelectedValueChanged(object sender, EventArgs e)
        {
            loadComboboxTags();
        }
    }
}
