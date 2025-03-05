namespace SosesPOS
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
            this.hlblPayeeCode = new System.Windows.Forms.Label();
            this.hlblBankShortName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVendorShortName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtBankType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPayee
            // 
            this.txtPayee.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayee.Location = new System.Drawing.Point(162, 166);
            this.txtPayee.Name = "txtPayee";
            this.txtPayee.Size = new System.Drawing.Size(311, 29);
            this.txtPayee.TabIndex = 35;
            this.txtPayee.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPayee_KeyDown);
            this.txtPayee.Leave += new System.EventHandler(this.txtPayee_Leave);
            // 
            // dtpCheckDate
            // 
            this.dtpCheckDate.CustomFormat = "MM/dd/yyyy";
            this.dtpCheckDate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckDate.Location = new System.Drawing.Point(505, 100);
            this.dtpCheckDate.Name = "dtpCheckDate";
            this.dtpCheckDate.Size = new System.Drawing.Size(139, 29);
            this.dtpCheckDate.TabIndex = 30;
            this.dtpCheckDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpCheckDate_KeyDown);
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(505, 166);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(139, 29);
            this.txtAmount.TabIndex = 40;
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
            this.lblWrittenInteger.Location = new System.Drawing.Point(99, 203);
            this.lblWrittenInteger.MaximumSize = new System.Drawing.Size(550, 0);
            this.lblWrittenInteger.Name = "lblWrittenInteger";
            this.lblWrittenInteger.Size = new System.Drawing.Size(0, 21);
            this.lblWrittenInteger.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(482, 344);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(139, 39);
            this.btnSubmit.TabIndex = 50;
            this.btnSubmit.Text = "Save and Print";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(180, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Check No";
            // 
            // txtCheckNo
            // 
            this.txtCheckNo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckNo.Location = new System.Drawing.Point(263, 100);
            this.txtCheckNo.Name = "txtCheckNo";
            this.txtCheckNo.Size = new System.Drawing.Size(133, 29);
            this.txtCheckNo.TabIndex = 25;
            this.txtCheckNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCheckNo_KeyDown);
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
            this.groupBox1.Location = new System.Drawing.Point(104, 257);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 56);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bank Type";
            this.groupBox1.Visible = false;
            // 
            // hlblCheckBankID
            // 
            this.hlblCheckBankID.AutoSize = true;
            this.hlblCheckBankID.Location = new System.Drawing.Point(101, 72);
            this.hlblCheckBankID.Name = "hlblCheckBankID";
            this.hlblCheckBankID.Size = new System.Drawing.Size(0, 13);
            this.hlblCheckBankID.TabIndex = 13;
            this.hlblCheckBankID.Visible = false;
            // 
            // hlblPayeeCode
            // 
            this.hlblPayeeCode.AutoSize = true;
            this.hlblPayeeCode.Location = new System.Drawing.Point(73, 117);
            this.hlblPayeeCode.Name = "hlblPayeeCode";
            this.hlblPayeeCode.Size = new System.Drawing.Size(0, 13);
            this.hlblPayeeCode.TabIndex = 14;
            this.hlblPayeeCode.Visible = false;
            // 
            // hlblBankShortName
            // 
            this.hlblBankShortName.AutoSize = true;
            this.hlblBankShortName.Location = new System.Drawing.Point(38, 99);
            this.hlblBankShortName.Name = "hlblBankShortName";
            this.hlblBankShortName.Size = new System.Drawing.Size(0, 13);
            this.hlblBankShortName.TabIndex = 15;
            this.hlblBankShortName.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 353);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 21);
            this.label2.TabIndex = 16;
            this.label2.Text = "Vendor Name";
            // 
            // txtVendorShortName
            // 
            this.txtVendorShortName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVendorShortName.Location = new System.Drawing.Point(155, 350);
            this.txtVendorShortName.Name = "txtVendorShortName";
            this.txtVendorShortName.Size = new System.Drawing.Size(295, 29);
            this.txtVendorShortName.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(340, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 21);
            this.label3.TabIndex = 19;
            this.label3.Text = "Category";
            // 
            // cboCategory
            // 
            this.cboCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(419, 34);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(225, 29);
            this.cboCategory.TabIndex = 20;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(627, 344);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 39);
            this.btnCancel.TabIndex = 51;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtBankType
            // 
            this.txtBankType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBankType.Location = new System.Drawing.Point(298, 37);
            this.txtBankType.MaxLength = 1;
            this.txtBankType.Name = "txtBankType";
            this.txtBankType.Size = new System.Drawing.Size(36, 29);
            this.txtBankType.TabIndex = 52;
            this.txtBankType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBankType_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(212, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 21);
            this.label4.TabIndex = 12;
            this.label4.Text = "Bank Type";
            // 
            // formWriteCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SosesPOS.Properties.Resources.blank_cheque1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(731, 406);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBankType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtVendorShortName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hlblBankShortName);
            this.Controls.Add(this.hlblPayeeCode);
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
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.formWriteCheck_KeyDown);
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
        public System.Windows.Forms.Label hlblPayeeCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.TextBox txtBankType;
        private System.Windows.Forms.Label label4;
    }
}