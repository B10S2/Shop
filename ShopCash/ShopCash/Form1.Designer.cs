namespace ShopCash
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tbBalanceDayStart = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tbBalanceReal = new System.Windows.Forms.ToolStripTextBox();
            this.dgv3 = new System.Windows.Forms.DataGridView();
            this.menuMoney = new System.Windows.Forms.MenuStrip();
            this.mMoneyInput = new System.Windows.Forms.ToolStripMenuItem();
            this.mMoneyOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgv4 = new System.Windows.Forms.DataGridView();
            this.menuStrip3 = new System.Windows.Forms.MenuStrip();
            this.mTopMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mTopSale = new System.Windows.Forms.ToolStripMenuItem();
            this.mTopReturn = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv3)).BeginInit();
            this.menuMoney.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv4)).BeginInit();
            this.menuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(655, 425);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.toolStrip1);
            this.tabPage3.Controls.Add(this.dgv3);
            this.tabPage3.Controls.Add(this.menuMoney);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(647, 399);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Операции с деньгами за текущий день";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tbBalanceDayStart,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.tbBalanceReal});
            this.toolStrip1.Location = new System.Drawing.Point(0, 372);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(544, 27);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(107, 24);
            this.toolStripLabel1.Text = "Баланс начало дня:";
            // 
            // tbBalanceDayStart
            // 
            this.tbBalanceDayStart.Name = "tbBalanceDayStart";
            this.tbBalanceDayStart.ReadOnly = true;
            this.tbBalanceDayStart.Size = new System.Drawing.Size(76, 27);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(20, 27);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(133, 24);
            this.toolStripLabel2.Text = "Баланс текущий момент:";
            // 
            // tbBalanceReal
            // 
            this.tbBalanceReal.Name = "tbBalanceReal";
            this.tbBalanceReal.ReadOnly = true;
            this.tbBalanceReal.Size = new System.Drawing.Size(58, 27);
            // 
            // dgv3
            // 
            this.dgv3.AllowUserToDeleteRows = false;
            this.dgv3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv3.Location = new System.Drawing.Point(0, 0);
            this.dgv3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgv3.Name = "dgv3";
            this.dgv3.RowTemplate.Height = 24;
            this.dgv3.Size = new System.Drawing.Size(544, 399);
            this.dgv3.TabIndex = 1;
            this.dgv3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv3_CellClick);
            this.dgv3.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv3_ColumnHeaderMouseClick);
            // 
            // menuMoney
            // 
            this.menuMoney.Dock = System.Windows.Forms.DockStyle.Right;
            this.menuMoney.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMoneyInput,
            this.mMoneyOutput});
            this.menuMoney.Location = new System.Drawing.Point(544, 0);
            this.menuMoney.Name = "menuMoney";
            this.menuMoney.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuMoney.Size = new System.Drawing.Size(103, 399);
            this.menuMoney.TabIndex = 2;
            this.menuMoney.Text = "menuStrip4";
            // 
            // mMoneyInput
            // 
            this.mMoneyInput.Name = "mMoneyInput";
            this.mMoneyInput.Size = new System.Drawing.Size(94, 17);
            this.mMoneyInput.Text = "Внесение денег";
            this.mMoneyInput.Click += new System.EventHandler(this.mMoneyInput_Click);
            // 
            // mMoneyOutput
            // 
            this.mMoneyOutput.Name = "mMoneyOutput";
            this.mMoneyOutput.Size = new System.Drawing.Size(94, 17);
            this.mMoneyOutput.Text = "Инкасация";
            this.mMoneyOutput.Click += new System.EventHandler(this.mMoneyOutput_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgv4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(647, 401);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Остатки на складе";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgv4
            // 
            this.dgv4.AllowUserToDeleteRows = false;
            this.dgv4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv4.Location = new System.Drawing.Point(0, 0);
            this.dgv4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgv4.Name = "dgv4";
            this.dgv4.RowTemplate.Height = 24;
            this.dgv4.Size = new System.Drawing.Size(647, 401);
            this.dgv4.TabIndex = 1;
            this.dgv4.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv4_ColumnHeaderMouseClick);
            // 
            // menuStrip3
            // 
            this.menuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTopMenu});
            this.menuStrip3.Location = new System.Drawing.Point(0, 0);
            this.menuStrip3.Name = "menuStrip3";
            this.menuStrip3.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip3.Size = new System.Drawing.Size(655, 24);
            this.menuStrip3.TabIndex = 1;
            this.menuStrip3.Text = "menuStrip3";
            // 
            // mTopMenu
            // 
            this.mTopMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTopSale,
            this.mTopReturn});
            this.mTopMenu.Name = "mTopMenu";
            this.mTopMenu.Size = new System.Drawing.Size(48, 20);
            this.mTopMenu.Text = "Меню";
            // 
            // mTopSale
            // 
            this.mTopSale.Name = "mTopSale";
            this.mTopSale.Size = new System.Drawing.Size(120, 22);
            this.mTopSale.Text = "Продажа";
            this.mTopSale.Click += new System.EventHandler(this.mTopSale_Click);
            // 
            // mTopReturn
            // 
            this.mTopReturn.Name = "mTopReturn";
            this.mTopReturn.Size = new System.Drawing.Size(120, 22);
            this.mTopReturn.Text = "Возврат";
            this.mTopReturn.Click += new System.EventHandler(this.mTopReturn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 449);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip3);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Модуль кассира";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv3)).EndInit();
            this.menuMoney.ResumeLayout(false);
            this.menuMoney.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv4)).EndInit();
            this.menuStrip3.ResumeLayout(false);
            this.menuStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgv3;
        private System.Windows.Forms.DataGridView dgv4;
        private System.Windows.Forms.MenuStrip menuStrip3;
        private System.Windows.Forms.ToolStripMenuItem mTopMenu;
        private System.Windows.Forms.ToolStripMenuItem mTopSale;
        private System.Windows.Forms.ToolStripMenuItem mTopReturn;
        private System.Windows.Forms.MenuStrip menuMoney;
        private System.Windows.Forms.ToolStripMenuItem mMoneyInput;
        private System.Windows.Forms.ToolStripMenuItem mMoneyOutput;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tbBalanceDayStart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tbBalanceReal;
    }
}

