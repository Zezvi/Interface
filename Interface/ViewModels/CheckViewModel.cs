using PetShop.DataLayer;
using PetShop.Models;
using PetShop.Records;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PetShop.ViewModels
{
	class CheckViewModel : ViewModelBase
	{
		private ICommand _bestSellerCommand;
		private ICommand _maxTotalCommand;
		private ICommand _checkDetails;
		private ICommand _deleteCommand;

		private CheckRepository checkRepository;
		private UserRepository userRepository;
		private BonusCardRepository bonusRepository;

		private Check check = null;
		private string seller = "";
		public string Seller
		{
			get { return seller; }
			set
			{
				seller = value;
				OnPropertyChanged("Seller");
			}
		}

		private decimal total = 0;
		public decimal Total
		{
			get { return total; }
			set
			{
				total = value;
				OnPropertyChanged("Total");
			}
		}

		public CheckRecord CheckRecord { get; set; }
		public ICommand GroupBySellersCommand
		{
			get
			{
				if (_bestSellerCommand == null)
					_bestSellerCommand = new RelayCommand(param => BestSeller(), null);

				return _bestSellerCommand;
			}
		}

		public ICommand MaxTotalCommand
		{
			get
			{
				if (_maxTotalCommand == null)
					_maxTotalCommand = new RelayCommand(param => FilterTotal(), null);

				return _maxTotalCommand;
			}
		}

		public ICommand CheckDetails
		{
			get
			{
				if (_checkDetails == null)
					_checkDetails = new RelayCommand(param => Details((int)param), null);
				return _checkDetails;
			}
		}
		public ICommand DeleteCommand
		{
			get
			{
				if (_deleteCommand == null)
					_deleteCommand = new RelayCommand(param => Delete((int)param), null);
				return _deleteCommand;
			}
		}

		public CheckViewModel()
		{
			check = new Check();
			checkRepository = new CheckRepository();
			userRepository = new UserRepository();
			bonusRepository = new BonusCardRepository();
			CheckRecord = new CheckRecord();
			GetAll();
		}

		public void ResetData()
		{
			CheckRecord.Check_id = 0;
			CheckRecord.Bonus_card_id = 0;
			CheckRecord.Date_sale = DateTime.MinValue;
			CheckRecord.User_id = 0;
			CheckRecord.Total_price = 0;
		}

		void BestSeller()
		{
			GetAll();
			IEnumerable<int?> temp = CheckRecord.CheckRecords.GroupBy(n => n.User_id).OrderByDescending(n => n.Sum(nn => nn.Total_price)).Select(p => p.Key);
			int? bestSellerId = temp.First();
			var sss = CheckRecord.CheckRecords.Where(n => n.User_id == bestSellerId);
			User bestSeller = userRepository.GetOne(bestSellerId);
			Seller = bestSeller.fio;
			CheckRecord.CheckRecords = new ObservableCollection<CheckRecord>();
			foreach (var item in sss)
			{
				CheckRecord.CheckRecords.Add(item);
			}
		}

		public void GetAll()
		{
			CheckRecord.CheckRecords = new ObservableCollection<CheckRecord>();
			List<User> users = userRepository.Get();
			List<BonusCard> bonusCards = bonusRepository.Get();
			List<Check> checks = checkRepository.Get();

			foreach (var item in checks)
			{
				CheckRecord checkRecord = new CheckRecord();
				checkRecord.Check_id = item.check_id;
				checkRecord.Bonus_card_id = item.bonus_card_id;
				BonusCard card = bonusCards.FirstOrDefault(n => n.bonus_card_id == item.bonus_card_id);
				if (card != null) checkRecord.Bonus_card_num = card.card_number;
				checkRecord.Date_sale = item.date_sale;
				checkRecord.User_id = item.user_id;
				User user = users.FirstOrDefault(n => n.user_id == item.user_id);
				if (user != null) checkRecord.User_name = user.fio;
				checkRecord.Total_price = item.total_price;
				CheckRecord.CheckRecords.Add(checkRecord);
			}

		}
		public void FilterTotal()
		{
			GetAll();
			var sss = CheckRecord.CheckRecords.Where(n => n.Total_price <= Total);
			CheckRecord.CheckRecords = new ObservableCollection<CheckRecord>();
			foreach (var item in sss)
			{
				CheckRecord.CheckRecords.Add(item);
			}
		}
		public void Details(int id)
		{
			Check check = checkRepository.GetOne(id);
			if (check != null)
			{
				User user = userRepository.GetOne(check.user_id);
				string userInfo = "\nПродавец : " + user.fio;
				string bonusInfo = "";
				if (check.bonus_card_id != null && check.bonus_card_id > 0)
				{
					BonusCard card = bonusRepository.GetOne(check.bonus_card_id);
					bonusInfo = "\nБонусная карта : " + card.card_number;
					bonusInfo += "\nКоличество бонусов : " + card.bonus;
				}
				string CheckInfo = "\nСтоимость покупки : " + check.total_price;
				CheckInfo += "\n Дата покупки : " + check.date_sale + userInfo + bonusInfo;
				MessageBox.Show(CheckInfo);
			}
		}
		public void Delete(int id)
		{
			if (MessageBox.Show("Подтверждаете удаление продажи?", "Check", MessageBoxButton.YesNo)
				== MessageBoxResult.Yes)
			{
				try
				{
					checkRepository.Remove(id);
					MessageBox.Show("Запись успешно удалена.");
				}
				catch (Exception ex)
				{
					MessageBox.Show("Возникла ошибка при удалении. " + ex.InnerException);
				}
				finally
				{
					GetAll();
				}
			}

		}
	}
}
