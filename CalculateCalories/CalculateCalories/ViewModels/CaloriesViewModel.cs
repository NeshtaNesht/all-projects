using CalculateCalories.Classes;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateCalories.ViewModels
{
    class CaloriesViewModel : ViewModelBase
    {        
        public CaloriesViewModel(Person person)
        {
            Person = person;
        }
        private Person Person { get; set; }
        public double? NeedCaloriesOne { get => Person.CalculateCalories(); }
        public double? NeedCaloriesTwo { get => Person.CalculateCaloriesWeightLoss();  }
        public double? NeedCaloriesThree { get => Person.CalculateCaloriesWeightLossFast(); }

    }
}
