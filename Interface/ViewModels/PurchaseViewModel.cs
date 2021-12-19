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
        private ICommand _resetCommand;

        private GoodsRepository goodRepository;
        private CheckRepository checkRepository;
        private ActionRepository actionRepository;

        private DateTime datePurchase;
        public DateTime DatePurchase
        {
            get { return datePurchase; }
            set { datePurchase = value; OnPropertyChanged("DatePurchase"); }
        }

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
        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                    _resetCommand = new RelayCommand(param => ResetData(), null);
                return _resetCommand;
            }
        }


        public PurchaseViewModel(User user)
        {
            goodRepository = new GoodsRepository();
            GoodRecord = new GoodRecord();
            PurchaseRecord = new PurchaseRecord();
            checkRepository = new CheckRepository();
            actionRepository = new ActionRepository();
            current = user;
            GetAll();
        }

        public void ResetData()
        {
            PurchaseRecord.PurchaseRecords.Clear();
            TotalCost = 0;
            Bonuse = 0;
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
                bool checkin = CheckInput();
                if (checkin)
                {
                    try
                    {
                        int? discountByaction = 0;
                        string actionInfo = "";
                        string bonuseInfo = "";
                        foreach (var item in PurchaseRecord.PurchaseRecords)
                        {
                            var good = goodRepository.GetOne(item.Product_id);
                            if (GoodRecord.Count_stock >= item.Count && good != null)
                            {
                                good.count_stock -= item.Count;
                                goodRepository.Edit(good);
                            }
                        }
                        var action = actionRepository.GetOnebyDate(DatePurchase);

                        decimal totalbyAction = 0;


                        if (action != null)
                        {
                            discountByaction = action.discount;
                            actionInfo = "Сегодня день проведения акции " + action.name + " скидка на покупку составит " + action.discount + " %";
                            totalbyAction = (decimal)(TotalCost / 100 * action.discount);
                            totalbyAction = Math.Round(totalbyAction, 2);
                        }
                        if (Bonuse > 0)
                        {
                            bonuseInfo = "Вы воспользовались бонусами " + Bonuse + " руб";
                        }

                        decimal? temp = TotalCost - Bonuse;
                        if (temp < 0) temp = 0;
                        Check check = new Check();
                        check.date_sale = DatePurchase;

                        check.total_price = temp - totalbyAction;
                        if (check.total_price < 0) check.total_price = 0;
                        check.user_id = current.user_id;
                        check.bonus_card_id = bonuse_card_id;
                        checkRepository.Add(check);
                        string checkInfo = "\nДата продажи :" + check.date_sale + "\n" + "Стоимость покупки : " + check.total_price;

                        string output = actionInfo + "\n" + bonuseInfo + checkInfo + "\nТовары проданы, чек сформирован";
                        MessageBox.Show(output);


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

        }
        public void AddPurchase(int id)
        {
            var good = goodRepository.GetOne(id);

            CountForm countForm = new CountForm();
            CountGoodsViewModel countGoodsViewModel = (CountGoodsViewModel)countForm.DataContext;



            countForm.ShowDialog();
            if (countGoodsViewModel.Count > 0)
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
                        Count = countGoodsViewModel.Count,
                        Price = good.price,
                    });
                    TotalCost += good.price * countGoodsViewModel.Count;
                }
            }
            else
            {
                MessageBox.Show("Укажите количество");
            }

        }
        public void CheckBonuse()
        {
            BonuseForm bonuseForm = new BonuseForm();
            BonuseCheckViewModel bonuseCheckViewModel = (BonuseCheckViewModel)bonuseForm.DataContext;

            bonuseForm.ShowDialog();

            if (bonuseCheckViewModel.Bonuses > 0)
            {
                Bonuse = bonuseCheckViewModel.Bonuses;
                bonuse_card_id = (int)bonuseCheckViewModel.Bonuse_card_id;
            }

        }
        private bool CheckInput()
        {
            if (DatePurchase == DateTime.MinValue)
            {
                MessageBox.Show("Не указана дата покупки");
                return false;
            }
            else if (PurchaseRecord.PurchaseRecords.Count == 0)
            {
                MessageBox.Show("В корзину ничего не добавлено");
                return false;
            }
            return true;
        }

    }
}

