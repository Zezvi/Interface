using PetShop.DataLayer;
using PetShop.ViewModels;
using System;
using System.Linq;

using System.Windows;
using System.Windows.Controls;


namespace PetShop.Views.Temp
{
    /// <summary>
    /// Interaction logic for BonuseForm.xaml
    /// </summary>
    public partial class BonuseForm : Window
    {

        public BonuseForm()
        {
            InitializeComponent();
            DataContext = new BonuseCheckViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
