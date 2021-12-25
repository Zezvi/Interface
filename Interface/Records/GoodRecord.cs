using PetShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Records
{
    class GoodRecord : ViewModelBase
    {
        private int good_id;

        public int Good_id
        {
            get { return good_id; }
            set
            {
                good_id = value;
                OnPropertyChanged("Good_id");

            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        private decimal? price;

        public decimal? Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged("Price"); }
        }

        private int? count_stock;

        public int? Count_stock
        {
            get { return count_stock; }
            set { count_stock = value; OnPropertyChanged("Count_stock"); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }

        private DateTime? shelf_life;

        public DateTime? Shelf_life
        {
            get { return shelf_life; }
            set { shelf_life = value; OnPropertyChanged("Shelf_life"); }
        }

        private int? category_id;

        public int? Category_id
        {
            get { return category_id; }
            set { category_id = value; OnPropertyChanged("Category_id"); }
        }
        private string category_name;
        public string Category_name
        {
            get { return category_name; }
            set { category_name = value; OnPropertyChanged("Category_name"); }
        }


        private string suplier_name;
        public string Supplier_name
        {
            get { return suplier_name; }
            set { suplier_name = value; OnPropertyChanged("Supplier_name"); }
        }
        private int? supplier_id;

        public int? Supplier_id
        {
            get { return supplier_id; }
            set { supplier_id = value; OnPropertyChanged("Supplier_id"); }
        }
        private ObservableCollection<GoodRecord> goodRecords;
        public ObservableCollection<GoodRecord> GoodRecords
        {
            get
            {
                return goodRecords;
            }
            set
            {
                goodRecords = value;
                OnPropertyChanged("GoodRecords");
            }
        }

        private void GoodModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("GoodRecords");
        }

    }
}
