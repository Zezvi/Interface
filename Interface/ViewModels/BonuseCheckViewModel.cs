using PetShop.DataLayer;
using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PetShop.ViewModels
{
    class BonuseCheckViewModel : ViewModelBase
    {

        BonusCardRepository cardRepository;
        private int? bonuses;

        public int? Bonuses
        {
            get
            {
                return bonuses;
            }
            set
            {
                bonuses = value;
                OnPropertyChanged("Bonuses");
            }
        }
        private List<BonusCard> cards;

        private List<string> card_numbers;
        public List<string> Card_numbers
        {
            get { return card_numbers; }
            set
            {
                card_numbers = value;
                OnPropertyChanged("Card_numbers");
            }
        }
        public BonuseCheckViewModel()
        {
            cardRepository = new BonusCardRepository();
            cards = cardRepository.Get();
            card_numbers = new List<string>();
            foreach (var item in cards)
            {
                card_numbers.Add(item.card_number);
            }

        }
        private int? bonuse_card_id;
        public int? Bonuse_card_id
        {
            get { return bonuse_card_id; }
            set
            {
                bonuse_card_id = value;
                OnPropertyChanged("Bonuse_card_id");
            }
        }
        private string card_number;
        public string Card_number
        {
            get { return card_number; }
            set
            {
                card_number = value;
                OnPropertyChanged("Card_number");
            }
        }

        private ICommand _checkCommand;

        public ICommand CheckCommand
        {
            get
            {
                if (_checkCommand == null)
                    _checkCommand = new RelayCommand(param => CheckBonuse(), null);

                return _checkCommand;
            }
        }
        public void CheckBonuse()
        {
            BonusCard card = cards.FirstOrDefault(n => n.card_number == Card_number);
            if (card != null)
            {
                Bonuses = card.bonus;
                Bonuse_card_id = card.bonus_card_id;
            }
            else
            {
                Bonuses = 0;
                Bonuse_card_id = 0;
            }
        }

    }
}
