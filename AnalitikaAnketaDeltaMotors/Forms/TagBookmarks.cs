using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using AnalitikaAnketaDeltaMotors.Controls;
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
        CtrlTagBookmarks _tagBookmarks;
        public List<Tag> Tags { get; set; }
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
            _tagBookmarks = new CtrlTagBookmarks(Tags);
            _tagBookmarks.Dock = DockStyle.Fill;
            Controls.Add(_tagBookmarks);
        }

        private void TagBookmarks_FormClosing(object sender, FormClosingEventArgs e)
        {
            Tags = _tagBookmarks.GetCheckedTags();
        }
    }
}
