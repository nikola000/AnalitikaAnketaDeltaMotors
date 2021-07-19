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
        public List<Subtopic> Subtopics { get; set; }
        public SubtopicBookmarks()
        {
            InitializeComponent();
        }
        public SubtopicBookmarks(List<Subtopic> Subtopics)
        {
            this.Subtopics = Subtopics;
            InitializeComponent();
        }
        private void SubtopicBookmarks_Load(object sender, EventArgs e)
        {
            _subtopicBookmarks = new CtrlSubtopicBookmarks(Subtopics);
            _subtopicBookmarks.Dock = DockStyle.Fill;
            Controls.Add(_subtopicBookmarks);
        }
        private void SubtopicBookmarks_FormClosing(object sender, FormClosingEventArgs e)
        {
            Subtopics = _subtopicBookmarks.GetCheckedSubtopics();
        }
    }
}
