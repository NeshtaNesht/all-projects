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
    public partial class F_A_AECustomers : Form
    {
        F_A_Customers parentForm;
        int operation;
        public F_A_AECustomers(F_A_Customers _parentForm, int oper)
        {
            InitializeComponent();
            parentForm = _parentForm;
            operation = oper;
            Fill();
        }
        void Fill()
        {
            SqlQuery query = new SqlQuery();

            for (int i = 0; i < query.GetAllIdContracts().Tables[0].Rows.Count; i++)
                comboBox1.Items.Add(query.GetAllIdContracts().Tables[0].Rows[i][0].ToString());
            if (operation == 1)
            {
                try
                {
                    textName.Text = parentForm.dataGrid.CurrentRow.Cells[1].Value.ToString();
                    textAddress.Text = parentForm.dataGrid.CurrentRow.Cells[2].Value.ToString();
                    textPhone.Text = parentForm.dataGrid.CurrentRow.Cells[3].Value.ToString();
                    textInn.Text = parentForm.dataGrid.CurrentRow.Cells[4].Value.ToString();
                    comboBox1.Text = parentForm.dataGrid.CurrentRow.Cells[5].Value.ToString();
                }
                catch
                {
                    MessageBox.Show("Выберите строку для редактирования", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlQuery query = new SqlQuery();

            try
            {
                int i = 0;
                query.AddEditCustomer(Convert.ToInt32(parentForm.dataGrid.CurrentRow.Cells[0].Value.ToString()), textName.Text, textAddress.Text, textPhone.Text, textInn.Text, int.TryParse(comboBox1.Text, out i) ? i : 0, operation);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
