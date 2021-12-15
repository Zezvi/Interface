using PetShop.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace PetShop.Records
{
    class BonusRecord : ViewModelBase
    {
        private int bonus_card_id;
        public int Bonuse_card_id
        {
            get { return bonus_card_id; }
            set { bonus_card_id = value; OnPropertyChanged("Bonuse_card_id"); }
        }
        private int? bonus;
        public int? Bonus
        {
            get { return bonus; }
            set { bonus = value; OnPropertyChanged("Bonuse"); }
        }
        private string card_number;
        public string Card_number
        {
            get { return card_number; }
            set { card_number = value; OnPropertyChanged("Card_number"); }
        }
        private ObservableCollection<BonusRecord> bonusRecords;
        public ObservableCollection<BonusRecord> BonusRecords
        {
            get
            {
                return bonusRecords;
            }
            set
            {
                bonusRecords = value;
                OnPropertyChanged("BonuseRecords");
            }
        }

        private void GoodModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("BonusRecords");
        }
    }
}
