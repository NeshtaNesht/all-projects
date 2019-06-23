using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateCalories.Classes
{
    public class Person
    {
        //private readonly Person _person;
        public string FIO { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public double? Weight { get; set; }
        public int? Stature { get; set; }
        public string Activity { get; set; }
        public ObservableCollection<string> List_Activity { get; } = new ObservableCollection<string>
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

        public Person()
        {

        }

        //Сколько нужно калорий в день, чтобы вес не менялся
        public virtual double? CalculateCalories()
        {
            if (IsMale())//Если это мужчина
                return ((10 * Weight) + (6.25 * Stature) - (5 * Age) + 5) * GetCoefficient();
            else //Иначе женщина
                return ((10 * Weight) + (6.25 * Stature) - (5 * Age) - 161) * GetCoefficient();
        }
        //Сколько нужно калорий в день для похудения
        public virtual double? CalculateCaloriesWeightLoss()
        {
            return CalculateCalories() * 0.8;
        }
        //Сколько нужно калорий в день для похудения
        public virtual double? CalculateCaloriesWeightLossFast()
        {
            return CalculateCalories() * 0.6;
        }
        public bool IsMale()
        {
            if (!Gender.Equals("Мужчина"))
                return false;
            return true;
        }
        public double GetCoefficient()
        {
            if (Activity.Equals(List_Activity[0])) //Минимум
                return 1.2;
            else if (Activity.Equals(List_Activity[1])) //Основной объем
                return 1.2;
            else if (Activity.Equals(List_Activity[2])) //3 раза в неделю
                return 1.375;
            else if (Activity.Equals(List_Activity[3])) //5 раз в неделю
                return 1.4625;
            else if (Activity.Equals(List_Activity[4])) //5 раз в неделю (интенсивно)
                return 1.55;
            else if (Activity.Equals(List_Activity[5])) //Каждый день
                return 1.6375;
            else if (Activity.Equals(List_Activity[6])) //Каждый день интенсивно или 2 раза в день
                return 1.725;
            else if (Activity.Equals(List_Activity[7])) //Ежедневная физ нагрузка + физ работа
                return 1.9;

            throw new Exception("Не работает");
        }
    }
}
