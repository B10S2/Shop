using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;

namespace ShopServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Shop : IShop
    {
        SqlConnection conn = null;
        User CurrentUser = null;

        //Конструктор
        public Shop()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopDB"].ConnectionString);
            try { conn.Open(); }
            catch (Exception ex) { }
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

        //Авторизация
        public User UserAuth(string Login, string Pass)
        {
            SqlCommand comm = new SqlCommand();
            User current = new User();
            this.CurrentUser = current;
            //Проверяем логин
            comm.Connection = conn;
            comm.CommandText = "Select * From tblUsers Where UsLogin like @Login";
            comm.Parameters.Add("@Login", sqlDbType: System.Data.SqlDbType.NVarChar).Value = Login;
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                current.Login = reader.GetString(2);
            }
            if (current.Login == null)
            {
                current.Err = User.ERRLOGIN;
                reader.Close();
                return current;
            }
            reader.Close();

            //Проверяем пароль
            comm.CommandText = "Select * From tblUsers Where Pass like @Pass and UsLogin like @Login";
            comm.Parameters.Clear();
            comm.Parameters.Add("@Pass", sqlDbType: System.Data.SqlDbType.NVarChar).Value = this.GetHashString(Pass + "izmail");
            comm.Parameters.Add("@Login", sqlDbType: System.Data.SqlDbType.NVarChar).Value = Login;
            reader = comm.ExecuteReader();

            int Role = 0;
            while (reader.Read())
            {
                current.ID = reader.GetInt32(0);
                current.Pass = reader.GetString(3);
                current.Phone = reader.GetSqlValue(4).ToString();
                current.FirstName = reader.GetString(5);
                current.LastName = reader.GetString(6);
                Role = reader.GetInt32(1);
            }

            if (current.Pass == null)
            {
                current.Err = User.ERRPASS;
                reader.Close();
                return current;
            }
            reader.Close();

            //Вытаскиваем роль пользователя
            comm.CommandText = "Select * From tblRoles Where id = @Role";
            comm.Parameters.Add("@Role", sqlDbType: System.Data.SqlDbType.Int).Value = Role;
            reader = comm.ExecuteReader();

            while (reader.Read())
            {
                current.Role = reader.GetString(1);
            }

            reader.Close();

            return current;
        }

        //Вернуть всех пользователей системы
        public User[] GetAllUsers()
        {
            if (this.CurrentUser.Err != 0) return null;
            List<User> users = new List<User>();
            SqlCommand comm = new SqlCommand();

            comm.Connection = conn;
            comm.CommandText = "Select u.UsLogin, u.fName, u.lName, u.Pass, r.rName, u.Phone  From tblUsers u, tblRoles r Where u.idRole = r.id";
            SqlDataReader reader = comm.ExecuteReader();
            User u = null;
            while (reader.Read())
            {
                u = new User();
                u.Login = reader.GetString(0);
                u.FirstName = reader.GetString(1);
                u.LastName = reader.GetString(2);
                u.Pass = reader.GetString(3);
                u.Role = reader.GetString(4);
                u.Phone = reader.GetString(5);
                users.Add(u);
            }
            reader.Close();
            return users.ToArray();

        }

        //Добавить нового пользователя
        public void AddUser(User user)
        {
            if (this.CurrentUser.Err != 0) return;

            string Password = this.GetHashString(user.Pass + "izmail");
            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.Connection = conn;
            comm.CommandText = "Insert INTO tblUsers (fName, lName, UsLogin, Pass, Phone, idRole) values (@fName, @lName, @Nick, @Pass, @Phone, (Select id From tblRoles where rName like @Role))";
            
            comm.Parameters.Add("@fName",SqlDbType.NVarChar).Value = user.FirstName;
            comm.Parameters.Add("@lName",SqlDbType.NVarChar).Value = user.LastName;
            comm.Parameters.Add("@Nick",SqlDbType.NVarChar).Value = user.Login;
            comm.Parameters.Add("@Pass", SqlDbType.NVarChar).Value = Password;
            comm.Parameters.Add("@Phone",SqlDbType.NVarChar).Value = user.Phone;
            comm.Parameters.Add("@Role",SqlDbType.NVarChar).Value = user.Role;

            comm.ExecuteNonQuery();
        }

        //Получить роли
        public string[] GetRoles()
        {
            if (this.CurrentUser.Err != 0) return null;

            List<string> roles = new List<string>();
            SqlCommand comm = new SqlCommand();

            comm.Connection = conn;
            comm.CommandText = "select rName from tblRoles";
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                roles.Add(reader.GetString(0));
            }
            reader.Close();
            return roles.ToArray();
        }

        //Добавить категории(ю) товаров
        public void AddCategory(string[] Cat)
        {
            if (this.CurrentUser.Err != 0) return;

            try
            {
                SqlCommand comm = new SqlCommand();
                comm.CommandType = System.Data.CommandType.Text;
                comm.Connection = conn;
                comm.CommandText = "Insert INTO tblCategoryes (cName) values (@Cat)";

                foreach (string str in Cat)
                {
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@Cat", System.Data.SqlDbType.NVarChar).Value = str;
                    comm.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { }
        }

        //Получить все категории товаров
        public string[] GetCategoryes()
        {
            if (this.CurrentUser.Err != 0) return null;

            List<string> Categ = new List<string>();
            SqlCommand comm = new SqlCommand();
            comm.CommandType = System.Data.CommandType.Text;
            comm.Connection = conn;
            comm.CommandText = "Select cName From tblCategoryes";

            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                Categ.Add(reader.GetString(0));
            }

            reader.Close();
            return Categ.ToArray();
        }

        //Добавление новой позизиции (Вид товара)
        public void AddGood(string Name, string Categoryes, string Barcode, double PriceOut = 0)
        {
            if (this.CurrentUser.Err != 0) return;

            SqlCommand comm = new SqlCommand();
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.CommandText = "AddGood";

            comm.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = Name;
            comm.Parameters.Add("@Category", System.Data.SqlDbType.NVarChar).Value = Categoryes;
            comm.Parameters.Add("@Barcode", System.Data.SqlDbType.NVarChar).Value = Barcode;
            comm.Parameters.Add("@PriceOut", System.Data.SqlDbType.Real).Value = PriceOut;

            comm.ExecuteNonQuery();
        }

        //Прием товара
        public void AddGoods(int idGoods, int Count, double PriceIn)
        {
            if (this.CurrentUser.Err != 0) return;

            if (this.CurrentUser.Err != 0) return;

            SqlCommand comm = new SqlCommand();
            comm.CommandType = System.Data.CommandType.Text;
            comm.Connection = conn;
            comm.CommandText = "Insert INTO tblWareHouse (idGoods, gCount, PriceIn, DateIn) values (@idGood, @Cnt, @PriceIn, @DateIn)";

            comm.Parameters.Add("@idGood", System.Data.SqlDbType.Int).Value = idGoods;
            comm.Parameters.Add("@Cnt", System.Data.SqlDbType.Int).Value = Count;
            comm.Parameters.Add("@PriceIn", System.Data.SqlDbType.Real).Value = PriceIn;
            comm.Parameters.Add("@DateIn", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
            comm.ExecuteNonQuery();

        }

        //Получить информацию о товаре
        public Good GetGood(string Barcode)
        {
            if (this.CurrentUser.Err != 0) return new Good();

            Barcode = Barcode.Trim('\n');
            Barcode = Barcode.Trim('\r');
            Barcode = Barcode.Trim('\n');
            SqlCommand comm = new SqlCommand();
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.CommandText = "GetGood";
            comm.Parameters.Add("@Barcode", System.Data.SqlDbType.NVarChar).Value = Barcode;

            SqlDataReader reader = comm.ExecuteReader();

            Good g = new Good();
            g.Barcode = "Err";
            while (reader.Read())
            {
                g.ID = reader.GetInt32(0);
                g.Name = reader.GetString(1);
                g.Category = reader.GetString(2);
                g.Barcode = reader.GetString(3);
                g.PriceOut = (double)reader.GetFloat(4);
                g.Count = reader.GetInt32(5);
            }
            reader.Close();

            if (g.Barcode.Equals(Barcode, StringComparison.InvariantCultureIgnoreCase))
                g.Err = 0;
            else
                g.Err = 1;

            return g;
        }

        //Получить все товари
        public Good[] GetAllGoods()
        {
            if (this.CurrentUser.Err != 0) return null;

            List<Good> goods = new List<Good>();
            SqlCommand comm = new SqlCommand();
            comm.CommandType = System.Data.CommandType.Text;
            comm.Connection = conn;
            comm.CommandText = "Select g.id, g.gName, c.cName, g.Barcode, g.PriceOut From tblGoods g, tblCategoryes c Where g.idCategory = c.id";

            SqlDataReader reader = comm.ExecuteReader();

            Good g = new Good();
            while (reader.Read())
            {
                g = new Good();
                g.ID = reader.GetInt32(0);
                g.Name = reader.GetString(1);
                g.Category = reader.GetString(2);
                g.Barcode = reader.GetString(3);
                g.PriceOut = (double)reader.GetFloat(4);
                goods.Add(g);
            }
            reader.Close();

            return goods.ToArray();
        }

        //Получить товари в наличии на складе
        public Good[] GetGoodsBalance()
        {
            if (this.CurrentUser.Err != 0) return null;

            List<Good> goods = new List<Good>();
            SqlCommand comm = new SqlCommand();
            comm.CommandType = System.Data.CommandType.Text;
            comm.Connection = conn;
            comm.CommandText = "Select g.id, g.gName, c.cName, g.Barcode, g.PriceOut, b.gCount From tblGoods g, tblCategoryes c, tblBalance b Where b.idGoods = g.id and g.idCategory = c.id and gCount <> 0";

            SqlDataReader reader = comm.ExecuteReader();

            Good g = new Good();
            while (reader.Read())
            {
                g = new Good();
                g.ID = reader.GetInt32(0);
                g.Name = reader.GetString(1);
                g.Category = reader.GetString(2);
                g.Barcode = reader.GetString(3);
                g.PriceOut = (double)reader.GetFloat(4);
                g.Count = reader.GetInt32(5);
                goods.Add(g);
            }
            reader.Close();

            return goods.ToArray();
        }

        //Добавление на склад за сегодня
        public Good[] GetGoodsToDay()
        {
            if (this.CurrentUser.Err != 0) return null;

            List<Good> goods = new List<Good>();
            SqlCommand comm = new SqlCommand();
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.CommandText = "GetGoodsTODay";

            SqlDataReader reader = comm.ExecuteReader();

            Good g = new Good();
            while (reader.Read())
            {
                g = new Good();
                g.ID = reader.GetInt32(0);
                g.Name = reader.GetString(1);
                g.Category = reader.GetString(2);
                g.Barcode = reader.GetString(3);
                g.Count = reader.GetInt32(4);
                g.PriceIn = (double)reader.GetFloat(5);
                g.DateIn = reader.GetDateTime(6);
                g.idWare = reader.GetInt32(7);
                goods.Add(g);
            }
            reader.Close();

            return goods.ToArray();
        }

        
        //Удаление прихода на склад
        public void DeleteBalance(int idWare)
        {
            if (this.CurrentUser.Err != 0) return;

            SqlCommand comm = new SqlCommand();
            comm.CommandType = System.Data.CommandType.Text;
            comm.Connection = conn;
            comm.CommandText = "Delete From tblWarehouse Where id = @ID";
            comm.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = idWare;
            comm.ExecuteNonQuery();
        }

        //Продажа одной или нескольких  едениц товара в том числе и разного
        public void SellGoods(Good[] goods, int idUser)
        {
            if (this.CurrentUser.Err != 0) return;

            double Cash = 0;
            List<int> ids = new List<int>();
            //try
            //{
                SqlCommand comm = new SqlCommand();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.CommandText = "Sale";


                //Добавляем в таблицу tblSales
                foreach (Good g in goods)
                {
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@idGood", SqlDbType.Int).Value = g.ID;
                    comm.Parameters.Add("@Count", SqlDbType.Int).Value = g.Count;
                    comm.Parameters.Add("@idUser", SqlDbType.Int).Value = idUser;
                    comm.Parameters.Add("@id", SqlDbType.Int);

                    comm.Parameters["@id"].Direction = ParameterDirection.Output;

                    comm.ExecuteNonQuery();

                    if (comm.Parameters["@id"].Value != DBNull.Value)
                        ids.Add((int)comm.Parameters["@id"].Value);
                    //Цена за все товары
                    Cash += g.Count * g.PriceOut;
                }

                if (ids.Count == 0) return;

                //Добавляем в таблицу tblCash
                int idCash = 0;
                comm.CommandText = "AddCashOperation";
                comm.Parameters.Clear();
                comm.Parameters.Add("@Cash", System.Data.SqlDbType.Real).Value = Cash;
                comm.Parameters.Add("@Operation", System.Data.SqlDbType.NVarChar).Value = "Продажа";
                comm.Parameters.Add("@idUser", System.Data.SqlDbType.Int).Value = idUser;
                comm.Parameters.Add("@id", SqlDbType.Int);
                comm.Parameters["@id"].Direction = ParameterDirection.Output;

                comm.ExecuteNonQuery();

                if (comm.Parameters["@id"].Value != DBNull.Value)
                    idCash = (int)comm.Parameters["@id"].Value;

                //Добавляем в таблицу tblGroupeSales
                comm.CommandType = CommandType.Text;
                comm.CommandText = "INSERT INTO tblGroupeSales (idSales, idCash) values (@idSales, @idCash)";

                foreach (int id in ids)
                {
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@idSales", SqlDbType.Int).Value = id;
                    comm.Parameters.Add("@idCash", SqlDbType.Int).Value = idCash;
                    comm.ExecuteNonQuery();
                }
            //}
            //catch (Exception ex) {}
        }

        //Кассовые Операции за текущий день
        public CashOperation[] GetToDayOperations()
        {
            if (this.CurrentUser.Err != 0) return null;

            List<CashOperation> CO = new List<CashOperation>();
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "GetOperationsTODay";

            SqlDataReader reader = comm.ExecuteReader();

            CashOperation c;
            while (reader.Read())
            {
                c = new CashOperation();
                c.ID = reader.GetInt32(0);
                c.CashBalance = (double)reader.GetFloat(1);
                c.TypeOperation = reader.GetString(2);
                c.PriceOperation = (double)reader.GetFloat(3);
                c.TimeOpertion = reader.GetDateTime(4);
                CO.Add(c);
            }
            reader.Close();

            return CO.ToArray();
        }

        //товары по конкретной операции
        public Good[] GetGoodsOperations(int idOperation)
        {
            if (this.CurrentUser.Err != 0) return null;

            List<Good> goods = new List<Good>();
            SqlCommand comm = new SqlCommand();
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.CommandText = "GetGoodsOperation";
            comm.Parameters.Add("@idCash", SqlDbType.Int).Value = idOperation;

            SqlDataReader reader = comm.ExecuteReader();

            Good g = new Good();
            while (reader.Read())
            {
                g = new Good();
                g.ID = reader.GetInt32(0);
                g.Barcode = reader.GetString(1);
                g.Name = reader.GetString(2);
                g.PriceOut = (double)reader.GetFloat(3);
                g.Count = reader.GetInt32(4);
                goods.Add(g);
            }
            reader.Close();

            return goods.ToArray();
        }

        //Внесение средств
        public void CashIn(double Cash, int idUser)
        {
            if (this.CurrentUser.Err != 0) return;

            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.CommandText = "AddCashOperation";

            comm.Parameters.Add("@Cash", System.Data.SqlDbType.Real).Value = Cash;
            comm.Parameters.Add("@Operation", System.Data.SqlDbType.NVarChar).Value = "Пополнение";
            comm.Parameters.Add("@idUser", System.Data.SqlDbType.Int).Value = idUser;
            comm.Parameters.Add("@id", SqlDbType.Int);
            comm.Parameters["@id"].Direction = ParameterDirection.Output;

            comm.ExecuteNonQuery();
        }

        //Инкасация
        public void CashOut(double Cash, int idUser)
        {
            if (this.CurrentUser.Err != 0) return;

            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.CommandText = "AddCashOperation";

            comm.Parameters.Add("@Cash", System.Data.SqlDbType.Real).Value = Cash;
            comm.Parameters.Add("@Operation", System.Data.SqlDbType.NVarChar).Value = "Инкасация";
            comm.Parameters.Add("@idUser", System.Data.SqlDbType.Int).Value = idUser;
            comm.Parameters.Add("@id", SqlDbType.Int);
            comm.Parameters["@id"].Direction = ParameterDirection.Output;

            comm.ExecuteNonQuery();
        }

        //Баланс на начало дня
        public double GetStartBalance()
        {
            if (this.CurrentUser.Err != 0) return 0;

            double StartDayBalance = 0;
            SqlCommand comm = new SqlCommand();
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.CommandText = "GetStartDayBalance";

            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
                StartDayBalance = (double)reader.GetFloat(0);
            reader.Close();

            return StartDayBalance;
        }
    }
}