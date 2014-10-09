namespace ShopCash
{
    partial class DetailsToSale
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
            this.lvDetails = new System.Windows.Forms.ListView();
            this.cName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cBarcode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cPriceCnt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvDetails
            // 
            this.lvDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cName,
            this.cBarcode,
            this.cPrice,
            this.cCount,
            this.cPriceCnt});
            this.lvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvDetails.GridLines = true;
            this.lvDetails.Location = new System.Drawing.Point(0, 0);
            this.lvDetails.Margin = new System.Windows.Forms.Padding(2);
            this.lvDetails.MultiSelect = false;
            this.lvDetails.Name = "lvDetails";
            this.lvDetails.Size = new System.Drawing.Size(575, 106);
            this.lvDetails.TabIndex = 0;
            this.lvDetails.UseCompatibleStateImageBehavior = false;
            this.lvDetails.View = System.Windows.Forms.View.Details;
            // 
            // cName
            // 
            this.cName.Text = "Наименование товара";
            this.cName.Width = 164;
            // 
            // cBarcode
            // 
            this.cBarcode.Text = "Штрих-код";
            this.cBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cBarcode.Width = 90;
            // 
            // cPrice
            // 
            this.cPrice.Text = "Цена за еденицу";
            this.cPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cPrice.Width = 130;
            // 
            // cCount
            // 
            this.cCount.Text = "Количество";
            this.cCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cCount.Width = 95;
            // 
            // cPriceCnt
            // 
            this.cPriceCnt.Text = "Общая цена";
            this.cPriceCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cPriceCnt.Width = 91;
            // 
            // DetailsToSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 106);
            this.Controls.Add(this.lvDetails);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailsToSale";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Детально о продаже";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvDetails;
        private System.Windows.Forms.ColumnHeader cName;
        private System.Windows.Forms.ColumnHeader cPrice;
        private System.Windows.Forms.ColumnHeader cCount;
        private System.Windows.Forms.ColumnHeader cBarcode;
        private System.Windows.Forms.ColumnHeader cPriceCnt;
    }
}