namespace CraigslistWatcher
{
    partial class CraigslistWatcher
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
            this.trFilters = new System.Windows.Forms.TreeView();
            this.btnNewFilter = new System.Windows.Forms.Button();
            this.btnDeleteFilter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.wbLog = new System.Windows.Forms.WebBrowser();
            this.wbEntries = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // trFilters
            // 
            this.trFilters.Location = new System.Drawing.Point(13, 25);
            this.trFilters.Name = "trFilters";
            this.trFilters.Size = new System.Drawing.Size(297, 161);
            this.trFilters.TabIndex = 8;
            // 
            // btnNewFilter
            // 
            this.btnNewFilter.Location = new System.Drawing.Point(121, 192);
            this.btnNewFilter.Name = "btnNewFilter";
            this.btnNewFilter.Size = new System.Drawing.Size(91, 23);
            this.btnNewFilter.TabIndex = 9;
            this.btnNewFilter.Text = "New Filter";
            this.btnNewFilter.UseVisualStyleBackColor = true;
            this.btnNewFilter.Click += new System.EventHandler(this.btnNewFilter_Click);
            // 
            // btnDeleteFilter
            // 
            this.btnDeleteFilter.Location = new System.Drawing.Point(218, 192);
            this.btnDeleteFilter.Name = "btnDeleteFilter";
            this.btnDeleteFilter.Size = new System.Drawing.Size(92, 23);
            this.btnDeleteFilter.TabIndex = 10;
            this.btnDeleteFilter.Text = "Delete Filter";
            this.btnDeleteFilter.UseVisualStyleBackColor = true;
            this.btnDeleteFilter.Click += new System.EventHandler(this.btnDeleteFilter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Current Filters:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Log:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Entries Found:";
            // 
            // wbLog
            // 
            this.wbLog.AllowNavigation = false;
            this.wbLog.AllowWebBrowserDrop = false;
            this.wbLog.IsWebBrowserContextMenuEnabled = false;
            this.wbLog.Location = new System.Drawing.Point(317, 25);
            this.wbLog.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbLog.Name = "wbLog";
            this.wbLog.Size = new System.Drawing.Size(392, 161);
            this.wbLog.TabIndex = 17;
            this.wbLog.WebBrowserShortcutsEnabled = false;
            // 
            // wbEntries
            // 
            this.wbEntries.AllowNavigation = false;
            this.wbEntries.AllowWebBrowserDrop = false;
            this.wbEntries.IsWebBrowserContextMenuEnabled = false;
            this.wbEntries.Location = new System.Drawing.Point(13, 229);
            this.wbEntries.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbEntries.Name = "wbEntries";
            this.wbEntries.Size = new System.Drawing.Size(696, 182);
            this.wbEntries.TabIndex = 18;
            this.wbEntries.WebBrowserShortcutsEnabled = false;
            // 
            // CraigslistWatcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 423);
            this.Controls.Add(this.wbEntries);
            this.Controls.Add(this.wbLog);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDeleteFilter);
            this.Controls.Add(this.btnNewFilter);
            this.Controls.Add(this.trFilters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CraigslistWatcher";
            this.Text = "Craigslist Watcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trFilters;
        private System.Windows.Forms.Button btnNewFilter;
        private System.Windows.Forms.Button btnDeleteFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.WebBrowser wbLog;
        private System.Windows.Forms.WebBrowser wbEntries;

    }
}

