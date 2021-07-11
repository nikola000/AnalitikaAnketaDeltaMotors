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
using UnitOfWorkExample.UnitOfWork;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class TagBookmarks : Form
    {
        public List<int> Ids { get; set; } = new List<int>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public TagBookmarks()
        {
            InitializeComponent();
        }
        public TagBookmarks(List<Tag> Tags)
        {
            this.Tags = Tags;
            InitializeComponent();
        }
        private void TagBookmarks_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.WrapContents = true;
            using (DatabaseContext db = new DatabaseContext())
            {
                var groups = db.Groups.Include(x=>x.Tags).Where(x => x.Id > 0);
                foreach (var item in groups)
                {
                    GroupBox groupBox = new GroupBox();
                    groupBox.Name = "gb" + item.Id;
                    groupBox.Text = item.Name;
                    FlowLayoutPanel groupBoxFlowLayout = new FlowLayoutPanel();
                    groupBoxFlowLayout.AutoSize = true;
                    groupBoxFlowLayout.Dock = DockStyle.Fill;
                    groupBoxFlowLayout.FlowDirection = FlowDirection.TopDown;
                    groupBox.Controls.Add(groupBoxFlowLayout);
                    foreach (var tag in item.Tags)
                    {
                        CheckBox checkBox = new CheckBox();
                        checkBox.Name = tag.Id.ToString();
                        checkBox.Text = tag.Name;
                        for (int i = 0; i < Tags.Count; i++)
                        {
                            if (Tags[i].Id == tag.Id)
                            {
                                Tags.Remove(Tags[i]);
                                checkBox.Checked = true;
                            }
                        }
                        groupBoxFlowLayout.Controls.Add(checkBox);
                    }
                    groupBox.AutoSize = true;
                    flowLayoutPanel1.Controls.Add(groupBox);
                }               
            }
        }

        private void TagBookmarks_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var group in flowLayoutPanel1.Controls)
            {
                foreach (var flowLayoutPanel in ((GroupBox)group).Controls)
                {
                    foreach (var item in ((FlowLayoutPanel)flowLayoutPanel).Controls)
                    {
                        if (item is CheckBox)
                        {
                            if (((CheckBox)item).Checked)
                            {
                                Ids.Add(int.Parse(((CheckBox)item).Name));
                            }
                        }
                    }                   
                }               
            }           
        }
    }
}
