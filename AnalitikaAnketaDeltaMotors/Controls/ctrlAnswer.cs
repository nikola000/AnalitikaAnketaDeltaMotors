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
    public partial class ctrlAnswer : UserControl
    {
        Entry entry;
        EntryScore entryScoreTemp;
        List<Subtopic> disabledSubtopics;
        public Subtopic SelectedSubtopic { get; set; }
        public Classes.Utils.Score SelectedScore { get; set; }
        DatabaseContext db = new DatabaseContext();
        
        public ctrlAnswer()
        {
            InitializeComponent();
        }
        public ctrlAnswer(Entry entry)
        {
            this.entry = db.Set<Entry>().Find(entry.Id);
            disabledSubtopics = new List<Subtopic>();
            foreach (var item in entry.EntryScores)
            {
                disabledSubtopics.Add(item.Subtopic);
            }
            InitializeComponent();
        }

        private void ctrlAnswer_Load(object sender, EventArgs e)
        {
            fillSubtopics();
            string textAnswer= db.Entries.Where(x => x.Id == entry.Id).FirstOrDefault().Odgovor;
            richTextBox1.Text = textAnswer;                       
        }

        private void DrawItemEventHandler(object sender, DrawItemEventArgs e)
        {
            throw new NotImplementedException();
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
            this.listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            this.listBox1.DrawItem += new DrawItemEventHandler(this.listBox1_DrawItem);
        }

        private void bMark_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem==null)
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
            ctrlAnswerGrades ctrlAnswerGrades = new ctrlAnswerGrades(entryScoreTemp);
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
            foreach (var Topic in db.Set<Topic>().AsEnumerable().ToList())
            {
                listBox1.Items.Add(Topic);
                foreach (var Subtopic in db.Subtopics.Where(x => x.TopicId == Topic.Id).ToList())
                {
                    listBox1.Items.Add(Subtopic);
                }
            }
            subtopicsAvailable();
            this.listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            this.listBox1.DrawItem += new DrawItemEventHandler(this.listBox1_DrawItem);
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
            fillSubtopics();
        }
        private List<Subtopic> filterSubtopics()
        {
            var filtered = db.Subtopics.Where(x => x.Name.Contains(textBox1.Text)).ToList();
            foreach (var item in disabledSubtopics)
            {
                filtered.Remove(item);
            }
            return filtered;
        }
        private List<Topic> filterTopics()
        {
            var filtered = db.Topics.Where(x => x.Name.Contains(textBox1.Text)).ToList();
            foreach (var item in filterSubtopics())
            {
                var temp = db.Topics.Where(x => x.Id==item.TopicId).FirstOrDefault();
                if (!filtered.Contains(temp))
                {
                    filtered.Add(temp);
                }
            }
            return filtered;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            db.SaveChanges();
        }
    }
}
