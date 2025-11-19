using KursClient.Models;
using KursClient.Services;
using KursClient.Utills;
using KursClient.Views;
using KursProjectISP31.Utills;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KursClient.ViewModels
{
    public class ChitateliViewModel:ViewModelBase
    {
        private ChitateliService chitateliService;
        private ObservableCollection<Chitateli> chitateliList;
        public ObservableCollection<Chitateli> ChitateliList
        {
            get { return chitateliList; }
            set
            {
                if (chitateliList != value)
                {
                    chitateliList = value;
                    OnPropertyChanged(nameof(ChitateliList));
                }
            }
        }

        private Chitateli selected;
        public Chitateli Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        public ChitateliViewModel()
        {
            chitateliService=new ChitateliService();
            Load();
        }
        private void Load()
        {
            try
            {
                ChitateliList = null!;
                Task<List<Chitateli>> task = Task.Run(() =>chitateliService.GetAll());
                ChitateliList =new ObservableCollection<Chitateli> (task.Result);
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(async obj =>
                  {
                      try
                      {
                          AddEditChitatel window = new AddEditChitatel(new Chitateli());
                          if (window.ShowDialog() == true)
                          {                             
                              await chitateliService.Add(window.Chitatel);
                              Load();
                          }
                      }
                      catch { }
                  }));
            }
        }
        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand(async obj =>
                  {
                      Chitateli chitatel = (obj as Chitateli)!;
                      AddEditChitatel window = new AddEditChitatel(chitatel);
                      if (window.ShowDialog() == true)
                      {
                          await chitateliService.Update(window.Chitatel);
                      }
                  }));
            }
        }
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(async obj =>
                  {
                      Chitateli chitatel = (obj as Chitateli)!;
                      MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить объект " + chitatel!.FirstName + " " +chitatel.LastName, "Удаление объекта", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                      if (result == MessageBoxResult.Yes)
                      {
                          await chitateliService.Delete(chitatel);
                          Load();
                      }
                  }));
            }
        }
    }
}
