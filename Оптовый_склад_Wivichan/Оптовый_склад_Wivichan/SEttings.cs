using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Оптовый_склад_Wivichan
{
    class Settings
    {
        private static string DataSource = @"COMPUTER\SQLEXPRESS01";//Сервер для подключения
        private static string DataBase = "'Оптовый склад'";//База данных. Обязательно в одинарых кавычках
        private static string User = null;//Имя пользователя. Изменить, если необходимо
        private static string Password = null;//Пароль пользователя. Изменить, если необходимо
        private static string connect = @"Data Source=" + DataSource + ";Initial Catalog=" + DataBase + ";Integrated Security=True";//Строка соединения
        private static SqlConnection sql;//Создание объекта подключения
        private static string current_user = null;//Поле для хранения текущего пользователя
        //Ссылки на отчеты
        private static string reportRest = @"..\..\Reports\Отчет - остатки.frx";
        private static string reportAdvent = @"..\..\Reports\Отчет - по приходу.frx";
        private static string reportOut = @"..\..\Reports\Отчет - по расходу.frx";
        private static string reportInvoiceAdvent = @"..\..\Reports\Отчет - накладная по приходу.frx";
        private static string reportInvoiceOut = @"..\..\Reports\Отчет - документ по расходу.frx";

        public static string Current_user { get => current_user; set => current_user = value; }
        public static SqlConnection Sql { get => sql; set => sql = value; }
        public static string Connect { get => connect; set => connect = value; }
        public static string ReportRest { get => reportRest; set => reportRest = value; }
        public static string ReportAdvent { get => reportAdvent; set => reportAdvent = value; }
        public static string ReportOut { get => reportOut; set => reportOut = value; }
        public static string ReportInvoiceAdvent { get => reportInvoiceAdvent; set => reportInvoiceAdvent = value; }
        public static string ReportInvoiceOut { get => reportInvoiceOut; set => reportInvoiceOut = value; }
        /// <summary>
        /// Метод открытия подключения
        /// </summary>
        public static void connectionOpen()
        {
            try
            {
                sql = new SqlConnection(connect);
                Sql.Open();
            }
            catch (Exception ex)
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
        /// <summary>
        /// Метод для использования только цифр
        /// </summary>
        /// <param name="e"></param>
        public static void textOnlyNumber(KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
        }

    }
}
