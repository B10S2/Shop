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
    public partial class AddGood : Form
    {
        ShopClient Shop = null;
        string Barcode = "";
        public AddGood(ShopClient _Shop, string _Barcode = "")
        {
            InitializeComponent();
            this.Shop = _Shop;
            this.Barcode = _Barcode;
            tabConstr();        
        }
        private void tabConstr()
        {
            DataGridViewCell celInt = new DataGridViewTextBoxCell();
            DataGridViewColumn dc = new DataGridViewColumn(celInt);
            dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //Добавление ComboBoxColumn в DataGridView
            DataGridViewComboBoxColumn dc1 = new DataGridViewComboBoxColumn();
            dc1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            
            string[] cat = null;
            try
            {
                cat = Shop.GetCategoryes();
                dc1.DataSource = cat;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DataGridViewColumn dc2 = new DataGridViewColumn(celInt);
            dc2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewColumn dc3 = new DataGridViewColumn(celInt);
            dc3.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgv.Columns.Add(dc);
            dgv.Columns.Add(dc1);
            dgv.Columns.Add(dc2);
            dgv.Columns.Add(dc3);

            dgv.Columns[0].ValueType = typeof(string);
            dgv.Columns[0].HeaderText = "штрих код";
            dgv.Columns[1].ValueType = typeof(string);
            dgv.Columns[1].HeaderText = "категория";
            dgv.Columns[2].ValueType = typeof(string);
            dgv.Columns[2].HeaderText = "название";
            dgv.Columns[3].ValueType = typeof(string);
            dgv.Columns[3].HeaderText = "цена на продажу";
            dgv.Dock = DockStyle.Fill;
            dgv.AllowUserToAddRows = false;
            dgv.RowCount = 1;
            dgv.Rows[0].Cells[0].Value =  this.Barcode;
            this.Controls.Add(dgv);
            dgv.RowsAdded += dgv_RowsAdded;
            //dgv.Rows[0].Cells[0].Value = bar;
        }

        void dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
           // (sender as DataGridView).Rows[(sender as DataGridView).NewRowIndex].Cells[0].Value = (sender as DataGridView).NewRowIndex + 1;
        }

        private void okToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string lname = dgv.Rows[0].Cells[2].Value.ToString();
                string lcat = dgv.Rows[0].Cells[1].Value.ToString();
                string lbar = dgv.Rows[0].Cells[0].Value.ToString();
                double lprice ;
                if (!(double.TryParse(dgv.Rows[0].Cells[3].Value.ToString(), out lprice)))
                {
                    lprice = 0;
                }                
                Shop.AddGood(lname, lcat, lbar, lprice);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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