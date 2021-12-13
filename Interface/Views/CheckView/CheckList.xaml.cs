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

namespace PetShop.Views.CheckView
{
    /// <summary>
    /// Interaction logic for CheckList.xaml
    /// </summary>
    public partial class CheckList : Window
    {
        UserRepository userLayer;
        BonusCardRepository BonusCardLayer;
        CheckRepository checkLayer;
        public CheckList()
        {
            InitializeComponent();
            userLayer = new UserRepository();
            BonusCardLayer = new BonusCardRepository();
            checkLayer = new CheckRepository();
            checkgrid.ItemsSource = checkLayer.Get();

        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            if (checkgrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < checkgrid.SelectedItems.Count; i++)
                {
                    string str = "";
                    Check check = checkgrid.SelectedItems[i] as Check;
                    if (check != null)
                    {
                        User user = userLayer.GetOne(check.user_id);
                        if (user != null)
                        {
                            str += "\nПродавец : " + user.fio;
                        }
                        BonusCard card = BonusCardLayer.GetOne(check.bonus_card_id);
                        if (card != null)
                        {
                            str += "Бонусная карта : " + card.card_number + "\n Количество бонусов : " + card.bonus;

                        }
                    }
                    str += "\nДата покупки : " + check.date_sale;
                    str += "\nСтоимость покупки : " + check.total_price;
                    MessageBox.Show(str);
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
