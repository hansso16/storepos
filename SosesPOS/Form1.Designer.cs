namespace SosesPOS
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.btnPassword = new System.Windows.Forms.Button();
            this.btnUser = new System.Windows.Forms.Button();
            this.btnWriteCheck = new System.Windows.Forms.Button();
            this.StockTransferPanel = new System.Windows.Forms.Panel();
            this.btnTransferAccept = new System.Windows.Forms.Button();
            this.btnTransferDispatch = new System.Windows.Forms.Button();
            this.btnTransferRequest = new System.Windows.Forms.Button();
            this.btnStockTransfer = new System.Windows.Forms.Button();
            this.btnStockReplenishment = new System.Windows.Forms.Button();
            this.btnReceivePayments = new System.Windows.Forms.Button();
            this.btnStockLocation = new System.Windows.Forms.Button();
            this.btnCategory = new System.Windows.Forms.Button();
            this.btnBank = new System.Windows.Forms.Button();
            this.btnBrand = new System.Windows.Forms.Button();
            this.btnUnits = new System.Windows.Forms.Button();
            this.btnVendor = new System.Windows.Forms.Button();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.btnProduct = new System.Windows.Forms.Button();
            this.btnBillingInvoice = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblUserCode = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnLiteCheckWriter = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.StockTransferPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(158)))), ((int)(((byte)(132)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 40);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panel2.Controls.Add(this.button8);
            this.panel2.Controls.Add(this.btnPassword);
            this.panel2.Controls.Add(this.btnUser);
            this.panel2.Controls.Add(this.btnLiteCheckWriter);
            this.panel2.Controls.Add(this.btnWriteCheck);
            this.panel2.Controls.Add(this.StockTransferPanel);
            this.panel2.Controls.Add(this.btnStockReplenishment);
            this.panel2.Controls.Add(this.btnReceivePayments);
            this.panel2.Controls.Add(this.btnStockLocation);
            this.panel2.Controls.Add(this.btnCategory);
            this.panel2.Controls.Add(this.btnBank);
            this.panel2.Controls.Add(this.btnBrand);
            this.panel2.Controls.Add(this.btnUnits);
            this.panel2.Controls.Add(this.btnVendor);
            this.panel2.Controls.Add(this.btnCustomer);
            this.panel2.Controls.Add(this.btnProduct);
            this.panel2.Controls.Add(this.btnBillingInvoice);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.lblRole);
            this.panel2.Controls.Add(this.lblUsername);
            this.panel2.Controls.Add(this.lblUserCode);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(301, 521);
            this.panel2.TabIndex = 1;
            // 
            // button8
            // 
            this.button8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.ForeColor = System.Drawing.SystemColors.Control;
            this.button8.Image = global::SosesPOS.Properties.Resources.logout;
            this.button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.Location = new System.Drawing.Point(0, 709);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(284, 37);
            this.button8.TabIndex = 20;
            this.button8.Text = "Logout";
            this.button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // btnPassword
            // 
            this.btnPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPassword.FlatAppearance.BorderSize = 0;
            this.btnPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPassword.ForeColor = System.Drawing.SystemColors.Control;
            this.btnPassword.Image = global::SosesPOS.Properties.Resources.change_password;
            this.btnPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPassword.Location = new System.Drawing.Point(0, 672);
            this.btnPassword.Name = "btnPassword";
            this.btnPassword.Size = new System.Drawing.Size(284, 37);
            this.btnPassword.TabIndex = 17;
            this.btnPassword.Text = "Change Password";
            this.btnPassword.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPassword.UseVisualStyleBackColor = true;
            this.btnPassword.Click += new System.EventHandler(this.btnPassword_Click);
            // 
            // btnUser
            // 
            this.btnUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUser.FlatAppearance.BorderSize = 0;
            this.btnUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUser.ForeColor = System.Drawing.SystemColors.Control;
            this.btnUser.Image = global::SosesPOS.Properties.Resources.user_group;
            this.btnUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUser.Location = new System.Drawing.Point(0, 635);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(284, 37);
            this.btnUser.TabIndex = 16;
            this.btnUser.Text = "User";
            this.btnUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUser.UseVisualStyleBackColor = true;
            this.btnUser.Visible = false;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // btnWriteCheck
            // 
            this.btnWriteCheck.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnWriteCheck.FlatAppearance.BorderSize = 0;
            this.btnWriteCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWriteCheck.ForeColor = System.Drawing.SystemColors.Control;
            this.btnWriteCheck.Image = global::SosesPOS.Properties.Resources.cheque;
            this.btnWriteCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWriteCheck.Location = new System.Drawing.Point(0, 561);
            this.btnWriteCheck.Name = "btnWriteCheck";
            this.btnWriteCheck.Size = new System.Drawing.Size(284, 37);
            this.btnWriteCheck.TabIndex = 48;
            this.btnWriteCheck.Text = "Check Writer";
            this.btnWriteCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnWriteCheck.UseVisualStyleBackColor = true;
            this.btnWriteCheck.Visible = false;
            this.btnWriteCheck.Click += new System.EventHandler(this.btnWriteCheck_Click);
            // 
            // StockTransferPanel
            // 
            this.StockTransferPanel.Controls.Add(this.btnTransferAccept);
            this.StockTransferPanel.Controls.Add(this.btnTransferDispatch);
            this.StockTransferPanel.Controls.Add(this.btnTransferRequest);
            this.StockTransferPanel.Controls.Add(this.btnStockTransfer);
            this.StockTransferPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.StockTransferPanel.Location = new System.Drawing.Point(0, 524);
            this.StockTransferPanel.MaximumSize = new System.Drawing.Size(284, 144);
            this.StockTransferPanel.MinimumSize = new System.Drawing.Size(284, 37);
            this.StockTransferPanel.Name = "StockTransferPanel";
            this.StockTransferPanel.Size = new System.Drawing.Size(284, 37);
            this.StockTransferPanel.TabIndex = 0;
            this.StockTransferPanel.Visible = false;
            // 
            // btnTransferAccept
            // 
            this.btnTransferAccept.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTransferAccept.FlatAppearance.BorderSize = 0;
            this.btnTransferAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransferAccept.ForeColor = System.Drawing.SystemColors.Control;
            this.btnTransferAccept.Image = global::SosesPOS.Properties.Resources.transfer;
            this.btnTransferAccept.Location = new System.Drawing.Point(0, 111);
            this.btnTransferAccept.Name = "btnTransferAccept";
            this.btnTransferAccept.Size = new System.Drawing.Size(284, 37);
            this.btnTransferAccept.TabIndex = 18;
            this.btnTransferAccept.Text = "Transfer Accept";
            this.btnTransferAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTransferAccept.UseVisualStyleBackColor = true;
            this.btnTransferAccept.Visible = false;
            this.btnTransferAccept.Click += new System.EventHandler(this.btnTransferAccept_Click);
            // 
            // btnTransferDispatch
            // 
            this.btnTransferDispatch.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTransferDispatch.FlatAppearance.BorderSize = 0;
            this.btnTransferDispatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransferDispatch.ForeColor = System.Drawing.SystemColors.Control;
            this.btnTransferDispatch.Image = global::SosesPOS.Properties.Resources.transfer;
            this.btnTransferDispatch.Location = new System.Drawing.Point(0, 74);
            this.btnTransferDispatch.Name = "btnTransferDispatch";
            this.btnTransferDispatch.Size = new System.Drawing.Size(284, 37);
            this.btnTransferDispatch.TabIndex = 17;
            this.btnTransferDispatch.Text = "Transfer Dispatch";
            this.btnTransferDispatch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTransferDispatch.UseVisualStyleBackColor = true;
            this.btnTransferDispatch.Visible = false;
            this.btnTransferDispatch.Click += new System.EventHandler(this.btnTransferDispatch_Click);
            // 
            // btnTransferRequest
            // 
            this.btnTransferRequest.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTransferRequest.FlatAppearance.BorderSize = 0;
            this.btnTransferRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransferRequest.ForeColor = System.Drawing.SystemColors.Control;
            this.btnTransferRequest.Image = global::SosesPOS.Properties.Resources.transfer;
            this.btnTransferRequest.Location = new System.Drawing.Point(0, 37);
            this.btnTransferRequest.Name = "btnTransferRequest";
            this.btnTransferRequest.Size = new System.Drawing.Size(284, 37);
            this.btnTransferRequest.TabIndex = 16;
            this.btnTransferRequest.Text = "Transfer Request";
            this.btnTransferRequest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTransferRequest.UseVisualStyleBackColor = true;
            this.btnTransferRequest.Visible = false;
            this.btnTransferRequest.Click += new System.EventHandler(this.btnTransferRequest_Click);
            // 
            // btnStockTransfer
            // 
            this.btnStockTransfer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnStockTransfer.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStockTransfer.FlatAppearance.BorderSize = 0;
            this.btnStockTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockTransfer.ForeColor = System.Drawing.SystemColors.Control;
            this.btnStockTransfer.Image = global::SosesPOS.Properties.Resources.expand_icon1;
            this.btnStockTransfer.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStockTransfer.Location = new System.Drawing.Point(0, 0);
            this.btnStockTransfer.Name = "btnStockTransfer";
            this.btnStockTransfer.Size = new System.Drawing.Size(284, 37);
            this.btnStockTransfer.TabIndex = 20;
            this.btnStockTransfer.Text = "Stock Transfer";
            this.btnStockTransfer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStockTransfer.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnStockTransfer.UseVisualStyleBackColor = true;
            this.btnStockTransfer.Visible = false;
            this.btnStockTransfer.Click += new System.EventHandler(this.btnStockTransfer_Click);
            // 
            // btnStockReplenishment
            // 
            this.btnStockReplenishment.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStockReplenishment.FlatAppearance.BorderSize = 0;
            this.btnStockReplenishment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockReplenishment.ForeColor = System.Drawing.SystemColors.Control;
            this.btnStockReplenishment.Image = global::SosesPOS.Properties.Resources.stock_replenishment;
            this.btnStockReplenishment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStockReplenishment.Location = new System.Drawing.Point(0, 487);
            this.btnStockReplenishment.Name = "btnStockReplenishment";
            this.btnStockReplenishment.Size = new System.Drawing.Size(284, 37);
            this.btnStockReplenishment.TabIndex = 14;
            this.btnStockReplenishment.Text = "Stock Replenishment";
            this.btnStockReplenishment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStockReplenishment.UseVisualStyleBackColor = true;
            this.btnStockReplenishment.Visible = false;
            this.btnStockReplenishment.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnReceivePayments
            // 
            this.btnReceivePayments.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReceivePayments.FlatAppearance.BorderSize = 0;
            this.btnReceivePayments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceivePayments.ForeColor = System.Drawing.SystemColors.Control;
            this.btnReceivePayments.Image = global::SosesPOS.Properties.Resources.payment;
            this.btnReceivePayments.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReceivePayments.Location = new System.Drawing.Point(0, 450);
            this.btnReceivePayments.Name = "btnReceivePayments";
            this.btnReceivePayments.Size = new System.Drawing.Size(284, 37);
            this.btnReceivePayments.TabIndex = 13;
            this.btnReceivePayments.Text = "Receive Payments";
            this.btnReceivePayments.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReceivePayments.UseVisualStyleBackColor = true;
            this.btnReceivePayments.Visible = false;
            this.btnReceivePayments.Click += new System.EventHandler(this.btnReceivePayments_Click);
            // 
            // btnStockLocation
            // 
            this.btnStockLocation.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStockLocation.FlatAppearance.BorderSize = 0;
            this.btnStockLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockLocation.ForeColor = System.Drawing.SystemColors.Control;
            this.btnStockLocation.Image = global::SosesPOS.Properties.Resources.stock;
            this.btnStockLocation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStockLocation.Location = new System.Drawing.Point(0, 413);
            this.btnStockLocation.Name = "btnStockLocation";
            this.btnStockLocation.Size = new System.Drawing.Size(284, 37);
            this.btnStockLocation.TabIndex = 12;
            this.btnStockLocation.Text = "Stock Location";
            this.btnStockLocation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStockLocation.UseVisualStyleBackColor = true;
            this.btnStockLocation.Visible = false;
            this.btnStockLocation.Click += new System.EventHandler(this.btnStockLocation_Click);
            // 
            // btnCategory
            // 
            this.btnCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCategory.FlatAppearance.BorderSize = 0;
            this.btnCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategory.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCategory.Image = global::SosesPOS.Properties.Resources.folder1;
            this.btnCategory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCategory.Location = new System.Drawing.Point(0, 376);
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Size = new System.Drawing.Size(284, 37);
            this.btnCategory.TabIndex = 11;
            this.btnCategory.Text = "Category";
            this.btnCategory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCategory.UseVisualStyleBackColor = true;
            this.btnCategory.Visible = false;
            this.btnCategory.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnBank
            // 
            this.btnBank.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBank.FlatAppearance.BorderSize = 0;
            this.btnBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBank.ForeColor = System.Drawing.SystemColors.Control;
            this.btnBank.Image = global::SosesPOS.Properties.Resources.folder1;
            this.btnBank.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBank.Location = new System.Drawing.Point(0, 339);
            this.btnBank.Name = "btnBank";
            this.btnBank.Size = new System.Drawing.Size(284, 37);
            this.btnBank.TabIndex = 10;
            this.btnBank.Text = "Bank";
            this.btnBank.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBank.UseVisualStyleBackColor = true;
            this.btnBank.Visible = false;
            this.btnBank.Click += new System.EventHandler(this.btnBank_Click);
            // 
            // btnBrand
            // 
            this.btnBrand.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBrand.FlatAppearance.BorderSize = 0;
            this.btnBrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrand.ForeColor = System.Drawing.SystemColors.Control;
            this.btnBrand.Image = global::SosesPOS.Properties.Resources.uom;
            this.btnBrand.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrand.Location = new System.Drawing.Point(0, 302);
            this.btnBrand.Name = "btnBrand";
            this.btnBrand.Size = new System.Drawing.Size(284, 37);
            this.btnBrand.TabIndex = 9;
            this.btnBrand.Text = "Unit of Measurement";
            this.btnBrand.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBrand.UseVisualStyleBackColor = true;
            this.btnBrand.Visible = false;
            this.btnBrand.Click += new System.EventHandler(this.btnBrand_Click);
            // 
            // btnUnits
            // 
            this.btnUnits.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUnits.FlatAppearance.BorderSize = 0;
            this.btnUnits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnits.ForeColor = System.Drawing.SystemColors.Control;
            this.btnUnits.Image = global::SosesPOS.Properties.Resources.folder1;
            this.btnUnits.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUnits.Location = new System.Drawing.Point(0, 265);
            this.btnUnits.Name = "btnUnits";
            this.btnUnits.Size = new System.Drawing.Size(284, 37);
            this.btnUnits.TabIndex = 8;
            this.btnUnits.Text = "Units Parameters";
            this.btnUnits.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUnits.UseVisualStyleBackColor = true;
            this.btnUnits.Visible = false;
            this.btnUnits.Click += new System.EventHandler(this.btnUnits_Click);
            // 
            // btnVendor
            // 
            this.btnVendor.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVendor.FlatAppearance.BorderSize = 0;
            this.btnVendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVendor.ForeColor = System.Drawing.SystemColors.Control;
            this.btnVendor.Image = global::SosesPOS.Properties.Resources.vendor;
            this.btnVendor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVendor.Location = new System.Drawing.Point(0, 228);
            this.btnVendor.Name = "btnVendor";
            this.btnVendor.Size = new System.Drawing.Size(284, 37);
            this.btnVendor.TabIndex = 7;
            this.btnVendor.Text = "Vendor";
            this.btnVendor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVendor.UseVisualStyleBackColor = true;
            this.btnVendor.Visible = false;
            this.btnVendor.Click += new System.EventHandler(this.btnVendor_Click);
            // 
            // btnCustomer
            // 
            this.btnCustomer.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCustomer.FlatAppearance.BorderSize = 0;
            this.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomer.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCustomer.Image = global::SosesPOS.Properties.Resources.customer;
            this.btnCustomer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCustomer.Location = new System.Drawing.Point(0, 191);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(284, 37);
            this.btnCustomer.TabIndex = 46;
            this.btnCustomer.Text = "Customer";
            this.btnCustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCustomer.UseVisualStyleBackColor = true;
            this.btnCustomer.Visible = false;
            this.btnCustomer.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnProduct
            // 
            this.btnProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProduct.FlatAppearance.BorderSize = 0;
            this.btnProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProduct.ForeColor = System.Drawing.SystemColors.Control;
            this.btnProduct.Image = global::SosesPOS.Properties.Resources.product;
            this.btnProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProduct.Location = new System.Drawing.Point(0, 154);
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Size = new System.Drawing.Size(284, 37);
            this.btnProduct.TabIndex = 5;
            this.btnProduct.Text = "Product";
            this.btnProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProduct.UseVisualStyleBackColor = true;
            this.btnProduct.Visible = false;
            this.btnProduct.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnBillingInvoice
            // 
            this.btnBillingInvoice.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBillingInvoice.FlatAppearance.BorderSize = 0;
            this.btnBillingInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBillingInvoice.ForeColor = System.Drawing.SystemColors.Control;
            this.btnBillingInvoice.Image = global::SosesPOS.Properties.Resources.invoice;
            this.btnBillingInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBillingInvoice.Location = new System.Drawing.Point(0, 117);
            this.btnBillingInvoice.Name = "btnBillingInvoice";
            this.btnBillingInvoice.Size = new System.Drawing.Size(284, 37);
            this.btnBillingInvoice.TabIndex = 4;
            this.btnBillingInvoice.Text = "Delivery Invoice";
            this.btnBillingInvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBillingInvoice.UseVisualStyleBackColor = true;
            this.btnBillingInvoice.Visible = false;
            this.btnBillingInvoice.Click += new System.EventHandler(this.btnBillingInvoice_Click);
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Top;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.SystemColors.Control;
            this.button5.Image = global::SosesPOS.Properties.Resources.invoice;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(0, 80);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(284, 37);
            this.button5.TabIndex = 3;
            this.button5.Text = "Store Invoice";
            this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // lblRole
            // 
            this.lblRole.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.ForeColor = System.Drawing.Color.White;
            this.lblRole.Location = new System.Drawing.Point(0, 58);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(284, 22);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "Role Name";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsername
            // 
            this.lblUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.lblUsername.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(158)))), ((int)(((byte)(132)))));
            this.lblUsername.Location = new System.Drawing.Point(0, 24);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(284, 34);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Username";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUserCode
            // 
            this.lblUserCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.lblUserCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUserCode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(158)))), ((int)(((byte)(132)))));
            this.lblUserCode.Location = new System.Drawing.Point(0, 0);
            this.lblUserCode.Name = "lblUserCode";
            this.lblUserCode.Size = new System.Drawing.Size(284, 24);
            this.lblUserCode.TabIndex = 47;
            this.lblUserCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUserCode.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(301, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(883, 521);
            this.panel3.TabIndex = 2;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // timer1
            // 
            this.timer1.Interval = 15;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnLiteCheckWriter
            // 
            this.btnLiteCheckWriter.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLiteCheckWriter.FlatAppearance.BorderSize = 0;
            this.btnLiteCheckWriter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLiteCheckWriter.ForeColor = System.Drawing.SystemColors.Control;
            this.btnLiteCheckWriter.Image = global::SosesPOS.Properties.Resources.cheque;
            this.btnLiteCheckWriter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLiteCheckWriter.Location = new System.Drawing.Point(0, 598);
            this.btnLiteCheckWriter.Name = "btnLiteCheckWriter";
            this.btnLiteCheckWriter.Size = new System.Drawing.Size(284, 37);
            this.btnLiteCheckWriter.TabIndex = 49;
            this.btnLiteCheckWriter.Text = "Lite Check Writer";
            this.btnLiteCheckWriter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLiteCheckWriter.UseVisualStyleBackColor = true;
            this.btnLiteCheckWriter.Visible = false;
            this.btnLiteCheckWriter.Click += new System.EventHandler(this.btnLiteCheckWriter_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SOSE\'S STORE POS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel2.ResumeLayout(false);
            this.StockTransferPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button btnVendor;
        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnBrand;
        private System.Windows.Forms.Button btnCategory;
        private System.Windows.Forms.Button btnProduct;
        private System.Windows.Forms.Button btnStockReplenishment;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnReceivePayments;
        private System.Windows.Forms.Button btnBank;
        private System.Windows.Forms.Button btnStockLocation;
        private System.Windows.Forms.Button btnBillingInvoice;
        private System.Windows.Forms.Button btnUnits;
        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.Button btnPassword;
        private System.Windows.Forms.Label lblUserCode;
        private System.Windows.Forms.Panel StockTransferPanel;
        private System.Windows.Forms.Button btnTransferAccept;
        private System.Windows.Forms.Button btnTransferDispatch;
        private System.Windows.Forms.Button btnTransferRequest;
        private System.Windows.Forms.Button btnStockTransfer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnWriteCheck;
        private System.Windows.Forms.Button btnLiteCheckWriter;
    }
}

