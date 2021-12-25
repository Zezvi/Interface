using PetShop.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace PetShop.Records
{
    class CheckRecord : ViewModelBase
    {
        private int check_id;
        public int Check_id
        {
            get { return check_id; }
            set
            {
                check_id = value;
                OnPropertyChanged("Check_id");
            }
        }
        private DateTime? date_sale;
        public DateTime? Date_sale
        {
            get { return date_sale; }
            set
            {
                date_sale = value;
                OnPropertyChanged("Date_sale");
            }
        }
        private decimal? total_price;
        public decimal? Total_price
        {
            get
            {
                return total_price;
            }
            set
            {
                total_price = value;
                OnPropertyChanged("Total_price");
            }
        }
        private int? bonus_card_id;
        public int? Bonus_card_id
        {
            get { return bonus_card_id; }
            set
            {
                bonus_card_id = value;
                OnPropertyChanged("Bonus_card_id");
            }
        }
        private string bonus_card_name;
        public string Bonus_card_num
        {
            get { return bonus_card_name; }
            set { bonus_card_name = value; OnPropertyChanged("Bonus_card_name"); }
        }

        private int? user_id;
        public int? User_id
        {
            get { return user_id; }
            set
            {
                user_id = value;

                OnPropertyChanged("User_id");
            }
        }
        private string user_name;
        public string User_name
        {
            get { return user_name; }
            set { user_name = value; OnPropertyChanged("User_name"); }
        }
        private ObservableCollection<CheckRecord> checkRecords;
        public ObservableCollection<CheckRecord> CheckRecords
        {
            get
            {
                return checkRecords;
            }
            set
            {
                checkRecords = value;
                OnPropertyChanged("CheckRecords");
            }
        }

        private void CheckModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("CheckRecords");
        }
    }
}
