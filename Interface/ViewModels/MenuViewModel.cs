using PetShop.Models;
using PetShop.Views.ActionView;
using PetShop.Views.BonuseView;
using PetShop.Views.CheckView;
using PetShop.Views.GoodsView;
using PetShop.Views.PurchaseView;
using PetShop.Views.SupplierView;
using PetShop.Views.UserView;
using System.Windows.Input;

namespace PetShop.ViewModels
{
	class MenuViewModel
	{

		private ICommand _actionDetailsCommand;
		private ICommand _goodDetailsCommand;
		private ICommand _supplierDetailsCommand;
		private ICommand _purchaseCommand;
		private ICommand _bonuseCommand;
		private ICommand _usersCommand;
		private ICommand _checkCommand;
		private User Current;


		public MenuViewModel(User user)
		{
			Current = user;
		}
		public ICommand UserCommand
		{
			get
			{
				if (_usersCommand == null)
					_usersCommand = new RelayCommand(param => UserWindow(), null);

				return _usersCommand;
			}
		}

		public ICommand GoodCommand
		{
			get
			{
				if (_goodDetailsCommand == null)
					_goodDetailsCommand = new RelayCommand(param => GoodsWindow(), null);

				return _goodDetailsCommand;
			}
		}

		public ICommand SupplierCommand
		{
			get
			{
				if (_supplierDetailsCommand == null)
					_supplierDetailsCommand = new RelayCommand(param => SupplierWindow(), null);

				return _supplierDetailsCommand;
			}
		}

		public ICommand PurchaseCommand
		{
			get
			{
				if (_purchaseCommand == null)
					_purchaseCommand = new RelayCommand(param => PurchaseWindow(Current), null);

				return _purchaseCommand;
			}
		}

		public ICommand ActionCommand
		{
			get
			{
				if (_actionDetailsCommand == null)
					_actionDetailsCommand = new RelayCommand(param => ActionsWindow(), null);

				return _actionDetailsCommand;
			}
		}

		public ICommand CheckCommand
		{
			get
			{
				if (_checkCommand == null)
					_checkCommand = new RelayCommand(param => CheckWindow(), null);

				return _checkCommand;
			}
		}

		public ICommand BonusCommand
		{
			get
			{
				if (_bonuseCommand == null)
					_bonuseCommand = new RelayCommand(param => BonuseWindow(), null);

				return _bonuseCommand;
			}
		}

		public void UserWindow()
		{
			UserDetails userDetails = new UserDetails();
			userDetails.ShowDialog();
		}
		public void CheckWindow()
		{
			CheckList view = new CheckList();
			view.ShowDialog();
		}
		public void ActionsWindow()
		{

			ActionDetails actionDeatails = new ActionDetails();
			actionDeatails.ShowDialog();
		}
		public void GoodsWindow()
		{
			ManageGoods manageGoods = new ManageGoods();
			manageGoods.ShowDialog();
		}
		public void SupplierWindow()
		{
			SupplierDetails suppliers = new SupplierDetails();
			suppliers.ShowDialog();
		}
		public void PurchaseWindow(User user)
		{
			MakePurchase makePurchase = new MakePurchase(user);
			makePurchase.ShowDialog();
		}
		public void BonuseWindow()
		{
			BonuseDetails bonuseDetails = new BonuseDetails();
			bonuseDetails.ShowDialog();
		}
	}
}

