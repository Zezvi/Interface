using PetShop.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace PetShop.Records
{
    class ActionRecord : ViewModelBase
    {
        private int action_id;
        public int Action_id
        {
            get
            {
                return action_id;
            }
            set
            {
                action_id = value;
                OnPropertyChanged("Action_id");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value; OnPropertyChanged("Name");
            }
        }

        private DateTime? data_start;
        public DateTime? Data_start
        {
            get
            {
                return data_start;
            }
            set
            {
                data_start = value;
                OnPropertyChanged("Data_start");
            }
        }

        private DateTime? data_end;
        public DateTime? Data_end
        {
            get { return data_end; }
            set { data_end = value; OnPropertyChanged("Data_end"); }
        }

        private int? discount { get; set; }
        public int? Discount
        {
            get { return discount; }
            set { discount = value; OnPropertyChanged("Discount"); }
        }

        private ObservableCollection<ActionRecord> actionRecords;
        public ObservableCollection<ActionRecord> ActionRecords
        {
            get
            {
                return actionRecords;
            }
            set
            {
                actionRecords = value;
                OnPropertyChanged("ActionRecords");
            }
        }

        private void ActionModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("GoodRecords");
        }

    }
}
