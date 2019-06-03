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
    public partial class FUser : Form
    {
        bool isExit = true;
        public FUser()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.connectionClose();
            FAuthorization auth = new FAuthorization();
            auth.Visible = true;
            isExit = false;
            Close();
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isExit)
                Application.Exit();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new F_U_AEAdvent().ShowDialog();
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new F_U_Search(0).ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getLastAdventOutByDate(radioButton1.Checked == true ? 0 : 1);
        }
        void getLastAdventOutByDate(int operation)
        {
            try
            {
                SqlQuery query = new SqlQuery();
                DataSet ds = query.GetLastAdventOutByDate(dateTime.Value, operation);
                if (ds.Tables[0].Rows.Count == 0)
                    MessageBox.Show("По выбранному фильтру информация не найдена", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else dataGrid.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при попытке получить данные из базы данных\n" + ex.Message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void поискToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new F_U_Search(1).ShowDialog();
        }

        private void добавитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new F_U_AEOut().ShowDialog();
        }
    }
}
