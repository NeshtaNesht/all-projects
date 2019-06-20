using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro.Controls.Dialogs;

namespace CalculateCalories.ViewModels
{
    public class AuthViewModel : ViewModelBase
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
                _fieldFio = value;
                RaisePropertyChanged(() => FieldFio);
            }
        }
        public ICommand CommandLogin //Это команда на кнопке
        {
            get
            {
                return _commandLogin ?? (_commandLogin = new RelayCommand(async () => {
                    if (!string.IsNullOrEmpty(FieldFio) && FieldFio.Equals("Admin"))
                        RootViewModel.root.CurrentContentVM = new CalculateViewModel();
                    else
                        await RootViewModel.DialogCoordinator.ShowMessageAsync(this, "Внимание", "Входная строка имела неверный формат");
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
