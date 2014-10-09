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
using System.Security.Cryptography;

namespace ShopWarehouse
{
    public partial class Authorization : Form
    {
        ShopClient Shop;
        public static ServiceShop.User user;
        public Authorization(ShopClient _Shop)
        {
            InitializeComponent();
            Shop = _Shop;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            try
            {
                user = Shop.UserAuth(tbLogin.Text, this.GetHashString(tbPassword.Text));
                if (user.Err == 0)
                    if (user.Role.Equals("Кладовщик", StringComparison.InvariantCultureIgnoreCase) || user.Role.Equals("Администратор", StringComparison.InvariantCultureIgnoreCase))
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    else
                        MessageBox.Show("Доступ запрещен", "Ошибка Авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Неверный логин или Пароль", "Ошибка Авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
            catch (Exception ex)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        //При нажати Esc - Выход
        private void EscExit(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }
    }
}