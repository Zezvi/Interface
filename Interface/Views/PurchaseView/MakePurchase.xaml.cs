using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PetShop.DataLayer;
using PetShop.Models;
using PetShop.ViewModels;

namespace PetShop.Views.PurchaseView
{
    /// <summary>
    /// Interaction logic for MakePurchase.xaml
    /// </summary>
    public partial class MakePurchase : Window
    {

        public MakePurchase(User user)
        {
            InitializeComponent();
            DataContext = new PurchaseViewModel(user);

        }


    }
}
