using KursClient.Models;
using KursClient.Services;
using KursClient.Utills;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

    }
}
