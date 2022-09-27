namespace SosesPOS
{
    partial class formCustomerPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formCustomerPayment));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblOpenBalance = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.cboPaymentMethod = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpCheckDate = new System.Windows.Forms.DateTimePicker();
            this.lblCheckDate = new System.Windows.Forms.Label();
            this.txtCheckNo = new System.Windows.Forms.TextBox();
            this.lblCheckNo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cboBank = new System.Windows.Forms.ComboBox();
            this.txtBankBranch = new System.Windows.Forms.TextBox();
            this.lblBankBranch = new System.Windows.Forms.Label();
            this.lblCheckBank = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblCustomerId = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(901, 40);
            this.panel1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(869, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer Payment";
            // 
            // cboCustomer
            // 
            this.cboCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.ItemHeight = 17;
            this.cboCustomer.Location = new System.Drawing.Point(134, 20);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(233, 25);
            this.cboCustomer.TabIndex = 23;
            this.cboCustomer.SelectionChangeCommitted += new System.EventHandler(this.cboCustomer_SelectionChangeCommitted);
            this.cboCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboCustomer_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Received From";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(386, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 17);
            this.label3.TabIndex = 24;
            this.label3.Text = "Open Balance";
            // 
            // lblOpenBalance
            // 
            this.lblOpenBalance.AutoSize = true;
            this.lblOpenBalance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenBalance.Location = new System.Drawing.Point(534, 23);
            this.lblOpenBalance.Name = "lblOpenBalance";
            this.lblOpenBalance.Size = new System.Drawing.Size(0, 17);
            this.lblOpenBalance.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 17);
            this.label5.TabIndex = 26;
            this.label5.Text = "Amount";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(134, 65);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(233, 25);
            this.txtAmount.TabIndex = 27;
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(386, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 17);
            this.label6.TabIndex = 28;
            this.label6.Text = "Payment Date";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpPaymentDate
            // 
            this.dtpPaymentDate.CustomFormat = "MM/dd/yyyy";
            this.dtpPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPaymentDate.Location = new System.Drawing.Point(480, 65);
            this.dtpPaymentDate.Name = "dtpPaymentDate";
            this.dtpPaymentDate.Size = new System.Drawing.Size(132, 25);
            this.dtpPaymentDate.TabIndex = 29;
            // 
            // cboPaymentMethod
            // 
            this.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentMethod.FormattingEnabled = true;
            this.cboPaymentMethod.Items.AddRange(new object[] {
            "Cash",
            "Check"});
            this.cboPaymentMethod.Location = new System.Drawing.Point(134, 107);
            this.cboPaymentMethod.Name = "cboPaymentMethod";
            this.cboPaymentMethod.Size = new System.Drawing.Size(233, 25);
            this.cboPaymentMethod.TabIndex = 30;
            this.cboPaymentMethod.SelectedValueChanged += new System.EventHandler(this.cboPaymentMethod_SelectedValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 17);
            this.label7.TabIndex = 31;
            this.label7.Text = "Payment Method";
            // 
            // dtpCheckDate
            // 
            this.dtpCheckDate.CustomFormat = "MM/dd/yyyy";
            this.dtpCheckDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckDate.Location = new System.Drawing.Point(714, 152);
            this.dtpCheckDate.Name = "dtpCheckDate";
            this.dtpCheckDate.Size = new System.Drawing.Size(132, 25);
            this.dtpCheckDate.TabIndex = 39;
            this.dtpCheckDate.Visible = false;
            // 
            // lblCheckDate
            // 
            this.lblCheckDate.AutoSize = true;
            this.lblCheckDate.Location = new System.Drawing.Point(635, 155);
            this.lblCheckDate.Name = "lblCheckDate";
            this.lblCheckDate.Size = new System.Drawing.Size(73, 17);
            this.lblCheckDate.TabIndex = 32;
            this.lblCheckDate.Text = "Check Date";
            this.lblCheckDate.Visible = false;
            // 
            // txtCheckNo
            // 
            this.txtCheckNo.Location = new System.Drawing.Point(480, 107);
            this.txtCheckNo.Name = "txtCheckNo";
            this.txtCheckNo.Size = new System.Drawing.Size(132, 25);
            this.txtCheckNo.TabIndex = 33;
            this.txtCheckNo.Visible = false;
            this.txtCheckNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCheckNo_KeyDown);
            this.txtCheckNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCheckNo_KeyPress);
            // 
            // lblCheckNo
            // 
            this.lblCheckNo.AutoSize = true;
            this.lblCheckNo.Location = new System.Drawing.Point(410, 110);
            this.lblCheckNo.Name = "lblCheckNo";
            this.lblCheckNo.Size = new System.Drawing.Size(64, 17);
            this.lblCheckNo.TabIndex = 34;
            this.lblCheckNo.Text = "Check No";
            this.lblCheckNo.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cboBank);
            this.panel2.Controls.Add(this.txtBankBranch);
            this.panel2.Controls.Add(this.lblBankBranch);
            this.panel2.Controls.Add(this.lblCheckBank);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.lblCustomerId);
            this.panel2.Controls.Add(this.btnSubmit);
            this.panel2.Controls.Add(this.txtCheckNo);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lblCheckNo);
            this.panel2.Controls.Add(this.cboCustomer);
            this.panel2.Controls.Add(this.dtpCheckDate);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lblCheckDate);
            this.panel2.Controls.Add(this.lblOpenBalance);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cboPaymentMethod);
            this.panel2.Controls.Add(this.txtAmount);
            this.panel2.Controls.Add(this.dtpPaymentDate);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(901, 239);
            this.panel2.TabIndex = 36;
            // 
            // cboBank
            // 
            this.cboBank.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboBank.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBank.FormattingEnabled = true;
            this.cboBank.ItemHeight = 17;
            this.cboBank.Location = new System.Drawing.Point(134, 152);
            this.cboBank.Name = "cboBank";
            this.cboBank.Size = new System.Drawing.Size(233, 25);
            this.cboBank.TabIndex = 35;
            this.cboBank.Visible = false;
            this.cboBank.SelectionChangeCommitted += new System.EventHandler(this.cboBank_SelectionChangeCommitted);
            this.cboBank.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboBank_KeyDown);
            // 
            // txtBankBranch
            // 
            this.txtBankBranch.Location = new System.Drawing.Point(480, 152);
            this.txtBankBranch.Name = "txtBankBranch";
            this.txtBankBranch.Size = new System.Drawing.Size(132, 25);
            this.txtBankBranch.TabIndex = 37;
            this.txtBankBranch.Visible = false;
            this.txtBankBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBankBranch_KeyDown);
            // 
            // lblBankBranch
            // 
            this.lblBankBranch.AutoSize = true;
            this.lblBankBranch.Location = new System.Drawing.Point(396, 155);
            this.lblBankBranch.Name = "lblBankBranch";
            this.lblBankBranch.Size = new System.Drawing.Size(78, 17);
            this.lblBankBranch.TabIndex = 43;
            this.lblBankBranch.Text = "Bank Branch";
            this.lblBankBranch.Visible = false;
            // 
            // lblCheckBank
            // 
            this.lblCheckBank.AutoSize = true;
            this.lblCheckBank.Location = new System.Drawing.Point(55, 155);
            this.lblCheckBank.Name = "lblCheckBank";
            this.lblCheckBank.Size = new System.Drawing.Size(73, 17);
            this.lblCheckBank.TabIndex = 41;
            this.lblCheckBank.Text = "Check Bank";
            this.lblCheckBank.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(215, 193);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 32);
            this.button1.TabIndex = 43;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblCustomerId
            // 
            this.lblCustomerId.AutoSize = true;
            this.lblCustomerId.Location = new System.Drawing.Point(126, 3);
            this.lblCustomerId.Name = "lblCustomerId";
            this.lblCustomerId.Size = new System.Drawing.Size(0, 17);
            this.lblCustomerId.TabIndex = 38;
            this.lblCustomerId.Visible = false;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.Black;
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(134, 193);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 32);
            this.btnSubmit.TabIndex = 41;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // formCustomerPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 514);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "formCustomerPayment";
            this.Text = "formCustomerPayment";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblOpenBalance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpPaymentDate;
        public System.Windows.Forms.ComboBox cboPaymentMethod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpCheckDate;
        private System.Windows.Forms.Label lblCheckDate;
        private System.Windows.Forms.TextBox txtCheckNo;
        private System.Windows.Forms.Label lblCheckNo;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblCustomerId;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblCheckBank;
        public System.Windows.Forms.ComboBox cboBank;
        private System.Windows.Forms.TextBox txtBankBranch;
        private System.Windows.Forms.Label lblBankBranch;
    }
}