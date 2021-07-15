using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalitikaAnketaDeltaMotors.Classes;
using UnitOfWorkExample.UnitOfWork.Models;

namespace AnalitikaAnketaDeltaMotors.Controls
{
    public partial class ctrlAnswerGrades : UserControl
    {
        Utils.Score Score;
        public ctrlAnswerGrades()
        {
            InitializeComponent();
        }
        public ctrlAnswerGrades(Subtopic subtopic)
        {
            label1.Text = subtopic.Name;
            label2.Text = subtopic.Topic.Name;
            InitializeComponent();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) 
            {
                Score = Utils.Score.Low;
            }
            if (radioButton2.Checked) 
            {
                Score = Utils.Score.Medium;
            }
            if (radioButton3.Checked) 
            {
                Score = Utils.Score.High;
            }
        }
    }
}
