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
    public partial class BarcodeEnter : Form
    {
        static private Random r;
        List<Good> masGoods;
        Good[] goodsCount;
        string Barcode;
        int param;
        ShopClient shop;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_shop"></param>
        /// <param name="param">1 означает что это возврат товара</param>
        public BarcodeEnter(ShopClient _shop, int param = 0)
        {
            InitializeComponent();
            masGoods = new List<Good>();
            textBox1.Select();
            this.shop = _shop;
            this.param = param;
            r = new Random();

            sale();

            if (param == 0)
            {
                this.Text = "Продажа товара";
                goodsCount = shop.GetGoodsBalance();
            }
            else
            {
                this.Text = "Возврат товара";    
            }
        }

        private void sale()
        {            
            int countColumn = 7;
            DataGridViewCell dgvCell = new DataGridViewTextBoxCell();
            DataGridViewColumn[] dgvColumns = new DataGridViewColumn[countColumn];
            for (int i = 0; i < countColumn - 1; i++)
            {
                dgvColumns[i] = new DataGridViewColumn(dgvCell);
                dgvColumns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvColumns[i].MinimumWidth = 20;
                dgvColumns[i].ValueType = typeof(string);
                dgvColumns[i].ReadOnly = true;
            }
            dgvColumns[3].ReadOnly = false;
            dgvColumns[countColumn - 1] = new DataGridViewCheckBoxColumn(false);
            dgvColumns[countColumn - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvColumns[countColumn - 1].CellTemplate = new DataGridViewCheckBoxCell();
            dgvColumns[countColumn - 1].ValueType = typeof(bool);

            DataGridViewColumn id = new DataGridViewColumn(dgvCell);
            id.ValueType = typeof(int);
            DataGridViewColumn MaxCount = new DataGridViewColumn(dgvCell);
            MaxCount.ValueType = typeof(int);


            foreach (DataGridViewColumn c in dgvColumns)
            {
                dgv1.Columns.Add(c);
            }
            dgv1.Columns.Add(id);
            dgv1.Columns[0].HeaderText = "Штрих код";
            dgv1.Columns[1].HeaderText = "Название";
            dgv1.Columns[2].HeaderText = "Цена за единицу";
            dgv1.Columns[3].HeaderText = "Количество";
            dgv1.Columns[4].HeaderText = "Сумма за позицию";
            dgv1.Columns[5].HeaderText = "Отмена продажы";
            dgv1.Columns[6].HeaderText = "id";
            dgv1.Columns[6].Visible = false;
            dgv1.Columns[7].HeaderText = "maxCount";
            dgv1.Columns[7].Visible = false;

            dgv1.CellValueChanged += dgv1_CellValueChanged;           
            dgv1.AllowUserToAddRows = false;
            dgv1.AllowUserToDeleteRows = false;
            dgv1.RowCount = 0;
            //shop.GetGoodsBalance();
        }

        private bool insertTo_SaleDGV1(string bar)
        {
            Good g = new Good();
            int maxCount = 0;
            dgv1.Rows.Add();
            int i = dgv1.Rows.Count - 1;
            
            if (param == 0)
            {
                g = shop.GetGood(bar);
                maxCount = gCount(bar);
                if (maxCount <= 0)
                {
                    MessageBox.Show("Товар закончился на складе");
                    return false;
                }
            }
            else if (param == 1)
            {
                //Вызов из архива g =shop.GetFromArchive 
            }
            if (g.Err == 1)
            {
                MessageBox.Show("Не внесен в базу данных");
                return false;
            }
            dgv1.Rows[i].Cells[0].Value = g.Barcode;
            dgv1.Rows[i].Cells[1].Value = g.Name;
            dgv1.Rows[i].Cells[2].Value = g.PriceOut;
            dgv1.Rows[i].Cells[3].Value = 1;
            dgv1.Rows[i].Cells[4].Value = g.PriceOut;
            dgv1.Rows[i].Cells[5].Value = false;
            dgv1.Rows[i].Cells[6].Value = g.ID;
            dgv1.Rows[i].Cells[7].Value = maxCount;
            return true;            
        }
        /// <summary>
        /// получает Количество товара на складе по баркоду
        /// </summary>
        /// <param name="bar"></param>
        /// <returns></returns>
        private int gCount(string bar)
        {
            int count = 0;
            foreach (Good g in goodsCount)
            {
                if (g.Barcode == bar)
                {
                    count = g.Count;
                    return count;
                }
            }
            return count;
        }

        public List<Good> MasGoods
        {
            get { return masGoods; }
        }
        /// <summary>
        /// Отправка масива товаров на сервер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Good g = new Good();
            int id;
            int count;
            int price;
            if (dgv1.Rows.Count > 0)
            {
                foreach (DataGridViewRow r in dgv1.Rows)
                {
                    g.Barcode = r.Cells[0].Value.ToString();
                    int.TryParse(r.Cells[6].Value.ToString(), out id);
                    g.ID = id;
                    int.TryParse(r.Cells[3].Value.ToString(), out count);
                    g.Count = count;
                    g.Name = r.Cells[1].Value.ToString();
                    int.TryParse(r.Cells[2].Value.ToString(), out price);
                    g.PriceOut = price;
                    masGoods.Add(g);
                }
                if (param == 0)
                {
                    shop.SellGoods(masGoods.ToArray(), Authorization.user.ID);
                }
                else if (param == 1)
                {
                    //Способ отправить массив возврата товара
                }
            }  
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        void dgv1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv1.IsCurrentCellDirty)
            {
                dgv1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bar">баркод</param>
        /// <returns>Если true то уже был введен такой штрих код в таблицу </returns>
        private bool checkDgvForBarcodeAndAvailability(string bar)
        {               
            string str;
            int count ;
            int maxCount;
            foreach (DataGridViewRow r in dgv1.Rows)
            {
                str = r.Cells[0].Value.ToString();
                if (str == bar)
                {
                    int.TryParse(r.Cells[3].Value.ToString(), out count);
                    count++;
                    int.TryParse(r.Cells[7].Value.ToString(), out maxCount);
                    if (count > maxCount)
                    {
                        count--;
                        MessageBox.Show("Больше нет на складе");
                    }
                    r.Cells[3].Value = count;                 
                    return true;
                }     
            }
            return false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
                Good tmpGood = new Good();    
                Barcode += e.KeyChar;
                if (e.KeyChar == '\r'||e.KeyChar == '\n')
                {
                    char[] trim = { '\r', '\n', '\r', '\n' };
                    Barcode = Barcode.Trim(trim);
                    tmpGood = shop.GetGood(Barcode);
                    
                    if (tmpGood.Err == 0)
                    {
                        if (tmpGood.Count == 0)
                        {
                            MessageBox.Show("Такого товара нет на складе!", "Товар не найден", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            Barcode = "";
                            return;
                        }

                        if (checkDgvForBarcodeAndAvailability(Barcode))
                        {
                            DataGridViewCellEventArgs f = new DataGridViewCellEventArgs(4, -1);
                            dgv1_CellValueChanged(this, f);
                        }
                        else
                        {
                            if (insertTo_SaleDGV1(Barcode))
                            {
                                DataGridViewCellEventArgs f = new DataGridViewCellEventArgs(4, -1);
                                dgv1_CellValueChanged(this, f);
                            }
                        }
                    }
                    else
                        MessageBox.Show("Такого товара нет в Базе данных","Товар не найден",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    
                    Barcode = string.Empty;
                    textBox1.Text = "";
                }          

        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgv1.FirstDisplayedScrollingRowIndex = dgv1.Rows.Count - 1;
        }

        private void dgv1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            double summ = 0;
            double price = 0;
            uint count;
            int maxCount;
 
            try
            {
                if (e.ColumnIndex == 5 && dgv1[e.ColumnIndex,e.RowIndex].Value != null)
                {   
                    rowDelete(e.RowIndex);                    
                    return;
                }
                if (e.ColumnIndex == 3 && dgv1[e.ColumnIndex, e.RowIndex].Value != null && dgv1[4, e.RowIndex].Value != null)
                {
                    
                    if (uint.TryParse(dgv1[3, e.RowIndex].Value.ToString(), out count) && double.TryParse(dgv1[2, e.RowIndex].Value.ToString(), out price))
                    {
                        if (count < 1)
                        {                            
                            count = 1;
                        }
                        int.TryParse(dgv1[7, e.RowIndex].Value.ToString(), out maxCount);
                        if (maxCount < count)
                        {
                            count =(uint) maxCount;
                            MessageBox.Show("Не хватает товара на складе");
                        }
                        dgv1[3, e.RowIndex].Value = count;
                        dgv1[4, e.RowIndex].Value = count * price;                        
                    }
                    else
                    {
                        count = 1;
                        if (!double.TryParse(dgv1[2, e.RowIndex].Value.ToString(), out price))
                        { price = 1; }
                        dgv1[4, e.RowIndex].Value = count * price;
                        dgv1[3, e.RowIndex].Value = count;
                    }
                }
                if (e.ColumnIndex == 4 )
                {
                    foreach (DataGridViewRow r in dgv1.Rows)
                    {
                        if (r.Cells[4].Value != null && double.TryParse(r.Cells[4].Value.ToString(), out price) )
                        {
                            summ += price;
                        }
                    }
                    lbSum.Text = summ.ToString();    
                }      
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        /// <summary>
        /// получаем индексы не отмененных рядов для таблицы продаж
        /// </summary>
        /// <returns></returns>
        void rowDelete(int rowIndex)
        {
            bool flag = false;
                if (dgv1[5, rowIndex].Value != null && (bool.TryParse(dgv1[5, rowIndex].Value.ToString(), out flag)))               
                {
                    if (flag)
                    {
                        dgv1.Rows.RemoveAt(rowIndex);                         
                    }
                }           
        }

        // пересщет суммы за товары
        private void dgv1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            DataGridViewCellEventArgs f = new DataGridViewCellEventArgs(4, -1);
            dgv1_CellValueChanged(this, f);
        }
    }
}