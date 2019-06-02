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


    }
}
