﻿namespace SosesPOS
{
    partial class formWriteCheckList
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtSearch = new MetroFramework.Controls.MetroTextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvVendorList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendorCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.term = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.View = new System.Windows.Forms.DataGridViewImageColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendorList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(883, 40);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(354, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(479, 40);
            this.panel2.TabIndex = 9;
            // 
            // txtSearch
            // 
            // 
            // 
            // 
            this.txtSearch.CustomButton.Image = null;
            this.txtSearch.CustomButton.Location = new System.Drawing.Point(446, 1);
            this.txtSearch.CustomButton.Name = "";
            this.txtSearch.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtSearch.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearch.CustomButton.TabIndex = 1;
            this.txtSearch.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearch.CustomButton.UseSelectable = true;
            this.txtSearch.CustomButton.Visible = false;
            this.txtSearch.DisplayIcon = true;
            this.txtSearch.Icon = global::SosesPOS.Properties.Resources.search;
            this.txtSearch.Lines = new string[0];
            this.txtSearch.Location = new System.Drawing.Point(3, 9);
            this.txtSearch.MaxLength = 32767;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.PromptText = "Search here...";
            this.txtSearch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearch.SelectedText = "";
            this.txtSearch.SelectionLength = 0;
            this.txtSearch.SelectionStart = 0;
            this.txtSearch.ShortcutsEnabled = true;
            this.txtSearch.Size = new System.Drawing.Size(470, 25);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.UseSelectable = true;
            this.txtSearch.WaterMark = "Search here...";
            this.txtSearch.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearch.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Image = global::SosesPOS.Properties.Resources.plus;
            this.pictureBox2.Location = new System.Drawing.Point(833, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(25, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::SosesPOS.Properties.Resources.cross__1_;
            this.pictureBox1.Location = new System.Drawing.Point(858, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 40);
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
            this.label1.Size = new System.Drawing.Size(106, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vendor List";
            // 
            // dgvVendorList
            // 
            this.dgvVendorList.AllowUserToAddRows = false;
            this.dgvVendorList.BackgroundColor = System.Drawing.Color.White;
            this.dgvVendorList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVendorList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVendorList.ColumnHeadersHeight = 30;
            this.dgvVendorList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.vendorCode,
            this.vendorName,
            this.payee,
            this.term,
            this.Column3,
            this.View,
            this.Edit,
            this.Delete});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVendorList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVendorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVendorList.EnableHeadersVisualStyles = false;
            this.dgvVendorList.Location = new System.Drawing.Point(0, 40);
            this.dgvVendorList.Name = "dgvVendorList";
            this.dgvVendorList.ReadOnly = true;
            this.dgvVendorList.RowHeadersVisible = false;
            this.dgvVendorList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVendorList.Size = new System.Drawing.Size(883, 481);
            this.dgvVendorList.TabIndex = 5;
            this.dgvVendorList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVendorList_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "#";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 42;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            // 
            // vendorCode
            // 
            this.vendorCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.vendorCode.HeaderText = "CODE";
            this.vendorCode.Name = "vendorCode";
            this.vendorCode.ReadOnly = true;
            this.vendorCode.Width = 74;
            // 
            // vendorName
            // 
            this.vendorName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.vendorName.HeaderText = "VENDOR NAME";
            this.vendorName.Name = "vendorName";
            this.vendorName.ReadOnly = true;
            // 
            // payee
            // 
            this.payee.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.payee.HeaderText = "PAYEE";
            this.payee.Name = "payee";
            this.payee.ReadOnly = true;
            this.payee.Width = 75;
            // 
            // term
            // 
            this.term.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.term.HeaderText = "TERM";
            this.term.Name = "term";
            this.term.ReadOnly = true;
            this.term.Width = 73;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "CATEGORY ID";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            // 
            // View
            // 
            this.View.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.View.HeaderText = "";
            this.View.Image = global::SosesPOS.Properties.Resources.search;
            this.View.Name = "View";
            this.View.ReadOnly = true;
            this.View.Visible = false;
            this.View.Width = 5;
            // 
            // Edit
            // 
            this.Edit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Edit.HeaderText = "";
            this.Edit.Image = global::SosesPOS.Properties.Resources.pencil;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Edit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Edit.Width = 17;
            // 
            // Delete
            // 
            this.Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Delete.HeaderText = "";
            this.Delete.Image = global::SosesPOS.Properties.Resources.trash;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Delete.Visible = false;
            this.Delete.Width = 17;
            // 
            // formWriteCheckList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 521);
            this.Controls.Add(this.dgvVendorList);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "formWriteCheckList";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendorList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroTextBox txtSearch;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvVendorList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendorCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn payee;
        private System.Windows.Forms.DataGridViewTextBoxColumn term;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewImageColumn View;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
    }
}