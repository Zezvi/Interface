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
using PetShop.ViewModels;
using PetShop.Views.GoodsView;

namespace PetShop.Views.Temp
{
    /// <summary>
    /// Interaction logic for CountForm.xaml
    /// </summary>
    public partial class CountForm : Window
    {

        public CountForm()
        {
            InitializeComponent();
            DataContext = new CountGoodsViewModel();

        }
    }
}
