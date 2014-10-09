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
    public partial class AddCategory : Form
    {
        IShop shop = null;
        public AddCategory(ShopClient _Shop)
        {            
            InitializeComponent();
            this.shop = _Shop;
            this.Text = "Введите категории(ю)";
            this.Width = 400;
            tabConstr();
            
        }
        private void tabConstr()
        {
            DataGridViewCell celInt = new DataGridViewTextBoxCell();
            DataGridViewColumn dc = new DataGridViewColumn(celInt);
            dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dc.MinimumWidth = 10;
            DataGridViewColumn dc1 = new DataGridViewColumn(celInt);
            dc1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dc1.MinimumWidth = 200;
            //    dc.CellTemplate = new DataGridViewCell();
            dgv.Columns.Add(dc);
            dgv.Columns.Add(dc1);
            dgv.Columns[0].ValueType = typeof(int);
            dgv.Columns[0].HeaderText = "№";
            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[1].ValueType = typeof(string);
            dgv.Columns[1].HeaderText = "Категория";
            // dgv.Columns[1].st
            dgv.Rows[0].Cells[0].Value = dgv.NewRowIndex + 1;
            dgv.Dock = DockStyle.Fill;
            this.Controls.Add(dgv);
            dgv.RowsAdded += dgv_RowsAdded;
        }
        //нумератор строк
        void dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            (sender as DataGridView).Rows[(sender as DataGridView).NewRowIndex].Cells[0].Value = (sender as DataGridView).NewRowIndex + 1;
        }

        private void okToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> cat = new List<string>();
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    DataGridViewCell cell = row.Cells[1];
                    if(cell.Value != null)
                        cat.Add(cell.Value.ToString());
                }
                shop.AddCategory(cat.ToArray());
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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