using PetShop.DataLayer;
using PetShop.Models;
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

namespace PetShop
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        UserRepository userLayer;
        public LoginForm()
        {
            InitializeComponent();
            userLayer = new UserRepository();

        }

        private void btnInput_Click(object sender, RoutedEventArgs e)
        {

            string login = txtLogin.Text;
            string password = txtPassword.Text;

            User user = userLayer.Get().FirstOrDefault(n => n.login == login && n.password == password);
            if (user != null)
            {
                MainWindow mainWindow = new MainWindow();
                this.Hide();
                mainWindow.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Не удалось найти пользователя");
            }





        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
