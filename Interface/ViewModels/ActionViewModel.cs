using PetShop.DataLayer;
using PetShop.Records;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PetShop.ViewModels
{
	class ActionViewModel
	{
		private ICommand _saveCommand;
		private ICommand _resetCommand;
		private ICommand _editCommand;
		private ICommand _deleteCommand;
		private ActionRepository repository;
		private Models.Action action = null;
		public ActionRecord ActionRecord { get; set; }
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
					_deleteCommand = new RelayCommand(param => RemoveAction((int)param), null);

				return _deleteCommand;
			}
		}

		public ActionViewModel()
		{
			action = new Models.Action();
			repository = new ActionRepository();
			ActionRecord = new ActionRecord();
			GetAll();
		}

		public void ResetData()
		{
			ActionRecord.Action_id = 0;
			ActionRecord.Name = "";
			ActionRecord.Data_start = DateTime.MinValue;
			ActionRecord.Data_end = DateTime.MinValue;
			ActionRecord.Discount = 0;
		}

		public void RemoveAction(int id)
		{
			if (MessageBox.Show("Подтверждаете удаление акции?", "Models.Action", MessageBoxButton.YesNo)
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
			if (ActionRecord != null)
			{
				bool check = CheckInput();
				if (check)
				{
					action.action_id = ActionRecord.Action_id;
					action.name = ActionRecord.Name;
					action.data_start = ActionRecord.Data_start;
					action.data_end = ActionRecord.Data_end;
					action.discount = ActionRecord.Discount;

					try
					{
						if (ActionRecord.Action_id <= 0)
						{
							repository.Add(action);
							MessageBox.Show("Новая запись успешно добавлена");
						}
						else
						{
							action.action_id = ActionRecord.Action_id;
							repository.Edit(action);
							MessageBox.Show("Запись успешно обновлена");
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("Возникли ошибки при сохранении. " + ex.InnerException);
					}
					finally
					{
						GetAll();
						ResetData();
					}
				}

			}
		}
		private bool CheckInput()
		{
			if (String.IsNullOrEmpty(ActionRecord.Name))
			{

				MessageBox.Show("Не задано название акции");
				return false;
			}
			if (ActionRecord.Data_end <= ActionRecord.Data_start)
			{
				MessageBox.Show("Некорректная дата проведения акции");
				return false;
			}
			if (ActionRecord.Discount > 50)
			{
				MessageBox.Show("Некорретно выбрана скидка по акции \n Размер скидки не должен превышать 50%");
				return false;
			}
			return true;
		}

		public void EditData(int id)
		{
			var model = repository.GetOne(id);
			ActionRecord.Action_id = model.action_id;
			ActionRecord.Data_end = model.data_end;
			ActionRecord.Data_start = model.data_start;
			ActionRecord.Name = model.name;
			ActionRecord.Discount = model.discount;
		}

		public void GetAll()
		{
			ActionRecord.ActionRecords = new ObservableCollection<ActionRecord>();
			repository.Get().ForEach(data => ActionRecord.ActionRecords.Add(new ActionRecord()
			{
				Action_id = data.action_id,
				Name = data.name,
				Data_start = data.data_start,
				Data_end = data.data_end,
				Discount = data.discount
			}));
		}
	}
}
