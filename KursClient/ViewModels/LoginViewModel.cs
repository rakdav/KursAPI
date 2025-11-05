using KursClient.Models;
using KursClient.Services;
using KursClient.Utills;
using KursClient.Views;
using KursProjectISP31.Utills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KursClient.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        private AuthService authService;

        public LoginViewModel()
        {
            authService = new AuthService();
        }

        private Visibility visibility;
        public Visibility Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }
        private string? login;
        public string Login
        {
            get { return login!; }
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        private string? password;
        public string LoginPassword
        {
            get { return password!; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(LoginPassword));
            }
        }
        private RelayCommand? loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                  (loginCommand = new RelayCommand(async obj =>
                  {
                      PasswordBox? password = obj as PasswordBox;
                      HttpClient client = new HttpClient();
                      Person user = new Person { Email = Login, Password = password!.Password };
                      Response response=await authService.SignIn(user);
                      if (response != null)
                      {
                          RegisterUser.UserName = response.username;
                          RegisterUser.access_token= response.access_token;
                          Visibility= Visibility.Hidden;
                          MainWindow window=new MainWindow();
                          window.Show();
                      }
                      else
                          MessageBox.Show("Пользователь с таким именем или паролем " +
                                  "не существует!");

                  }));
            }
        }
    }
}
