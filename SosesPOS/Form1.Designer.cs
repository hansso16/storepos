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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.btnStockIn = new System.Windows.Forms.Button();
            this.btnReceivePayments = new System.Windows.Forms.Button();
            this.btnStockLocation = new System.Windows.Forms.Button();
            this.btnCategory = new System.Windows.Forms.Button();
            this.btnBank = new System.Windows.Forms.Button();
            this.btnBrand = new System.Windows.Forms.Button();
            this.btnVendor = new System.Windows.Forms.Button();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.btnProduct = new System.Windows.Forms.Button();
            this.btnBillingInvoice = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
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
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panel2.Controls.Add(this.button8);
            this.panel2.Controls.Add(this.btnStockIn);
            this.panel2.Controls.Add(this.btnReceivePayments);
            this.panel2.Controls.Add(this.btnStockLocation);
            this.panel2.Controls.Add(this.btnCategory);
            this.panel2.Controls.Add(this.btnBank);
            this.panel2.Controls.Add(this.btnBrand);
            this.panel2.Controls.Add(this.btnVendor);
            this.panel2.Controls.Add(this.btnCustomer);
            this.panel2.Controls.Add(this.btnProduct);
            this.panel2.Controls.Add(this.btnBillingInvoice);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.lblRole);
            this.panel2.Controls.Add(this.lblUsername);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(301, 521);
            this.panel2.TabIndex = 1;
            // 
            // button8
            // 
            this.button8.Dock = System.Windows.Forms.DockStyle.Top;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.ForeColor = System.Drawing.SystemColors.Control;
            this.button8.Image = global::SosesPOS.Properties.Resources.logout;
            this.button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.Location = new System.Drawing.Point(0, 475);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(301, 37);
            this.button8.TabIndex = 11;
            this.button8.Text = "Logout";
            this.button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Visible = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // btnStockIn
            // 
            this.btnStockIn.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStockIn.FlatAppearance.BorderSize = 0;
            this.btnStockIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockIn.ForeColor = System.Drawing.SystemColors.Control;
            this.btnStockIn.Image = global::SosesPOS.Properties.Resources.stock_replenishment;
            this.btnStockIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStockIn.Location = new System.Drawing.Point(0, 438);
            this.btnStockIn.Name = "btnStockIn";
            this.btnStockIn.Size = new System.Drawing.Size(301, 37);
            this.btnStockIn.TabIndex = 6;
            this.btnStockIn.Text = "Stock Replenishment";
            this.btnStockIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStockIn.UseVisualStyleBackColor = true;
            this.btnStockIn.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnReceivePayments
            // 
            this.btnReceivePayments.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReceivePayments.FlatAppearance.BorderSize = 0;
            this.btnReceivePayments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceivePayments.ForeColor = System.Drawing.SystemColors.Control;
            this.btnReceivePayments.Image = global::SosesPOS.Properties.Resources.payment;
            this.btnReceivePayments.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReceivePayments.Location = new System.Drawing.Point(0, 401);
            this.btnReceivePayments.Name = "btnReceivePayments";
            this.btnReceivePayments.Size = new System.Drawing.Size(301, 37);
            this.btnReceivePayments.TabIndex = 9;
            this.btnReceivePayments.Text = "Receive Payments";
            this.btnReceivePayments.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReceivePayments.UseVisualStyleBackColor = true;
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
            this.btnStockLocation.Location = new System.Drawing.Point(0, 364);
            this.btnStockLocation.Name = "btnStockLocation";
            this.btnStockLocation.Size = new System.Drawing.Size(301, 37);
            this.btnStockLocation.TabIndex = 13;
            this.btnStockLocation.Text = "Stock Location";
            this.btnStockLocation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStockLocation.UseVisualStyleBackColor = true;
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
            this.btnCategory.Location = new System.Drawing.Point(0, 327);
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Size = new System.Drawing.Size(301, 37);
            this.btnCategory.TabIndex = 8;
            this.btnCategory.Text = "Category";
            this.btnCategory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCategory.UseVisualStyleBackColor = true;
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
            this.btnBank.Location = new System.Drawing.Point(0, 290);
            this.btnBank.Name = "btnBank";
            this.btnBank.Size = new System.Drawing.Size(301, 37);
            this.btnBank.TabIndex = 12;
            this.btnBank.Text = "Bank";
            this.btnBank.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBank.UseVisualStyleBackColor = true;
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
            this.btnBrand.Location = new System.Drawing.Point(0, 253);
            this.btnBrand.Name = "btnBrand";
            this.btnBrand.Size = new System.Drawing.Size(301, 37);
            this.btnBrand.TabIndex = 7;
            this.btnBrand.Text = "Unit of Measurement";
            this.btnBrand.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBrand.UseVisualStyleBackColor = true;
            this.btnBrand.Click += new System.EventHandler(this.btnBrand_Click);
            // 
            // btnVendor
            // 
            this.btnVendor.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVendor.FlatAppearance.BorderSize = 0;
            this.btnVendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVendor.ForeColor = System.Drawing.SystemColors.Control;
            this.btnVendor.Image = global::SosesPOS.Properties.Resources.vendor;
            this.btnVendor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVendor.Location = new System.Drawing.Point(0, 216);
            this.btnVendor.Name = "btnVendor";
            this.btnVendor.Size = new System.Drawing.Size(301, 37);
            this.btnVendor.TabIndex = 10;
            this.btnVendor.Text = "Vendor";
            this.btnVendor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVendor.UseVisualStyleBackColor = true;
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
            this.btnCustomer.Location = new System.Drawing.Point(0, 179);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(301, 37);
            this.btnCustomer.TabIndex = 4;
            this.btnCustomer.Text = "Customer";
            this.btnCustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCustomer.UseVisualStyleBackColor = true;
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
            this.btnProduct.Location = new System.Drawing.Point(0, 142);
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Size = new System.Drawing.Size(301, 37);
            this.btnProduct.TabIndex = 5;
            this.btnProduct.Text = "Product";
            this.btnProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProduct.UseVisualStyleBackColor = true;
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
            this.btnBillingInvoice.Location = new System.Drawing.Point(0, 105);
            this.btnBillingInvoice.Name = "btnBillingInvoice";
            this.btnBillingInvoice.Size = new System.Drawing.Size(301, 37);
            this.btnBillingInvoice.TabIndex = 14;
            this.btnBillingInvoice.Text = "Delivery Invoice";
            this.btnBillingInvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBillingInvoice.UseVisualStyleBackColor = true;
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
            this.button5.Location = new System.Drawing.Point(0, 68);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(301, 37);
            this.button5.TabIndex = 3;
            this.button5.Text = "Store Invoice";
            this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // lblRole
            // 
            this.lblRole.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.ForeColor = System.Drawing.Color.White;
            this.lblRole.Location = new System.Drawing.Point(0, 34);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(301, 34);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "Administrator";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsername
            // 
            this.lblUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.lblUsername.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(158)))), ((int)(((byte)(132)))));
            this.lblUsername.Location = new System.Drawing.Point(0, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(301, 34);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Username";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.panel2.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnStockIn;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnReceivePayments;
        private System.Windows.Forms.Button btnBank;
        private System.Windows.Forms.Button btnStockLocation;
        private System.Windows.Forms.Button btnBillingInvoice;
    }
}

