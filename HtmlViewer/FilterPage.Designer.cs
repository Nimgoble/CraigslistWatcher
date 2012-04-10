namespace HtmlViewer
{
    partial class FilterPage
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
            this.lblClassName = new System.Windows.Forms.Label();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.lstvwClassMembers = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeleteMember = new System.Windows.Forms.Button();
            this.btnAddMember = new System.Windows.Forms.Button();
            this.btnEditMember = new System.Windows.Forms.Button();
            this.btnGenerateClass = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblClassName
            // 
            this.lblClassName.AutoSize = true;
            this.lblClassName.Location = new System.Drawing.Point(143, 2);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(66, 13);
            this.lblClassName.TabIndex = 8;
            this.lblClassName.Text = "Class Name:";
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(143, 22);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(335, 20);
            this.txtClassName.TabIndex = 7;
            // 
            // lstvwClassMembers
            // 
            this.lstvwClassMembers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstvwClassMembers.Location = new System.Drawing.Point(142, 48);
            this.lstvwClassMembers.Name = "lstvwClassMembers";
            this.lstvwClassMembers.Size = new System.Drawing.Size(335, 294);
            this.lstvwClassMembers.TabIndex = 11;
            this.lstvwClassMembers.UseCompatibleStateImageBehavior = false;
            this.lstvwClassMembers.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnGenerateClass);
            this.panel1.Controls.Add(this.btnEditMember);
            this.panel1.Controls.Add(this.btnAddMember);
            this.panel1.Controls.Add(this.btnDeleteMember);
            this.panel1.Location = new System.Drawing.Point(4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(133, 339);
            this.panel1.TabIndex = 12;
            // 
            // btnDeleteMember
            // 
            this.btnDeleteMember.Location = new System.Drawing.Point(3, 61);
            this.btnDeleteMember.Name = "btnDeleteMember";
            this.btnDeleteMember.Size = new System.Drawing.Size(125, 23);
            this.btnDeleteMember.TabIndex = 14;
            this.btnDeleteMember.Text = "Delete Member";
            this.btnDeleteMember.UseVisualStyleBackColor = true;
            this.btnDeleteMember.Click += new System.EventHandler(this.btnDeleteMember_Click);
            // 
            // btnAddMember
            // 
            this.btnAddMember.Location = new System.Drawing.Point(3, 3);
            this.btnAddMember.Name = "btnAddMember";
            this.btnAddMember.Size = new System.Drawing.Size(125, 23);
            this.btnAddMember.TabIndex = 13;
            this.btnAddMember.Text = "Add Member";
            this.btnAddMember.UseVisualStyleBackColor = true;
            this.btnAddMember.Click += new System.EventHandler(this.btnAddMember_Click);
            // 
            // btnEditMember
            // 
            this.btnEditMember.Location = new System.Drawing.Point(3, 32);
            this.btnEditMember.Name = "btnEditMember";
            this.btnEditMember.Size = new System.Drawing.Size(125, 23);
            this.btnEditMember.TabIndex = 15;
            this.btnEditMember.Text = "Edit Member";
            this.btnEditMember.UseVisualStyleBackColor = true;
            // 
            // btnGenerateClass
            // 
            this.btnGenerateClass.Location = new System.Drawing.Point(3, 90);
            this.btnGenerateClass.Name = "btnGenerateClass";
            this.btnGenerateClass.Size = new System.Drawing.Size(125, 23);
            this.btnGenerateClass.TabIndex = 17;
            this.btnGenerateClass.Text = "Generate Class";
            this.btnGenerateClass.UseVisualStyleBackColor = true;
            this.btnGenerateClass.Click += new System.EventHandler(this.btnGenerateClass_Click);
            // 
            // FilterPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lstvwClassMembers);
            this.Controls.Add(this.lblClassName);
            this.Controls.Add(this.txtClassName);
            this.Name = "FilterPage";
            this.Size = new System.Drawing.Size(480, 347);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblClassName;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.ListView lstvwClassMembers;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddMember;
        private System.Windows.Forms.Button btnDeleteMember;
        private System.Windows.Forms.Button btnEditMember;
        private System.Windows.Forms.Button btnGenerateClass;
    }
}
