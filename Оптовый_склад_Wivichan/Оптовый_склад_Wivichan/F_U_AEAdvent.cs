using FastReport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Оптовый_склад_Wivichan
{
    public partial class F_U_AEAdvent : Form
    {
        bool isSave = false;//Флаг сохранения
        bool isSearch = false;//Флаг поиска
        bool isAdmin = false;//Флаг админа
        int numberSearch = 0;
        public F_U_AEAdvent(bool _isSearch = false, int _numberSearch = 0, bool _isAdmin = false)
        {
            InitializeComponent();
            isSearch = _isSearch;
            numberSearch = _numberSearch;
            isAdmin = _isAdmin;
            Fill();
        }

        void Fill()
        {
            SqlQuery query = new SqlQuery();
            for (int i = 0; i < query.GetAllIdContracts().Tables[0].Rows.Count; i++)
                comboBox1.Items.Add(query.GetAllIdContracts().Tables[0].Rows[i][0].ToString());
            //Если это поиск
            if (isSearch == true)
            {
                try
                {
                    //Проверка на админа
                    if (!isAdmin)
                    {
                        button1.Enabled = button2.Enabled = button3.Enabled =
                            button4.Enabled = button6.Enabled = textNumber.Enabled = comboBox1.Enabled = dateTime.Enabled = false;
                    }
                    //Заполняем грид данными
                    dataGrid.DataSource = query.GetAdventDetailByNumber(numberSearch).Tables[0].DefaultView;

                    textNumber.Text = query.GetAdventByNumber(numberSearch).Tables[0].Rows[0][0].ToString();
                    dateTime.Value = Convert.ToDateTime(query.GetAdventByNumber(numberSearch).Tables[0].Rows[0][1]);                    
                    comboBox1.Text = query.GetAdventByNumber(numberSearch).Tables[0].Rows[0][2].ToString();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Данные по приходу не найдены\n" + ex.Message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //Обновляем детали прихода
                updateAventDetail();
            }
        }

        private void F_U_AEAdvent_Load(object sender, EventArgs e)
        {
            //Если это не поиск
            if (isSearch == false)
            {
                try
                {
                    //Вставляем пустые данные. Потом заполним
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Advent (number_invoice)
                                                 VALUES
                                                       (0)");
                    cmd.Connection = Settings.Sql;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void F_U_AEAdvent_FormClosing(object sender, FormClosingEventArgs e)
        {
            CheckRowAndDelete(e);
        }

        void CheckRowAndDelete(FormClosingEventArgs e)
        {
            //Проверяем, что это не операция сохранения, поиска и что пользователь попал к нам не из админки
            if (isSave == false && isSearch == false && isAdmin)
            {
                try
                {
                    //Удаляем данные по приходу
                    SqlCommand cmd = new SqlCommand(@"DELETE FROM Advent_Detail WHERE id_advent = (SELECT TOP (1) a.id FROM Advent a ORDER BY a.id DESC)
                                                        DELETE FROM Advent WHERE id = IDENT_CURRENT('Advent')");
                    cmd.Connection = Settings.Sql;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGrid.CurrentRow == null)
                {
                    MessageBox.Show("Сначала выберите строку для удаления", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                DialogResult dr = MessageBox.Show("Вы собираетесь удалить строку с id = " + dataGrid.CurrentRow.Cells[0].Value.ToString(), "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    SqlQuery query = new SqlQuery();
                    query.DeleteAdventDetail(Convert.ToInt32(dataGrid.CurrentRow.Cells[0].Value.ToString()));
                    updateAventDetail();
                }
                else return;
            }
            catch
            {
                return;
            }
        }
        void updateAventDetail()
        {
            SqlQuery query = new SqlQuery();
            try
            {
                //Если это не поиск, то заполняем грид данными
                if (isSearch == false)
                    dataGrid.DataSource = query.GetAdventDetail().Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            F_U_AEAdventPos F_U_AEA = new F_U_AEAdventPos(this, 0);

            F_U_AEA.Text += " [Работает пользователь: " + Settings.Current_user + "]";
            F_U_AEA.Text = "Добавить позицию прихода";
            //Открываемой форме ставим событие при закрытии на обновление данных грида
            F_U_AEA.FormClosing += (s, ev) =>
            {
                updateAventDetail();
            };
            F_U_AEA.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGrid.CurrentRow == null)
            {
                MessageBox.Show("Выберите строку для редактирования", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            F_U_AEAdventPos F_U_AEA = new F_U_AEAdventPos(this, 1);

            F_U_AEA.Text += " [Работает пользователь: " + Settings.Current_user + "]";
            F_U_AEA.Text = "Изменить позицию прихода";
            F_U_AEA.button1.Text = "Изменить";
            F_U_AEA.FormClosing += (s, ev) =>
            {
                updateAventDetail();
            };
            F_U_AEA.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlQuery query = new SqlQuery();
            try
            {
                int i = 0, j = 0;
                query.UpdateAdvent(int.TryParse(textNumber.Text, out i) ? i : 0, dateTime.Value, int.TryParse(comboBox1.Text, out j) ? j : 0);
                isSave = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            Settings.textOnlyNumber(e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlQuery query = new SqlQuery();
            try
            {
                //Открыть отчет
                using (Report report = new Report())
                {
                    report.Load(Settings.ReportInvoiceAdvent);
                    report.SetParameterValue("invoice", textNumber.Text);
                    report.SetParameterValue("date", dateTime.Value);
                    report.SetParameterValue("contract", comboBox1.Text);
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
