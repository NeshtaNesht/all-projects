using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculateCalories
{
    /// <summary>
    /// Логика взаимодействия для AuthControl.xaml
    /// </summary>
    public partial class AuthControl : UserControl
    {
        public static string Fio;
        Window parentWindow;
        public AuthControl(Window _parentWindow)
        {
            InitializeComponent();
            parentWindow = _parentWindow;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Fio = textLogin.Text;
            parentWindow.Content = new CalculateCalories.Controls.CalculateControl();
        }
    }
}
