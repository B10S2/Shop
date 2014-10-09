using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ShopWarehouse
{
    public partial class BarcodeIn : Form
    {
        public string Barcode { set; get; }
        public BarcodeIn()
        {
            InitializeComponent();
            this.Focus();
        }

        private void btBarcodeKey_Click(object sender, EventArgs e)
        {
            tbBarcode.ReadOnly = false;
        }

        private void tbBarcode_TextChanged(object sender, EventArgs e)
        {
            
            if (tbBarcode.Text.EndsWith("\n"))
            {
                Barcode = tbBarcode.Text;
                Barcode = Barcode.Trim('\r');
                Barcode = Barcode.Trim('\n');
                Barcode = Barcode.Trim('\r');
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }


        private void tbBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            Barcode += e.KeyChar;
            if (e.KeyChar == '\r')
            {
                tbBarcode.Text = Barcode + '\n';
            }
        }
    }
}
