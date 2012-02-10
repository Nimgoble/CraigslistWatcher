namespace CraigslistWatcher
{
    partial class TabStripTest
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
            this.tbQueries = new System.Windows.Forms.TabControl();
            this.wbLog = new System.Windows.Forms.WebBrowser();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNewSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbQueries
            // 
            this.tbQueries.Location = new System.Drawing.Point(12, 196);
            this.tbQueries.Name = "tbQueries";
            this.tbQueries.SelectedIndex = 0;
            this.tbQueries.Size = new System.Drawing.Size(849, 461);
            this.tbQueries.TabIndex = 2;
            // 
            // wbLog
            // 
            this.wbLog.AllowNavigation = false;
            this.wbLog.AllowWebBrowserDrop = false;
            this.wbLog.IsWebBrowserContextMenuEnabled = false;
            this.wbLog.Location = new System.Drawing.Point(140, 21);
            this.wbLog.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbLog.Name = "wbLog";
            this.wbLog.Size = new System.Drawing.Size(708, 86);
            this.wbLog.TabIndex = 19;
            this.wbLog.WebBrowserShortcutsEnabled = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Log:";
            // 
            // btnNewSearch
            // 
            this.btnNewSearch.Location = new System.Drawing.Point(12, 21);
            this.btnNewSearch.Name = "btnNewSearch";
            this.btnNewSearch.Size = new System.Drawing.Size(122, 23);
            this.btnNewSearch.TabIndex = 20;
            this.btnNewSearch.Text = "New Search";
            this.btnNewSearch.UseVisualStyleBackColor = true;
            this.btnNewSearch.Click += new System.EventHandler(this.btnNewSearch_Click);
            // 
            // TabStripTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 669);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.wbLog);
            this.Controls.Add(this.tbQueries);
            this.Controls.Add(this.btnNewSearch);
            this.Name = "TabStripTest";
            this.Text = "TabStripTest";
            this.Load += new System.EventHandler(this.TabStripTest_Load);
            this.tbQueries.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tbQueries;
        private System.Windows.Forms.WebBrowser wbLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNewSearch;
    }
}