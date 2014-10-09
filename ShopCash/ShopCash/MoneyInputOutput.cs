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
    public partial class MoneyInputOutput : Form
    {
        double money;
        bool flag;
        ShopClient Shop;
        public MoneyInputOutput(ShopClient _Shop, bool fl = false)
        {
            InitializeComponent();
            flag = fl;
            this.Shop = _Shop;
        }
        private void headerCheck()
        {
            if (flag) this.Text = "Инкасация";
            else this.Text = "Введите сумму которую вносите в кассу";
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                if (true)//Запрос на авторизацию и право на операцию
                {
                    money = (double)numGrn.Value + ((double)numCop.Value / 100);
                    if (MessageBox.Show("Сумма для вывода: " + money.ToString(), "Изьятие денег из кассы", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        Shop.CashOut(money, Authorization.user.ID);
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                    money = 0;
                } 
            }
            if (!flag)
            {
                money = (double)numGrn.Value + ((double)numCop.Value / 100);
                if (MessageBox.Show("Сумма для ввода: " + money.ToString(), "Внесение денег в кассу", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Shop.CashIn(money, Authorization.user.ID);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                money = 0;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {      
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                money = 0;
                this.Close();  
        }
        public double Money
        {
            get { return money; }
            set { money = value; }
        }

    }
}
