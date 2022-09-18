namespace Integral.Setup
{
    partial class Setup
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setup));
            this.btnCreateDatabase = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDbConnString = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxRecreateIfExists = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCreateDatabase
            // 
            this.btnCreateDatabase.Location = new System.Drawing.Point(245, 162);
            this.btnCreateDatabase.Name = "btnCreateDatabase";
            this.btnCreateDatabase.Size = new System.Drawing.Size(140, 37);
            this.btnCreateDatabase.TabIndex = 0;
            this.btnCreateDatabase.Text = "Create Database";
            this.btnCreateDatabase.UseVisualStyleBackColor = true;
            this.btnCreateDatabase.Click += new System.EventHandler(this.btnCreateDatabase_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Database Connection String";
            // 
            // txtDbConnString
            // 
            this.txtDbConnString.Location = new System.Drawing.Point(195, 31);
            this.txtDbConnString.Name = "txtDbConnString";
            this.txtDbConnString.Size = new System.Drawing.Size(190, 23);
            this.txtDbConnString.TabIndex = 2;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(195, 84);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(190, 23);
            this.txtUsername.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Username";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(195, 113);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(190, 23);
            this.txtPassword.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password";
            // 
            // cbxRecreateIfExists
            // 
            this.cbxRecreateIfExists.AutoSize = true;
            this.cbxRecreateIfExists.Location = new System.Drawing.Point(126, 180);
            this.cbxRecreateIfExists.Name = "cbxRecreateIfExists";
            this.cbxRecreateIfExists.Size = new System.Drawing.Size(113, 19);
            this.cbxRecreateIfExists.TabIndex = 7;
            this.cbxRecreateIfExists.Text = "Recreate If Exists";
            this.cbxRecreateIfExists.UseVisualStyleBackColor = true;
            // 
            // Setup
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(420, 231);
            this.Controls.Add(this.cbxRecreateIfExists);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDbConnString);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCreateDatabase);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnCreateDatabase;
        private Label label1;
        private TextBox txtDbConnString;
        private TextBox txtUsername;
        private Label label2;
        private TextBox txtPassword;
        private Label label3;
        private CheckBox cbxRecreateIfExists;
    }
}