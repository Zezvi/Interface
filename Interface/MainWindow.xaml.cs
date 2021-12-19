using System.Windows;
using PetShop.Models;
using PetShop.ViewModels;

namespace PetShop
{
    public partial class MainWindow : Window
    {

        public MainWindow(User user)
        {

            InitializeComponent();
            DataContext = new MenuViewModel(user);
        }


    }
}
