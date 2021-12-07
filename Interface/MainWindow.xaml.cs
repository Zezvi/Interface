using System.Windows;
using PetShop.Models;
using PetShop.Views.CheckView;
using PetShop.Views.GoodsView;

namespace PetShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User current;
        public MainWindow(User user)
        {
            InitializeComponent();
            current = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GoodsList goodsList = new GoodsList(current);
            goodsList.ShowDialog();
        }

        private void btnSupplier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGoods_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
