namespace ShopWarehouse
{
    partial class BarcodeIn
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
            this.btBarcodeKey = new System.Windows.Forms.Button();
            this.tbBarcode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btBarcodeKey
            // 
            this.btBarcodeKey.Location = new System.Drawing.Point(187, 12);
            this.btBarcodeKey.Name = "btBarcodeKey";
            this.btBarcodeKey.Size = new System.Drawing.Size(124, 20);
            this.btBarcodeKey.TabIndex = 5;
            this.btBarcodeKey.Text = "Ввести вручную";
            this.btBarcodeKey.UseVisualStyleBackColor = true;
            this.btBarcodeKey.Click += new System.EventHandler(this.btBarcodeKey_Click);
            // 
            // tbBarcode
            // 
            this.tbBarcode.Location = new System.Drawing.Point(12, 12);
            this.tbBarcode.Multiline = true;
            this.tbBarcode.Name = "tbBarcode";
            this.tbBarcode.ReadOnly = true;
            this.tbBarcode.Size = new System.Drawing.Size(157, 20);
            this.tbBarcode.TabIndex = 1;
            this.tbBarcode.TextChanged += new System.EventHandler(this.tbBarcode_TextChanged);
            this.tbBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbBarcode_KeyPress);
            // 
            // BarcodeIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 46);
            this.Controls.Add(this.tbBarcode);
            this.Controls.Add(this.btBarcodeKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BarcodeIn";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Чтение штрих кода";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btBarcodeKey;
        private System.Windows.Forms.TextBox tbBarcode;
    }
}