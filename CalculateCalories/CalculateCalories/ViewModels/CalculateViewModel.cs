using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    class CalculateViewModel : ViewModelBase, IGlobalFormable
    {
        private Person _person;
        private ObservableCollection<string> _list_activity;        
        private ICommand _commandCalculate;
        public CalculateViewModel(Person person)
        {
            Person = person;
        }
        public ICommand CommandCalculate
        {
            get
            {
                return _commandCalculate ?? (_commandCalculate = new RelayCommand(() => {                    
                    RootViewModel.root.CurrentContentVM = new CaloriesViewModel(Person);
                }));
            }
        }
        public Person Person
        {
            get
            {
                return _person;
            }
            set
            {
                _person = value;
                RaisePropertyChanged(() => Person);
            }
        }
        public ObservableCollection<string> List_Activity
        {
            get
            {
                return _list_activity;
            }
            set
            {
                _list_activity = value;
                RaisePropertyChanged(() => List_Activity);
            }
        }

    }
}
