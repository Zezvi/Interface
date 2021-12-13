using PetShop.DataLayer;
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
        BonusCardRepository cardRepository;
        private int bonus_card_id;
        private int? bonuseSize;
        public BonuseForm()
        {
            InitializeComponent();
            cardRepository = new BonusCardRepository();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        void PassInt(Object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int iValue = -1;

            if (Int32.TryParse(textBox.Text, out iValue) == false)
            {
                TextChange textChange = e.Changes.ElementAt<TextChange>(0);
                int iAddedLength = textChange.AddedLength;
                int iOffset = textChange.Offset;
                textBox.Text = textBox.Text.Remove(iOffset, iAddedLength);
            }
        }
        public int Bonuse_Card_Id
        {
            get { return bonus_card_id; }
        }
        public int? Bonuse_Size
        {
            get { return bonuseSize; }
        }

        private void txtBonuse_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBonuse.Text))
            {
                lbTemp.Items.Clear();
                PassInt(sender, e);
                var cards = cardRepository.Get().Where(n => n.card_number.Contains(txtBonuse.Text));
                foreach (var item in cards)
                {
                    lbTemp.Items.Add(item.card_number);
                }
                var card = cardRepository.Get().FirstOrDefault(n => n.card_number == txtBonuse.Text);
                if (card != null)
                {
                    tbTemp.Text = card.bonus.ToString();
                    bonus_card_id = card.bonus_card_id;
                    bonuseSize = card.bonus;
                }
                else
                {
                    tbTemp.Text = "0";
                    bonus_card_id = 0;
                }
            }


        }
    }
}
