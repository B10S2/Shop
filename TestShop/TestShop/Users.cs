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
    public partial class Users : Form
    {
        ShopClient Shop;
        
        public Users(ShopClient _Shop)
        {
            InitializeComponent();
            this.Shop = _Shop;
            
            //Заполняем ComboBox Ролями
            cbRole.Items.AddRange(Shop.GetRoles());
            cbRole.SelectedIndex = 0;

            //this.AddUsersListView();
        }

        //Заполнение ListView
        private void AddUsersListView()
        {
            lvUsers.Items.Clear();
            

            User[] users = Shop.GetAllUsers();
            foreach (User u in users)
            {
                ListViewItem lvi = new ListViewItem();
                ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();

                lvi.Text = u.FirstName;
                lvi.Tag = u.ID;

                lvsi.Text = u.LastName;
                lvi.SubItems.Add(lvsi);

                lvsi = new ListViewItem.ListViewSubItem();
                lvsi.Text = u.Login;
                lvi.SubItems.Add(lvsi);

                lvUsers.Items.Add(lvi);
            }
        }

        private void btNewUser_Click(object sender, EventArgs e)
        {
            tbName.Text = String.Empty;
            tbLastName.Text = String.Empty;
            tbLogin.Text = String.Empty;
            tbPassword.Text = String.Empty;
            tbPhone.Text = String.Empty;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User();
                user.FirstName = tbName.Text;
                user.LastName = tbLastName.Text;
                user.Login = tbLogin.Text;
                user.Pass = this.GetHashString(tbPassword.Text);
                user.Phone = tbPhone.Text;
                user.Role = cbRole.SelectedItem.ToString();

                Shop.AddUser(user);
                MessageBox.Show("Сохранено!","Информция",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.AddUsersListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void btOK_Click(object sender, EventArgs e)
        {   
            this.Close();
        }

        private void lvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

            ListViewItem lvi = new ListViewItem();

            foreach (ListViewItem l in lvUsers.SelectedItems)
                lvi = l;

            
        }
    }

    public class test : ShopClient
    {
        
    }
}