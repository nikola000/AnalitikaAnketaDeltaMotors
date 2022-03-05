using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnitOfWorkExample.UnitOfWork;
using UnitOfWorkExample.UnitOfWork.Models;
using System.Data.Entity;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class Topics : Form
    {
        public BindingList<Topic> AllTopics { get; set; }
        int _selectedTopicId = -1;
        BindingList<Subtopic> _listOfSubtopics;
        IEnumerable<Subtopic> _filteredData;
        bool change = false;
        DatabaseContext context = new DatabaseContext();
        public Topics()
        {
            InitializeComponent();
        }

        private void Topics_Load(object sender, EventArgs e)
        {
            context.Topics.Include(x => x.Subtopics).Load();
            AllTopics = context.Topics.Local.ToBindingList();
            SetDataGrid();
        }
        private void SetDataGrid()
        {
            dataGridView1.DataSource = AllTopics;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            InitializeDataGridView();
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if ((Topic)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DataBoundItem != null
                && ((Topic)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DataBoundItem).Id != 0)
                {
                    _selectedTopicId = ((Topic)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DataBoundItem).Id;
                    _filteredData = AllTopics.Where(x => x.Id == _selectedTopicId).FirstOrDefault().Subtopics;
                    InitializeListOfTags();
                    InitDatagrid();
                }
                else
                {
                    dataGridView2.DataSource = null;
                }
            }
            catch (Exception)
            {

            }
            
        }
        private void _listOfSubtopics_ListChanged(object sender, ListChangedEventArgs e)
        {
            change = true;
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                _listOfSubtopics[e.NewIndex].TopicId = _selectedTopicId;
                _listOfSubtopics[e.NewIndex].Id = -1;
            }
        }
        private void InitializeDataGridView()
        {
            dataGridView1.Columns["Name"].HeaderText = "Naziv";
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Subtopics"].Visible = false;
            dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateData();
            TestChanges();
            int result = context.SaveChanges();

            if (AllTopics.Where(x => x.Id == _selectedTopicId).FirstOrDefault() != null)
            {
                _selectedTopicId = ((Topic)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DataBoundItem).Id;
                _filteredData = AllTopics.Where(x => x.Id == _selectedTopicId).FirstOrDefault().Subtopics;
                InitializeListOfTags();
                InitDatagrid();
            }

            change = false;

            if (result > 0)
            {
                MessageBox.Show("Izmene su uspesno sacuvane", "Cuvanje");
            }
        }
        

        private void UpdateData()
        {
            if (_listOfSubtopics == null)
            {
                return;
            }
            var forAdding = _listOfSubtopics.Where(x => x.Id == -1);
            var forDeletion = _filteredData.Select(x => x.Id).Except(_listOfSubtopics.Where(x => x.Id != -1).Select(x => x.Id)).ToList();
            foreach (var id in forDeletion)
            {
                var ob = context.Subtopics.Find(id);
                context.Subtopics.Remove(ob);
            }
            foreach (var item in forAdding)
            {
                AllTopics
                    .Where(x => x.Id == _listOfSubtopics.FirstOrDefault().TopicId)
                    .FirstOrDefault()
                    .Subtopics
                    .Add(new UnitOfWorkExample.UnitOfWork.Models.Subtopic()
                    {
                        TopicId = item.TopicId,
                        Name = item.Name,
                    });
            }
        }
        private void InitDatagrid()
        {
            dataGridView2.DataSource = _listOfSubtopics;

            dataGridView2.Columns["Name"].HeaderText = "Naziv";
            dataGridView2.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns["Id"].Visible = false;
            dataGridView2.Columns["TopicId"].Visible = false;
            dataGridView2.Columns["Topic"].Visible = false;
        }

        private void InitializeListOfTags()
        {
            _listOfSubtopics = new BindingList<Subtopic>();

            _listOfSubtopics.AllowNew = true;
            _listOfSubtopics.AllowRemove = true;
            _listOfSubtopics.RaiseListChangedEvents = true;
            _listOfSubtopics.AllowEdit = true;

            foreach (var item in _filteredData)
            {
                _listOfSubtopics.Add(item);
            }

            _listOfSubtopics.ListChanged += new ListChangedEventHandler(_listOfSubtopics_ListChanged);
        }
        private void TestChanges()
        {
            foreach (var item in context.ChangeTracker.Entries())
            {
                if (item.State == EntityState.Deleted)
                {
                    if (item.Entity is Subtopic)
                    {
                        int id = (item.Entity as Subtopic).Id;
                        if (context.EntryScores.Where(x => x.SubtopicId == id).ToList().Count > 0)
                        {
                            item.State = EntityState.Unchanged;
                        }
                    }
                    if (item.Entity is Topic)
                    {
                        int id = (item.Entity as Topic).Id;
                        if (context.Subtopics.Where(x => x.TopicId == id).ToList().Count > 0)
                        {
                            item.State = EntityState.Unchanged;
                            foreach (var tagItem in context.ChangeTracker.Entries())
                            {
                                if (tagItem.Entity is Subtopic)
                                {
                                    int topicIdTag = (tagItem.Entity as Subtopic).TopicId;
                                    if (id == topicIdTag)
                                    {
                                        tagItem.State = EntityState.Unchanged;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
