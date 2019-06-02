using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Оптовый_склад_Wivichan
{
    static class Settings
    {
        private static string DataSource = @"COMPUTER\SQLEXPRESS01";//Сервер для подключения
        private static string DataBase = "'Подсчёт готовой продукции'";//База данных. Обязательно в одинарых кавычках
        private static string User = null;//Имя пользователя. Изменить, если необходимо
        private static string Password = null;//Пароль пользователя. Изменить, если необходимо

        private static string connect = @"Data Source=" + DataSource + ";Initial Catalog=" + DataBase + ";Integrated Security=True";//Строка соединения
        private static SqlConnection sql = new SqlConnection(Connect);//Создание объекта подключения
        private static string current_user = null;//Поле для хранения текущего пользователя

        public static string Current_user { get => current_user; set => current_user = value; }
        public static SqlConnection Sql { get => sql; set => sql = value; }
        public static string Connect { get => connect; set => connect = value; }

        public static void connectionOpen()
        {
            try
            {
                Sql.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка соединения с БД. Сообщение ошибки: \n" + ex.Message, "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Метод для закрытия подключения
        public static void connectionClose()
        {
            Sql.Close();
            Sql.Dispose();
        }

        //Метод для использования только цифр, точки и клавиши BackSpace
        public static void textForFloat(KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 46) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
            }
        }

        public static void textOnlyNumber(KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
        }

    }
}
