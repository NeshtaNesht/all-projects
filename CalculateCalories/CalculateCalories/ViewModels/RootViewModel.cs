using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CalculateCalories.ViewModels
{
    class RootViewModel : ViewModelBase
    {
        public RootViewModel()
        {
            CurrentContentVM = new AuthViewModel();
        }

        private object currentContentVM;
        public object CurrentContentVM
        {
            get
            {
                return currentContentVM;
            }
            set
            {
                currentContentVM = value;
                RaisePropertyChanged(() => CurrentContentVM);
            }
        }
    }
}
