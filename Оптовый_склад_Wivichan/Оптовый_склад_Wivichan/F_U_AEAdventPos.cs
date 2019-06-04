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
    public partial class F_U_AEAdventPos : Form
    {
        F_U_AEAdvent parentForm; //Ссылка на родительскую форму
        int operation; //Номер операции
        int id = 0; //Переменная для хранения id
        public F_U_AEAdventPos(F_U_AEAdvent _parentForm, int oper)
        {
            InitializeComponent();
            parentForm = _parentForm;
            operation = oper;
            //Заполняем форму данными
            Fill();
        }

        void Fill()
        {
            SqlQuery query = new SqlQuery();
            for (int i = 0; i < query.GetAllUnits().Tables[0].Rows.Count; i++)
                comboUnits.Items.Add(query.GetAllUnits().Tables[0].Rows[i][0].ToString());
            //Если это изменение данных, заполняем данные из выделенной строки datagrid родителя
            if(operation == 1)
            {
                id = Convert.ToInt32(parentForm.dataGrid.CurrentRow.Cells[0].Value.ToString());
                textName.Text = parentForm.dataGrid.CurrentRow.Cells[1].Value.ToString();
                comboUnits.Text = parentForm.dataGrid.CurrentRow.Cells[2].Value.ToString();
                textCount.Text = parentForm.dataGrid.CurrentRow.Cells[3].Value.ToString();
                textPrice.Text = parentForm.dataGrid.CurrentRow.Cells[4].Value.ToString();
                //Обновляем общую цену
                updateTotal();
            }
        }

        void updateTotal()
        {
            double c = 0, p = 0;
            textTotal.Text = ((double.TryParse(textCount.Text, out c) ? c : 0) * (double.TryParse(textPrice.Text, out p) ? p : 0)).ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            SqlQuery query = new SqlQuery();
            try
            {
                //Переменные для попытки парсинга
                int i = 0;
                double d = 0;
                int p = 0;
                query.AddEditAdventDetail(id, textName.Text, comboUnits.Text, int.TryParse(textCount.Text, out i) ? i : 0, double.TryParse(textPrice.Text, out d) ? d : 0, operation);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            Settings.textOnlyNumber(e);
        }

        private void textPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            Settings.textForFloat(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textPrice_TextChanged(object sender, EventArgs e)
        {
            updateTotal();
        }

        private void textCount_TextChanged(object sender, EventArgs e)
        {
            updateTotal();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
