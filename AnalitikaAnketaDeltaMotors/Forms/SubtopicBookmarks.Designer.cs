
namespace AnalitikaAnketaDeltaMotors.Forms
{
    partial class SubtopicBookmarks
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bCancel = new System.Windows.Forms.Button();
            this.bOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(440, 334);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 3;
            this.bCancel.Text = "Odustani";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(359, 334);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 2;
            this.bOk.Text = "U redu";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // SubtopicBookmarks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 369);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOk);
            this.Name = "SubtopicBookmarks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SubtopicBookmarks";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubtopicBookmarks_FormClosing);
            this.Load += new System.EventHandler(this.SubtopicBookmarks_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOk;
    }
}