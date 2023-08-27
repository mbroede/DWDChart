namespace DWDChart
{
    partial class frmMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statlblDWDLink = new System.Windows.Forms.ToolStripStatusLabel();
            this.panTop = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTag = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMonat = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbKennzahl = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbStation = new System.Windows.Forms.ComboBox();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.mnuDatei = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDateiSpeichern = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDateiBeenden = new System.Windows.Forms.ToolStripMenuItem();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabChart = new System.Windows.Forms.TabPage();
            this.chartDWD = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabGrid = new System.Windows.Forms.TabPage();
            this.gridDWD = new System.Windows.Forms.DataGridView();
            this.splitter = new System.Windows.Forms.Splitter();
            this.panBottom = new System.Windows.Forms.Panel();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.cbJahr = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.panTop.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tabChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDWD)).BeginInit();
            this.tabGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDWD)).BeginInit();
            this.panBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // cbStation
            // 
            this.cbStation.FormattingEnabled = true;
            this.cbStation.Location = new System.Drawing.Point(12, 22);
            this.cbStation.Name = "cbStation";
            this.cbStation.Size = new System.Drawing.Size(282, 21);
            this.cbStation.TabIndex = 1;
            // 
            // cbKennzahl
            // 
            this.cbKennzahl.FormattingEnabled = true;
            this.cbKennzahl.Location = new System.Drawing.Point(303, 22);
            this.cbKennzahl.Name = "cbKennzahl";
            this.cbKennzahl.Size = new System.Drawing.Size(161, 21);
            this.cbKennzahl.TabIndex = 2;
            // 
            // cbJahr
            // 
            this.cbJahr.FormattingEnabled = true;
            this.cbJahr.Location = new System.Drawing.Point(472, 22);
            this.cbJahr.Name = "cbJahr";
            this.cbJahr.Size = new System.Drawing.Size(82, 21);
            this.cbJahr.TabIndex = 3;
            // 
            // cbMonat
            // 
            this.cbMonat.FormattingEnabled = true;
            this.cbMonat.Location = new System.Drawing.Point(561, 22);
            this.cbMonat.Name = "cbMonat";
            this.cbMonat.Size = new System.Drawing.Size(82, 21);
            this.cbMonat.TabIndex = 4;
            // 
            // cbTag
            // 
            this.cbTag.FormattingEnabled = true;
            this.cbTag.Location = new System.Drawing.Point(650, 22);
            this.cbTag.Name = "cbTag";
            this.cbTag.Size = new System.Drawing.Size(50, 21);
            this.cbTag.TabIndex = 5;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.statlblDWDLink});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(265, 17);
            this.toolStripStatusLabel1.Text = "Daten bereitgestellt vom Deutschen Wetterdienst";
            // 
            // statlblDWDLink
            // 
            this.statlblDWDLink.IsLink = true;
            this.statlblDWDLink.Name = "statlblDWDLink";
            this.statlblDWDLink.Size = new System.Drawing.Size(76, 17);
            this.statlblDWDLink.Text = "www.dwd.de";
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.label4);
            this.panTop.Controls.Add(this.cbTag);
            this.panTop.Controls.Add(this.label5);
            this.panTop.Controls.Add(this.label3);
            this.panTop.Controls.Add(this.cbJahr);
            this.panTop.Controls.Add(this.cbMonat);
            this.panTop.Controls.Add(this.label2);
            this.panTop.Controls.Add(this.cbKennzahl);
            this.panTop.Controls.Add(this.label1);
            this.panTop.Controls.Add(this.cbStation);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(0, 24);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(800, 52);
            this.panTop.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(649, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tag";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(560, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Monat";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(302, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Kennzahl";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Station";
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDatei});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(800, 24);
            this.menuMain.TabIndex = 10;
            this.menuMain.Text = "menuStrip1";
            // 
            // mnuDatei
            // 
            this.mnuDatei.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDateiSpeichern,
            this.toolStripMenuItem1,
            this.mnuDateiBeenden});
            this.mnuDatei.Name = "mnuDatei";
            this.mnuDatei.Size = new System.Drawing.Size(46, 20);
            this.mnuDatei.Text = "Datei";
            // 
            // mnuDateiSpeichern
            // 
            this.mnuDateiSpeichern.Name = "mnuDateiSpeichern";
            this.mnuDateiSpeichern.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuDateiSpeichern.Size = new System.Drawing.Size(208, 22);
            this.mnuDateiSpeichern.Text = "Chart speichern...";
            this.mnuDateiSpeichern.Click += new System.EventHandler(this.mnuDateiSpeichern_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(205, 6);
            // 
            // mnuDateiBeenden
            // 
            this.mnuDateiBeenden.Name = "mnuDateiBeenden";
            this.mnuDateiBeenden.Size = new System.Drawing.Size(208, 22);
            this.mnuDateiBeenden.Text = "Beenden";
            this.mnuDateiBeenden.Click += new System.EventHandler(this.mnuDateiBeenden_Click);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabChart);
            this.tcMain.Controls.Add(this.tabGrid);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcMain.Location = new System.Drawing.Point(0, 76);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(800, 280);
            this.tcMain.TabIndex = 5;
            // 
            // tabChart
            // 
            this.tabChart.Controls.Add(this.chartDWD);
            this.tabChart.Location = new System.Drawing.Point(4, 22);
            this.tabChart.Name = "tabChart";
            this.tabChart.Padding = new System.Windows.Forms.Padding(3);
            this.tabChart.Size = new System.Drawing.Size(792, 254);
            this.tabChart.TabIndex = 0;
            this.tabChart.Text = "Chart";
            this.tabChart.UseVisualStyleBackColor = true;
            // 
            // chartDWD
            // 
            chartArea2.Name = "ChartArea1";
            this.chartDWD.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartDWD.Legends.Add(legend2);
            this.chartDWD.Location = new System.Drawing.Point(65, 51);
            this.chartDWD.Name = "chartDWD";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartDWD.Series.Add(series2);
            this.chartDWD.Size = new System.Drawing.Size(300, 120);
            this.chartDWD.TabIndex = 0;
            this.chartDWD.Text = "chart1";
            // 
            // tabGrid
            // 
            this.tabGrid.Controls.Add(this.gridDWD);
            this.tabGrid.Location = new System.Drawing.Point(4, 22);
            this.tabGrid.Name = "tabGrid";
            this.tabGrid.Padding = new System.Windows.Forms.Padding(3);
            this.tabGrid.Size = new System.Drawing.Size(792, 254);
            this.tabGrid.TabIndex = 1;
            this.tabGrid.Text = "Tabelle";
            this.tabGrid.UseVisualStyleBackColor = true;
            // 
            // gridDWD
            // 
            this.gridDWD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDWD.Location = new System.Drawing.Point(55, 36);
            this.gridDWD.Name = "gridDWD";
            this.gridDWD.Size = new System.Drawing.Size(240, 150);
            this.gridDWD.TabIndex = 0;
            // 
            // splitter
            // 
            this.splitter.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.splitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter.Location = new System.Drawing.Point(0, 356);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(800, 10);
            this.splitter.TabIndex = 6;
            this.splitter.TabStop = false;
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.txtInfo);
            this.panBottom.Controls.Add(this.picLogo);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panBottom.Location = new System.Drawing.Point(0, 366);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(800, 62);
            this.panBottom.TabIndex = 7;
            // 
            // txtInfo
            // 
            this.txtInfo.Location = new System.Drawing.Point(349, 6);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(100, 20);
            this.txtInfo.TabIndex = 3;
            // 
            // picLogo
            // 
            this.picLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.picLogo.Image = global::DWDChart.Properties.Resources.dwd_logo_png;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(226, 62);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 2;
            this.picLogo.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(471, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Jahr";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panBottom);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.panTop);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "frmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panTop.ResumeLayout(false);
            this.panTop.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.tabChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDWD)).EndInit();
            this.tabGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDWD)).EndInit();
            this.panBottom.ResumeLayout(false);
            this.panBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panTop;
        private System.Windows.Forms.ComboBox cbStation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbTag;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMonat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbKennzahl;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuDatei;
        private System.Windows.Forms.ToolStripMenuItem mnuDateiBeenden;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statlblDWDLink;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tabChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDWD;
        private System.Windows.Forms.TabPage tabGrid;
        private System.Windows.Forms.DataGridView gridDWD;
        private System.Windows.Forms.Splitter splitter;
        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.ToolStripMenuItem mnuDateiSpeichern;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbJahr;
    }
}

