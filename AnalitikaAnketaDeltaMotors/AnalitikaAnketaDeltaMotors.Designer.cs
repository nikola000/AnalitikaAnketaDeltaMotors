
namespace AnalitikaAnketaDeltaMotors
{
    partial class AnalitikaAnketaDeltaMotors
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sistemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prijavaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.korisniciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.grupeOznakaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oznakeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.topicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subtopicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.izlazToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.podesavanjaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bazaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelLogin = new System.Windows.Forms.ToolStripStatusLabel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.entityCommand1 = new System.Data.Entity.Core.EntityClient.EntityCommand();
            this.bUcitaj = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sistemToolStripMenuItem,
            this.ToolStripMenuItem2,
            this.podesavanjaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(862, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // sistemToolStripMenuItem
            // 
            this.sistemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prijavaToolStripMenuItem,
            this.korisniciToolStripMenuItem,
            this.toolStripSeparator2,
            this.grupeOznakaToolStripMenuItem,
            this.oznakeToolStripMenuItem,
            this.toolStripSeparator1,
            this.topicToolStripMenuItem,
            this.subtopicToolStripMenuItem,
            this.toolStripSeparator3,
            this.izlazToolStripMenuItem});
            this.sistemToolStripMenuItem.Name = "sistemToolStripMenuItem";
            this.sistemToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.sistemToolStripMenuItem.Text = "Sistem";
            // 
            // prijavaToolStripMenuItem
            // 
            this.prijavaToolStripMenuItem.Name = "prijavaToolStripMenuItem";
            this.prijavaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.prijavaToolStripMenuItem.Text = "Prijava";
            this.prijavaToolStripMenuItem.Click += new System.EventHandler(this.prijavaToolStripMenuItem_Click);
            // 
            // korisniciToolStripMenuItem
            // 
            this.korisniciToolStripMenuItem.Name = "korisniciToolStripMenuItem";
            this.korisniciToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.korisniciToolStripMenuItem.Text = "Korisnici";
            this.korisniciToolStripMenuItem.Click += new System.EventHandler(this.korisniciToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // grupeOznakaToolStripMenuItem
            // 
            this.grupeOznakaToolStripMenuItem.Name = "grupeOznakaToolStripMenuItem";
            this.grupeOznakaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.grupeOznakaToolStripMenuItem.Text = "Grupe oznaka";
            this.grupeOznakaToolStripMenuItem.Click += new System.EventHandler(this.grupeOznakaToolStripMenuItem_Click);
            // 
            // oznakeToolStripMenuItem
            // 
            this.oznakeToolStripMenuItem.Name = "oznakeToolStripMenuItem";
            this.oznakeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.oznakeToolStripMenuItem.Text = "Oznake";
            this.oznakeToolStripMenuItem.Click += new System.EventHandler(this.oznakeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // topicToolStripMenuItem
            // 
            this.topicToolStripMenuItem.Name = "topicToolStripMenuItem";
            this.topicToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.topicToolStripMenuItem.Text = "Topic";
            this.topicToolStripMenuItem.Click += new System.EventHandler(this.topicToolStripMenuItem_Click);
            // 
            // subtopicToolStripMenuItem
            // 
            this.subtopicToolStripMenuItem.Name = "subtopicToolStripMenuItem";
            this.subtopicToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.subtopicToolStripMenuItem.Text = "Subtopic";
            this.subtopicToolStripMenuItem.Click += new System.EventHandler(this.subtopicToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // izlazToolStripMenuItem
            // 
            this.izlazToolStripMenuItem.Name = "izlazToolStripMenuItem";
            this.izlazToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.izlazToolStripMenuItem.Text = "Izlaz";
            this.izlazToolStripMenuItem.Click += new System.EventHandler(this.izlazToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem2
            // 
            this.ToolStripMenuItem2.Name = "ToolStripMenuItem2";
            this.ToolStripMenuItem2.Size = new System.Drawing.Size(107, 20);
            this.ToolStripMenuItem2.Text = "Import podataka";
            // 
            // podesavanjaToolStripMenuItem
            // 
            this.podesavanjaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bazaToolStripMenuItem});
            this.podesavanjaToolStripMenuItem.Name = "podesavanjaToolStripMenuItem";
            this.podesavanjaToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.podesavanjaToolStripMenuItem.Text = "Podešavanja";
            // 
            // bazaToolStripMenuItem
            // 
            this.bazaToolStripMenuItem.Name = "bazaToolStripMenuItem";
            this.bazaToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.bazaToolStripMenuItem.Text = "Baza";
            this.bazaToolStripMenuItem.Click += new System.EventHandler(this.bazaToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelLogin});
            this.statusStrip1.Location = new System.Drawing.Point(0, 495);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(862, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelLogin
            // 
            this.toolStripStatusLabelLogin.Name = "toolStripStatusLabelLogin";
            this.toolStripStatusLabelLogin.Size = new System.Drawing.Size(0, 17);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new System.Drawing.Size(30, 120);
            this.tabControl1.Location = new System.Drawing.Point(210, 79);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(640, 413);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 5;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(124, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(512, 405);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dashboard";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(124, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(710, 433);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "All entries";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // entityCommand1
            // 
            this.entityCommand1.CommandTimeout = 0;
            this.entityCommand1.CommandTree = null;
            this.entityCommand1.Connection = null;
            this.entityCommand1.EnablePlanCaching = true;
            this.entityCommand1.Transaction = null;
            // 
            // bUcitaj
            // 
            this.bUcitaj.Location = new System.Drawing.Point(210, 50);
            this.bUcitaj.Name = "bUcitaj";
            this.bUcitaj.Size = new System.Drawing.Size(75, 23);
            this.bUcitaj.TabIndex = 6;
            this.bUcitaj.Text = "Ucitaj";
            this.bUcitaj.UseVisualStyleBackColor = true;
            this.bUcitaj.Click += new System.EventHandler(this.bUcitaj_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 74);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(191, 20);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(12, 121);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(191, 20);
            this.dateTimePicker2.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Datum od";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Datum do";
            // 
            // AnalitikaAnketaDeltaMotors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 517);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.bUcitaj);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AnalitikaAnketaDeltaMotors";
            this.Text = "Analitika anketa";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelLogin;
        private System.Windows.Forms.ToolStripMenuItem podesavanjaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sistemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prijavaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem korisniciToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem grupeOznakaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oznakeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem izlazToolStripMenuItem;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripMenuItem bazaToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Data.Entity.Core.EntityClient.EntityCommand entityCommand1;
        private System.Windows.Forms.ToolStripMenuItem topicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subtopicToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Button bUcitaj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}

