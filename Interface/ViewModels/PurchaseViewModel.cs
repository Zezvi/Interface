using PetShop.DataLayer;
using PetShop.Models;
using PetShop.Records;
using PetShop.Views.Temp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PetShop.ViewModels
{
    class PurchaseViewModel : ViewModelBase
    {
        private ICommand _purchaseCommand;
        private ICommand _selectCommand;
        private ICommand _bonuseCommand;
        private GoodsRepository goodRepository;
        private CheckRepository checkRepository;
        private int bonuse_card_id;
        public PurchaseRecord PurchaseRecord { get; set; }
        User current;
        private decimal? totalCost = 0;
        public decimal? TotalCost
        {
            get { return totalCost; }
            set
            {
                totalCost = value;
                OnPropertyChanged("TotalCost");

            }
        }

        private int? bonuse = 0;
        public int? Bonuse
        {
            get { return bonuse; }
            set
            {
                bonuse = value;
                OnPropertyChanged("Bonuse");
            }
        }
        public GoodRecord GoodRecord { get; set; }


        public ICommand PurchaseCommand
        {
            get
            {
                if (_purchaseCommand == null)
                    _purchaseCommand = new RelayCommand(param => PurchaseGood(), null);
                return _purchaseCommand;
            }
        }
        public ICommand SelectCommand
        {
            get
            {
                if (_selectCommand == null)
                    _selectCommand = new RelayCommand(param => AddPurchase((int)param), null);
                return _selectCommand;
            }
        }
        public ICommand BonuseCommand
        {
            get
            {
                if (_bonuseCommand == null)
                    _bonuseCommand = new RelayCommand(param => CheckBonuse(), null);
                return _bonuseCommand;
            }
        }


        public PurchaseViewModel(User user)
        {
            goodRepository = new GoodsRepository();
            GoodRecord = new GoodRecord();
            PurchaseRecord = new PurchaseRecord();
            checkRepository = new CheckRepository();
            current = user;
            GetAll();
        }

        public void ResetData()
        {
            GoodRecord.Good_id = 0;
            GoodRecord.Name = "";
            GoodRecord.Price = 0;
            GoodRecord.Supplier_id = 0;
            GoodRecord.Shelf_life = DateTime.MinValue;
        }

        public void GetAll()
        {
            GoodRecord.GoodRecords = new ObservableCollection<GoodRecord>();
            goodRepository.Get().ForEach(data => GoodRecord.GoodRecords.Add(new GoodRecord()
            {
                Good_id = data.good_id,
                Description = data.description,
                Category_id = data.category_id,
                Supplier_id = data.supplier_id,
                Count_stock = data.count_stock,
                Name = data.name,
                Price = data.price,
                Shelf_life = data.shelf_life

            }));
        }

        public void PurchaseGood()
        {
            if (PurchaseRecord.PurchaseRecords != null)
            {
                try
                {

                    foreach (var item in PurchaseRecord.PurchaseRecords)
                    {
                        var good = goodRepository.GetOne(item.Product_id);
                        if (GoodRecord.Count_stock >= item.Count && good != null)
                        {
                            good.count_stock -= item.Count;
                            goodRepository.Edit(good);
                        }
                    }
                    decimal? temp = TotalCost - bonuse;
                    Check check = new Check();
                    check.date_sale = DateTime.Now;
                    check.total_price = temp;
                    check.user_id = current.user_id;
                    check.bonus_card_id = bonuse_card_id;
                    checkRepository.Add(check);
                    MessageBox.Show("Товары проданы, чек сформирован.");


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Возникли ошибки при добавлении данных");

                }
                finally
                {
                    GetAll();
                    ResetData();
                    PurchaseRecord.PurchaseRecords.Clear();
                }
            }

        }
        public void AddPurchase(int id)
        {
            var good = goodRepository.GetOne(id);

            CountForm countForm = new CountForm();
            if (countForm.ShowDialog() == true)
            {
                if (!String.IsNullOrEmpty(countForm.Count))
                {
                    GoodRecord.Good_id = good.good_id;
                    GoodRecord.Name = good.name;
                    GoodRecord.Price = good.price;
                    GoodRecord.Count_stock = good.count_stock;
                    if (PurchaseRecord.PurchaseRecords == null)
                    {
                        PurchaseRecord.PurchaseRecords = new ObservableCollection<PurchaseRecord>();
                    }
                    bool check = true;
                    foreach (var item in PurchaseRecord.PurchaseRecords)
                    {
                        if (item.Product_id == good.good_id)
                        {
                            check = false;
                            break;
                        }
                    }
                    if (check)
                    {

                        var temp = new PurchaseRecord();
                        PurchaseRecord.PurchaseRecords.Add(new PurchaseRecord()
                        {
                            Product_id = good.good_id,
                            Name = good.name,
                            Count = int.Parse(countForm.Count),
                            Price = good.price,
                        });
                        TotalCost += good.price;
                    }
                }
                else
                {
                    MessageBox.Show("Укажите количество");
                }
            }
        }
        public void CheckBonuse()
        {
            BonuseForm bonuseForm = new BonuseForm();
            if (bonuseForm.ShowDialog() == true)
            {
                if (bonuseForm.Bonuse_Card_Id > 0)
                {
                    bonuse_card_id = bonuseForm.Bonuse_Card_Id;
                    Bonuse = bonuseForm.Bonuse_Size;

                }
                else
                {
                    bonuse_card_id = 0;
                    Bonuse = 0;
                }
            }
        }
    }
}

