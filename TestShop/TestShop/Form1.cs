using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TestShop.ServiceShop;
using System.ServiceModel;
using System.Security.Cryptography;

namespace TestShop
{
    public partial class Form1 : Form
    {
        ShopClient Shop;
        public Form1()
        {
            try
            {
                InitializeComponent();
                Shop = new ShopClient();
                //Shop.UserAuth("Serg", this.GetHashString("123"));
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void btUsers_Click(object sender, EventArgs e)
        {
            Users users = new Users(this.Shop);
            users.ShowDialog();
        }

        private string GetHashString(string Pass)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(Pass);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }  
    }
}
