using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalculateCalories.ViewModels
{
    class AboutProgramViewModel : ViewModelBase
    {
        private ViewModelBase Parent;
        private ICommand _commandBack;
        private string _aboutText;
        public AboutProgramViewModel(ViewModelBase _parent)
        {
            //Если переход осуществлен с главной формы - присваиваем ссылку.
            //Иначе присваиваем глобальную форму через CurrentGlobalVM, куда пишутся только все формы
            //Реализующие IGlobalFormable
            _ = _parent is IGlobalFormable ? Parent = _parent : Parent = RootViewModel.root.CurrentGlobalVM;
            AboutText = @"Программа предназначена для расчёта каллорий, которые необходимы для поддержания нужного для Вас веса";
        }

        public string AboutText
        {
            get
            {
                return _aboutText;
            }
            set
            {
                _aboutText = value;
                RaisePropertyChanged(() => AboutText);
            }
        }
        public ICommand CommandBack
        {
            get
            {
                return _commandBack ?? (_commandBack = new RelayCommand(() => {
                    RootViewModel.root.CurrentContentVM = Parent;
                }));
            }
        }

    }
}
