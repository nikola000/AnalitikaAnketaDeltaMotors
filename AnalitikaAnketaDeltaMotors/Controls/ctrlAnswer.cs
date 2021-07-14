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
using System.Data.Entity;
using UnitOfWorkExample.UnitOfWork.Models;

namespace AnalitikaAnketaDeltaMotors.Controls
{
    public partial class ctrlAnswer : UserControl
    {
        public ctrlAnswer()
        {
            InitializeComponent();
        }

        private void ctrlAnswer_Load(object sender, EventArgs e)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.Topics.Include(x => x.Subtopics).Load();
                foreach (var Topic in db.Topics.Where(x => x.Id > 0).ToList())
                {
                    listBox1.Items.Add(Topic);
                    foreach (var Subtopic in db.Subtopics.Where(x => x.TopicId == Topic.Id).ToList())
                    {
                        listBox1.Items.Add(Subtopic);
                    }
                }
                this.listBox1.DrawMode = DrawMode.OwnerDrawFixed;
                this.listBox1.DrawItem += new DrawItemEventHandler(this.listBox1_DrawItem); ;
            }
        }

        private void DrawItemEventHandler(object sender, DrawItemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            if (listBox1.Items[e.Index] is Topic)
            {
                e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), new Font(Font, FontStyle.Bold), new SolidBrush(ForeColor), e.Bounds.X, e.Bounds.Y);
            }
            else
            {
                e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), new Font(Font, FontStyle.Regular), new SolidBrush(ForeColor), e.Bounds.X, e.Bounds.Y);
            }
        }
    }
}
