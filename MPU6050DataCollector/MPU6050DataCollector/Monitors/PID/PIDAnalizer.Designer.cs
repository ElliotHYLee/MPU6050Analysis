namespace MPU6050DataCollector.Monitors.PID
{
    partial class PIDAnalyzer
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chPitch = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnPitchSP = new System.Windows.Forms.Button();
            this.txtPitchSP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPitchStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chPitch)).BeginInit();
            this.SuspendLayout();
            // 
            // chPitch
            // 
            chartArea1.AxisY.Maximum = 30D;
            chartArea1.AxisY.Minimum = -30D;
            chartArea1.Name = "ChartArea1";
            this.chPitch.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chPitch.Legends.Add(legend1);
            this.chPitch.Location = new System.Drawing.Point(12, 12);
            this.chPitch.Name = "chPitch";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            this.chPitch.Series.Add(series1);
            this.chPitch.Series.Add(series2);
            this.chPitch.Size = new System.Drawing.Size(999, 239);
            this.chPitch.TabIndex = 0;
            // 
            // btnPitchSP
            // 
            this.btnPitchSP.Location = new System.Drawing.Point(1224, 53);
            this.btnPitchSP.Name = "btnPitchSP";
            this.btnPitchSP.Size = new System.Drawing.Size(85, 20);
            this.btnPitchSP.TabIndex = 1;
            this.btnPitchSP.Text = "Set";
            this.btnPitchSP.UseVisualStyleBackColor = true;
            this.btnPitchSP.Click += new System.EventHandler(this.btnPitchSP_Click);
            // 
            // txtPitchSP
            // 
            this.txtPitchSP.Location = new System.Drawing.Point(1129, 53);
            this.txtPitchSP.Name = "txtPitchSP";
            this.txtPitchSP.Size = new System.Drawing.Size(80, 20);
            this.txtPitchSP.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1043, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pitch Set Point";
            // 
            // btnPitchStart
            // 
            this.btnPitchStart.Location = new System.Drawing.Point(1028, 12);
            this.btnPitchStart.Name = "btnPitchStart";
            this.btnPitchStart.Size = new System.Drawing.Size(92, 32);
            this.btnPitchStart.TabIndex = 4;
            this.btnPitchStart.Text = "Start Pitch";
            this.btnPitchStart.UseVisualStyleBackColor = true;
            this.btnPitchStart.Click += new System.EventHandler(this.btnPitchStart_Click);
            // 
            // PIDAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1510, 620);
            this.Controls.Add(this.btnPitchStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPitchSP);
            this.Controls.Add(this.btnPitchSP);
            this.Controls.Add(this.chPitch);
            this.Name = "PIDAnalyzer";
            this.Text = "PIDAnalizer";
            this.Load += new System.EventHandler(this.PIDAnalizer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chPitch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chPitch;
        private System.Windows.Forms.Button btnPitchSP;
        private System.Windows.Forms.TextBox txtPitchSP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPitchStart;
    }
}