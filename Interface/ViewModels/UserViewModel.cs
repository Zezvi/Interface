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
	public class UserViewModel
	{
		private ICommand _saveCommand;
		private ICommand _resetCommand;
		private ICommand _editCommand;
		private ICommand _deleteCommand;
		private UserRepository repository;
		private User user = null;
		public UserRecord UserRecord { get; set; }
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
					_deleteCommand = new RelayCommand(param => DeleteUser((int)param), null);

				return _deleteCommand;
			}
		}

		public UserViewModel()
		{
			user = new User();
			repository = new UserRepository();
			UserRecord = new UserRecord();
			GetAll();
		}

		public void ResetData()
		{
			UserRecord.User_id = 0;
			UserRecord.Login = "";
			UserRecord.Fio = string.Empty;
			UserRecord.Password = string.Empty;
			UserRecord.Role = 0;
		}

		public void DeleteUser(int id)
		{
			if (MessageBox.Show("Подтверждаете удаение пользователя?", "User", MessageBoxButton.YesNo)
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
			if (UserRecord != null)
			{
				user.user_id = UserRecord.User_id;
				user.fio = UserRecord.Fio;
				user.login = UserRecord.Login;
				user.password = UserRecord.Password;
				user.role = UserRecord.Role;

				try
				{
					if (UserRecord.User_id <= 0)
					{
						repository.Add(user);
						MessageBox.Show("Новая запись успешно добавлена");
					}
					else
					{
						user.user_id = UserRecord.User_id;
						repository.Edit(user);
						MessageBox.Show("Запись успешно обновлена");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error occured while saving. " + ex.InnerException);
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
			UserRecord.User_id = model.user_id;
			UserRecord.Login = model.login;
			UserRecord.Fio = model.fio;
			UserRecord.Password = model.password;
			UserRecord.Role = model.role;
		}

		public void GetAll()
		{
			UserRecord.UserRecords = new ObservableCollection<UserRecord>();
			repository.Get().ForEach(data => UserRecord.UserRecords.Add(new UserRecord()
			{
				User_id = data.user_id,
				Fio = data.fio,
				Login = data.login,
				Password = data.password,
				Role = data.role
			}));
		}
	}
}
