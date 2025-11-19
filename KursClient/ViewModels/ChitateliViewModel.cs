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
                              Chitateli chitatel = new Chitateli();
                              chitatel.FirstName = window.Chitatel.FirstName;
                              chitatel.LastName = window.Chitatel.LastName;
                              chitatel.Email = window.Chitatel.Email;
                              chitatel.Address = window.Chitatel.Address;
                              chitatel.Phone = window.Chitatel.Phone;
                              await chitateliService.Add(chitatel);
                              Load();
                          }
                      }
                      catch { }
                  }));
            }
        }

    }
}
