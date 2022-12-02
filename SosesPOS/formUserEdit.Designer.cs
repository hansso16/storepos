namespace SosesPOS
{
    partial class formUserEdit
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdateTerminationDate = new System.Windows.Forms.Button();
            this.btnUpdateAccessLevel = new System.Windows.Forms.Button();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtAccessLevel = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtLastChangedTimestamp = new System.Windows.Forms.TextBox();
            this.txtLastChangedUserCode = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblUserCode = new System.Windows.Forms.Label();
            this.btnReactivateUser = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(158)))), ((int)(((byte)(132)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(693, 40);
            this.panel1.TabIndex = 20;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::SosesPOS.Properties.Resources.cross__1_;
            this.pictureBox1.Location = new System.Drawing.Point(668, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Module";
            // 
            // btnUpdateTerminationDate
            // 
            this.btnUpdateTerminationDate.BackColor = System.Drawing.Color.Black;
            this.btnUpdateTerminationDate.ForeColor = System.Drawing.Color.White;
            this.btnUpdateTerminationDate.Location = new System.Drawing.Point(493, 232);
            this.btnUpdateTerminationDate.Name = "btnUpdateTerminationDate";
            this.btnUpdateTerminationDate.Size = new System.Drawing.Size(117, 35);
            this.btnUpdateTerminationDate.TabIndex = 28;
            this.btnUpdateTerminationDate.Text = "Terminate User";
            this.btnUpdateTerminationDate.UseVisualStyleBackColor = false;
            this.btnUpdateTerminationDate.Click += new System.EventHandler(this.btnUpdateTerminationDate_Click);
            // 
            // btnUpdateAccessLevel
            // 
            this.btnUpdateAccessLevel.BackColor = System.Drawing.Color.Black;
            this.btnUpdateAccessLevel.ForeColor = System.Drawing.Color.White;
            this.btnUpdateAccessLevel.Location = new System.Drawing.Point(333, 232);
            this.btnUpdateAccessLevel.Name = "btnUpdateAccessLevel";
            this.btnUpdateAccessLevel.Size = new System.Drawing.Size(154, 35);
            this.btnUpdateAccessLevel.TabIndex = 26;
            this.btnUpdateAccessLevel.Text = "Update Access Level";
            this.btnUpdateAccessLevel.UseVisualStyleBackColor = false;
            this.btnUpdateAccessLevel.Click += new System.EventHandler(this.btnUpdateAccessLevel_Click);
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.BackColor = System.Drawing.Color.Black;
            this.btnResetPassword.ForeColor = System.Drawing.Color.White;
            this.btnResetPassword.Location = new System.Drawing.Point(198, 232);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(129, 35);
            this.btnResetPassword.TabIndex = 25;
            this.btnResetPassword.Text = "Reset Password";
            this.btnResetPassword.UseVisualStyleBackColor = false;
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(244, 77);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.ReadOnly = true;
            this.txtUsername.Size = new System.Drawing.Size(306, 25);
            this.txtUsername.TabIndex = 29;
            // 
            // txtAccessLevel
            // 
            this.txtAccessLevel.Location = new System.Drawing.Point(244, 108);
            this.txtAccessLevel.Name = "txtAccessLevel";
            this.txtAccessLevel.ReadOnly = true;
            this.txtAccessLevel.Size = new System.Drawing.Size(306, 25);
            this.txtAccessLevel.TabIndex = 30;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(244, 139);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(306, 25);
            this.txtStatus.TabIndex = 31;
            // 
            // txtLastChangedTimestamp
            // 
            this.txtLastChangedTimestamp.Location = new System.Drawing.Point(244, 170);
            this.txtLastChangedTimestamp.Name = "txtLastChangedTimestamp";
            this.txtLastChangedTimestamp.ReadOnly = true;
            this.txtLastChangedTimestamp.Size = new System.Drawing.Size(306, 25);
            this.txtLastChangedTimestamp.TabIndex = 32;
            // 
            // txtLastChangedUserCode
            // 
            this.txtLastChangedUserCode.Location = new System.Drawing.Point(244, 201);
            this.txtLastChangedUserCode.Name = "txtLastChangedUserCode";
            this.txtLastChangedUserCode.ReadOnly = true;
            this.txtLastChangedUserCode.Size = new System.Drawing.Size(306, 25);
            this.txtLastChangedUserCode.TabIndex = 33;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(171, 80);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(67, 17);
            this.lblUsername.TabIndex = 34;
            this.lblUsername.Text = "Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(158, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 35;
            this.label3.Text = "Access Level";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(195, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 17);
            this.label4.TabIndex = 36;
            this.label4.Text = "Status";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(83, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 17);
            this.label5.TabIndex = 37;
            this.label5.Text = "Last Changed Timestamp";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(67, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(171, 17);
            this.label6.TabIndex = 38;
            this.label6.Text = "Last Changed by User Code";
            // 
            // lblUserCode
            // 
            this.lblUserCode.AutoSize = true;
            this.lblUserCode.Location = new System.Drawing.Point(12, 43);
            this.lblUserCode.Name = "lblUserCode";
            this.lblUserCode.Size = new System.Drawing.Size(0, 17);
            this.lblUserCode.TabIndex = 39;
            this.lblUserCode.Visible = false;
            // 
            // btnReactivateUser
            // 
            this.btnReactivateUser.BackColor = System.Drawing.Color.Black;
            this.btnReactivateUser.ForeColor = System.Drawing.Color.White;
            this.btnReactivateUser.Location = new System.Drawing.Point(75, 232);
            this.btnReactivateUser.Name = "btnReactivateUser";
            this.btnReactivateUser.Size = new System.Drawing.Size(117, 35);
            this.btnReactivateUser.TabIndex = 40;
            this.btnReactivateUser.Text = "Reactivate User";
            this.btnReactivateUser.UseVisualStyleBackColor = false;
            this.btnReactivateUser.Click += new System.EventHandler(this.btnReactivateUser_Click);
            // 
            // formUserEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 326);
            this.ControlBox = false;
            this.Controls.Add(this.btnReactivateUser);
            this.Controls.Add(this.lblUserCode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtLastChangedUserCode);
            this.Controls.Add(this.txtLastChangedTimestamp);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtAccessLevel);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnUpdateTerminationDate);
            this.Controls.Add(this.btnUpdateAccessLevel);
            this.Controls.Add(this.btnResetPassword);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "formUserEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnUpdateAccessLevel;
        public System.Windows.Forms.Button btnResetPassword;
        private System.Windows.Forms.TextBox txtLastChangedTimestamp;
        private System.Windows.Forms.TextBox txtLastChangedUserCode;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label lblUserCode;
        internal System.Windows.Forms.TextBox txtUsername;
        internal System.Windows.Forms.TextBox txtStatus;
        internal System.Windows.Forms.TextBox txtAccessLevel;
        internal System.Windows.Forms.Button btnUpdateTerminationDate;
        internal System.Windows.Forms.Button btnReactivateUser;
    }
}