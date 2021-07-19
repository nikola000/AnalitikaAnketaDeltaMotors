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
using UnitOfWorkExample.UnitOfWork.Models;

namespace AnalitikaAnketaDeltaMotors.Controls
{
    public partial class CtrlSubtopicBookmarks : UserControl
    {
        public List<Subtopic> Subtopics;
        public CtrlSubtopicBookmarks()
        {
            InitializeComponent();
        }
        public CtrlSubtopicBookmarks(List<Subtopic> Subtopics)
        {
            this.Subtopics = Subtopics;
            InitializeComponent();
        }
        public List<Subtopic> GetCheckedSubtopics()
        {
            Subtopics.Clear();
            foreach (var topic in flowLayoutPanel1.Controls)
            {
                foreach (var flowLayoutPanel in ((GroupBox)topic).Controls)
                {
                    foreach (var item in ((FlowLayoutPanel)flowLayoutPanel).Controls)
                    {
                        if (item is CheckBox)
                        {
                            if (((CheckBox)item).Checked)
                            {
                                using (DatabaseContext db = new DatabaseContext())
                                {
                                    int id = int.Parse(((CheckBox)item).Name);
                                    Subtopics.Add(db.Subtopics.Where(x => x.Id == id).FirstOrDefault());
                                }
                            }
                        }
                    }
                }
            }
            return Subtopics;
        }
        private void CtrlSubtopicBookmarks_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.WrapContents = true;
            using (DatabaseContext db = new DatabaseContext())
            {
                foreach (var item in db.Topics.Include(x => x.Subtopics).Where(x => x.Id > 0))
                {
                    GroupBox groupBox = new GroupBox();
                    groupBox.Name = "gb" + item.Id;
                    groupBox.Text = item.Name;
                    FlowLayoutPanel groupBoxFlowLayout = new FlowLayoutPanel();
                    groupBoxFlowLayout.AutoSize = true;
                    groupBoxFlowLayout.Dock = DockStyle.Fill;
                    groupBoxFlowLayout.FlowDirection = FlowDirection.TopDown;
                    groupBox.Controls.Add(groupBoxFlowLayout);
                    foreach (var subtopic in item.Subtopics)
                    {
                        CheckBox checkBox = new CheckBox();
                        checkBox.Name = subtopic.Id.ToString();
                        checkBox.Text = subtopic.Name;
                        if (Subtopics == null)
                        {
                            Subtopics = new List<Subtopic>();
                        }
                        for (int i = 0; i < Subtopics.Count; i++)
                        {
                            if (Subtopics[i].Id == subtopic.Id)
                            {
                                Subtopics.Remove(Subtopics[i]);
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
    }
}
