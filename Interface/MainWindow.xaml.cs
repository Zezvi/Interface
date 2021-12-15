using System.Windows;
using PetShop.Models;
using PetShop.ViewModels;

namespace PetShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow(User user)
        {

            InitializeComponent();
            DataContext = new MenuViewModel(user);
        }


    }
}
