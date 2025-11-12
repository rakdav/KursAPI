using KursClient.Utills;
using KursProjectISP31.Utills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KursClient.ViewModels
{
    public class NavigationViewModel:ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        public ICommand HomeCommand { get; set; }
        public ICommand ChitateliCommand { get; set; }
        private void HomeView(object obj) => CurrentView = new HomeViewModel();
        private void ChitateliView(object obj) => CurrentView = new ChitateliViewModel();
        public NavigationViewModel()
        {
            HomeCommand = new RelayCommand(HomeView);
            ChitateliCommand = new RelayCommand(ChitateliView);
            CurrentView = new HomeViewModel();
        }
    }
}
