namespace SosesPOS
{
    partial class formBillingPOS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formBillingPOS));
            this.SuspendLayout();
            // 
            // txtTransNo
            // 
            this.txtTransNo.Text = "";
            // 
            // cboUOM
            // 
            this.cboUOM.Size = new System.Drawing.Size(64, 25);
            // 
            // cboSearch
            // 
            this.cboSearch.Size = new System.Drawing.Size(352, 25);
            // 
            // btnPrint
            // 
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.Location = new System.Drawing.Point(0, 148);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            // 
            // btnSaveAndPrint
            // 
            this.btnSaveAndPrint.FlatAppearance.BorderSize = 0;
            // 
            // cboLocation
            // 
            this.cboLocation.Size = new System.Drawing.Size(102, 25);
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.FlatAppearance.BorderSize = 0;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // formBillingPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formBillingPOS";
            this.Text = "Billing Invoice";
            this.ResumeLayout(false);

        }

        #endregion
    }
}