using FastReport;
using FastReport.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Оптовый_склад_Wivichan
{
    public partial class FUser : Form
    {
        //Флаг закрытия
        bool isExit = true;
        public FUser()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //При выходе закрывает подключение
            Settings.connectionClose();
            //Открываем форму авторизации
            FAuthorization auth = new FAuthorization();
            auth.Visible = true;            
            isExit = false;
            Close();
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Выход из программы
            Application.Exit();
        }

        private void FUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Если флаг закрытия true - выходим из программы
            if (isExit)
                Application.Exit();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Открываем форму добавления прихода
            new F_U_AEAdvent().ShowDialog();
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Открываем форму поиска
            new F_U_Search(0).ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getLastAdventOutByDate(radioButton1.Checked == true ? 0 : 1);
        }
        void getLastAdventOutByDate(int operation)
        {
            try
            {
                //Объект класса SQL запросов
                SqlQuery query = new SqlQuery();
                //Формируем набор данных
                DataSet ds = query.GetLastAdventOutByDate(dateTime.Value, operation);
                //Если ничего не нашли - выводим сообщение
                if (ds.Tables[0].Rows.Count == 0)
                    MessageBox.Show("По выбранному фильтру информация не найдена", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else dataGrid.DataSource = ds.Tables[0].DefaultView;//Иначе выводим данные в грид
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при попытке получить данные из базы данных\n" + ex.Message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void поискToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new F_U_Search(1).ShowDialog();
        }

        private void добавитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new F_U_AEOut().ShowDialog();
        }

        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            SqlQuery query = new SqlQuery();
            try
            {
                //Открыть отчет
                using (Report report = new Report())
                {
                    if (radioButton1.Checked == true)                    
                        report.Load(Settings.ReportAdvent); //Загружаем отчет по приходу                   
                    else 
                        report.Load(Settings.ReportOut);    //Загружаем отчет по расходу                

                    //Передаем параметр в отчет
                    report.SetParameterValue("date", dateTime.Value);
                    report.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при открытии отчета\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void просмотрОстатковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlQuery query = new SqlQuery();
            try
            {
                //Открыть отчет
                using (Report report = new Report())
                {
                    report.Load(Settings.ReportRest);
                    report.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при открытии отчета\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
