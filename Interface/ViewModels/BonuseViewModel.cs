using PetShop.DataLayer;
using PetShop.Models;
using PetShop.Records;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PetShop.ViewModels
{
	class BonuseViewModel
	{
		private ICommand _saveCommand;
		private ICommand _resetCommand;
		private ICommand _editCommand;
		private ICommand _deleteCommand;
		private BonusCardRepository repository;
		private BonusCard bonuse = null;
		public BonusRecord BonusRecord { get; set; }
		public ICommand ResetCommand
		{
			get
			{
				if (_resetCommand == null)
					_resetCommand = new RelayCommand(param => ResetData(), null);

				return _resetCommand;
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				if (_saveCommand == null)
					_saveCommand = new RelayCommand(param => SaveData(), null);

				return _saveCommand;
			}
		}

		public ICommand EditCommand
		{
			get
			{
				if (_editCommand == null)
					_editCommand = new RelayCommand(param => EditData((int)param), null);

				return _editCommand;
			}
		}

		public ICommand DeleteCommand
		{
			get
			{
				if (_deleteCommand == null)
					_deleteCommand = new RelayCommand(param => DeleteBonuse((int)param), null);

				return _deleteCommand;
			}
		}

		public BonuseViewModel()
		{
			bonuse = new BonusCard();
			repository = new BonusCardRepository();
			BonusRecord = new BonusRecord();
			GetAll();
		}

		public void ResetData()
		{
			BonusRecord.Bonuse_card_id = 0;
			BonusRecord.Bonus = 0;
			BonusRecord.Card_number = "";
		}

		public void DeleteBonuse(int id)
		{
			if (MessageBox.Show("Подтверждаете удаление пользователя?", "BonuseCard", MessageBoxButton.YesNo)
				== MessageBoxResult.Yes)
			{
				try
				{
					repository.Remove(id);
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

		public void SaveData()
		{
			if (BonusRecord != null)
			{
				bonuse.bonus_card_id = BonusRecord.Bonuse_card_id;
				bonuse.bonus = BonusRecord.Bonus;
				bonuse.card_number = BonusRecord.Card_number;

				try
				{
					if (BonusRecord.Bonuse_card_id <= 0)
					{
						repository.Add(bonuse);
						MessageBox.Show("Новая запись успешно добавлена");
					}
					else
					{
						bonuse.bonus_card_id = BonusRecord.Bonuse_card_id;
						bonuse.bonus = BonusRecord.Bonus;
						bonuse.card_number = BonusRecord.Card_number;

						repository.Edit(bonuse);
						MessageBox.Show("Запись успешно обновлена");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Возникла ошибка при сохранении записи " + ex.InnerException);
				}
				finally
				{
					GetAll();
					ResetData();
				}
			}
		}

		public void EditData(int id)
		{
			var model = repository.GetOne(id);
			BonusRecord.Bonuse_card_id = model.bonus_card_id;
			BonusRecord.Bonus = model.bonus;
			BonusRecord.Card_number = model.card_number;

		}

		public void GetAll()
		{
			BonusRecord.BonusRecords = new ObservableCollection<BonusRecord>();
			repository.Get().ForEach(data => BonusRecord.BonusRecords.Add(new BonusRecord()
			{
				Bonuse_card_id = data.bonus_card_id,
				Bonus = data.bonus,
				Card_number = data.card_number
			}));
		}
	}
}
