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
    public partial class DetailsToSale : Form
    {
        ShopClient Shop;
        public DetailsToSale(ShopClient _Shop, int idCash)
        {
            InitializeComponent();
            this.Shop = _Shop;

            AddItemListView(idCash);
        }


        private void AddItemListView(int idCash)
        {
            Good[] details = Shop.GetGoodsOperations(idCash);
            lvDetails.Items.Clear();
            ListViewItem lvi;
            ListViewItem.ListViewSubItem lvsi;

            foreach (Good g in details)
            {
                lvi = new ListViewItem();
                lvi.Text = g.Name;

                lvsi = new ListViewItem.ListViewSubItem();
                lvsi.Text = g.Barcode;
                lvi.SubItems.Add(lvsi);

                lvsi = new ListViewItem.ListViewSubItem();
                lvsi.Text = g.PriceOut.ToString();
                lvi.SubItems.Add(lvsi);

                lvsi = new ListViewItem.ListViewSubItem();
                lvsi.Text = g.Count.ToString();
                lvi.SubItems.Add(lvsi);

                lvsi = new ListViewItem.ListViewSubItem();
                lvsi.Text = (g.Count * g.PriceOut).ToString();
                lvi.SubItems.Add(lvsi);

                lvDetails.Items.Add(lvi);
            }
        }
    }
}
