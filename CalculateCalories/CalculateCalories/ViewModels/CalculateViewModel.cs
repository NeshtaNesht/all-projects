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
    class CalculateViewModel : ViewModelBase
    {
        //private int? _age;
        //private string _gender;
        //private double? _weight;
        //private int? _stature;
        private Person _person;
        private ObservableCollection<string> _list_activity;
        private string _selectedActivity;
        private ICommand _commandCalculate;
        public CalculateViewModel(string _fio)
        {
            Person = new Person
            {
                FIO = _fio
            };
            //Создаем коллекцию активности
            List_Activity = new ObservableCollection<string>
            {
                "Минимум/отсутствие физической нагрузки",
                "Основной обмен",
                "3 раза в неделю",
                "5 раз в неделю",
                "5 раз в неделю (интенсивно)",
                "Каждый день",
                "Каждый день интенсивно или 2 раза в день",
                "Ежедневная физическая нагрузка + физическая работа"
            };
        }
        public ICommand CommandCalculate
        {
            get
            {
                return _commandCalculate ?? (_commandCalculate = new RelayCommand(async () => {
                    await RootViewModel.DialogCoordinator.ShowMessageAsync(this, "Ваши данные", "Ваш возраст: " + Person.Age +
                                                                                                "\nВаш пол: " + Person.Gender +
                                                                                                "\nВаш вес: " + Person.Weight + " кг." +
                                                                                                "\nВаш рост: " + Person.Stature + " см." +
                                                                                                "\nВаша активность: " + Person.Activity + 
                                                                                                "\nВаше имя: " + Person.FIO);

                }));
            }
        }

        //public int? Age
        //{
        //    get
        //    {
        //        return _age;
        //    }
        //    set
        //    {
        //        _age = value;
        //        RaisePropertyChanged(() => Age);
        //    }
        //}
        //public string Gender
        //{
        //    get
        //    {
        //        return _gender;
        //    }
        //    set
        //    {
        //        _gender = value;
        //        RaisePropertyChanged(() => Gender);
        //    }
        //}
        //public double? Weight
        //{
        //    get
        //    {
        //        return _weight;
        //    }
        //    set
        //    {
        //        _weight = value;
        //        RaisePropertyChanged(() => Weight);
        //    }
        //}
        //public int? Stature
        //{
        //    get
        //    {
        //        return _stature;
        //    }
        //    set
        //    {
        //        _stature = value;
        //        RaisePropertyChanged(() => Stature);
        //    }
        //}
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
        public string SelectedActivity
        {
            get
            {
                return _selectedActivity;
            }
            set
            {
                _selectedActivity = value;
                RaisePropertyChanged(() => SelectedActivity);
            }
        }


    }
}
