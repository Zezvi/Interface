using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PetShop.DataLayer;
using PetShop.Models;

namespace PetShop.Views.PurchaseView
{
    /// <summary>
    /// Interaction logic for MakePurchase.xaml
    /// </summary>
    public partial class MakePurchase : Window
    {
        Dictionary<int, int> Order;
        BonusCardLayer bonusCardLayer;
        GoodsLayer goodsLayer;
        CheckLayer checkLayer;
        decimal TotalPrice = 0;
        ActionLayer actionLayer;
        List<BonusCard> bonuses;
        User current;
        public MakePurchase(Dictionary<int, int> order, User user)
        {
            InitializeComponent();
            goodsLayer = new GoodsLayer();
            actionLayer = new ActionLayer();
            bonusCardLayer = new BonusCardLayer();
            checkLayer = new CheckLayer();
            Order = order;
            current = user;
            InitOrder();
        }
        void InitOrder()
        {
            lxOrder.Items.Clear();
            foreach (KeyValuePair<int, int> entry in Order)
            {
                var item = goodsLayer.Get().ToList<Good>().FirstOrDefault(n => n.good_id == entry.Key);
                if (item != null && item.count_stock >= entry.Value)
                {
                    TotalPrice += item.price.Value;
                    lxOrder.Items.Add("Название : " + item.name + " Количество " + entry.Value + " Стоимость за единицу " + item.price);


                }
            }
            tbTotal.Text = TotalPrice.ToString();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            decimal? totalprice = 0;
            int? bonuses = 0;
            foreach (KeyValuePair<int, int> entry in Order)
            {
                var item = goodsLayer.GetOne(entry.Key);
                if (item != null)
                {
                    item.count_stock -= entry.Value;
                    totalprice += item.price;
                    goodsLayer.Edit(item);
                }
            }
            string cardnum = txtCardNumber.Text;
            BonusCard bonus = null;
            if (!String.IsNullOrEmpty(cardnum))
            {
                bonus = bonusCardLayer.Get().FirstOrDefault(n => n.card_number == cardnum);
                if (bonus != null)
                {
                    bonuses = bonus.bonus;
                    totalprice -= bonuses;
                    MessageBox.Show("Бонусы потрачены в размере : " + bonuses);
                }
            }

            Check check = new Check();
            check.total_price = totalprice;
            check.user_id = current.user_id;
            if (bonus != null) check.bonus_card_id = bonus.bonus_card_id;
            check.date_sale = DateTime.Now;
            if (checkLayer.Add(check))
            {
                MessageBox.Show("Покупка оплачена. Чек сформирован");
            }
            else
            {
                MessageBox.Show("Что то пошло не так");

            }



            tbTotal.Text = totalprice.ToString();
            lxOrder.Items.Clear();
            Close();
        }
        private void txtCardNumber_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            lxBonuses.Items.Clear();
            if (textBox.Text != "")
            {
                try
                {
                    List<BonusCard> cards = Find(textBox.Text);
                    foreach (var item in cards)
                    {
                        lxBonuses.Items.Add(item.card_number);
                    }
                    BonusCard bonus = bonusCardLayer.Get().FirstOrDefault(n => n.card_number == textBox.Text);
                    if (bonus != null)
                    {
                        tbBonuses.Text = bonus.bonus.ToString();
                    }
                    else
                    {
                        tbBonuses.Text = "0";
                    }
                }
                catch (Exception ex)
                {
                    lxBonuses.Items.Clear();
                }
            }
        }

        List<BonusCard> Find(string str)
        {
            List<BonusCard> cards = new List<BonusCard>();

            cards = bonusCardLayer.Get().Where(n => n.card_number.Contains(str)).ToList();

            return cards;
        }
    }
}
