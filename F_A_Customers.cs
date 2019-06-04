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
    public partial class F_A_Customers : Form
    {
        SqlQuery query;
        public F_A_Customers()
        {
            InitializeComponent();
            query = new SqlQuery();
            updateCustomers();
        }

        private void addCust_Click(object sender, EventArgs e)
        {
            F_A_AECustomers f_A_AEC = new F_A_AECustomers(this, 0);

            f_A_AEC.Text += " [Работает пользователь: " + Settings.Current_user + "]";
            f_A_AEC.Text = "Добавить покупателя";
            f_A_AEC.FormClosing += (s, ev) =>
            {
                updateCustomers();
            };
            f_A_AEC.ShowDialog();
        }
        void updateCustomers()
        {
            dataGrid.DataSource = query.GetAllCustomers().Tables[0].DefaultView;
        }

        private void editCust_Click(object sender, EventArgs e)
        {
            if (dataGrid.CurrentRow == null)
            {
                MessageBox.Show("Выберите строку для редактирования", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            F_A_AECustomers f_A_AEC = new F_A_AECustomers(this, 1);

            f_A_AEC.Text += " [Работает пользователь: " + Settings.Current_user + "]";
            f_A_AEC.button1.Text = "Изменить";
            f_A_AEC.Text = "Изменить данные покупателя";
            f_A_AEC.FormClosing += (s, ev) =>
            {
                updateCustomers();
            };
            f_A_AEC.ShowDialog();
        }

        private void delCust_Click(object sender, EventArgs e)
        {
            if (dataGrid.CurrentRow == null)
            {
                MessageBox.Show("Сначала выберите строку для удаления", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            DialogResult dr = MessageBox.Show("Вы собираетесь удалить строку с id = " + dataGrid.CurrentRow.Cells[0].Value.ToString(), "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                SqlQuery query = new SqlQuery();
                query.DeleteCustomer(Convert.ToInt32(dataGrid.CurrentRow.Cells[0].Value.ToString()));
                updateCustomers();
            }
            else return;
        }
    }
}
