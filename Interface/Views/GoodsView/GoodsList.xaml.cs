using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using PetShop.Models;
using PetShop.Views.PurchaseView;
using PetShop.Views.Temp;

namespace PetShop.Views.GoodsView
{

    public partial class GoodsList : Window
    {
        PetShopContext context;
        Dictionary<int, int> selectedProducts;
        User current;
        public GoodsList(User user)
        {
            InitializeComponent();
            context = new PetShopContext();
            List<Good> goods = context.Good.ToList();
            selectedProducts = new Dictionary<int, int>();
            current = user;
            updateDisplayData();
        }
        private void btnGoodsList_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProducts.Count > 0)
            {
                MakePurchase makePurchase = new MakePurchase(selectedProducts, current);
                makePurchase.ShowDialog();
            }
            updateDisplayData();
        }
        public void setGoods(int prod, int count)
        {
            selectedProducts.Add(prod, count);
        }

        private void btnClearSelected_Click(object sender, RoutedEventArgs e)
        {
            lxdisplaySelected.Items.Clear();
            selectedProducts.Clear();
        }

        private void goodsgrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (goodsgrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < goodsgrid.SelectedItems.Count; i++)
                {
                    Good good = goodsgrid.SelectedItems[i] as Good;
                    if (good != null)
                    {
                        if (!selectedProducts.ContainsKey(good.good_id))
                        {
                            CountForm countForm = new CountForm(this, good.good_id, good.count_stock);
                            countForm.ShowDialog();
                        }
                        lxdisplaySelected.Items.Clear();
                        foreach (KeyValuePair<int, int> entry in selectedProducts)
                        {
                            var item = context.Good.ToList<Good>().FirstOrDefault(n => n.good_id == entry.Key);
                            if (item != null && item.count_stock >= entry.Value)
                            {
                                lxdisplaySelected.Items.Add(" Название : " + item.name + " Количество " + entry.Value);
                            }
                            else if (item != null && item.count_stock < entry.Value)
                            {

                            }
                        }
                    }
                }
            }
        }
        void updateDisplayData()
        {

            List<Good> goods = new List<Good>();
            using (PetShopContext ctx = new PetShopContext())
            {
                goods = ctx.Good.ToList();
                goodsgrid.ItemsSource = null;
                goodsgrid.ItemsSource = goods;
                lxdisplaySelected.Items.Clear();
                selectedProducts.Clear();
            }

        }


    }
}
