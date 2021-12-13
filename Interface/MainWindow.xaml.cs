using System.Windows;
using PetShop.Models;
using PetShop.Views.CheckView;
using PetShop.Views.GoodsView;
using PetShop.Views.PurchaseView;
using PetShop.Views.UserView;

namespace PetShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User current;
        public MainWindow()
        {

            InitializeComponent();
            User user = new User();
            user.fio = "asdfsdfsad";
            user.login = "asd";
            user.password = "asdasd";
            user.role = 1;
            current = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MakePurchase makePurchase = new MakePurchase(current);
            makePurchase.ShowDialog();
        }

        private void btnSupplier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGoods_Click(object sender, RoutedEventArgs e)
        {
            ManageGoods manageGoods = new ManageGoods();
            manageGoods.ShowDialog();
        }

        private void btnActions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBonus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            CheckList view = new CheckList();
            view.ShowDialog();
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            UserDetails userDetails = new UserDetails();
            userDetails.ShowDialog();
        }
    }
}
