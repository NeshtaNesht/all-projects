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
    public partial class FAdmin : Form
    {
        public FAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new F_A_Suppliers().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new F_A_Customers().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new F_A_Contracts().ShowDialog();
        }

        private void FAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.connectionClose();
            FAuthorization auth = new FAuthorization();
            auth.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
