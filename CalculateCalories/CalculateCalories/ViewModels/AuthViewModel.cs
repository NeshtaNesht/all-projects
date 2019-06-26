using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CalculateCalories.Classes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro.Controls.Dialogs;

namespace CalculateCalories.ViewModels
{
    public class AuthViewModel : ViewModelBase, IGlobalFormable
    {        
        public AuthViewModel()
        {
            
        }
        private string _fieldFio;
        public string FieldFio
        {
            get
            {
                return _fieldFio;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _fieldFio = value;
                else
                    value = null;
                RaisePropertyChanged(() => FieldFio);
            }
        }
        public ICommand CommandLogin //Это команда на кнопке
        {
            get
            {
                return _commandLogin ?? (_commandLogin = new RelayCommand(async () => {
                    if (_fieldFio == null)
                    {
                        await RootViewModel.DialogCoordinator.ShowMessageAsync(this,
                                                                                "Внимание",
                                                                                "Входная строка имела неверный формат",
                                                                                MessageDialogStyle.Affirmative,
                                                                                new MetroDialogSettings { AnimateShow = true });
                        return;
                    }
                    Person person = new Person
                    {
                        FIO = _fieldFio
                    };
                    RootViewModel.root.CurrentContentVM = new CalculateViewModel(person);                    
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
