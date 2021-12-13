using PetShop.Models;
using PetShop.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace PetShop.Records
{
    public class PurchaseRecord : ViewModelBase
    {
        private int product_id;
        public int Product_id
        {
            get
            {
                return product_id;
            }
            set
            {
                product_id = value;
                OnPropertyChanged("Product_id");
            }
        }
        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }

        private decimal? price;
        public decimal? Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private ObservableCollection<PurchaseRecord> purchaseRecords;
        public ObservableCollection<PurchaseRecord> PurchaseRecords
        {
            get
            {
                return purchaseRecords;
            }
            set
            {
                purchaseRecords = value;
                OnPropertyChanged("PurchaseRecords");
            }
        }

        private void PurchaseModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("PurchaseRecords");
        }

    }
}
