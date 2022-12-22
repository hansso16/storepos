namespace SosesPOS
{
    partial class formStockTransferDispatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formStockTransferDispatch));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toLocationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSaveAndPrint = new System.Windows.Forms.Button();
            this.btnNewTrans = new System.Windows.Forms.Button();
            this.toQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.toSLID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtRefNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboTo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboFrom = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.cboSearch = new System.Windows.Forms.ComboBox();
            this.txtPDesc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dgvFromInv = new System.Windows.Forms.DataGridView();
            this.fromSLID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fromLocationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fromQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvToInv = new System.Windows.Forms.DataGridView();
            this.hlblFromSLID = new System.Windows.Forms.Label();
            this.hlblToSLID = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFromInv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToInv)).BeginInit();
            this.SuspendLayout();
            // 
            // toLocationName
            // 
            this.toLocationName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.toLocationName.HeaderText = "TO SITE";
            this.toLocationName.Name = "toLocationName";
            this.toLocationName.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Search Product";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panel1.Controls.Add(this.btnGenerateReport);
            this.panel1.Controls.Add(this.btnSet);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnSaveAndPrint);
            this.panel1.Controls.Add(this.btnNewTrans);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 214);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 447);
            this.panel1.TabIndex = 29;
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGenerateReport.FlatAppearance.BorderSize = 0;
            this.btnGenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateReport.ForeColor = System.Drawing.Color.White;
            this.btnGenerateReport.Image = global::SosesPOS.Properties.Resources.search_white;
            this.btnGenerateReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerateReport.Location = new System.Drawing.Point(0, 222);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(200, 37);
            this.btnGenerateReport.TabIndex = 57;
            this.btnGenerateReport.Text = "     [F7] Generate Report";
            this.btnGenerateReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Visible = false;
            // 
            // btnSet
            // 
            this.btnSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSet.FlatAppearance.BorderSize = 0;
            this.btnSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSet.ForeColor = System.Drawing.Color.White;
            this.btnSet.Image = global::SosesPOS.Properties.Resources.search_white;
            this.btnSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSet.Location = new System.Drawing.Point(0, 185);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(200, 37);
            this.btnSet.TabIndex = 56;
            this.btnSet.Text = "     [F6] Set";
            this.btnSet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::SosesPOS.Properties.Resources.apps_white;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(0, 410);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(200, 37);
            this.btnClose.TabIndex = 60;
            this.btnClose.Text = "     [F10] Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPrint.Enabled = false;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Image = global::SosesPOS.Properties.Resources.print;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(0, 148);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(200, 37);
            this.btnPrint.TabIndex = 55;
            this.btnPrint.Text = "     [F5] Print";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLoad.FlatAppearance.BorderSize = 0;
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Image = global::SosesPOS.Properties.Resources.load;
            this.btnLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoad.Location = new System.Drawing.Point(0, 111);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(200, 37);
            this.btnLoad.TabIndex = 54;
            this.btnLoad.Text = "     [F4] Load";
            this.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoad.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSave.Enabled = false;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Image = global::SosesPOS.Properties.Resources.save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(0, 74);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 37);
            this.btnSave.TabIndex = 53;
            this.btnSave.Text = "     [F3] Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAndPrint
            // 
            this.btnSaveAndPrint.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSaveAndPrint.Enabled = false;
            this.btnSaveAndPrint.FlatAppearance.BorderSize = 0;
            this.btnSaveAndPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveAndPrint.ForeColor = System.Drawing.Color.White;
            this.btnSaveAndPrint.Image = global::SosesPOS.Properties.Resources.save;
            this.btnSaveAndPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveAndPrint.Location = new System.Drawing.Point(0, 37);
            this.btnSaveAndPrint.Name = "btnSaveAndPrint";
            this.btnSaveAndPrint.Size = new System.Drawing.Size(200, 37);
            this.btnSaveAndPrint.TabIndex = 52;
            this.btnSaveAndPrint.Text = "     [F2] Save and Print";
            this.btnSaveAndPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveAndPrint.UseVisualStyleBackColor = true;
            this.btnSaveAndPrint.Click += new System.EventHandler(this.btnSaveAndPrint_Click);
            // 
            // btnNewTrans
            // 
            this.btnNewTrans.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNewTrans.FlatAppearance.BorderSize = 0;
            this.btnNewTrans.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewTrans.ForeColor = System.Drawing.Color.White;
            this.btnNewTrans.Image = global::SosesPOS.Properties.Resources.plus_white;
            this.btnNewTrans.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewTrans.Location = new System.Drawing.Point(0, 0);
            this.btnNewTrans.Name = "btnNewTrans";
            this.btnNewTrans.Size = new System.Drawing.Size(200, 37);
            this.btnNewTrans.TabIndex = 51;
            this.btnNewTrans.Text = "     [F1] New Transaction";
            this.btnNewTrans.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNewTrans.UseVisualStyleBackColor = true;
            this.btnNewTrans.Click += new System.EventHandler(this.btnNewTrans_Click);
            // 
            // toQty
            // 
            this.toQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.toQty.DataPropertyName = "columnQty";
            this.toQty.HeaderText = "INV";
            this.toQty.Name = "toQty";
            this.toQty.ReadOnly = true;
            this.toQty.Width = 59;
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.BackgroundColor = System.Drawing.Color.White;
            this.dgvCart.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCart.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCart.ColumnHeadersHeight = 30;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCart.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.pcode,
            this.pdesc,
            this.qty,
            this.count,
            this.Delete});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCart.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCart.EnableHeadersVisualStyles = false;
            this.dgvCart.Location = new System.Drawing.Point(0, 214);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(984, 447);
            this.dgvCart.TabIndex = 30;
            this.dgvCart.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCart_CellContentClick);
            this.dgvCart.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCart_CellValueChanged);
            this.dgvCart.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvCart_EditingControlShowing);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.HeaderText = "#";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 44;
            // 
            // pcode
            // 
            this.pcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pcode.DataPropertyName = "columnPCode";
            this.pcode.HeaderText = "PCODE";
            this.pcode.Name = "pcode";
            this.pcode.ReadOnly = true;
            this.pcode.Width = 83;
            // 
            // pdesc
            // 
            this.pdesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.pdesc.HeaderText = "DESCRIPTION";
            this.pdesc.Name = "pdesc";
            this.pdesc.ReadOnly = true;
            // 
            // qty
            // 
            this.qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.qty.DataPropertyName = "qty";
            this.qty.HeaderText = "QTY";
            this.qty.Name = "qty";
            this.qty.Width = 61;
            // 
            // count
            // 
            this.count.HeaderText = "COUNT";
            this.count.Name = "count";
            this.count.ReadOnly = true;
            this.count.Visible = false;
            // 
            // Delete
            // 
            this.Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Delete.HeaderText = "";
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Delete.Width = 17;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn1.Image")));
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // toSLID
            // 
            this.toSLID.HeaderText = "SLID";
            this.toSLID.Name = "toSLID";
            this.toSLID.ReadOnly = true;
            this.toSLID.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.hlblToSLID);
            this.panel3.Controls.Add(this.hlblFromSLID);
            this.panel3.Controls.Add(this.txtRefNo);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.cboTo);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.cboFrom);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.txtCount);
            this.panel3.Controls.Add(this.cboSearch);
            this.panel3.Controls.Add(this.txtPDesc);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtPCode);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtQty);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(984, 214);
            this.panel3.TabIndex = 28;
            // 
            // txtRefNo
            // 
            this.txtRefNo.Location = new System.Drawing.Point(206, 7);
            this.txtRefNo.MaxLength = 8;
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.Size = new System.Drawing.Size(418, 29);
            this.txtRefNo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 21);
            this.label1.TabIndex = 37;
            this.label1.Text = "Stock Transfer Request No";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(326, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 21);
            this.label7.TabIndex = 34;
            this.label7.Text = "TO";
            // 
            // cboTo
            // 
            this.cboTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboTo.FormattingEnabled = true;
            this.cboTo.Items.AddRange(new object[] {
            "Warehouse",
            "Store"});
            this.cboTo.Location = new System.Drawing.Point(330, 67);
            this.cboTo.Name = "cboTo";
            this.cboTo.Size = new System.Drawing.Size(294, 29);
            this.cboTo.TabIndex = 6;
            this.cboTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboTo_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(308, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 21);
            this.label6.TabIndex = 32;
            this.label6.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 21);
            this.label3.TabIndex = 31;
            this.label3.Text = "FROM";
            // 
            // cboFrom
            // 
            this.cboFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboFrom.FormattingEnabled = true;
            this.cboFrom.Location = new System.Drawing.Point(12, 67);
            this.cboFrom.Name = "cboFrom";
            this.cboFrom.Size = new System.Drawing.Size(290, 29);
            this.cboFrom.TabIndex = 3;
            this.cboFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboFrom_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(532, 155);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 21);
            this.label14.TabIndex = 29;
            this.label14.Text = "Count";
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(536, 179);
            this.txtCount.Name = "txtCount";
            this.txtCount.ReadOnly = true;
            this.txtCount.Size = new System.Drawing.Size(88, 29);
            this.txtCount.TabIndex = 31;
            // 
            // cboSearch
            // 
            this.cboSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSearch.Enabled = false;
            this.cboSearch.FormattingEnabled = true;
            this.cboSearch.Location = new System.Drawing.Point(12, 123);
            this.cboSearch.Name = "cboSearch";
            this.cboSearch.Size = new System.Drawing.Size(518, 29);
            this.cboSearch.TabIndex = 11;
            this.cboSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboSearch_KeyDown);
            // 
            // txtPDesc
            // 
            this.txtPDesc.Location = new System.Drawing.Point(170, 179);
            this.txtPDesc.Name = "txtPDesc";
            this.txtPDesc.ReadOnly = true;
            this.txtPDesc.Size = new System.Drawing.Size(360, 29);
            this.txtPDesc.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(166, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 21);
            this.label5.TabIndex = 3;
            this.label5.Text = "Product Description";
            // 
            // txtPCode
            // 
            this.txtPCode.Location = new System.Drawing.Point(12, 179);
            this.txtPCode.Name = "txtPCode";
            this.txtPCode.ReadOnly = true;
            this.txtPCode.Size = new System.Drawing.Size(152, 29);
            this.txtPCode.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "Product Code";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(536, 123);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(88, 29);
            this.txtQty.TabIndex = 16;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(532, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 21);
            this.label8.TabIndex = 9;
            this.label8.Text = "Qty";
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.dgvFromInv);
            this.panel6.Controls.Add(this.dgvToInv);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(634, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(350, 214);
            this.panel6.TabIndex = 8;
            // 
            // dgvFromInv
            // 
            this.dgvFromInv.AllowUserToAddRows = false;
            this.dgvFromInv.BackgroundColor = System.Drawing.Color.White;
            this.dgvFromInv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFromInv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFromInv.ColumnHeadersHeight = 30;
            this.dgvFromInv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFromInv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fromSLID,
            this.fromLocationName,
            this.fromQty});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFromInv.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFromInv.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgvFromInv.EnableHeadersVisualStyles = false;
            this.dgvFromInv.Location = new System.Drawing.Point(-1, 0);
            this.dgvFromInv.Name = "dgvFromInv";
            this.dgvFromInv.RowHeadersVisible = false;
            this.dgvFromInv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFromInv.Size = new System.Drawing.Size(176, 212);
            this.dgvFromInv.TabIndex = 32;
            // 
            // fromSLID
            // 
            this.fromSLID.HeaderText = "SLID";
            this.fromSLID.Name = "fromSLID";
            this.fromSLID.ReadOnly = true;
            this.fromSLID.Visible = false;
            // 
            // fromLocationName
            // 
            this.fromLocationName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fromLocationName.HeaderText = "FROM SITE";
            this.fromLocationName.Name = "fromLocationName";
            this.fromLocationName.ReadOnly = true;
            // 
            // fromQty
            // 
            this.fromQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.fromQty.DataPropertyName = "columnQty";
            this.fromQty.HeaderText = "INV";
            this.fromQty.Name = "fromQty";
            this.fromQty.ReadOnly = true;
            this.fromQty.Width = 59;
            // 
            // dgvToInv
            // 
            this.dgvToInv.AllowUserToAddRows = false;
            this.dgvToInv.BackgroundColor = System.Drawing.Color.White;
            this.dgvToInv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvToInv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvToInv.ColumnHeadersHeight = 30;
            this.dgvToInv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvToInv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.toSLID,
            this.toLocationName,
            this.toQty});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvToInv.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvToInv.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgvToInv.EnableHeadersVisualStyles = false;
            this.dgvToInv.Location = new System.Drawing.Point(175, 0);
            this.dgvToInv.Name = "dgvToInv";
            this.dgvToInv.RowHeadersVisible = false;
            this.dgvToInv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvToInv.Size = new System.Drawing.Size(173, 212);
            this.dgvToInv.TabIndex = 31;
            // 
            // hlblFromSLID
            // 
            this.hlblFromSLID.AutoSize = true;
            this.hlblFromSLID.Location = new System.Drawing.Point(68, 30);
            this.hlblFromSLID.Name = "hlblFromSLID";
            this.hlblFromSLID.Size = new System.Drawing.Size(0, 21);
            this.hlblFromSLID.TabIndex = 38;
            this.hlblFromSLID.Visible = false;
            // 
            // hlblToSLID
            // 
            this.hlblToSLID.AutoSize = true;
            this.hlblToSLID.Location = new System.Drawing.Point(74, 30);
            this.hlblToSLID.Name = "hlblToSLID";
            this.hlblToSLID.Size = new System.Drawing.Size(0, 21);
            this.hlblToSLID.TabIndex = 39;
            this.hlblToSLID.Visible = false;
            // 
            // formStockTransferDispatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "formStockTransferDispatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Transfer Dispatch";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.formStockTransferDispatch_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFromInv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToInv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn toLocationName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        protected internal System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnLoad;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Button btnSaveAndPrint;
        private System.Windows.Forms.Button btnNewTrans;
        private System.Windows.Forms.DataGridViewTextBoxColumn toQty;
        public System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn pdesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn count;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn toSLID;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox cboTo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cboFrom;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtCount;
        public System.Windows.Forms.ComboBox cboSearch;
        private System.Windows.Forms.TextBox txtPDesc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPCode;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView dgvFromInv;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromSLID;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromLocationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromQty;
        private System.Windows.Forms.DataGridView dgvToInv;
        private System.Windows.Forms.TextBox txtRefNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label hlblFromSLID;
        private System.Windows.Forms.Label hlblToSLID;
    }
}