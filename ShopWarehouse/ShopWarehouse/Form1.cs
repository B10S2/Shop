using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.ServiceModel;
using ShopWarehouse.ServiceShop;

namespace ShopWarehouse
{
    public partial class Form1 : Form
    {
        ShopClient Shop = null;
        bool flagSort;
        public Form1()
        {
            InitializeComponent();
            tabConstr();
            try
            {
                Shop = new ShopClient();
                flagSort = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
        }

        //Создать новую категорию товара
        private void addCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new AddCategory(this.Shop);
            if (f.ShowDialog() == DialogResult.OK)
            {
                tabControl1_SelectedIndexChanged(tabControl1, EventArgs.Empty);
            }
        }

        //Создать новую позицию товара
        private void addNewPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new AddGood(this.Shop);
            if (f.ShowDialog() == DialogResult.OK)
            {
                tabControl1_SelectedIndexChanged(tabControl1, EventArgs.Empty);
            }
        }

        //Внести на склад  позицию товара
        private void receptionGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new ReceiptGood(this.Shop);
            if (f.ShowDialog() == DialogResult.OK)
            {
                tabControl1_SelectedIndexChanged(tabControl1, EventArgs.Empty);
            }
        }

        //Конструктор DataGridView
        private void tabConstr()
        {
            DataGridView dgvTmp = new DataGridView();
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                dgvTmp = tabControl1.TabPages[i].Controls[0] as DataGridView;
                switch (i)
                {
                    case 0:
                        {//Товары на складе
                            DataGridViewCell celInt = new DataGridViewTextBoxCell();
                            DataGridViewColumn dc = new DataGridViewColumn(celInt);
                            dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dc.MinimumWidth = 10;
                            DataGridViewColumn dc1 = new DataGridViewColumn(celInt);
                            dc1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                            DataGridViewColumn dc2 = new DataGridViewColumn(celInt);
                            dc1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                            DataGridViewColumn dc3 = new DataGridViewColumn(celInt);
                            dc3.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                            DataGridViewColumn dc4 = new DataGridViewColumn(celInt);
                            dc4.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                            dgvTmp.Columns.Add(dc);
                            dgvTmp.Columns.Add(dc1);
                            dgvTmp.Columns.Add(dc2);
                            dgvTmp.Columns.Add(dc3);
                            dgvTmp.Columns.Add(dc4);
                            dgvTmp.Columns[0].ValueType = typeof(string);
                            dgvTmp.Columns[0].HeaderText = "Штрих код";
                            dgvTmp.Columns[0].ReadOnly = true;
                            dgvTmp.Columns[1].ValueType = typeof(string);
                            dgvTmp.Columns[1].HeaderText = "Название";
                            dgvTmp.Columns[1].ReadOnly = true;
                            dgvTmp.Columns[2].ValueType = typeof(string);
                            dgvTmp.Columns[2].HeaderText = "Категория";
                            dgvTmp.Columns[2].ReadOnly = true;
                            dgvTmp.Columns[3].ValueType = typeof(string);
                            dgvTmp.Columns[3].HeaderText = "Количество";
                            dgvTmp.Columns[3].ReadOnly = true;
                            dgvTmp.Columns[4].ValueType = typeof(string);
                            dgvTmp.Columns[4].HeaderText = "Цена за единицу (Продажа)";
                            dgvTmp.Columns[4].ReadOnly = true;
                            dgvTmp.AllowUserToAddRows = false;
                            dgvTmp.RowCount = 1;
                            //this.Controls.Add(dgvTmp);
                            //this.Show();
                        }
                        break;
                    case 1:
                        {//Товары внесенные на склад сегодня
                            DataGridViewCell celInt = new DataGridViewTextBoxCell();
                            DataGridViewColumn dc = new DataGridViewColumn(celInt);
                            dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dc.MinimumWidth = 10;
                            DataGridViewColumn dc1 = new DataGridViewColumn(celInt);
                            dc1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                            DataGridViewColumn dc2 = new DataGridViewColumn(celInt);
                            dc1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                            DataGridViewColumn dc3 = new DataGridViewColumn(celInt);
                            dc3.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                            DataGridViewColumn dc4 = new DataGridViewColumn(celInt);
                            dc4.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                            dgvTmp.Columns.Add(dc);
                            dgvTmp.Columns.Add(dc1);
                            dgvTmp.Columns.Add(dc2);
                            dgvTmp.Columns.Add(dc3);
                            dgvTmp.Columns.Add(dc4);
                            dgvTmp.Columns[0].ValueType = typeof(string);
                            dgvTmp.Columns[0].HeaderText = "Штрих код";
                            dgvTmp.Columns[0].ReadOnly = true;
                            dgvTmp.Columns[1].ValueType = typeof(string);
                            dgvTmp.Columns[1].HeaderText = "Название";
                            dgvTmp.Columns[1].ReadOnly = true;
                            dgvTmp.Columns[2].ValueType = typeof(string);
                            dgvTmp.Columns[2].HeaderText = "Категория";
                            dgvTmp.Columns[2].ReadOnly = true;
                            dgvTmp.Columns[3].ValueType = typeof(string);
                            dgvTmp.Columns[3].HeaderText = "Количество";
                            dgvTmp.Columns[3].ReadOnly = true;
                            dgvTmp.Columns[4].ValueType = typeof(string);
                            dgvTmp.Columns[4].HeaderText = "Цена за единицу (Прием на склад)";
                            dgvTmp.Columns[4].ReadOnly = true;
                            dgvTmp.AllowUserToAddRows = false;
                            dgvTmp.RowCount = 1;
                            //this.Controls.Add(dgvTmp);
                            //this.Show();
                        }
                        break;
                    case 2:
                        {//Все товары
                            DataGridViewCell celInt = new DataGridViewTextBoxCell();
                            DataGridViewColumn dc = new DataGridViewColumn(celInt);
                            dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dc.MinimumWidth = 10;
                            DataGridViewColumn dc1 = new DataGridViewColumn(celInt);
                            dc1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                            dgvTmp.Columns.Add(dc);
                            dgvTmp.Columns.Add(dc1);

                            dgvTmp.Columns[0].ValueType = typeof(string);
                            dgvTmp.Columns[0].HeaderText = "Штрих код";
                            dgvTmp.Columns[0].ReadOnly = true;
                            dgvTmp.Columns[1].ValueType = typeof(string);
                            dgvTmp.Columns[1].HeaderText = "Название";
                            dgvTmp.Columns[1].ReadOnly = true;
                            dgvTmp.AllowUserToAddRows = false;
                        }
                        break;
                    case 3:
                        {//Все категори
                            DataGridViewCell celInt = new DataGridViewTextBoxCell();
                            DataGridViewColumn dc = new DataGridViewColumn(celInt);
                            dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dc.MinimumWidth = 10;

                            dgvTmp.Columns.Add(dc);

                            dgvTmp.Columns[0].ValueType = typeof(string);
                            dgvTmp.Columns[0].HeaderText = "Категории";
                            dgvTmp.Columns[0].ReadOnly = true;
                            dgvTmp.AllowUserToAddRows = false;
                        }
                        break;
                    default:
                        MessageBox.Show("Default case");
                        break;
                }
            }
        }
        
        //Перед стартом выбрасываем форму для Авторизации
        private void Form1_Load(object sender, EventArgs e)
        {
            Authorization auth = new Authorization(this.Shop);
            if (auth.ShowDialog() == DialogResult.OK)
            {
                tabControl1_SelectedIndexChanged(tabControl1, EventArgs.Empty);
                this.Text = "Добро пожаловать " + Authorization.user.FirstName + " " + Authorization.user.LastName;
            }
            else
            {
                try { Shop.Close(); }
                catch (Exception ex) { }
                this.Close();
            }
        }

        //Обновление информации в TabPages
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl control = (sender as TabControl);
            if (control.SelectedTab.Text == "Все категории")
            {
                string[] Cat = Shop.GetCategoryes();
                dataGridView3.Rows.Clear();
                foreach (string s in Cat)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    DataGridViewCell cel = new DataGridViewTextBoxCell();
                    cel.Value = s;
                    row.Cells.Add(cel);
                    dataGridView3.Rows.Add(row);
                }
            }
            else if (control.SelectedTab.Text == "Все товары")
            {
                Good[] goods = Shop.GetAllGoods();
                dgvAllGoods.Rows.Clear();
                foreach (Good g in goods)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    DataGridViewCell cel = new DataGridViewTextBoxCell();
                    cel.Value = g.Barcode;
                    row.Cells.Add(cel);

                    cel = new DataGridViewTextBoxCell();
                    cel.Value = g.Name;
                    row.Cells.Add(cel);

                    dgvAllGoods.Rows.Add(row);
                }
            }
            else if (control.SelectedTab.Text == "Товаров на складе")
            {
                Good[] goods = Shop.GetGoodsBalance();
                dgvBalance.Rows.Clear();
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
                    cel.Value = g.Category;
                    row.Cells.Add(cel);


                    cel = new DataGridViewTextBoxCell();
                    cel.Value = g.Count;
                    row.Cells.Add(cel);


                    cel = new DataGridViewTextBoxCell();
                    cel.Value = g.PriceOut;
                    row.Cells.Add(cel);

                    dgvBalance.Rows.Add(row);
                }
            }
            else if (control.SelectedTab.Text == "Добавленные сегодня")
            {
                Good[] goods = Shop.GetGoodsToDay();
                dgvChangeInserted.Rows.Clear();
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
                    cel.Value = g.Category;
                    row.Cells.Add(cel);


                    cel = new DataGridViewTextBoxCell();
                    cel.Value = g.Count;
                    row.Cells.Add(cel);


                    cel = new DataGridViewTextBoxCell();
                    cel.Value = g.PriceIn;
                    row.Cells.Add(cel);
                    row.Tag = g.idWare;

                    dgvChangeInserted.Rows.Add(row);
                }
            }
        }

        private void dgvChangeInserted_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную операцию?","Удаление операции",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                DataGridViewRow row = dgvChangeInserted.Rows[e.RowIndex];
                int id = (int)row.Tag;
                Shop.DeleteBalance(id);
                tabControl1_SelectedIndexChanged(tabControl1, EventArgs.Empty);
            }
        }
        //сортировка баланса
        private void dgvBalance_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (flagSort)
            {
                dgvBalance.Sort(dgvBalance.Columns[e.ColumnIndex], ListSortDirection.Ascending);
                flagSort = false;
            }
            else
            {
                dgvBalance.Sort(dgvBalance.Columns[e.ColumnIndex], ListSortDirection.Descending);
                flagSort = true;
            }
        }


        //сортировка Внесенные сегодня
        private void dgvChangeInserted_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (flagSort)
            {
                dgvChangeInserted.Sort(dgvChangeInserted.Columns[e.ColumnIndex], ListSortDirection.Ascending);
                flagSort = false;
            }
            else
            {
                dgvChangeInserted.Sort(dgvChangeInserted.Columns[e.ColumnIndex], ListSortDirection.Descending);
                flagSort = true;
            }
        }
        //сортировка все товары
        private void dgvAllGoods_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (flagSort)
            {
                dgvAllGoods.Sort(dgvAllGoods.Columns[e.ColumnIndex], ListSortDirection.Ascending);
                flagSort = false;
            }
            else
            {
                dgvAllGoods.Sort(dgvAllGoods.Columns[e.ColumnIndex], ListSortDirection.Descending);
                flagSort = true;
            }
        }

        //сортировка все категории
        private void dataGridView3_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (flagSort)
            {
                dataGridView3.Sort(dataGridView3.Columns[e.ColumnIndex], ListSortDirection.Ascending);
                flagSort = false;
            }
            else
            {
                dataGridView3.Sort(dataGridView3.Columns[e.ColumnIndex], ListSortDirection.Descending);
                flagSort = true;
            }

        }

        //Закрываем соиденние с службой перед выходом
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { Shop.Close(); }
            catch(Exception ex) { }
        }
    }
}