using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CalculateCalories.Classes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using static MahApps.Metro.Controls.Dialogs.DialogCoordinator;

namespace CalculateCalories.ViewModels
{
    class RootViewModel : ViewModelBase
    {
        private ICommand _commandExit;
        private ICommand _commandAboutProgram;
        private ICommand _commandAboutDeveloper;
        private ICommand _commandOpenResult;
        private Person person;

        public static RootViewModel root;
        public RootViewModel()
        {
            CurrentContentVM = new AuthViewModel();
            root = this;
            DialogCoordinator = Instance;
        }        

        private ViewModelBase currentContentVM;
        public ViewModelBase CurrentContentVM
        {
            get
            {
                return currentContentVM;
            }
            set
            {
                //Главные формы реализуют интерфейс IGlobalFormable для того, 
                //чтобы не было перехода назад с, например, формы разработчиков на форму о программе
                //В CurrentGlobalVM будут записываться только формы, реализующие этот интерфейс.
                //В CurrentContentVM будут записываться все экземпляры VM
                currentContentVM = value;
                if (value is IGlobalFormable)
                    CurrentGlobalVM = value;
                RaisePropertyChanged(() => CurrentContentVM);
            }
        }
        /// <summary>
        /// Свойство для хранения форм, реализующих IGlobalFormable
        /// </summary>
        public ViewModelBase CurrentGlobalVM { get; set; }
        /// <summary>
        /// Свойство для работы с диалоговыми окнами
        /// </summary>
        public static IDialogCoordinator DialogCoordinator { get; set; }
        public ICommand CommandExit
        {
            get
            {
                return _commandExit ?? (_commandExit = new RelayCommand(() => {
                    Application.Current.Shutdown();
                }));
            }
        }
        public ICommand CommandAboutProgram
        {
            get
            {
                return _commandAboutProgram ?? (_commandAboutProgram = new RelayCommand(() => {
                    CurrentContentVM = new AboutProgramViewModel(CurrentContentVM);
                }));
            }
        }
        public ICommand CommandAboutDeveloper
        {
            get
            {
                return _commandAboutDeveloper ?? (_commandAboutDeveloper = new RelayCommand(() => {
                    CurrentContentVM = new AboutDeveloperViewModel(CurrentContentVM);
                }));
            }
        }
        public ICommand CommandOpenResult
        {
            get
            {
                return _commandOpenResult ?? (_commandOpenResult = new RelayCommand(async () => {
                    person = new Person();
                    OpenFileDialog dialog = new OpenFileDialog();
                    if (dialog.ShowDialog() == false) return; 
                    var file = person.OpenResultFromBinary(dialog.FileName);
                    if (file is null)
                        await DialogCoordinator.ShowMessageAsync(this, "error", "error");
                    else
                    {
                        //CurrentContentVM = new CaloriesViewModel(person.OpenResultFromBinary(dialog.FileName));
                    }
                }));
            }
        }
    }
}
