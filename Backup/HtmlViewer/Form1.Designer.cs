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
            this.label2 = new System.Windows.Forms.Label();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.lblClassName = new System.Windows.Forms.Label();
            this.lstvwClassMembers = new System.Windows.Forms.ListView();
            this.txtMemberName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddMember = new System.Windows.Forms.Button();
            this.btnDeleteMember = new System.Windows.Forms.Button();
            this.btnGenerateClass = new System.Windows.Forms.Button();
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
            this.btnQuery.Location = new System.Drawing.Point(659, 48);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(78, 23);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "Query HTML";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // trParsedURL
            // 
            this.trParsedURL.CheckBoxes = true;
            this.trParsedURL.Location = new System.Drawing.Point(12, 92);
            this.trParsedURL.Name = "trParsedURL";
            this.trParsedURL.Size = new System.Drawing.Size(725, 411);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Parsed Message:";
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(766, 22);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(335, 20);
            this.txtClassName.TabIndex = 5;
            // 
            // lblClassName
            // 
            this.lblClassName.AutoSize = true;
            this.lblClassName.Location = new System.Drawing.Point(766, 2);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(66, 13);
            this.lblClassName.TabIndex = 6;
            this.lblClassName.Text = "Class Name:";
            // 
            // lstvwClassMembers
            // 
            this.lstvwClassMembers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstvwClassMembers.Location = new System.Drawing.Point(766, 147);
            this.lstvwClassMembers.Name = "lstvwClassMembers";
            this.lstvwClassMembers.Size = new System.Drawing.Size(335, 356);
            this.lstvwClassMembers.TabIndex = 10;
            this.lstvwClassMembers.UseCompatibleStateImageBehavior = false;
            this.lstvwClassMembers.View = System.Windows.Forms.View.Details;
            // 
            // txtMemberName
            // 
            this.txtMemberName.Location = new System.Drawing.Point(766, 92);
            this.txtMemberName.Name = "txtMemberName";
            this.txtMemberName.Size = new System.Drawing.Size(335, 20);
            this.txtMemberName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(763, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Member Name:";
            // 
            // btnAddMember
            // 
            this.btnAddMember.Location = new System.Drawing.Point(766, 118);
            this.btnAddMember.Name = "btnAddMember";
            this.btnAddMember.Size = new System.Drawing.Size(75, 23);
            this.btnAddMember.TabIndex = 7;
            this.btnAddMember.Text = "Add Member";
            this.btnAddMember.UseVisualStyleBackColor = true;
            this.btnAddMember.Click += new System.EventHandler(this.btnAddMember_Click);
            // 
            // btnDeleteMember
            // 
            this.btnDeleteMember.Location = new System.Drawing.Point(848, 118);
            this.btnDeleteMember.Name = "btnDeleteMember";
            this.btnDeleteMember.Size = new System.Drawing.Size(88, 23);
            this.btnDeleteMember.TabIndex = 8;
            this.btnDeleteMember.Text = "Delete Member";
            this.btnDeleteMember.UseVisualStyleBackColor = true;
            this.btnDeleteMember.Click += new System.EventHandler(this.btnDeleteMember_Click);
            // 
            // btnGenerateClass
            // 
            this.btnGenerateClass.Location = new System.Drawing.Point(1002, 118);
            this.btnGenerateClass.Name = "btnGenerateClass";
            this.btnGenerateClass.Size = new System.Drawing.Size(99, 23);
            this.btnGenerateClass.TabIndex = 9;
            this.btnGenerateClass.Text = "Generate Class";
            this.btnGenerateClass.UseVisualStyleBackColor = true;
            this.btnGenerateClass.Click += new System.EventHandler(this.btnGenerateClass_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 515);
            this.Controls.Add(this.btnGenerateClass);
            this.Controls.Add(this.btnDeleteMember);
            this.Controls.Add(this.btnAddMember);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMemberName);
            this.Controls.Add(this.lstvwClassMembers);
            this.Controls.Add(this.lblClassName);
            this.Controls.Add(this.txtClassName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trParsedURL);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtURL);
            this.Name = "Form1";
            this.Text = "HTML Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TreeView trParsedURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label lblClassName;
        private System.Windows.Forms.ListView lstvwClassMembers;
        private System.Windows.Forms.TextBox txtMemberName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddMember;
        private System.Windows.Forms.Button btnDeleteMember;
        private System.Windows.Forms.Button btnGenerateClass;
    }
}

