﻿namespace SosesPOS
{
    partial class formWriteCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formWriteCheck));
            this.txtPayee = new System.Windows.Forms.TextBox();
            this.dtpCheckDate = new System.Windows.Forms.DateTimePicker();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblWrittenInteger = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCheckNo = new System.Windows.Forms.TextBox();
            this.rbBDO = new System.Windows.Forms.RadioButton();
            this.rbSS = new System.Windows.Forms.RadioButton();
            this.rbOther = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hlblCheckBankID = new System.Windows.Forms.Label();
            this.hlblVendorCode = new System.Windows.Forms.Label();
            this.hlblBankShortName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVendorShortName = new System.Windows.Forms.TextBox();
            this.hlblCategoryID = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPayee
            // 
            this.txtPayee.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayee.Location = new System.Drawing.Point(190, 162);
            this.txtPayee.Name = "txtPayee";
            this.txtPayee.Size = new System.Drawing.Size(311, 29);
            this.txtPayee.TabIndex = 0;
            this.txtPayee.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPayee_KeyDown);
            this.txtPayee.Leave += new System.EventHandler(this.txtPayee_Leave);
            // 
            // dtpCheckDate
            // 
            this.dtpCheckDate.CustomFormat = "MM/dd/yyyy";
            this.dtpCheckDate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckDate.Location = new System.Drawing.Point(533, 96);
            this.dtpCheckDate.Name = "dtpCheckDate";
            this.dtpCheckDate.Size = new System.Drawing.Size(139, 29);
            this.dtpCheckDate.TabIndex = 1;
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(533, 162);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(139, 29);
            this.txtAmount.TabIndex = 2;
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            this.txtAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            // 
            // lblWrittenInteger
            // 
            this.lblWrittenInteger.AutoSize = true;
            this.lblWrittenInteger.BackColor = System.Drawing.Color.Transparent;
            this.lblWrittenInteger.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWrittenInteger.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lblWrittenInteger.Location = new System.Drawing.Point(114, 204);
            this.lblWrittenInteger.MaximumSize = new System.Drawing.Size(550, 0);
            this.lblWrittenInteger.Name = "lblWrittenInteger";
            this.lblWrittenInteger.Size = new System.Drawing.Size(0, 21);
            this.lblWrittenInteger.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(533, 330);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(139, 39);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Save and Print";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(208, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Check No";
            // 
            // txtCheckNo
            // 
            this.txtCheckNo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckNo.Location = new System.Drawing.Point(291, 96);
            this.txtCheckNo.Name = "txtCheckNo";
            this.txtCheckNo.Size = new System.Drawing.Size(133, 29);
            this.txtCheckNo.TabIndex = 6;
            this.txtCheckNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCheckNo_KeyPress);
            // 
            // rbBDO
            // 
            this.rbBDO.AutoSize = true;
            this.rbBDO.Checked = true;
            this.rbBDO.Location = new System.Drawing.Point(6, 25);
            this.rbBDO.Name = "rbBDO";
            this.rbBDO.Size = new System.Drawing.Size(60, 25);
            this.rbBDO.TabIndex = 9;
            this.rbBDO.TabStop = true;
            this.rbBDO.Text = "BDO";
            this.rbBDO.UseVisualStyleBackColor = true;
            this.rbBDO.CheckedChanged += new System.EventHandler(this.rbBDO_CheckedChanged);
            // 
            // rbSS
            // 
            this.rbSS.AutoSize = true;
            this.rbSS.Location = new System.Drawing.Point(72, 25);
            this.rbSS.Name = "rbSS";
            this.rbSS.Size = new System.Drawing.Size(85, 25);
            this.rbSS.TabIndex = 10;
            this.rbSS.Text = "SuySing";
            this.rbSS.UseVisualStyleBackColor = true;
            this.rbSS.CheckedChanged += new System.EventHandler(this.rbSS_CheckedChanged);
            // 
            // rbOther
            // 
            this.rbOther.AutoSize = true;
            this.rbOther.Location = new System.Drawing.Point(175, 25);
            this.rbOther.Name = "rbOther";
            this.rbOther.Size = new System.Drawing.Size(68, 25);
            this.rbOther.TabIndex = 11;
            this.rbOther.Text = "Other";
            this.rbOther.UseVisualStyleBackColor = true;
            this.rbOther.CheckedChanged += new System.EventHandler(this.rbOther_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbBDO);
            this.groupBox1.Controls.Add(this.rbOther);
            this.groupBox1.Controls.Add(this.rbSS);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(190, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 56);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bank Type";
            // 
            // hlblCheckBankID
            // 
            this.hlblCheckBankID.AutoSize = true;
            this.hlblCheckBankID.Location = new System.Drawing.Point(149, 12);
            this.hlblCheckBankID.Name = "hlblCheckBankID";
            this.hlblCheckBankID.Size = new System.Drawing.Size(0, 13);
            this.hlblCheckBankID.TabIndex = 13;
            this.hlblCheckBankID.Visible = false;
            // 
            // hlblVendorCode
            // 
            this.hlblVendorCode.AutoSize = true;
            this.hlblVendorCode.Location = new System.Drawing.Point(149, 55);
            this.hlblVendorCode.Name = "hlblVendorCode";
            this.hlblVendorCode.Size = new System.Drawing.Size(0, 13);
            this.hlblVendorCode.TabIndex = 14;
            this.hlblVendorCode.Visible = false;
            // 
            // hlblBankShortName
            // 
            this.hlblBankShortName.AutoSize = true;
            this.hlblBankShortName.Location = new System.Drawing.Point(114, 37);
            this.hlblBankShortName.Name = "hlblBankShortName";
            this.hlblBankShortName.Size = new System.Drawing.Size(0, 13);
            this.hlblBankShortName.TabIndex = 15;
            this.hlblBankShortName.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(94, 339);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 21);
            this.label2.TabIndex = 16;
            this.label2.Text = "Vendor Name";
            // 
            // txtVendorShortName
            // 
            this.txtVendorShortName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVendorShortName.Location = new System.Drawing.Point(206, 336);
            this.txtVendorShortName.Name = "txtVendorShortName";
            this.txtVendorShortName.Size = new System.Drawing.Size(295, 29);
            this.txtVendorShortName.TabIndex = 17;
            // 
            // hlblCategoryID
            // 
            this.hlblCategoryID.AutoSize = true;
            this.hlblCategoryID.Location = new System.Drawing.Point(56, 55);
            this.hlblCategoryID.Name = "hlblCategoryID";
            this.hlblCategoryID.Size = new System.Drawing.Size(0, 13);
            this.hlblCategoryID.TabIndex = 18;
            this.hlblCategoryID.Visible = false;
            // 
            // formWriteCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SosesPOS.Properties.Resources.blank_cheque1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(782, 398);
            this.Controls.Add(this.hlblCategoryID);
            this.Controls.Add(this.txtVendorShortName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hlblBankShortName);
            this.Controls.Add(this.hlblVendorCode);
            this.Controls.Add(this.hlblCheckBankID);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtCheckNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lblWrittenInteger);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.dtpCheckDate);
            this.Controls.Add(this.txtPayee);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formWriteCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Check Issue Form";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblWrittenInteger;
        private System.Windows.Forms.Button btnSubmit;
        public System.Windows.Forms.TextBox txtPayee;
        public System.Windows.Forms.DateTimePicker dtpCheckDate;
        public System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtCheckNo;
        private System.Windows.Forms.RadioButton rbBDO;
        private System.Windows.Forms.RadioButton rbSS;
        private System.Windows.Forms.RadioButton rbOther;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label hlblCheckBankID;
        private System.Windows.Forms.Label hlblBankShortName;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtVendorShortName;
        public System.Windows.Forms.Label hlblVendorCode;
        public System.Windows.Forms.Label hlblCategoryID;
    }
}