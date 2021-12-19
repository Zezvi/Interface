using PetShop.DataLayer;
using PetShop.Models;
using PetShop.Records;
using PetShop.Views.GoodsView;
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
	class GoodViewModel : ViewModelBase
	{
		private ICommand _saveCommand;
		private ICommand _resetCommand;
		private ICommand _editCommand;
		private ICommand _deleteCommand;

		private ICommand _supplierCommand;
		private ICommand _categoryCommand;
		private GoodsRepository repository;
		private Good good = null;
		public GoodRecord GoodRecord { get; set; }

		private int? category_id = 0;
		public int? Category_id
		{
			get { return category_id; }
			set
			{
				category_id = value;
				OnPropertyChanged("Category_id");

			}
		}

		private int? supplier_id = 0;
		public int? Supplier_id
		{
			get { return supplier_id; }
			set
			{
				supplier_id = value;
				OnPropertyChanged("Supplier_id");

			}
		}

		public ICommand SupplierCommand
		{
			get
			{
				if (_supplierCommand == null)
					_supplierCommand = new RelayCommand(param => GetSupplier(), null);

				return _supplierCommand;
			}
		}

		public ICommand CategoryCommand
		{
			get
			{
				if (_categoryCommand == null)
					_categoryCommand = new RelayCommand(param => GetCategory(), null);

				return _categoryCommand;
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
					_deleteCommand = new RelayCommand(param => DeleteGood((int)param), null);

				return _deleteCommand;
			}
		}


		public GoodViewModel()
		{
			good = new Good();
			repository = new GoodsRepository();
			GoodRecord = new GoodRecord();
			GetAll();
		}

		public void ResetData()
		{
			GoodRecord.Good_id = 0;
			GoodRecord.Name = "";
			GoodRecord.Price = 0;
			GoodRecord.Supplier_id = 0;
			GoodRecord.Category_id = 0;
			GoodRecord.Shelf_life = DateTime.MinValue;
			GoodRecord.Description = "";
			GoodRecord.Count_stock = 0;
		}

		public void DeleteGood(int id)
		{
			if (MessageBox.Show("Подтверждаете удаление товара?", "Good", MessageBoxButton.YesNo)
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
			if (GoodRecord != null)
			{
				good.good_id = GoodRecord.Good_id;
				good.name = GoodRecord.Name;
				good.price = GoodRecord.Price;
				good.category_id = GoodRecord.Category_id;
				good.count_stock = GoodRecord.Count_stock;
				good.supplier_id = GoodRecord.Supplier_id;
				good.shelf_life = GoodRecord.Shelf_life;
				good.description = GoodRecord.Description;

				try
				{
					if (GoodRecord.Good_id <= 0)
					{
						repository.Add(good);
						MessageBox.Show("Новая запись успешно добавлена");
					}
					else
					{
						good.good_id = GoodRecord.Good_id;
						repository.Edit(good);
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



		public void EditData(int id)
		{
			var good = repository.GetOne(id);
			GoodRecord.Good_id = good.good_id;
			GoodRecord.Name = good.name;
			GoodRecord.Price = good.price;
			GoodRecord.Category_id = good.category_id;
			GoodRecord.Count_stock = good.count_stock;
			GoodRecord.Supplier_id = good.supplier_id;
			GoodRecord.Shelf_life = good.shelf_life;
			GoodRecord.Description = good.description;

		}

		public void GetAll()
		{
			GoodRecord.GoodRecords = new ObservableCollection<GoodRecord>();
			repository.Get().ForEach(data => GoodRecord.GoodRecords.Add(new GoodRecord()
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
		public void GetCategory()
		{
			GetCategoryForm getCategoryForm = new GetCategoryForm();
			if (getCategoryForm.ShowDialog() == true)
			{
				if (getCategoryForm.Category_id > 0)
				{
					GoodRecord.Category_id = getCategoryForm.Category_id;
				}
			}
		}
		public void GetSupplier()
		{
			GetSupplierForm getSupplierForm = new GetSupplierForm();
			if (getSupplierForm.ShowDialog() == true)
			{
				if (getSupplierForm.Supplier_id > 0)
				{
					GoodRecord.Supplier_id = getSupplierForm.Supplier_id;
				}
			}
		}
	}
}
