using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AnalitikaAnketaDeltaMotors.Forms
{
    public partial class Podesavanja : Form
    {
        public Podesavanja()
        {
            InitializeComponent();
            UcitajFajl();
        }

        private void UcitajFajl()
        {
            using (FileStream fs = File.Open("config.db", FileMode.OpenOrCreate))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    textBox1.Text = temp.GetString(b);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FileStream fs = File.Create("config.db"))
            {
                AddText(fs, textBox1.Text);
            }
        }
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}
