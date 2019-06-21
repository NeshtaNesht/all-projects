using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    class CalculateViewModel : ViewModelBase
    {
        private int? _age;
        private string _gender;
        private double? _weight;
        private int? _stature;
        private ObservableCollection<string> _activity;
        private string _selectedActivity;
        private ICommand _commandCalculate;
        public CalculateViewModel()
        {
            //Создаем коллекцию активности
            Activity = new ObservableCollection<string>
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
                return _commandCalculate ?? (_commandCalculate = new RelayCommand(() => {
                    //await RootViewModel.DialogCoordinator.ShowMessageAsync(this, "Ваши данные", "Ваш возраст: " + Age + 
                    //                                                                            "\nВаш пол: " + Gender + 
                    //                                                                            "\nВаш вес: " + Weight + " кг." +
                    //                                                                            "\nВаш рост: " + Stature + " см." +
                    //                                                                            "\nВаша активность: " + SelectedActivity);

                }));
            }
        }
        public int? Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
                RaisePropertyChanged(() => Age);
            }
        }
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                RaisePropertyChanged(() => Gender);
            }
        }
        public double? Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                _weight = value;
                RaisePropertyChanged(() => Weight);
            }
        }
        public int? Stature
        {
            get
            {
                return _stature;
            }
            set
            {
                _stature = value;
                RaisePropertyChanged(() => Stature);
            }
        }
        public ObservableCollection<string> Activity
        {
            get
            {
                return _activity;
            }
            set
            {
                _activity = value;
                RaisePropertyChanged(() => Activity);
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
