using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetShop.ViewModels
{
	class CountGoodsViewModel : ViewModelBase
	{
		private ICommand _closeCommand;

		private int count;
		public int Count
		{
			get { return count; }
			set
			{
				count = value;
				OnPropertyChanged("Count");
			}
		}


		public ICommand CloseCommand
		{
			get
			{
				if (_closeCommand == null)
					_closeCommand = new RelayCommand(param => Close(), null);

				return _closeCommand;
			}
		}
		public void Close()
		{
			this.Close();
		}

	}
}

