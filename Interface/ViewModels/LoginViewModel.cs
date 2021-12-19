using PetShop.DataLayer;
using PetShop.Models;
using PetShop.Views.SellerView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PetShop.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        UserRepository userRepository;
        List<User> users;
        private ICommand _inputCommand;
        public LoginViewModel()
        {
            userRepository = new UserRepository();
            users = userRepository.Get();
        }
        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged("Password"); }

        }

        public ICommand InputCommand
        {
            get
            {
                if (_inputCommand == null)
                    _inputCommand = new RelayCommand(param => Input(), null);

                return _inputCommand;
            }
        }

        public void Input()
        {
            if (!String.IsNullOrEmpty(Password) && !String.IsNullOrEmpty(Login))
            {
                User user = users.FirstOrDefault(n => n.login == Login && n.password == Password);
                if (user != null)
                {
                    if (user.role == 3)
                    {
                        Views.ManagerView.ManagerMenu mainWindow = new Views.ManagerView.ManagerMenu(user);
                        mainWindow.ShowDialog();
                    }
                    else if (user.role == 2)
                    {
                        SellerMenu mainWindow = new SellerMenu(user);
                        mainWindow.ShowDialog();
                    }
                    else if (user.role == 1)
                    {
                        MainWindow mainWindow = new MainWindow(user);
                        mainWindow.ShowDialog();
                    }

                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином не найден!");
                }
            }

        }



    }
}
