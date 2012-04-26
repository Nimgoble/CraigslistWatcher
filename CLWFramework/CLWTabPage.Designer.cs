namespace CLWFramework
{
    partial class CLWTabPage
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
            this.lstKeywords = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKeywords = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trSections = new System.Windows.Forms.TreeView();
            this.trAreas = new System.Windows.Forms.TreeView();
            this.wbEntries = new System.Windows.Forms.WebBrowser();
            this.lblEntriesFound = new System.Windows.Forms.Label();
            this.nudMin1 = new System.Windows.Forms.NumericUpDown();
            this.nudMin2 = new System.Windows.Forms.NumericUpDown();
            this.nudSec1 = new System.Windows.Forms.NumericUpDown();
            this.nudSec2 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnForceRefresh = new System.Windows.Forms.Button();
            this.lblRefreshCountdown = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblEntriesSearched = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSec1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSec2)).BeginInit();
            this.SuspendLayout();
            // 
            // lstKeywords
            // 
            this.lstKeywords.FormattingEnabled = true;
            this.lstKeywords.Location = new System.Drawing.Point(3, 239);
            this.lstKeywords.Name = "lstKeywords";
            this.lstKeywords.Size = new System.Drawing.Size(414, 108);
            this.lstKeywords.TabIndex = 36;
            this.lstKeywords.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstKeywords_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Enter Keywords:";
            // 
            // txtKeywords
            // 
            this.txtKeywords.Location = new System.Drawing.Point(3, 216);
            this.txtKeywords.Name = "txtKeywords";
            this.txtKeywords.Size = new System.Drawing.Size(414, 20);
            this.txtKeywords.TabIndex = 34;
            this.txtKeywords.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKeywords_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Sections:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Areas:";
            // 
            // trSections
            // 
            this.trSections.Location = new System.Drawing.Point(213, 20);
            this.trSections.Name = "trSections";
            this.trSections.Size = new System.Drawing.Size(204, 173);
            this.trSections.TabIndex = 31;
            this.trSections.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trSections_AfterCheck);
            // 
            // trAreas
            // 
            this.trAreas.Location = new System.Drawing.Point(3, 20);
            this.trAreas.Name = "trAreas";
            this.trAreas.Size = new System.Drawing.Size(204, 173);
            this.trAreas.TabIndex = 30;
            this.trAreas.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trAreas_AfterCheck);
            // 
            // wbEntries
            // 
            this.wbEntries.AllowNavigation = true;
            this.wbEntries.AllowWebBrowserDrop = false;
            this.wbEntries.IsWebBrowserContextMenuEnabled = false;
            this.wbEntries.Location = new System.Drawing.Point(438, 36);
            this.wbEntries.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbEntries.Name = "wbEntries";
            this.wbEntries.Size = new System.Drawing.Size(394, 375);
            this.wbEntries.TabIndex = 29;
            this.wbEntries.WebBrowserShortcutsEnabled = false;
            this.wbEntries.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.wbEntries_Navigating);
            // 
            // lblEntriesFound
            // 
            this.lblEntriesFound.AutoSize = true;
            this.lblEntriesFound.Location = new System.Drawing.Point(435, 4);
            this.lblEntriesFound.Name = "lblEntriesFound";
            this.lblEntriesFound.Size = new System.Drawing.Size(75, 13);
            this.lblEntriesFound.TabIndex = 28;
            this.lblEntriesFound.Text = "Entries Found:";
            // 
            // nudMin1
            // 
            this.nudMin1.Location = new System.Drawing.Point(3, 366);
            this.nudMin1.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nudMin1.Name = "nudMin1";
            this.nudMin1.Size = new System.Drawing.Size(34, 20);
            this.nudMin1.TabIndex = 37;
            this.nudMin1.ValueChanged += new System.EventHandler(this.nudMin1_ValueChanged);
            // 
            // nudMin2
            // 
            this.nudMin2.Location = new System.Drawing.Point(34, 366);
            this.nudMin2.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudMin2.Name = "nudMin2";
            this.nudMin2.Size = new System.Drawing.Size(34, 20);
            this.nudMin2.TabIndex = 38;
            this.nudMin2.ValueChanged += new System.EventHandler(this.nudMin2_ValueChanged);
            // 
            // nudSec1
            // 
            this.nudSec1.Location = new System.Drawing.Point(77, 366);
            this.nudSec1.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudSec1.Name = "nudSec1";
            this.nudSec1.Size = new System.Drawing.Size(34, 20);
            this.nudSec1.TabIndex = 39;
            this.nudSec1.ValueChanged += new System.EventHandler(this.nudSec1_ValueChanged);
            // 
            // nudSec2
            // 
            this.nudSec2.Location = new System.Drawing.Point(108, 366);
            this.nudSec2.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudSec2.Name = "nudSec2";
            this.nudSec2.Size = new System.Drawing.Size(34, 20);
            this.nudSec2.TabIndex = 40;
            this.nudSec2.ValueChanged += new System.EventHandler(this.nudSec2_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(65, 363);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 23);
            this.label2.TabIndex = 41;
            this.label2.Text = ":";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 350);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Refresh Rate:";
            // 
            // btnForceRefresh
            // 
            this.btnForceRefresh.Location = new System.Drawing.Point(3, 389);
            this.btnForceRefresh.Name = "btnForceRefresh";
            this.btnForceRefresh.Size = new System.Drawing.Size(89, 23);
            this.btnForceRefresh.TabIndex = 43;
            this.btnForceRefresh.Text = "Start Search";
            this.btnForceRefresh.UseVisualStyleBackColor = true;
            this.btnForceRefresh.Click += new System.EventHandler(this.btnForceRefresh_Click);
            // 
            // lblRefreshCountdown
            // 
            this.lblRefreshCountdown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRefreshCountdown.Location = new System.Drawing.Point(213, 363);
            this.lblRefreshCountdown.Name = "lblRefreshCountdown";
            this.lblRefreshCountdown.Size = new System.Drawing.Size(121, 23);
            this.lblRefreshCountdown.TabIndex = 44;
            this.lblRefreshCountdown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(213, 347);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 45;
            this.label7.Text = "Next Refresh:";
            // 
            // lblEntriesSearched
            // 
            this.lblEntriesSearched.AutoSize = true;
            this.lblEntriesSearched.Location = new System.Drawing.Point(435, 20);
            this.lblEntriesSearched.Name = "lblEntriesSearched";
            this.lblEntriesSearched.Size = new System.Drawing.Size(91, 13);
            this.lblEntriesSearched.TabIndex = 46;
            this.lblEntriesSearched.Text = "Entries Searched:";
            // 
            // CLWTabPage
            // 
            this.Controls.Add(this.lblEntriesSearched);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblRefreshCountdown);
            this.Controls.Add(this.btnForceRefresh);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudSec2);
            this.Controls.Add(this.nudSec1);
            this.Controls.Add(this.nudMin2);
            this.Controls.Add(this.nudMin1);
            this.Controls.Add(this.lstKeywords);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtKeywords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trSections);
            this.Controls.Add(this.trAreas);
            this.Controls.Add(this.wbEntries);
            this.Controls.Add(this.lblEntriesFound);
            this.Name = "CLWTabPage";
            this.Size = new System.Drawing.Size(835, 420);
            ((System.ComponentModel.ISupportInitialize)(this.nudMin1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSec1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSec2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstKeywords;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtKeywords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView trSections;
        private System.Windows.Forms.TreeView trAreas;
        private System.Windows.Forms.WebBrowser wbEntries;
        private System.Windows.Forms.Label lblEntriesFound;
        private System.Windows.Forms.NumericUpDown nudMin1;
        private System.Windows.Forms.NumericUpDown nudMin2;
        private System.Windows.Forms.NumericUpDown nudSec1;
        private System.Windows.Forms.NumericUpDown nudSec2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnForceRefresh;
        private System.Windows.Forms.Label lblRefreshCountdown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblEntriesSearched;
    }
}
