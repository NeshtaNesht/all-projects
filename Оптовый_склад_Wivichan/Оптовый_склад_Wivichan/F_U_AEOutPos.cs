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
    public partial class F_U_AEOutPos : Form
    {
        F_U_AEOut parentForm;
        int operation;
        int id = 0;
        public F_U_AEOutPos(F_U_AEOut _parentForm, int oper)
        {
            InitializeComponent();
            parentForm = _parentForm;
            operation = oper;
            Fill();
        }
        void Fill()
        {
            try
            {
                SqlQuery query = new SqlQuery();

                label5.Text = "";
                label6.Text = "";
                
                for (int i = 0; i < query.GetAllProducts().Tables[0].Rows.Count; i++)
                    comboBox1.Items.Add(query.GetAllProducts().Tables[0].Rows[i][0].ToString());
               

                if (operation == 1)
                {
                    id = Convert.ToInt32(parentForm.dataGrid.CurrentRow.Cells[0].Value.ToString());
                    //label5.Text += query.GetCountPriceByProduct(comboBox1.Text).Tables[0].Rows[0][0].ToString();
                    label6.Text += query.GetCountPriceByProduct(comboBox1.Text).Tables[0].Rows[0][1].ToString();
                    textCount.Text = parentForm.dataGrid.CurrentRow.Cells[1].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); ;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlQuery query = new SqlQuery();
            try
            {
                if(int.Parse(label5.Text) < int.Parse(textCount.Text))
                {
                    MessageBox.Show("Количество расхода не может быть больше количества остатков на складе", "Внимание", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }                    
                int i = 0;
                double d = 0;
                int p = 0;
                query.AddEditOutDetail(id, comboBox1.Text, int.TryParse(textCount.Text, out i) ? i : 0, double.TryParse(textPrice.Text, out d) ? d : 0, operation);
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

        private void textCount_TextChanged(object sender, EventArgs e)
        {
            updateTotal();
        }

        private void textPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            Settings.textForFloat(e);
        }

        private void textPrice_TextChanged(object sender, EventArgs e)
        {
            updateTotal();
        }
        
        void updateTotal()
        {
            double c = 0, p = 0;
            textTotal.Text = ((double.TryParse(textCount.Text, out c) ? c : 0) * (double.TryParse(textPrice.Text, out p) ? p : 0)).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlQuery query = new SqlQuery();
            int a = 0, b = 0;
            int j = (int.TryParse(query.GetCountPriceByProduct(comboBox1.Text).Tables[0].Rows[0][0].ToString(), out a) ? a : 0)
                - (int.TryParse(query.GetCountProductOutDetail(comboBox1.Text).Tables[0].Rows[0][0].ToString(), out b) ? b : 0);
            label5.Text = j.ToString();
            label6.Text = query.GetCountPriceByProduct(comboBox1.Text).Tables[0].Rows[0][1].ToString();
        }
    }
}
