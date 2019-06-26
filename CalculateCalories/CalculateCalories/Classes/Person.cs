using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CalculateCalories.Classes
{
    public class Person : ViewModelBase
    {
        #region PrivateField
        private ObservableCollection<Grid> _gridCalories;
        #endregion
        #region PublicField
        public string FIO { get; set; }
        public int? Age { get; set; }
        public Enum_Gender Gender { get; set; } = Enum_Gender.Мужской;
        public double? Weight { get; set; }
        public int? Stature { get; set; }
        public string Activity { get; set; } = "Основной обмен";
        public ObservableCollection<Grid> GridCalories
        {
            get
            {
                return _gridCalories;
            }
            set
            {
                _gridCalories = value;
            }
        }
        #endregion
        /// <summary>
        /// Перечисление полов
        /// </summary>
        public enum Enum_Gender
        {
            Мужской = 1,
            Женский = 2
        }
        /// <summary>
        /// Словарь объема нагрузок и их коэффициент
        /// </summary>
        public Dictionary<string, double> Dict_Activity { get; } = new Dictionary<string, double>
        {
            ["Минимум/отсутствие физической нагрузки"] = 1.2,
            ["Основной обмен"] = 1.2,
            ["3 раза в неделю"] = 1.375,
            ["5 раз в неделю"] = 1.4625,
            ["5 раз в неделю (интенсивно)"] = 1.55,
            ["Каждый день"] = 1.6375,
            ["Каждый день интенсивно или 2 раза в день"] = 1.725,
            ["Ежедневная физическая нагрузка + физическая работа"] = 1.9
        };
        /// <summary>
        /// Сколько нужно калорий в день, чтобы вес не менялся
        /// </summary>
        /// <returns>double результат для каждого пола. 0 - в случае исключения</returns>
        public virtual double? CalculateCalories()
        {
            try
            {
                if (IsMale())//Если это мужчина
                    return Math.Round(((10 * Weight.Value) + (6.25 * Stature.Value) - (5 * Age.Value) + 5) * GetCoefficient(), 2);
                else //Иначе женщина
                    return Math.Round(((10 * Weight.Value) + (6.25 * Stature.Value) - (5 * Age.Value) - 161) * GetCoefficient(), 2);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Сколько нужно калорий в день для похудения
        /// </summary>
        /// <returns></returns>
        public virtual double? CalculateCaloriesWeightLoss()
        {
            return CalculateCalories() * 0.8;
        }
        /// <summary>
        /// Сколько нужно калорий в день для похудения
        /// </summary>
        /// <returns></returns>
        public virtual double? CalculateCaloriesWeightLossFast()
        {
            return CalculateCalories() * 0.6;
        }
        /// <summary>
        /// Проверка пола
        /// </summary>
        /// <returns>true - если мужской</returns>
        public bool IsMale()
        {
            if (!Gender.Equals(Enum_Gender.Мужской.ToString()))
                return false;
            return true;
        }
        /// <summary>
        /// Метод получения коэффициента
        /// </summary>
        /// <returns></returns>
        public double GetCoefficient()
        {
            return Dict_Activity[Activity];

            throw new Exception("GetCoefficient() не работает");
        }
        /// <summary>
        /// Получение Списка объектов Grid для формирования DataGrid
        /// </summary>
        /// <returns></returns>
        public List<Grid> GetGridCalories()
        {
            var result = new List<Grid>();
            for (int i = 0; i < 7; i++)
            {
                Grid grid = new Grid();
                grid.Day = FirstColumn.Keys.ElementAt(i).ToString();
                grid.CalcCalories = FirstColumn.Values.ElementAt(i) * this.CalculateCalories();
                grid.CalcCaloriesWeightLoss = FirstColumn.Values.ElementAt(i) * this.CalculateCaloriesWeightLoss();
                grid.CalcCaloriesWeightLossFast = FirstColumn.Values.ElementAt(i) * this.CalculateCaloriesWeightLossFast();
                result.Add(grid);
            }
            return result;            
        }
        /// <summary>
        /// Словарь дней недели и коэффициент умножения
        /// </summary>
        public Dictionary<DayWeek, double> FirstColumn { get; } = new Dictionary<DayWeek, double>
        {
            [DayWeek.ПН] = 1,
            [DayWeek.ВТ] = 0.8,
            [DayWeek.СР] = 1.2,
            [DayWeek.ЧТ] = 1,
            [DayWeek.ПТ] = 0.9,
            [DayWeek.СБ] = 1.1,
            [DayWeek.ВС] = 1,
        };
        /// <summary>
        /// Дни недели
        /// </summary>
        public enum DayWeek
        {
            ПН = 1,
            ВТ = 2,
            СР = 3,
            ЧТ = 4,
            ПТ = 5,
            СБ = 6,
            ВС = 7
        }
        public string SaveResultInBinary(object objGraph, string fileName)
        {
            try
            {
                //XmlSerializer xmlSave = new XmlSerializer(typeof(Grid));
                BinaryFormatter binFormat = new BinaryFormatter();
                using (Stream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fileStream, objGraph);
                }
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
        public Grid OpenResultFromBinary(string fileName)
        {
            try
            {
                //XmlSerializer xmlSave = new XmlSerializer(typeof(Grid));
                BinaryFormatter binFormat = new BinaryFormatter();
                using (Stream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    Grid grid = (Grid)binFormat.Deserialize(fileStream);
                    return grid;
                }
            }
            catch
            {
                //Реализовать открытие файла!!!
                return null;
            }
        }
        [Serializable]
        /// <summary>
        /// Класс, представляющий собой ItemSource в DataGrid
        /// </summary>
        public class Grid
        {
            [DisplayName("Дни недели")]
            public string Day { get; set; }
            [DisplayName("Не изменяя вес")]
            public double? CalcCalories { get; set; }
            [DisplayName("Потеря веса")]
            public double? CalcCaloriesWeightLoss { get; set; }
            [DisplayName("Быстрая потеря веса")]
            public double? CalcCaloriesWeightLossFast { get; set; }                      
        }
    }

    
}
