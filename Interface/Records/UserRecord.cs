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
	public class UserRecord : ViewModelBase
	{
		private int user_id;
		public int User_id
		{
			get
			{
				return user_id;
			}
			set
			{
				user_id = value;
				OnPropertyChanged("User_id");
			}
		}

		private string login;
		public string Login
		{
			get
			{
				return login;
			}
			set
			{
				login = value;
				OnPropertyChanged("Login");
			}
		}

		private string password;
		public string Password
		{
			get
			{
				return password;
			}
			set
			{
				password = value;
				OnPropertyChanged("Password");
			}
		}

		private string fio;
		public string Fio
		{
			get
			{
				return fio;
			}
			set
			{
				fio = value;
				OnPropertyChanged("Fio");
			}
		}

		private int? role;
		public int? Role
		{
			get
			{
				return role;
			}
			set
			{
				role = value;
				OnPropertyChanged("Role");
			}
		}

		private ObservableCollection<UserRecord> userRecords;
		public ObservableCollection<UserRecord> UserRecords
		{
			get
			{
				return userRecords;
			}
			set
			{
				userRecords = value;
				OnPropertyChanged("UserRecords");
			}
		}

		private void UserModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			OnPropertyChanged("UserRecords");
		}
	}
}
