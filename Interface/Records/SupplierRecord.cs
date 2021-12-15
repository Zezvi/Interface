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
    class SupplierRecord : ViewModelBase
    {
        private int supplier_id;
        public int Supplier_id
        {
            get
            {
                return supplier_id;
            }
            set
            {
                supplier_id = value;
                OnPropertyChanged("Supplier_id");

            }
        }
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");

            }
        }

        private string phonenumber;
        public string PhoneNumber
        {
            get { return phonenumber; }
            set
            {
                phonenumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        private ObservableCollection<SupplierRecord> supplierRecords;
        public ObservableCollection<SupplierRecord> SupplierRecords
        {
            get
            {
                return supplierRecords;
            }
            set
            {
                supplierRecords = value;
                OnPropertyChanged("SupplierRecords");
            }
        }

        private void SupplierModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("SupplierRecords");
        }
    }
}
