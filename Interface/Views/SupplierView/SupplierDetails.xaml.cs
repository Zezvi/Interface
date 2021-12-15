using PetShop.ViewModels;
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

namespace PetShop.Views.SupplierView
{
    /// <summary>
    /// Interaction logic for SupplierDetails.xaml
    /// </summary>
    public partial class SupplierDetails : Window
    {
        public SupplierDetails()
        {
            InitializeComponent();
            DataContext = new SupplierViewModel();
        }
    }
}
