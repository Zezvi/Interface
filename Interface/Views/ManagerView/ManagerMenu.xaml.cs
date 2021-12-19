using PetShop.Models;
using PetShop.ViewModels;
using System.Windows;


namespace PetShop.Views.ManagerView
{
    /// <summary>
    /// Interaction logic for ManagerMenu.xaml
    /// </summary>
    public partial class ManagerMenu : Window
    {
        public ManagerMenu(User user)
        {
            InitializeComponent();
            DataContext = new MenuViewModel(user);
        }
    }
}
