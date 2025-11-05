using KursClient.Models;
using KursClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KursClient.Views
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private AuthService authService;

        public RegisterWindow()
        {
            InitializeComponent();
            authService = new AuthService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Password.Password == PasswordRepeat.Password)
            {
                Person person = new Person { Email = Login.Text, Password = Password.Password };
                Task<string> message=Task.Run(() => Register(person));
                MessageBox.Show(message.Result);
                this.Close();
            }
        }
        private async Task<string> Register(Person person)
        {
            return await authService.Register(person);
        }
    }
}
