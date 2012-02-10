namespace CraigslistWatcher
{
    partial class EntryForm
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
            this.trAreas = new System.Windows.Forms.TreeView();
            this.trSections = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKeywords = new System.Windows.Forms.TextBox();
            this.lstKeywords = new System.Windows.Forms.ListBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // trAreas
            // 
            this.trAreas.Location = new System.Drawing.Point(12, 38);
            this.trAreas.Name = "trAreas";
            this.trAreas.Size = new System.Drawing.Size(204, 220);
            this.trAreas.TabIndex = 0;
            this.trAreas.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trAreas_AfterCheck);
            // 
            // trSections
            // 
            this.trSections.Location = new System.Drawing.Point(222, 38);
            this.trSections.Name = "trSections";
            this.trSections.Size = new System.Drawing.Size(204, 220);
            this.trSections.TabIndex = 1;
            this.trSections.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trSections_AfterCheck);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Areas:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sections:";
            // 
            // txtKeywords
            // 
            this.txtKeywords.Location = new System.Drawing.Point(432, 38);
            this.txtKeywords.Name = "txtKeywords";
            this.txtKeywords.Size = new System.Drawing.Size(250, 20);
            this.txtKeywords.TabIndex = 4;
            this.txtKeywords.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKeywords_KeyPress);
            // 
            // lstKeywords
            // 
            this.lstKeywords.FormattingEnabled = true;
            this.lstKeywords.Location = new System.Drawing.Point(432, 72);
            this.lstKeywords.Name = "lstKeywords";
            this.lstKeywords.Size = new System.Drawing.Size(250, 186);
            this.lstKeywords.TabIndex = 5;
            this.lstKeywords.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstKeywords_KeyDown);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(432, 266);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 6;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(607, 266);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(429, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Enter Keywords:";
            // 
            // EntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 301);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lstKeywords);
            this.Controls.Add(this.txtKeywords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trSections);
            this.Controls.Add(this.trAreas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EntryForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "EntryForm";
            this.Load += new System.EventHandler(this.EntryForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trAreas;
        private System.Windows.Forms.TreeView trSections;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKeywords;
        private System.Windows.Forms.ListBox lstKeywords;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;


    }
}