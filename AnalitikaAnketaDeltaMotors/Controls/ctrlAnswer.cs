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
using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using AnalitikaAnketaDeltaMotors.Classes;


namespace AnalitikaAnketaDeltaMotors.Controls
{
    public partial class ctrlAnswer : UserControl
    {
        Entry entry;
        EntryScore entryScore;
        List<Subtopic> disabledSubtopics;
        DatabaseContext db = new DatabaseContext();
        public ctrlAnswer()
        {
            InitializeComponent();
        }
        public ctrlAnswer(Entry entry)
        {
            this.entry = entry;
            entryScore = new EntryScore();
            entryScore.Entry = entry;
            entryScore.EntryId = entry.Id;
            disabledSubtopics = new List<Subtopic>();
            InitializeComponent();
        }

        private void ctrlAnswer_Load(object sender, EventArgs e)
        {            
            db.Topics.Include(x => x.Subtopics).Load();
            foreach (var Topic in db.Set<Topic>().AsEnumerable().ToList())
            {
                listBox1.Items.Add(Topic);
                foreach (var Subtopic in db.Subtopics.Where(x => x.TopicId == Topic.Id).ToList())
                {
                    listBox1.Items.Add(Subtopic);
                }
            }
            this.listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            this.listBox1.DrawItem += new DrawItemEventHandler(this.listBox1_DrawItem);
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
            if (listBox1.Items[e.Index] is Topic)
            {
                e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), new Font(Font, FontStyle.Bold), new SolidBrush(ForeColor), e.Bounds.X, e.Bounds.Y);
            }
            else
            {
                e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), new Font(Font, FontStyle.Regular), new SolidBrush(ForeColor), e.Bounds.X, e.Bounds.Y);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();            
            db.Topics.Include(x => x.Subtopics).Load();
            var filtered = db.Subtopics.Where(x => x.Name.Contains(textBox1.Text)).ToList();
            List<Topic> filteredTopics=new List<Topic>();
            foreach (var item in filtered)
            {             
                filteredTopics = db.Topics.Where(x => x.Name.Contains(textBox1.Text) || x.Id == item.TopicId).ToList();
            }               
            foreach (var Topic in filteredTopics)
            {
                listBox1.Items.Add(Topic);
                foreach (var Subtopic in filtered)
                {                    
                    if (Subtopic.TopicId == Topic.Id)
                        listBox1.Items.Add(Subtopic);
                }
            }
            subtopicsAvailable();
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
            EntryScore entryScoreTemp = new EntryScore();
            entryScoreTemp = entryScore;
            ctrlAnswerGrades ctrlAnswerGrades = new ctrlAnswerGrades(entryScoreTemp);
            flowLayoutPanel1.Controls.Add(ctrlAnswerGrades);
            entry.EntryScores.Add(entryScore);
            disabledSubtopics.Add(entryScoreTemp.Subtopic);
            entryScore.Subtopic = null;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                entryScore.Score = Utils.Score.Low;
            }
            if (radioButton2.Checked)
            {
                entryScore.Score = Utils.Score.Medium;
            }
            if (radioButton3.Checked)
            {
                entryScore.Score = Utils.Score.High;
            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Topic)
            {
                listBox1.SelectedItem = null;
            }
            entryScore.Subtopic = (Subtopic)listBox1.SelectedItem;
        }
        
        private void subtopicsAvailable()
        {
            foreach (var item in disabledSubtopics)
            {
                listBox1.Items.Remove(item);
            }            
        }
    }
}
