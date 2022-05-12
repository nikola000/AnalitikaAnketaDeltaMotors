using AnalitikaAnketaDeltaMotors.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnitOfWorkExample.UnitOfWork.Models;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class SubtopicBookmarks : Form
    {
        CtrlSubtopicBookmarks _subtopicBookmarks;
        bool isOk = false;
        public List<Subtopic> Subtopics { get; set; }
        List<Subtopic> subtopicsTemp { get; set; }
        public SubtopicBookmarks()
        {
            InitializeComponent();
        }
        public SubtopicBookmarks(List<Subtopic> Subtopics)
        {
            this.Subtopics = Subtopics;
            subtopicsTemp = new List<Subtopic>();
            subtopicsTemp.AddRange(Subtopics);
            InitializeComponent();
        }
        private void SubtopicBookmarks_Load(object sender, EventArgs e)
        {
            _subtopicBookmarks = new CtrlSubtopicBookmarks(Subtopics);
            _subtopicBookmarks.Dock = DockStyle.Fill;
            panel1.Controls.Add(_subtopicBookmarks);
        }
        private void SubtopicBookmarks_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isOk)
            {
                Subtopics = _subtopicBookmarks.GetCheckedSubtopics();     
            }
            else
            {
                Subtopics = subtopicsTemp;
            }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            isOk = true;
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            isOk = false;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
