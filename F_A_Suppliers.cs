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
    public partial class F_A_Suppliers : Form
    {
        SqlQuery query;
        public F_A_Suppliers()
        {
            InitializeComponent();
            query = new SqlQuery();
            //Заполняем грид данными
            updateSuppliers();
        }

        private void addSup_Click(object sender, EventArgs e)
        {
            F_A_AESuppliers f_A_AES = new F_A_AESuppliers(this, 0);

            f_A_AES.Text += " [Работает пользователь: " + Settings.Current_user + "]";
            f_A_AES.Text = "Добавить поставщика";
            f_A_AES.FormClosing += (s, ev) =>
            {
                updateSuppliers();
            };
            f_A_AES.ShowDialog();
        }

        private void editSup_Click(object sender, EventArgs e)
        {
            if (dataGrid.CurrentRow.Cells[0].Value == null)
            {
                MessageBox.Show("Выберите строку для редактирования", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            F_A_AESuppliers f_A_AES = new F_A_AESuppliers(this, 1);

            f_A_AES.Text += " [Работает пользователь: " + Settings.Current_user + "]";
            f_A_AES.button1.Text = "Изменить";
            f_A_AES.Text = "Изменить данные поставщика";
            f_A_AES.FormClosing += (s, ev) =>
            {
                updateSuppliers();
            };
            f_A_AES.ShowDialog();
        }

        void updateSuppliers()
        {
            dataGrid.DataSource = query.GetAllSuppliers().Tables[0].DefaultView;
        }

        private void delSup_Click(object sender, EventArgs e)
        {
            if (dataGrid.CurrentRow == null)
            {
                MessageBox.Show("Сначала выберите строку для удаления", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            DialogResult dr = MessageBox.Show("Вы собираетесь удалить строку с id = " + dataGrid.CurrentRow.Cells[0].Value.ToString(), "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                SqlQuery query = new SqlQuery();
                query.DeleteSupplier(Convert.ToInt32(dataGrid.CurrentRow.Cells[0].Value.ToString()));
                updateSuppliers();
            }
            else return;
        }
    }
}
