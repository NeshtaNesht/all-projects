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
    public partial class F_U_AEOut : Form
    {
        bool isSave = false;
        bool isSearch = false;
        bool isAdmin = false;
        int numberSearch = 0;

        public F_U_AEOut(bool _isSearch = false, int _numberSearch = 0, bool _isAdmin = false)
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
            if (isSearch == true)
            {
                try
                {
                    if (!isAdmin)
                    {
                        button1.Enabled = button3.Enabled =
                            button4.Enabled = button6.Enabled = textNumber.Enabled = comboBox1.Enabled = dateTime.Enabled = false;
                    }
                    dataGrid.DataSource = query.GetOutDetailByNumber(numberSearch).Tables[0].DefaultView;

                    textNumber.Text = query.GetOutByNumber(numberSearch).Tables[0].Rows[0][0].ToString();
                    dateTime.Value = Convert.ToDateTime(query.GetOutByNumber(numberSearch).Tables[0].Rows[0][1]);
                    comboBox1.Text = query.GetOutByNumber(numberSearch).Tables[0].Rows[0][2].ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Данные по расходу не найдены\n" + ex.Message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                updateOutDetail();
            }
        }

        void updateOutDetail()
        {
            SqlQuery query = new SqlQuery();
            try
            {
                if (isSearch == false)
                    dataGrid.DataSource = query.GetOutDetail().Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            F_U_AEOutPos F_U_AEO = new F_U_AEOutPos(this, 0);

            F_U_AEO.Text += " [Работает пользователь: " + Settings.Current_user + "]";
            F_U_AEO.Text = "Добавить позицию расхода";
            F_U_AEO.FormClosing += (s, ev) =>
            {
                updateOutDetail();
            };
            F_U_AEO.ShowDialog();
        }

        private void F_U_AEOut_Load(object sender, EventArgs e)
        {
            if (isSearch == false)
            {
                try
                {
                    //Вставляем пустые данные. Потом заполним
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Out (number_invoice)
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
        void CheckRowAndDelete(FormClosingEventArgs e)
        {
            if (isSave == false && isSearch == false && isAdmin == false)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(@"DELETE FROM Out_Detail WHERE id_out = (SELECT TOP (1) o.id FROM Out o ORDER BY o.id DESC)
                                                        DELETE FROM Out WHERE id = IDENT_CURRENT('Out')");
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
                    query.DeleteOutDetail(Convert.ToInt32(dataGrid.CurrentRow.Cells[0].Value.ToString()));
                    updateOutDetail();
                }
                else return;
            }
            catch
            {
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            F_U_AEOutPos F_U_AEO = new F_U_AEOutPos(this, 1);

            F_U_AEO.Text += " [Работает пользователь: " + Settings.Current_user + "]";
            F_U_AEO.Text = "Изменить позицию расхода";
            F_U_AEO.button1.Text = "Изменить";
            F_U_AEO.FormClosing += (s, ev) =>
            {
                updateOutDetail();
            };
            F_U_AEO.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlQuery query = new SqlQuery();
            try
            {
                if(textNumber.Text == null)
                {
                    MessageBox.Show("Введите номер документа", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                int i = 0, j = 0;
                query.UpdateOut(int.TryParse(textNumber.Text, out i) ? i : 0, dateTime.Value, int.TryParse(comboBox1.Text, out j) ? j : 0);
                isSave = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void F_U_AEOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            CheckRowAndDelete(e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlQuery query = new SqlQuery();
            try
            {
                //Открыть отчет
                using (Report report = new Report())
                {
                    report.Load(Settings.ReportInvoiceOut);
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
