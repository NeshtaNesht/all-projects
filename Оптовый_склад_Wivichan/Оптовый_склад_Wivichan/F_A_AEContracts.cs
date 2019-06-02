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
    public partial class F_A_AEContracts : Form
    {
        F_A_Contracts parentForm;
        int operation;
        public F_A_AEContracts(F_A_Contracts _parentForm, int oper)
        {
            InitializeComponent();
            operation = oper;
            parentForm = _parentForm;
            Fill();
        }
        void Fill()
        {
            SqlQuery query = new SqlQuery();
            if (operation == 1)
                textDur.Text = parentForm.dataGrid.CurrentRow.Cells[1].Value.ToString();
        }

        private void textName_KeyPress(object sender, KeyPressEventArgs e)
        {
            Settings.textOnlyNumber(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlQuery query = new SqlQuery();
            try
            {
                int i = 0;
                query.AddEditContract(Convert.ToInt32(parentForm.dataGrid.CurrentRow.Cells[0].Value.ToString()), checkBox1.Checked == true ? -1 : (int.TryParse(textDur.Text, out i) ? i : 0), operation);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textDur.Enabled == true)
                textDur.Enabled = false;
            else textDur.Enabled = true;
            textDur.Text = null;
        }
    }
}
