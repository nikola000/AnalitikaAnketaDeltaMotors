using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UnitOfWorkExample.UnitOfWork;
using System.Data.Entity;
using UnitOfWorkExample.UnitOfWork.Models;
using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using UnitOfWorkExample.Services;
using AnalitikaAnketaDeltaMotors.Classes;

namespace AnalitikaAnketaDeltaMotors.Controls
{
    public partial class CtrlAnswer : UserControl
    {
        Entry entry;
        EntryScore entryScoreTemp;
        List<Subtopic> disabledSubtopics;
        public Subtopic SelectedSubtopic { get; set; }
        public Classes.Utils.Score SelectedScore { get; set; }
        DatabaseContext dbContext = new DatabaseContext();
        Action<int> changeEntry;
        public CtrlAnswer()
        {
            InitializeComponent();
        }
        public CtrlAnswer(Entry entry, DatabaseContext dbContext, Action<int> changeEntry)
        {
            this.entry = entry;
            this.dbContext = dbContext;
            this.changeEntry = changeEntry;
            disabledSubtopics = new List<Subtopic>();
            InitializeComponent();
            this.listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            this.listBox1.DrawItem += new DrawItemEventHandler(this.listBox1_DrawItem);
            tbKomentar.Text = entry.Komentar;
        }

        private void ctrlAnswer_Load(object sender, EventArgs e)
        {
            foreach (var item in this.entry.EntryScores)
            {
                ctrlAnswerGrades ctrlAnswerGrades = new ctrlAnswerGrades(item);
                ctrlAnswerGrades.Width = flowLayoutPanel1.Width - 5;
                flowLayoutPanel1.Controls.Add(ctrlAnswerGrades);
                disabledSubtopics.Add(item.Subtopic);
            }
            fillSubtopics();
            richTextBox1.Text = entry.Odgovor;
        }

        private void DrawItemEventHandler(object sender, DrawItemEventArgs e)
        {
            
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            if (listBox1.Items.Count > 0)
            {
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var Topic in filterTopics())
            {
                listBox1.Items.Add(Topic);
                foreach (var Subtopic in filterSubtopics())
                {
                    if (Subtopic.TopicId == Topic.Id)
                        listBox1.Items.Add(Subtopic);
                }
            }
            
        }

        private void bMark_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                return;
            }
            if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked)
            {
                return;
            }
            entryScoreTemp = new EntryScore();
            entryScoreTemp.Subtopic = SelectedSubtopic;
            entryScoreTemp.Score = SelectedScore;
            entryScoreTemp.UserId = Configuration.GetInstance().CurrentUser.Id;
            ctrlAnswerGrades ctrlAnswerGrades = new ctrlAnswerGrades(entryScoreTemp);
            ctrlAnswerGrades.Width = flowLayoutPanel1.Width - 5;
            flowLayoutPanel1.Controls.Add(ctrlAnswerGrades);
            entry.EntryScores.Add(entryScoreTemp);
            disabledSubtopics.Add(entryScoreTemp.Subtopic);
            subtopicsAvailable();
            //entryScore.Subtopic = null;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                SelectedScore = Utils.Score.Low;
            }
            if (radioButton2.Checked)
            {
                SelectedScore = Utils.Score.Medium;
            }
            if (radioButton3.Checked)
            {
                SelectedScore = Utils.Score.High;
            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Topic)
            {
                listBox1.SelectedItem = null;
                return;
            }
            SelectedSubtopic = (Subtopic)listBox1.SelectedItem;
        }

        private void fillSubtopics()
        {
            listBox1.Visible = false;
            IEnumerable<Topic> topics = dbContext.Set<Topic>().AsEnumerable().ToList();
            int total = topics.Count();
            progressBar1.Maximum = total;
            progressBar1.Value = 0;
            foreach (var Topic in topics)
            {
                listBox1.Items.Add(Topic);
                foreach (var Subtopic in dbContext.Subtopics.Where(x => x.TopicId == Topic.Id).ToList())
                {
                    listBox1.Items.Add(Subtopic);
                }
                progressBar1.Value++;
            }
            subtopicsAvailable();
            listBox1.Visible = true;
            progressBar1.Value = 0;
        }
        private void subtopicsAvailable()
        {
            foreach (var item in disabledSubtopics)
            {
                listBox1.Items.Remove(item);
            }            
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            disabledSubtopics.Clear();
            listBox1.Items.Clear();
            dbContext.Set<EntryScore>().RemoveRange(this.entry.EntryScores);
            fillSubtopics();
        }
        private List<Subtopic> filterSubtopics()
        {
            var filtered = dbContext.Subtopics.Where(x => x.Name.Contains(textBox1.Text)).ToList();
            foreach (var item in disabledSubtopics)
            {
                filtered.Remove(item);
            }
            return filtered;
        }
        private List<Topic> filterTopics()
        {
            var filtered = dbContext.Topics.Where(x => x.Name.Contains(textBox1.Text)).ToList();
            foreach (var item in filterSubtopics())
            {
                var temp = dbContext.Topics.Where(x => x.Id==item.TopicId).FirstOrDefault();
                if (!filtered.Contains(temp))
                {
                    filtered.Add(temp);
                }
            }
            return filtered;
        }

        
        private void bSave_Click(object sender, EventArgs e)
        {
            dbContext.SaveChanges();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in flowLayoutPanel1.Controls)
            {
                if ((item as ctrlAnswerGrades).selected)
                {
                    disabledSubtopics.Remove((item as ctrlAnswerGrades).entryScore.Subtopic);
                    dbContext.Set<EntryScore>().Remove((item as ctrlAnswerGrades).entryScore);
                }
            }
            flowLayoutPanel1.Controls.Clear();
            foreach (var item in this.entry.EntryScores)
            {
                ctrlAnswerGrades ctrlAnswerGrades = new ctrlAnswerGrades(item);
                ctrlAnswerGrades.Width = flowLayoutPanel1.Width - 5;
                flowLayoutPanel1.Controls.Add(ctrlAnswerGrades);
                disabledSubtopics.Add(item.Subtopic);
            }
            listBox1.Items.Clear();
            fillSubtopics();
        }

        private void bNext_Click(object sender, EventArgs e)
        {
            changeEntry.Invoke(1);
        }

        private void bPrevious_Click(object sender, EventArgs e)
        {
            changeEntry.Invoke(-1);
        }

        internal void DisablePrevious(bool value)
        {
            bPrevious.Enabled = !value;
        }

        internal void DisableNext(bool value)
        {
            bNext.Enabled = !value;
        }
    }
}
