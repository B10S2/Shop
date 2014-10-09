using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ShopCash.ServiceShop;

namespace ShopCash
{
    public partial class Form1 : Form
    {
        ShopClient shop;
        bool flagSort;
        public Form1()
        {
            InitializeComponent();
            shop = new ShopClient();

            //Строим таблицы
            cashWorkToday();
            goodsBalance();
        }
      
        //Работа с наличными сегодня и баланс в кассе (Конструктор таблицы)
        private void cashWorkToday()
        {
            int count = 4;
            //Операции с деньгами за текущий день
            DataGridViewCell dgvCell3 = new DataGridViewTextBoxCell();
            DataGridViewColumn[] dgvColumns = new DataGridViewColumn[count];

            for (int i = 0; i < count; i++)
            {
                dgvColumns[i] = new DataGridViewColumn(dgvCell3);
                dgvColumns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvColumns[i].MinimumWidth = 20;
                dgvColumns[i].ValueType = typeof(string);
                dgvColumns[i].ReadOnly = true;
            }
            foreach (DataGridViewColumn d in dgvColumns)
            {
                dgv3.Columns.Add(d);
            }
            DataGridViewCell btCell = new DataGridViewButtonCell();
            btCell.Value = "Подробности";
            DataGridViewColumn btColumn = new DataGridViewColumn();
            btColumn = new DataGridViewColumn(btCell);
            btColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            btColumn.MinimumWidth = 20;
            btColumn.ValueType = typeof(string);
            dgv3.Columns.Add(btColumn);
            
      

            dgv3.Columns[0].HeaderText = "Сумма операции";
            dgv3.Columns[1].HeaderText = "Время";
            dgv3.Columns[2].HeaderText = "тип операции";
            dgv3.Columns[3].HeaderText = "баланс после операции";
            dgv3.Columns[4].HeaderText = "подробности";
            dgv3.AllowUserToAddRows = false;
            dgv3.Columns[4].ReadOnly = false;
            dgv3.RowCount = 0;

            //tbBalanceDayStart.Text = shop.
            tbBalanceDayStart.Text = "0";
        }

        //Остаток товаров на складе (Конструктор)
        private void goodsBalance()
        {
            int count = 5;            
            DataGridViewCell dgvCell4 = new DataGridViewTextBoxCell();
            DataGridViewColumn[] dgvColumns = new DataGridViewColumn[count];

            for (int i = 0; i < count; i++)
            {
                dgvColumns[i] = new DataGridViewColumn(dgvCell4);
                dgvColumns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvColumns[i].MinimumWidth = 20;
                dgvColumns[i].ValueType = typeof(string);
                dgvColumns[i].ReadOnly = true;
            }

            foreach (DataGridViewColumn d in dgvColumns)
            {
                dgv4.Columns.Add(d);
            }
            dgv4.Columns[0].HeaderText = "Штрих код";
            dgv4.Columns[1].HeaderText = "Название";
            dgv4.Columns[2].HeaderText = "Цена за единицу";
            dgv4.Columns[3].HeaderText = "Количество";
            dgv4.Columns[4].HeaderText = "Категория";
            dgv4.AllowUserToAddRows = false;
            dgv4.RowCount = 0;
         }

        //Продажа товаров
        private void mTopSale_Click(object sender, EventArgs e)
        {
            BarcodeEnter barEnter = new BarcodeEnter(this.shop);

            if (barEnter.ShowDialog() == DialogResult.OK)
            {
                tabControl1_SelectedIndexChanged(tabControl1, EventArgs.Empty);
                mTopSale_Click(this, EventArgs.Empty);
            }            
        }
        
        //Получение товара из АрхиваБД(нужна ф-я по работе с архивом) и заполнение таблицы возврата
        private void mTopReturn_Click(object sender, EventArgs e)
        {           
            BarcodeEnter barEnter = new BarcodeEnter(this.shop, 1);
            barEnter.ShowDialog();
            if (barEnter.DialogResult == DialogResult.OK)
            {      
            }            
        }

        //Внесение средств
        private void mMoneyInput_Click(object sender, EventArgs e)
        {
            MoneyInputOutput mi = new MoneyInputOutput(this.shop);
            if (mi.ShowDialog() == DialogResult.OK)
            {
                tabControl1_SelectedIndexChanged(tabControl1, EventArgs.Empty);
            }
        }

        //Инкасация
        private void mMoneyOutput_Click(object sender, EventArgs e)
        {
            MoneyInputOutput mi = new MoneyInputOutput(this.shop, true);
            if (mi.ShowDialog() == DialogResult.OK)
            {
                tabControl1_SelectedIndexChanged(tabControl1, EventArgs.Empty);
            }
        }

        //Перед загрузкой программы выбрасываем форму авторизации
        private void Form1_Load(object sender, EventArgs e)
        {
            Authorization auth = new Authorization(this.shop);
            flagSort = false;

            if (auth.ShowDialog() == DialogResult.OK)
            {
                tabControl1_SelectedIndexChanged(tabControl1, EventArgs.Empty);
                this.Text = "Добро пожаловать " + Authorization.user.FirstName + " " + Authorization.user.LastName;
            }
            else
            {
                try { shop.Close(); }
                catch (Exception ex) { }
                this.Close();
            }
        }

        //Обновление инф в таб контроле
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl control = (sender as TabControl);

            try
            {
                if (control.SelectedTab.Text == "Остатки на складе")
                {
                    #region
                    Good[] goods = shop.GetGoodsBalance();
                    dgv4.Rows.Clear();
                    foreach (Good g in goods)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewCell cel = new DataGridViewTextBoxCell();
                        cel.Value = g.Barcode;
                        row.Cells.Add(cel);

                        cel = new DataGridViewTextBoxCell();
                        cel.Value = g.Name;
                        row.Cells.Add(cel);

                        cel = new DataGridViewTextBoxCell();
                        cel.Value = g.PriceOut;
                        row.Cells.Add(cel);

                        cel = new DataGridViewTextBoxCell();
                        cel.Value = g.Count;
                        row.Cells.Add(cel);

                        cel = new DataGridViewTextBoxCell();
                        cel.Value = g.Category;
                        row.Cells.Add(cel);

                        dgv4.Rows.Add(row);
                    }
                    #endregion
                }
                if (control.SelectedTab.Text == "Операции с деньгами за текущий день")
                {
                    #region
                    dgv3.Rows.Clear();
                    CashOperation[] saleToday = shop.GetToDayOperations();
                    foreach (CashOperation CO in saleToday)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewCell cel = new DataGridViewTextBoxCell();
                        DataGridViewButtonCell Bcel = new DataGridViewButtonCell();
                        Color color = new Color();
                        if (CO.TypeOperation.Equals("Инкасация", StringComparison.InvariantCultureIgnoreCase)) 
                            color = Color.Red;
                        if (CO.TypeOperation.Equals("Пополнение", StringComparison.InvariantCultureIgnoreCase))
                            color = Color.Green;
                        if (CO.TypeOperation.Equals("Продажа", StringComparison.InvariantCultureIgnoreCase))
                            color = Color.White;


                        cel.Value = CO.PriceOperation;
                        cel.Style.BackColor = color;
                        row.Cells.Add(cel);

                        cel = new DataGridViewTextBoxCell();
                        cel.Value = CO.TimeOpertion;
                        cel.Style.BackColor = color;
                        row.Cells.Add(cel);

                        cel = new DataGridViewTextBoxCell();
                        cel.Value = CO.TypeOperation;
                        cel.Style.BackColor = color;
                        row.Cells.Add(cel);

                        cel = new DataGridViewTextBoxCell();
                        cel.Value = CO.CashBalance;
                        cel.Style.BackColor = color;
                        row.Cells.Add(cel);

                        Bcel.Style.BackColor = color;
                        Bcel.Value = "Подробности";
                        row.Cells.Add(Bcel);

                        row.Tag = CO.ID;
                        dgv3.Rows.Add(row);
                    }
                    tbBalanceDayStart.Text = shop.GetStartBalance().ToString();

                    if (saleToday.Count() != 0)
                    {
                        //Находим максимальное значение id в коллекции saleToday
                        int id = saleToday.Max(x => x.ID);
                        //Инициализируем структуру CashOperation с найденным максимальным id
                        CashOperation c = saleToday.First(x => x.ID == id);
                        //Заносим текущий баланс в текстовое поле
                        tbBalanceReal.Text = c.CashBalance.ToString();
                    }
                    else
                        tbBalanceReal.Text = shop.GetStartBalance().ToString();
                    #endregion
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); MessageBox.Show(ex.StackTrace); }
        }

        //Подробности операции Продажа товаров
        private void dgv3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 4 || e.ColumnIndex == -1 || e.RowIndex == -1) return;
            DataGridViewRow row = dgv3.Rows[e.RowIndex];
            
            if (row.Cells[2].Value.ToString().Equals("Продажа", StringComparison.InvariantCultureIgnoreCase)) 
            {

                DetailsToSale dts = new DetailsToSale(this.shop, (int)row.Tag);
                dts.ShowDialog();
            }
        }
        //cортировка операций за текущий день
        private void dgv3_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (flagSort)
            {
                dgv3.Sort(dgv3.Columns[e.ColumnIndex], ListSortDirection.Ascending);
                flagSort = false;
            }
            else
            {
                dgv3.Sort(dgv3.Columns[e.ColumnIndex], ListSortDirection.Descending);
                flagSort = true;
            }
        }
        
        //cортировка остатков на складе
        private void dgv4_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (flagSort)
            {
                dgv4.Sort(dgv4.Columns[e.ColumnIndex], ListSortDirection.Ascending);
                flagSort = false;
            }
            else
            {
                dgv4.Sort(dgv4.Columns[e.ColumnIndex], ListSortDirection.Descending);
                flagSort = true;
            }
        }

        //Закрываем соиденние с службой перед выходом
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { shop.Close(); }
            catch (Exception ex) { }
        }

    }
}