using CalculateCalories.Classes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalculateCalories.ViewModels
{
    class CaloriesViewModel : ViewModelBase, IGlobalFormable
    {
        private Person _person;
        private double? _needCaloriesOne;
        private double? _needCaloriesTwo;
        private double? _needCaloriesThree;
        private List<Person.Grid> _gridCalories;
        private ICommand _saveCommand;
        public CaloriesViewModel(Person person)
        {
            Person = person;
            NeedCaloriesOne = Person.CalculateCalories();
            NeedCaloriesTwo = Person.CalculateCaloriesWeightLoss();
            NeedCaloriesThree = Person.CalculateCaloriesWeightLossFast();
            GridCalories = Person.GetGridCalories();
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
        public double? NeedCaloriesOne
        {
            get
            {
                return _needCaloriesOne;
            }
            set
            {
                _needCaloriesOne = value;
                RaisePropertyChanged(() => NeedCaloriesOne);
            }
        }
        public double? NeedCaloriesTwo
        {
            get
            {
                return _needCaloriesTwo;
            }
            set
            {
                _needCaloriesTwo = value;
                RaisePropertyChanged(() => NeedCaloriesTwo);
            }
        }
        public double? NeedCaloriesThree
        {
            get
            {
                return _needCaloriesThree;
            }
            set
            {
                _needCaloriesThree = value;
                RaisePropertyChanged(() => NeedCaloriesThree);
            }
        }
        public List<Person.Grid> GridCalories
        {
            get
            {
                return _gridCalories;
            }
            set
            {
                _gridCalories = value;
                RaisePropertyChanged(() => GridCalories);
            }
        }  
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(async () => {
                    //var s = await RootViewModel.DialogCoordinator.ShowInputAsync(this, "Имя файла", "Введите имя файла");                    
                    //if (s == null)
                    //    return;
                    //string result = Person.SaveResultInXML(GridCalories, s);
                    //if (result == null)
                    //    await RootViewModel.DialogCoordinator.ShowMessageAsync(this, "Сохранить", "Данные успешно сохранены");
                    //else
                    //    await RootViewModel.DialogCoordinator.ShowMessageAsync(this, "Ошибка при сохранении", result);
                }));
            }
        }

    }
}
