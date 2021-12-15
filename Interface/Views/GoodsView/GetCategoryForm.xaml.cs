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
    /// Interaction logic for GetCategoryForm.xaml
    /// </summary>
    public partial class GetCategoryForm : Window
    {
        public GetCategoryForm()
        {
            InitializeComponent();
            CategoryRepository categoryRepository = new CategoryRepository();
            greedCat.ItemsSource = categoryRepository.Get().ToList();
        }
        public int Category_id { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (greedCat.SelectedItems.Count > 0)
            {
                for (int i = 0; i < greedCat.SelectedItems.Count; i++)
                {
                    Category category = greedCat.SelectedItems[i] as Category;
                    if (category != null)
                    {
                        Category_id = category.category_id;
                    }
                }
            }
            this.DialogResult = true;
        }
    }
}
