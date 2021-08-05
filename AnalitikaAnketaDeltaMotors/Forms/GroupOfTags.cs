using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using UnitOfWorkExample.UnitOfWork;
using UnitOfWorkExample.UnitOfWork.Models;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class GroupOfTags : Form
    {
        public BindingList<Group> AllGroups { get; set; }

        int _selectedGroupId = -1;

        BindingList<Tag> _listOfTags;

        IEnumerable<Tag> _filteredData;

        bool change = false;

        DatabaseContext context = new DatabaseContext();
        public GroupOfTags()
        {
            InitializeComponent();
        }

        private void GroupOfTags_Load(object sender, EventArgs e)
        {
            context.Groups.Include(x => x.Tags).Load();
            AllGroups = context.Groups.Local.ToBindingList();
            SetDataGrid();
        }
        private void SetDataGrid()
        {
            dataGridView1.DataSource = AllGroups;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            InitializeDataGridView();
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if ((Group)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DataBoundItem != null 
                && ((Group)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DataBoundItem).Id != 0)
            {
                _selectedGroupId = ((Group)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DataBoundItem).Id;
                _filteredData = AllGroups.Where(x => x.Id == _selectedGroupId).FirstOrDefault().Tags;
                InitializeListOfTags();
                InitDatagrid();
            }
            else
            {
                dataGridView2.DataSource = null;
            }
        }

        private void listOfTags_ListChanged(object sender, ListChangedEventArgs e)
        {
            change = true;
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                _listOfTags[e.NewIndex].GroupId = _selectedGroupId;
                _listOfTags[e.NewIndex].Id = -1;
            }
        }

        private void InitializeDataGridView()
        {
            dataGridView1.Columns["Name"].HeaderText = "Naziv";
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Tags"].Visible = false;
            dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateData();
            TestChanges();
            int result = context.SaveChanges();

            if (AllGroups.Where(x => x.Id == _selectedGroupId).FirstOrDefault() != null)
            {
                _selectedGroupId = ((Group)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DataBoundItem).Id;
                _filteredData = AllGroups.Where(x => x.Id == _selectedGroupId).FirstOrDefault().Tags;
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
            if (_listOfTags == null)
            {
                return;
            }
            var forAdding = _listOfTags.Where(x => x.Id == -1);
            var forDeletion = _filteredData.Select(x => x.Id).Except(_listOfTags.Where(x => x.Id != -1).Select(x => x.Id)).ToList();
            foreach (var id in forDeletion)
            {
                var ob = context.Tags.Find(id);
                context.Tags.Remove(ob);
            }
            foreach (var item in forAdding)
            {
                AllGroups
                    .Where(x => x.Id == _listOfTags.FirstOrDefault().GroupId)
                    .FirstOrDefault()
                    .Tags
                    .Add(new UnitOfWork.Models.Tag()
                    {
                        GroupId = item.GroupId,
                        Name = item.Name,
                    });
            }
        }

        private void InitDatagrid()
        { 
            dataGridView2.DataSource = _listOfTags;

            dataGridView2.Columns["Name"].HeaderText = "Naziv";
            dataGridView2.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns["Id"].Visible = false;
            dataGridView2.Columns["GroupId"].Visible = false;
            dataGridView2.Columns["Group"].Visible = false;
            dataGridView2.Columns["ImportDatas"].Visible = false;
        }

        private void InitializeListOfTags()
        {
            _listOfTags = new BindingList<Tag>();

            _listOfTags.AllowNew = true;
            _listOfTags.AllowRemove = true;
            _listOfTags.RaiseListChangedEvents = true;
            _listOfTags.AllowEdit = true;

            foreach (var item in _filteredData)
            {
                _listOfTags.Add(item);
            }

            _listOfTags.ListChanged += new ListChangedEventHandler(listOfTags_ListChanged);
        }
        private void TestChanges()
        {
            foreach (var item in context.ChangeTracker.Entries())
            {
                if (item.State == EntityState.Deleted)
                {
                    if (item.Entity is Tag)
                    {
                        int id = (item.Entity as Tag).Id;
                        if (context.ImportDatas.Where(x => x.Tags.Where(c => c.Id == id).ToList().Count > 0).ToList().Count > 0)
                        {
                            item.State = EntityState.Unchanged;
                        }
                    }
                    if (item.Entity is Group)
                    {
                        int id = (item.Entity as Group).Id;
                        int nTags = (item.Entity as Group).Tags.Count;
                        if (context.Tags.Where(x => x.GroupId == id).ToList().Count > 0) 
                        {
                            item.State = EntityState.Unchanged;
                            foreach (var tagItem in context.ChangeTracker.Entries())
                            {
                                if (tagItem.Entity is Tag)
                                {
                                    int groupIdTag = (tagItem.Entity as Tag).GroupId;
                                    if (id == groupIdTag)
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
