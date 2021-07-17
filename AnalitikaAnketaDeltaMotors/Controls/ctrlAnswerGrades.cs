using System;
using System.Windows.Forms;
using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using static AnalitikaAnketaDeltaMotors.Classes.Utils;

namespace AnalitikaAnketaDeltaMotors.Controls
{
    public partial class ctrlAnswerGrades : UserControl
    {
        public EntryScore entryScore;
        public ctrlAnswerGrades()
        {
            InitializeComponent();
        }
        public ctrlAnswerGrades(EntryScore entryScore)
        {
            InitializeComponent();
            label1.Text = entryScore.Subtopic.Name;
            label2.Text = "("+entryScore.Subtopic.Topic.Name+")";
            this.entryScore = entryScore;
            checkRadioButton(entryScore.Score);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) 
            {
                entryScore.Score = Score.Low;
            }
            if (radioButton2.Checked) 
            {
                entryScore.Score = Score.Medium;
            }
            if (radioButton3.Checked) 
            {
                entryScore.Score = Score.High;
            }
        }
        private void checkRadioButton(Score score)
        {
            if (score == Score.Low)
            {
                radioButton1.Checked = true;
            }
            if (score == Score.Medium)
            {
                radioButton2.Checked = true;
            }
            if (score == Score.High)
            {
                radioButton3.Checked = true;
            }
        }
    }
}
