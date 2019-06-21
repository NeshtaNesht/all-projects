using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateCalories.Classes
{
    abstract class Person
    {
        private readonly Person _person;
        public string FIO { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public double Weight { get; set; }
        public int Strature { get; set; }
        public string Activity { get; set; }

        public Person()
        {
            //FIO = _fio;
            //Age = _age;
            //Weight = _weight;
            //Strature = _strature;
            //Activity = _activity;
        }

        //Сколько нужно калорий в день, чтобы вес не менялся
        public virtual double CalculateCalories(Person person, double coefficient = 0)
        {
            //if (person is Male)//Если это мужчина
            //    return ((10 * Weight) + (6.25 * Strature) - (5 * Age) + 5) * coefficient;
            //else //Иначе женщина
            //    return ((10 * Weight) + (6.25 * Strature) - (5 * Age) - 161) * coefficient;
            return 0;
        }
        //Сколько нужно калорий в день для похудения
        public virtual double CalculateCaloriesWeightLoss(Person person)
        {
            return CalculateCalories(person) * 0.8;
        }
        //Сколько нужно калорий в день для похудения
        public virtual double CalculateCaloriesWeightLossFast(Person person)
        {
            return CalculateCalories(person) * 0.6;
        }
    }
}
