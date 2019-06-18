using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace CalculateCalories.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        private string _fieldFio;
        public string FieldFio
        {
            get
            {
                return _fieldFio;
            }
            set
            {
                _fieldFio = value;
                RaisePropertyChanged(() => FieldFio);
            }
        }

        public ICommand CommandLogin
        {
            get
            {
                return _commandLogin ?? (_commandLogin = new RelayCommand(() => {
                    if (!string.IsNullOrEmpty(FieldFio))
                    {
                        MessageBox.Show("Добро пожаловать, " + FieldFio);
                    }
                    else
                        MessageBox.Show("Вы не ввели ФИО");
                }));
            }
        }
        public ICommand CommandExit
        {
            get
            {
                return _commandExit ?? (_commandExit = new RelayCommand(() => {
                    Application.Current.Shutdown();
                }));
            }
        }

        private ICommand _commandLogin;
        private ICommand _commandExit;

    }
}
