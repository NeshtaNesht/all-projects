using System;
using System.Windows.Forms;

namespace Оптовый_склад_Wivichan
{
    public partial class FAuthorization : Form
    {
        public FAuthorization()
        {
            InitializeComponent();
            Settings.connectionOpen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlQuery query = new SqlQuery();
            string login = textBox1.Text;
            string password = textBox2.Text;
            if (!login.Equals(query.GetUser(login, password).Tables[0].Rows[0][0].ToString()) 
                  && !password.Equals(query.GetUser(login, password).Tables[0].Rows[0][1].ToString()))
            {
                MessageBox.Show("Пользователь с таким логином и паролем не найден", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (query.GetUser(login, password).Tables[0].Rows[0][2].ToString() == "Администратор")
                    new FAdmin().Show();
                else new FUser().Show();
                //Запоминаем пользователя
                Settings.Current_user = login;
            }
            Visible = false;
        }

        private void FAuthorization_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.connectionClose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FAuthorization_Load(object sender, EventArgs e)
        {
            
        }
    }
}
