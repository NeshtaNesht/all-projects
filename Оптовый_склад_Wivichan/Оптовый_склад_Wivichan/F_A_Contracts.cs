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
    public partial class F_A_Contracts : Form
    {
        bool isDelete = false;
        SqlQuery query;
        public F_A_Contracts()
        {
            InitializeComponent();
            query = new SqlQuery();
            updateContracts();
        }

        private void radioDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (addCont.Enabled == true)
                addCont.Enabled = false;
            if (editCont.Enabled == true)
                editCont.Enabled = false;
            if (delCont.Enabled == true)
                delCont.Enabled = false;
            else
            {
                addCont.Enabled = true;
                editCont.Enabled = true;
                delCont.Enabled = true;
            }
            if (isDelete == false)
                isDelete = true;
            else isDelete = false;
            updateContracts();
            
        }

        private void F_A_Contracts_Load(object sender, EventArgs e)
        {
            
        }
        
        void updateContracts()
        {
            dataGrid.DataSource = query.GetAllContracts(isDelete).Tables[0].DefaultView;
        }

        private void addCont_Click(object sender, EventArgs e)
        {
            F_A_AEContracts f_A_AEC = new F_A_AEContracts(this, 0);

            f_A_AEC.Text += " [Работает пользователь: " + Settings.Current_user + "]";
            f_A_AEC.Text = "Добавить договор";
            f_A_AEC.FormClosing += (s, ev) =>
            {
                updateContracts();
            };
            f_A_AEC.ShowDialog();
        }

        private void editCont_Click(object sender, EventArgs e)
        {
            if (dataGrid.CurrentRow == null)
            {
                MessageBox.Show("Выберите строку для редактирования", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            F_A_AEContracts f_A_AEC = new F_A_AEContracts(this, 1);

            f_A_AEC.Text += " [Работает пользователь: " + Settings.Current_user + "]";
            f_A_AEC.Text = "Изменить договор";
            f_A_AEC.button1.Text = "Изменить";
            f_A_AEC.FormClosing += (s, ev) =>
            {
                updateContracts();
            };
            f_A_AEC.ShowDialog();
        }

        private void delCont_Click(object sender, EventArgs e)
        {
            if (dataGrid.CurrentRow == null)
            {
                MessageBox.Show("Сначала выберите строку для удаления", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            DialogResult dr = MessageBox.Show("Вы собираетесь удалить строку с id = " + dataGrid.CurrentRow.Cells[0].Value.ToString(), "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                SqlQuery query = new SqlQuery();
                query.DeleteContract(Convert.ToInt32(dataGrid.CurrentRow.Cells[0].Value.ToString()));
                updateContracts();
            }
            else return;
        }
    }
}
