using PetShop.ViewModels;
using System.Windows;


namespace PetShop.Views.GoodsView
{
    /// <summary>
    /// Interaction logic for ManageGoods.xaml
    /// </summary>
    public partial class ManageGoods : Window
    {
        public ManageGoods()
        {
            InitializeComponent();
            DataContext = new GoodViewModel();
        }
    }
}
