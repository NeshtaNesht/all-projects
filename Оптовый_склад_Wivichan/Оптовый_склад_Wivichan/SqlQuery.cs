using System;
using System.Data;
using System.Data.SqlClient;

namespace Оптовый_склад_Wivichan
{
    class SqlQuery
    {
        public SqlQuery()
        {
            try
            {
                Settings.connectionOpen();//Открываем соединение
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// Получить всех поставщиков
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllSuppliers()
        {
            string cmd = @"SELECT
                              s.id AS 'Номер'
                             ,s.name AS 'Наименование поставщика'
                             ,s.address AS 'Адрес'
                             ,s.phone AS 'Телефон'
                             ,s.inn AS 'ИНН'
                             ,s.id_contract AS 'Номер договора'
                            FROM Suppliers s";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// Получить список продукции на остатках
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllProducts()
        {
            string cmd = @"SELECT r.product FROM Rest r";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// Получить цену и количество продукциина остатках
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public DataSet GetCountPriceByProduct(string product)
        {
            string cmd = @"SELECT r.count,
                          (SELECT ad.price 
                            FROM Advent_Detail ad 
                            WHERE ad.product = '" + product + @"' 
                            GROUP BY ad.price, ad.product) AS price
                         FROM Rest r WHERE r.product = '" + product + "'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// Получить сумму количества продукции из деталей расхода
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public DataSet GetCountProductOutDetail(string product)
        {
            string cmd = @"SELECT 
                        SUM(od.count) 
                        FROM Out_Detail od 
                        WHERE od.product = '" + product + "'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// Получить всех покупателей
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllCustomers()
        {
            string cmd = @"SELECT
                              s.id AS 'Номер'
                             ,s.name AS 'Наименование поставщика'
                             ,s.address AS 'Адрес'
                             ,s.phone AS 'Телефон'
                             ,s.inn AS 'ИНН'
                             ,s.id_contract AS 'Номер договора'
                            FROM Customers s";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// Получить все номера договоров
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllIdContracts()
        {
            string cmd = @"SELECT
                                c.id
                              FROM Contracts c WHERE c.isDelete = 0";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// Получить список деталей прихода
        /// </summary>
        /// <returns></returns>
        public DataSet GetAdventDetail()
        {
            string cmd = @"SELECT 
                              ad.id as 'Номер',
                              ad.product as 'Товар',
                              u.name AS 'Ед. измерения',
                              ad.count AS 'Количество',
                              ad.price AS 'Цена'
                             FROM Advent_Detail ad
                             INNER JOIN Units u ON u.id = ad.id_unit
                             LEFT JOIN Advent a ON a.id = ad.id_advent                              
                            WHERE a.id = IDENT_CURRENT('Advent')";

            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// Получить список заказов
        /// </summary>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        public DataSet GetAllContracts(bool isDelete)
        {
            string cmd = @"SELECT c.id as 'Номер договора', 
                            c.duration as 'Длительность, мес',
                            (CASE c.duration
	                            WHEN -1 THEN 'Бессрочный'
                            ELSE 'Долгосрочный'
                            END
                            ) AS 'Тип договора'
                            FROM Contracts c";
            if (isDelete == true)//Если стоит условие поиска удаленных контрактов
                cmd += " WHERE c.isDelete = 1";
            else cmd += " WHERE c.isDelete = 0";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// Получить список ед. измерения
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllUnits()
        {
            string cmd = @"SELECT u.name FROM Units u";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// Получить детали прихода по номеру накладной
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataSet GetAdventDetailByNumber(int number)
        {
            string cmd = @"SELECT 
                              ad.id as 'Номер',
                              ad.product as 'Товар',
                              u.name AS 'Ед. измерения',
                              ad.count AS 'Количество',
                              ad.price AS 'Цена'
                             FROM Advent_Detail ad
                             INNER JOIN Units u ON u.id = ad.id_unit
                             LEFT JOIN Advent a ON a.id = ad.id_advent                              
                            WHERE a.number_invoice = " + number;

            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// Получить детали расхода по номеру документа расхода
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataSet GetOutDetailByNumber(int number)
        {
            string cmd = @"SELECT 
                                od.id as 'Номер',
                                od.product AS 'Товар',
                                u.name AS 'Ед. измерения',
                                od.count AS 'Количество'
                                

                                FROM Out_Detail od
                                INNER JOIN Units u ON u.id = od.id_unit
                                LEFT JOIN Out o ON o.id = od.id_out
                                WHERE o.number_invoice = " + number;

            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// Получить приход по номеру накладной
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataSet GetAdventByNumber(int number)
        {
            string cmd = @"SELECT 
                            a.number_invoice,
                            a.date,
                            a.id_contract
                             FROM Advent a WHERE a.number_invoice = " + number;

            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// Получить расход по номеру расходного документа
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataSet GetOutByNumber(int number)
        {
            string cmd = @"SELECT 
                            o.number_invoice,
                            o.date,
                            o.id_contract
                             FROM Out o WHERE o.number_invoice = " + number;

            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// Получить список расхода или прихода за дату
        /// </summary>
        /// <param name="date"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public DataSet GetLastAdventOutByDate(DateTime date, int operation)
        {
            try
            {
                string cmd = null;
                if (operation == 0)//Приход
                {
                    cmd = @"SELECT 
                            a.id as 'Номер прихода' ,
                            a.number_invoice AS 'Номер накладной',
                            a.id_contract AS 'Номер контракта',
                            ad.product AS 'Товар',
                            ad.count AS 'Количество',
                            ad.price AS 'Цена, руб',
                            (ad.count * ad.price) AS 'Общий итог',
                            s.name AS 'Поставщик',
                            s.address AS 'Адрес поставщика',
                            s.phone AS 'Телефон поставщика',
                            s.inn AS 'ИНН'
                            FROM Advent a
                            INNER JOIN Contracts c ON c.id = a.id_contract
                            LEFT JOIN Suppliers s ON s.id_contract = c.id
                            INNER JOIN Advent_Detail ad ON ad.id_advent = a.id
                            WHERE a.date = '" + date + "'";
                }
                else//Расход
                {
                    cmd = @"SELECT 
                            a.id as 'Номер расхода' ,
                            a.number_invoice AS 'Номер документа',
                            a.id_contract AS 'Номер контракта',
                            ad.product AS 'Товар',
                            ad.count AS 'Количество',
                            ad.price AS 'Цена, руб',
                            (ad.count * ad.price) AS 'Общий итог',
                            s.name AS 'Покупатель',
                            s.address AS 'Адрес покупателя',
                            s.phone AS 'Телефон покупателя',
                            s.inn AS 'ИНН'
                            FROM Out a
                            INNER JOIN Contracts c ON c.id = a.id_contract
                            LEFT JOIN Customers s ON s.id_contract = c.id
                            INNER JOIN Out_Detail ad ON ad.id_out = a.id
                            WHERE a.date = '" + date + "'";
                }

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
                da.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Получить список деталей расхода
        /// </summary>
        /// <returns></returns>
        public DataSet GetOutDetail()
        {
            try
            {
                string cmd = @"SELECT 
                                od.id AS 'Номер',
                                od.product as 'Товар',
                                u.name AS 'Ед. измерения',
                                od.count AS 'Количество',
                                od.price AS 'Цена, руб'
                                FROM Out_Detail od
                                INNER JOIN Units u ON u.id = od.id_unit
                                INNER JOIN Out o ON o.id = od.id_out
                                LEFT JOIN Contracts c ON c.id = o.id_contract
                                WHERE o.id = IDENT_CURRENT('Out')";

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
                da.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Получить данные для авторизации
        /// </summary>
        /// <returns></returns>
        public DataSet GetUser(string login, string password)
        {
            string cmd = @"SELECT u.name, 
                            u.password, 
                            r.name_role 
                            FROM Users u
                            INNER JOIN Roles r ON r.id = u.id_role
                            WHERE u.name = '" + login + "' " +
                            "AND u.password = '" + password + "'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }

        /// <summary>
        /// Добавление/редактирование поставщика
        /// </summary>
        /// <param name="id_for_update"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="phone"></param>
        /// <param name="inn"></param>
        /// <param name="contract"></param>
        /// <param name="operation"></param>
        public void AddEditSupplier(int id_for_update, string name, string address, string phone, string inn, int contract, int operation)
        {
            using (SqlCommand cmd = new SqlCommand("AddEditSupplier", Settings.Sql))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                cmd.Parameters.AddWithValue("@id_for_update", id_for_update);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@inn", inn);
                cmd.Parameters.AddWithValue("@contract", contract);
                cmd.Parameters.AddWithValue("@operation", operation);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Добавление/редактирование покупателя
        /// </summary>
        /// <param name="id_for_update"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="phone"></param>
        /// <param name="inn"></param>
        /// <param name="contract"></param>
        /// <param name="operation"></param>
        public void AddEditCustomer(int id_for_update, string name, string address, string phone, string inn, int contract, int operation)
        {
            using (SqlCommand cmd = new SqlCommand("AddEditCustomer", Settings.Sql))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                cmd.Parameters.AddWithValue("@id_for_update", id_for_update);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@inn", inn);
                cmd.Parameters.AddWithValue("@contract", contract);
                cmd.Parameters.AddWithValue("@operation", operation);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Добавление/редактирование контрактов
        /// </summary>
        /// <param name="id_for_update"></param>
        /// <param name="duration"></param>
        /// <param name="operation"></param>
        public void AddEditContract(int id_for_update, int duration, int operation)
        {
            using (SqlCommand cmd = new SqlCommand("AddEditContract", Settings.Sql))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                cmd.Parameters.AddWithValue("@id_for_update", id_for_update);
                cmd.Parameters.AddWithValue("@duration", duration);
                cmd.Parameters.AddWithValue("@operation", operation);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Добавление/редактирование деталей прихода
        /// </summary>
        /// <param name="id_for_update"></param>
        /// <param name="product"></param>
        /// <param name="unit"></param>
        /// <param name="count"></param>
        /// <param name="price"></param>
        /// <param name="operation"></param>
        public void AddEditAdventDetail(int id_for_update, string product, string unit, int count, double price, int operation)
        {
            using (SqlCommand cmd = new SqlCommand("AddEditAdventDetail", Settings.Sql))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                cmd.Parameters.AddWithValue("@id_for_update", id_for_update);
                cmd.Parameters.AddWithValue("@product", product);
                cmd.Parameters.AddWithValue("@unit", unit);
                cmd.Parameters.AddWithValue("@count", count);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@operation", operation);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Добавление/редактирование деталей расхода
        /// </summary>
        /// <param name="id_for_update"></param>
        /// <param name="product"></param>
        /// <param name="count"></param>
        /// <param name="price"></param>
        /// <param name="operation"></param>
        public void AddEditOutDetail(int id_for_update, string product, int count, double price, int operation)
        {
            using (SqlCommand cmd = new SqlCommand("AddEditOutDetail", Settings.Sql))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                cmd.Parameters.AddWithValue("@id_for_update", id_for_update);
                cmd.Parameters.AddWithValue("@product", product);
                cmd.Parameters.AddWithValue("@count", count);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@operation", operation);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Обновление документа прихода
        /// </summary>
        /// <param name="number_invoice"></param>
        /// <param name="date"></param>
        /// <param name="number_contract"></param>
        public void UpdateAdvent(int number_invoice, DateTime date, int number_contract)
        {
            using (SqlCommand cmd = new SqlCommand("UpdateAdvent", Settings.Sql))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                cmd.Parameters.AddWithValue("@number_invoice", number_invoice);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@number_contract", number_contract);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Обновление документа расхода
        /// </summary>
        /// <param name="number_invoice"></param>
        /// <param name="date"></param>
        /// <param name="number_contract"></param>
        public void UpdateOut(int number_invoice, DateTime date, int number_contract)
        {
            using (SqlCommand cmd = new SqlCommand("UpdateOut", Settings.Sql))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                cmd.Parameters.AddWithValue("@number_invoice", number_invoice);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@number_contract", number_contract);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Удаление поставщика
        /// </summary>
        /// <param name="id"></param>
        public void DeleteSupplier(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DeleteSupplier", Settings.Sql))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Удалить покупателя
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCustomer(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DeleteCustomer", Settings.Sql))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Удалить контракт
        /// </summary>
        /// <param name="id"></param>
        public void DeleteContract(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DeleteContract", Settings.Sql))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Удалить детали прихода
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAdventDetail(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DeleteAdventDetail", Settings.Sql))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Удалить детали расхода
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOutDetail(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DeleteOutDetail", Settings.Sql))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }     
    }
}
