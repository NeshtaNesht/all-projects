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
    public partial class F_U_Search : Form
    {
        int operation;//0 - поиск по приходу, 1 - поиск по расходу
        bool isAdmin = false;
        public F_U_Search(int oper, bool _isAdmin = false)
        {
            InitializeComponent();
            operation = oper;
            isAdmin = _isAdmin;
            if(oper == 1)            
                label1.Text = "Введите номер документа";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlQuery query = new SqlQuery();
            if (operation == 0)
            {
                try
                {
                    F_U_AEAdvent f1 = new F_U_AEAdvent(true, Convert.ToInt32(textBox1.Text), isAdmin);
                    f1.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else
            {
                try
                {
                    F_U_AEOut f2 = new F_U_AEOut(true, Convert.ToInt32(textBox1.Text), isAdmin);
                    f2.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Settings.textOnlyNumber(e);
        }
    }
}
