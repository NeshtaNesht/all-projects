using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MahApps.Metro.Controls.Dialogs;
using static MahApps.Metro.Controls.Dialogs.DialogCoordinator;

namespace CalculateCalories.ViewModels
{
    class RootViewModel : ViewModelBase
    {
        public static RootViewModel root;
        public RootViewModel()
        {
            CurrentContentVM = new AuthViewModel();
            root = this;
            DialogCoordinator = Instance;
        }        

        private ViewModelBase currentContentVM;
        public ViewModelBase CurrentContentVM
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
        public static IDialogCoordinator DialogCoordinator { get; set; }
    }
}
