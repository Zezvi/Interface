using PetShop.DataLayer;
using PetShop.Models;
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

namespace PetShop.Views.GoodsView
{
    /// <summary>
    /// Interaction logic for GetSupplierForm.xaml
    /// </summary>
    public partial class GetSupplierForm : Window
    {
        public GetSupplierForm()
        {
            InitializeComponent();
            SupplierRepository supplierRepository = new SupplierRepository();
            greedSup.ItemsSource = supplierRepository.Get().ToList();

        }
        public int? Supplier_id { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (greedSup.SelectedItems.Count > 0)
            {
                for (int i = 0; i < greedSup.SelectedItems.Count; i++)
                {
                    Supplier supplier = greedSup.SelectedItems[i] as Supplier;
                    if (supplier != null)
                    {
                        Supplier_id = supplier.supplier_id;
                    }
                }
            }
            this.DialogResult = true;
        }
    }
}
