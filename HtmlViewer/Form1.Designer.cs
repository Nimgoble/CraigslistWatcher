namespace HtmlViewer
{
    partial class Form1
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
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.trParsedURL = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFilterOutTags = new System.Windows.Forms.Button();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.btnRemoveClass = new System.Windows.Forms.Button();
            this.btnAddClass = new System.Windows.Forms.Button();
            this.tpParsedURL = new System.Windows.Forms.TabPage();
            this.tbTabs = new System.Windows.Forms.TabControl();
            this.grpOptions.SuspendLayout();
            this.tpParsedURL.SuspendLayout();
            this.tbTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(13, 22);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(724, 20);
            this.txtURL.TabIndex = 0;
            // 
            // btnQuery
            // 
            this.btnQuery.AutoSize = true;
            this.btnQuery.Location = new System.Drawing.Point(6, 19);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(88, 23);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "Query URL";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // trParsedURL
            // 
            this.trParsedURL.CheckBoxes = true;
            this.trParsedURL.Location = new System.Drawing.Point(3, 3);
            this.trParsedURL.Name = "trParsedURL";
            this.trParsedURL.Size = new System.Drawing.Size(609, 426);
            this.trParsedURL.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Query URL:";
            // 
            // btnFilterOutTags
            // 
            this.btnFilterOutTags.Location = new System.Drawing.Point(6, 57);
            this.btnFilterOutTags.Name = "btnFilterOutTags";
            this.btnFilterOutTags.Size = new System.Drawing.Size(88, 23);
            this.btnFilterOutTags.TabIndex = 11;
            this.btnFilterOutTags.Text = "Filter Out Tags";
            this.btnFilterOutTags.UseVisualStyleBackColor = true;
            this.btnFilterOutTags.Click += new System.EventHandler(this.btnFilterOutTag_Click);
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.btnRemoveClass);
            this.grpOptions.Controls.Add(this.btnAddClass);
            this.grpOptions.Controls.Add(this.btnFilterOutTags);
            this.grpOptions.Controls.Add(this.btnQuery);
            this.grpOptions.Location = new System.Drawing.Point(13, 48);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(100, 455);
            this.grpOptions.TabIndex = 12;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // btnRemoveClass
            // 
            this.btnRemoveClass.Location = new System.Drawing.Point(6, 140);
            this.btnRemoveClass.Name = "btnRemoveClass";
            this.btnRemoveClass.Size = new System.Drawing.Size(88, 23);
            this.btnRemoveClass.TabIndex = 13;
            this.btnRemoveClass.Text = "Remove Class";
            this.btnRemoveClass.UseVisualStyleBackColor = true;
            this.btnRemoveClass.Click += new System.EventHandler(this.btnRemoveClass_Click);
            // 
            // btnAddClass
            // 
            this.btnAddClass.Location = new System.Drawing.Point(6, 98);
            this.btnAddClass.Name = "btnAddClass";
            this.btnAddClass.Size = new System.Drawing.Size(88, 23);
            this.btnAddClass.TabIndex = 12;
            this.btnAddClass.Text = "Add Class";
            this.btnAddClass.UseVisualStyleBackColor = true;
            this.btnAddClass.Click += new System.EventHandler(this.btnAddClass_Click);
            // 
            // tpParsedURL
            // 
            this.tpParsedURL.Controls.Add(this.trParsedURL);
            this.tpParsedURL.Location = new System.Drawing.Point(4, 22);
            this.tpParsedURL.Name = "tpParsedURL";
            this.tpParsedURL.Padding = new System.Windows.Forms.Padding(3);
            this.tpParsedURL.Size = new System.Drawing.Size(615, 429);
            this.tpParsedURL.TabIndex = 0;
            this.tpParsedURL.Text = "Parsed URL";
            this.tpParsedURL.UseVisualStyleBackColor = true;
            // 
            // tbTabs
            // 
            this.tbTabs.Controls.Add(this.tpParsedURL);
            this.tbTabs.Location = new System.Drawing.Point(114, 48);
            this.tbTabs.Name = "tbTabs";
            this.tbTabs.SelectedIndex = 0;
            this.tbTabs.Size = new System.Drawing.Size(623, 455);
            this.tbTabs.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 512);
            this.Controls.Add(this.tbTabs);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtURL);
            this.Name = "Form1";
            this.Text = "HTML Viewer";
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.tpParsedURL.ResumeLayout(false);
            this.tbTabs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TreeView trParsedURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFilterOutTags;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.Button btnRemoveClass;
        private System.Windows.Forms.Button btnAddClass;
        private System.Windows.Forms.TabPage tpParsedURL;
        private System.Windows.Forms.TabControl tbTabs;
    }
}

