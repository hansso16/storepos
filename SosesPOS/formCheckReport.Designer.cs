namespace SosesPOS
{
    partial class formCheckReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboBank = new System.Windows.Forms.ComboBox();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnBlankCheck = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCheckList = new System.Windows.Forms.DataGridView();
            this.btnExportToCSV = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SELECT = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CheckDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Payee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsExported = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colCancel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CheckId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnExportToCSV);
            this.panel1.Controls.Add(this.cboBank);
            this.panel1.Controls.Add(this.btnUnselectAll);
            this.panel1.Controls.Add(this.btnSelectAll);
            this.panel1.Controls.Add(this.btnBlankCheck);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1072, 49);
            this.panel1.TabIndex = 3;
            // 
            // cboBank
            // 
            this.cboBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBank.FormattingEnabled = true;
            this.cboBank.Location = new System.Drawing.Point(690, 13);
            this.cboBank.Name = "cboBank";
            this.cboBank.Size = new System.Drawing.Size(140, 29);
            this.cboBank.TabIndex = 14;
            this.cboBank.SelectedIndexChanged += new System.EventHandler(this.cboBank_SelectedIndexChanged);
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(158)))), ((int)(((byte)(132)))));
            this.btnUnselectAll.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnUnselectAll.Location = new System.Drawing.Point(571, 8);
            this.btnUnselectAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(112, 35);
            this.btnUnselectAll.TabIndex = 13;
            this.btnUnselectAll.Text = "Unselect All";
            this.btnUnselectAll.UseVisualStyleBackColor = false;
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(158)))), ((int)(((byte)(132)))));
            this.btnSelectAll.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSelectAll.Location = new System.Drawing.Point(458, 8);
            this.btnSelectAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(105, 35);
            this.btnSelectAll.TabIndex = 12;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnBlankCheck
            // 
            this.btnBlankCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(158)))), ((int)(((byte)(132)))));
            this.btnBlankCheck.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnBlankCheck.Location = new System.Drawing.Point(837, 7);
            this.btnBlankCheck.Margin = new System.Windows.Forms.Padding(4);
            this.btnBlankCheck.Name = "btnBlankCheck";
            this.btnBlankCheck.Size = new System.Drawing.Size(195, 35);
            this.btnBlankCheck.TabIndex = 11;
            this.btnBlankCheck.Text = "Print";
            this.btnBlankCheck.UseVisualStyleBackColor = false;
            this.btnBlankCheck.Click += new System.EventHandler(this.btnBlankCheck_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::SosesPOS.Properties.Resources.cross__1_;
            this.pictureBox1.Location = new System.Drawing.Point(1040, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 49);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Today\'s Check List";
            // 
            // dgvCheckList
            // 
            this.dgvCheckList.AllowUserToAddRows = false;
            this.dgvCheckList.BackgroundColor = System.Drawing.Color.White;
            this.dgvCheckList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCheckList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCheckList.ColumnHeadersHeight = 30;
            this.dgvCheckList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCheckList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.SELECT,
            this.CheckDate,
            this.CheckNo,
            this.Payee,
            this.Amount,
            this.colIsExported,
            this.colCancel,
            this.CheckId});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCheckList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCheckList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCheckList.EnableHeadersVisualStyles = false;
            this.dgvCheckList.Location = new System.Drawing.Point(0, 49);
            this.dgvCheckList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvCheckList.Name = "dgvCheckList";
            this.dgvCheckList.RowHeadersVisible = false;
            this.dgvCheckList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCheckList.Size = new System.Drawing.Size(1072, 495);
            this.dgvCheckList.TabIndex = 4;
            this.dgvCheckList.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvCheckList_CellBeginEdit);
            this.dgvCheckList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCheckList_CellContentClick);
            this.dgvCheckList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCheckList_CellValueChanged);
            this.dgvCheckList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvCheckList_KeyDown);
            this.dgvCheckList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvCheckList_KeyUp);
            // 
            // btnExportToCSV
            // 
            this.btnExportToCSV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(158)))), ((int)(((byte)(132)))));
            this.btnExportToCSV.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnExportToCSV.Location = new System.Drawing.Point(185, 7);
            this.btnExportToCSV.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportToCSV.Name = "btnExportToCSV";
            this.btnExportToCSV.Size = new System.Drawing.Size(129, 35);
            this.btnExportToCSV.TabIndex = 15;
            this.btnExportToCSV.Text = "Export to CSV";
            this.btnExportToCSV.UseVisualStyleBackColor = false;
            this.btnExportToCSV.Click += new System.EventHandler(this.btnExportToCSV_Click);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "#";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 42;
            // 
            // SELECT
            // 
            this.SELECT.HeaderText = "SELECT";
            this.SELECT.Name = "SELECT";
            // 
            // CheckDate
            // 
            this.CheckDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CheckDate.HeaderText = "Check Date";
            this.CheckDate.Name = "CheckDate";
            this.CheckDate.ReadOnly = true;
            this.CheckDate.Width = 111;
            // 
            // CheckNo
            // 
            this.CheckNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CheckNo.HeaderText = "Check No";
            this.CheckNo.Name = "CheckNo";
            // 
            // Payee
            // 
            this.Payee.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Payee.HeaderText = "Payee";
            this.Payee.Name = "Payee";
            this.Payee.ReadOnly = true;
            // 
            // Amount
            // 
            this.Amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 89;
            // 
            // colIsExported
            // 
            this.colIsExported.HeaderText = "EXP";
            this.colIsExported.Name = "colIsExported";
            this.colIsExported.ReadOnly = true;
            this.colIsExported.Visible = false;
            // 
            // colCancel
            // 
            this.colCancel.HeaderText = "CANCEL";
            this.colCancel.Name = "colCancel";
            this.colCancel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CheckId
            // 
            this.CheckId.HeaderText = "CheckId";
            this.CheckId.Name = "CheckId";
            this.CheckId.ReadOnly = true;
            this.CheckId.Visible = false;
            // 
            // formCheckReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 544);
            this.ControlBox = false;
            this.Controls.Add(this.dgvCheckList);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "formCheckReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCheckList;
        private System.Windows.Forms.Button btnBlankCheck;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.ComboBox cboBank;
        private System.Windows.Forms.Button btnExportToCSV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SELECT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CheckDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CheckNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Payee;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewButtonColumn colIsExported;
        private System.Windows.Forms.DataGridViewButtonColumn colCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn CheckId;
    }
}