using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.IdentityModel.Selectors;

namespace ShopServer
{
    public class CustomUserNameValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (!(userName == "Test" && password == "test"))
                throw new FaultException("Неверный логин или пароль!");
        }
    }

    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IShop
    {
        //Авторизировать пользователя
        [OperationContract]
        User UserAuth(string Login, string Pass);

        //Вернуть всех пользователей системы
        [OperationContract]
        User[] GetAllUsers();

        //Добавить нового пользователя
        [OperationContract]
        void AddUser(User user);

        //Получить роли пользователей
        [OperationContract]
        string[] GetRoles();

        //Добавить категории(ю) товаров
        [OperationContract]
        void AddCategory(string[] Cat);

        //Получить все категории товаров
        [OperationContract]
        string[] GetCategoryes();

        //Добавление новой позизиции (Вид товара)
        [OperationContract]
        void AddGood(string Name, string Categoryes, string Barcode, double PriceOut = 0);

        //Прием товара
        [OperationContract]
        void AddGoods(int idGoods, int Count, double PriceIn);

        //Получить информацию о товаре
        [OperationContract]
        Good GetGood(string Barcode);

        //Продажа одной или нескольких  едениц товара в том числе и разного
        [OperationContract]
        void SellGoods(Good[] goods, int idUser);

        //Получить все товари
        [OperationContract]
        Good[] GetAllGoods();

        //Получить товари в наличии на складе
        [OperationContract]
        Good[] GetGoodsBalance();

        /// <summary>
        /// //Добавленные на склад за сегодня
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Good[] GetGoodsToDay();

        /// <summary>
        /// //Удаление прихода на склад
        /// </summary>
        /// <param name="Time"></param>
        /// <returns></returns>
        [OperationContract]
        void DeleteBalance(int idWare);

        //Кассовые Операции за текущий день
        [OperationContract]
        CashOperation[] GetToDayOperations();

        //товары по конкретной операции
        [OperationContract]
        Good[] GetGoodsOperations(int idOperation);

        //Внесение средств
        [OperationContract]
        void CashIn(double Cash, int idUser);

        //Инкасация
        [OperationContract]
        void CashOut(double Cash, int idUser);

        //Баланс на начало дня
        [OperationContract]
        double GetStartBalance();
    }

    [DataContract]
    public struct Good
    {
        [DataMember]
        public int ID { set; get; }
        [DataMember]
        public string Name { set; get; }
        [DataMember]
        public string Category { set; get; }
        [DataMember]
        public string Barcode { set; get; }
        [DataMember]
        public double PriceOut { set; get; }
        [DataMember]
        public double PriceIn { set; get; }
        [DataMember]
        public int Count { set; get; }
        [DataMember]
        public DateTime DateIn { set; get; }
        [DataMember]
        public int idWare { set; get; }

        //Если 0, то структура заполнена искомым товаром, если 1, то такой товар не найден.
        [DataMember]
        public int Err { set; get; }
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public const int ERRORCONNDB = 1; //Ошибка подключения к БД
        [DataMember]
        public const int ERRLOGIN = 2; //Неверный логин
        [DataMember]
        public const int ERRPASS = 3; //Неверный пароль

        [DataMember]
        public int ID { set; get; }
        [DataMember]
        public string Login { set; get; }
        [DataMember]
        public string Pass { set; get; }
        [DataMember]
        public string FirstName { set; get; }
        [DataMember]
        public string LastName { set; get; }
        [DataMember]
        public string Role { set; get; }
        [DataMember]
        public int Err { set; get; }
        [DataMember]
        public string Phone { set; get; }

        public User(int _Err, string _Fn, string _Ln, string _Role, string _Phone)
        {
            this.LastName = _Ln;
            this.FirstName = _Fn;
            this.Role = _Role;
            this.Err = _Err;
            this.Phone = _Phone;
        }
        public User() { }
    }

    [DataContract]
    public struct CashOperation
    {
        [DataMember]
        public int ID { set; get; }
        [DataMember]
        public double PriceOperation { set; get; }
        [DataMember]
        public double CashBalance { set; get; }
        [DataMember]
        public string TypeOperation { set; get; }
        [DataMember]
        public DateTime TimeOpertion { set; get; }
    }
}