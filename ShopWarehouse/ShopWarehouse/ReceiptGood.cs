using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ShopWarehouse.ServiceShop;

namespace ShopWarehouse
{
    public partial class ReceiptGood : Form
    {
        ShopClient Shop = null;
        ShopWarehouse.ServiceShop.Good g;
        public ReceiptGood(ShopClient _Shop)
        {           
            InitializeComponent();
            this.Shop = _Shop;            
        }

        private void tabConstr()
        {
            //Шабон ячейки
            DataGridViewCell celInt = new DataGridViewTextBoxCell();
            //Не использую массив колонок в связи с неадекватным AddRange
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

            dgv.Columns.Add(dc);
            dgv.Columns.Add(dc1);
            dgv.Columns.Add(dc2);
            dgv.Columns.Add(dc3);
            dgv.Columns.Add(dc4);

            dgv.Columns[0].ValueType = typeof(string);
            dgv.Columns[0].HeaderText = "Штрих код";

            dgv.Columns[1].ValueType = typeof(string);
            dgv.Columns[1].HeaderText = "Название";

            dgv.Columns[2].ValueType = typeof(string);
            dgv.Columns[2].HeaderText = "Категория";
            dgv.Columns[3].ValueType = typeof(int);
            dgv.Columns[3].HeaderText = "Количество";
            dgv.Columns[4].ValueType = typeof(Double);
            dgv.Columns[4].HeaderText = "Цена за единицу (Прием на склад)";

            dgv.AllowUserToAddRows = false;
            dgv.RowCount = 1;
            
            dgv.CellValidating += dgv_CellValidating;
            try
            {
                    dgv.Rows[0].Cells[0].Value = g.Barcode;
                    dgv.Rows[0].Cells[1].Value = g.Name;
                    dgv.Rows[0].Cells[2].Value = g.Category;
            }
            catch (Exception ex)
            {
            }
        }

        //Проверка правильности ввода данных в ячейки
        void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
            {
            if (e.ColumnIndex == 3)
            {
                int tmp;
                if (Int32.TryParse(e.FormattedValue.ToString(),out tmp))
                {                    
                    return;              
                }
                if (e.FormattedValue.ToString() == "")
                {                
                    return;          
                }
                MessageBox.Show("Не верно введено число");
                e.Cancel = true;
            }
            if (e.ColumnIndex == 4)
            {
                double tmp;
                if (Double.TryParse(e.FormattedValue.ToString(), out tmp))
                {
                    return;
                }
                if (e.FormattedValue.ToString() == "")
                {
                    return;
                }
                MessageBox.Show("Не верно введено число");
                e.Cancel = true;
            }
        }
        
        //OK
        private void okToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                double Price = 0;
                int Count = 0;
                Int32.TryParse(dgv.Rows[0].Cells[3].Value.ToString(), out Count);
                Double.TryParse(dgv.Rows[0].Cells[4].Value.ToString(),out Price);
                
                Shop.AddGoods(g.ID, Count, Price);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
        }

        //Отмена
        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        //Перед загрузкой окна выбрасываем форму для сканирования штрих-кода
        private void ReceiptGood_Load(object sender, EventArgs e)
        {
            BarcodeIn BarIn = new BarcodeIn();
            if (BarIn.ShowDialog() == DialogResult.OK)
            {
                g = Shop.GetGood(BarIn.Barcode);
                if (g.Err == 0)
                    tabConstr();
                else
                    if (MessageBox.Show("Такого товара не существует! Создать его?", "Товар не найден в БД", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        this.Close();
                        AddGood addgood = new AddGood(this.Shop, BarIn.Barcode);
                        addgood.ShowDialog();
                    }
                    else
                        this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv.IsCurrentCellDirty)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }   
        }
    }
}
