using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DataSet GetAllProducts()
        {
            string cmd = @"SELECT r.product FROM Rest r";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
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
            if (isDelete == true)
                cmd += " WHERE c.isDelete = 1";
            else cmd += " WHERE c.isDelete = 0";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
        public DataSet GetAllUnits()
        {
            string cmd = @"SELECT u.name FROM Units u";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
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
        public DataSet GetOutDetailByNumber(int number)
        {
            string cmd = @"SELECT 
                                od.id as 'Номер'
                                od.product AS 'Товар',
                                od.count AS 'Количество',
                                u.name AS 'Ед. измерения'

                                FROM Out_Detail od
                                INNER JOIN Units u ON u.id = od.id_unit
                                INNER JOIN Advent_Detail ad ON ad.id_unit = u.id
                                INNER JOIN Out o ON o.id = od.id_out
                                INNER JOIN Contracts c ON c.id = od.id_out
                                LEFT JOIN Customers c1 ON c1.id_contract = c.id
                                WHERE o.number_invoice = " + number;

            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
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
        public DataSet GetOutByNumber(int number)
        {
            string cmd = @"SELECT 
                            a.number_invoice,
                            a.date,
                            a.id_contract
                             FROM Out o WHERE o.number_invoice = " + number;

            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(cmd, Settings.Sql);
            da.Fill(ds);
            return ds;
        }
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
                    cmd = @"SELECT";
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
