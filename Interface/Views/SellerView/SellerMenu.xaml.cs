using PetShop.Models;
using PetShop.ViewModels;
using System.Windows;


namespace PetShop.Views.SellerView
{
    public partial class SellerMenu : Window
    {
        public SellerMenu(User user)
        {
            InitializeComponent();
            DataContext = new MenuViewModel(user);

        }
    }
}
