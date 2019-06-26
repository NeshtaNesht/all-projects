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
    class AboutDeveloperViewModel : ViewModelBase
    {
        private ViewModelBase Parent;
        private ICommand _commandBack;
        public AboutDeveloperViewModel(ViewModelBase _parent)
        {
            //Если переход осуществлен с главной формы - присваиваем ссылку.
            //Иначе присваиваем глобальную форму через CurrentGlobalVM, куда пишутся только все формы
            //Реализующие IGlobalFormable
            _ = _parent is IGlobalFormable ? Parent = _parent : Parent = RootViewModel.root.CurrentGlobalVM;
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
