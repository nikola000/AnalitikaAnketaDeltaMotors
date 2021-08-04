
namespace AnalitikaAnketaDeltaMotors.Controls
{
    partial class CtrlChartNPS
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartCompareNPS = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cmbCompare = new System.Windows.Forms.ComboBox();
            this.bCompare = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.labelNPS = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chartOverallNPS = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cmbCompareTags = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartCompareNPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartOverallNPS)).BeginInit();
            this.SuspendLayout();
            // 
            // chartCompareNPS
            // 
            chartArea1.Name = "ChartArea1";
            this.chartCompareNPS.ChartAreas.Add(chartArea1);
            this.chartCompareNPS.Location = new System.Drawing.Point(358, 0);
            this.chartCompareNPS.Name = "chartCompareNPS";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            this.chartCompareNPS.Series.Add(series1);
            this.chartCompareNPS.Size = new System.Drawing.Size(722, 581);
            this.chartCompareNPS.TabIndex = 0;
            this.chartCompareNPS.Text = "chart1";
            // 
            // cmbCompare
            // 
            this.cmbCompare.FormattingEnabled = true;
            this.cmbCompare.Location = new System.Drawing.Point(38, 24);
            this.cmbCompare.Name = "cmbCompare";
            this.cmbCompare.Size = new System.Drawing.Size(121, 21);
            this.cmbCompare.TabIndex = 1;
            this.cmbCompare.SelectedValueChanged += new System.EventHandler(this.cmbCompare_SelectedValueChanged);
            // 
            // bCompare
            // 
            this.bCompare.Location = new System.Drawing.Point(38, 86);
            this.bCompare.Name = "bCompare";
            this.bCompare.Size = new System.Drawing.Size(75, 23);
            this.bCompare.TabIndex = 2;
            this.bCompare.Text = "Prikazi";
            this.bCompare.UseVisualStyleBackColor = true;
            this.bCompare.Click += new System.EventHandler(this.bCompare_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(222, 24);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(118, 17);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Poslednjih 6 meseci";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(222, 63);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(124, 17);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.Text = "Poslednjih 12 meseci";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // labelNPS
            // 
            this.labelNPS.AutoSize = true;
            this.labelNPS.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNPS.Location = new System.Drawing.Point(55, 167);
            this.labelNPS.Name = "labelNPS";
            this.labelNPS.Size = new System.Drawing.Size(78, 55);
            this.labelNPS.TabIndex = 10;
            this.labelNPS.Text = "00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 29);
            this.label3.TabIndex = 9;
            this.label3.Text = "NPS";
            // 
            // chartOverallNPS
            // 
            chartArea2.Name = "ChartArea1";
            this.chartOverallNPS.ChartAreas.Add(chartArea2);
            legend1.Name = "Legend1";
            this.chartOverallNPS.Legends.Add(legend1);
            this.chartOverallNPS.Location = new System.Drawing.Point(38, 244);
            this.chartOverallNPS.Name = "chartOverallNPS";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartOverallNPS.Series.Add(series2);
            this.chartOverallNPS.Size = new System.Drawing.Size(300, 300);
            this.chartOverallNPS.TabIndex = 11;
            this.chartOverallNPS.Text = "chart1";
            // 
            // cmbCompareTags
            // 
            this.cmbCompareTags.FormattingEnabled = true;
            this.cmbCompareTags.Location = new System.Drawing.Point(38, 51);
            this.cmbCompareTags.Name = "cmbCompareTags";
            this.cmbCompareTags.Size = new System.Drawing.Size(121, 21);
            this.cmbCompareTags.TabIndex = 12;
            // 
            // CtrlChartNPS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbCompareTags);
            this.Controls.Add(this.chartOverallNPS);
            this.Controls.Add(this.labelNPS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.bCompare);
            this.Controls.Add(this.cmbCompare);
            this.Controls.Add(this.chartCompareNPS);
            this.Name = "CtrlChartNPS";
            this.Size = new System.Drawing.Size(1080, 581);
            this.Load += new System.EventHandler(this.CtrlChartNPS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartCompareNPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartOverallNPS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartCompareNPS;
        private System.Windows.Forms.ComboBox cmbCompare;
        private System.Windows.Forms.Button bCompare;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label labelNPS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartOverallNPS;
        private System.Windows.Forms.ComboBox cmbCompareTags;
    }
}
