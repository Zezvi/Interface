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
		private GoodsRepository goodRepository;
		private SupplierRepository supplierRepository;
		private CategoryRepository categoryRepository;
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
			goodRepository = new GoodsRepository();
			categoryRepository = new CategoryRepository();
			supplierRepository = new SupplierRepository();
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
					goodRepository.Remove(id);
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
						goodRepository.Add(good);
						MessageBox.Show("Новая запись успешно добавлена");
					}
					else
					{
						good.good_id = GoodRecord.Good_id;
						goodRepository.Edit(good);
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
			List<Supplier> suppliers = supplierRepository.Get();
			List<Category> categories = categoryRepository.Get();
			var good = goodRepository.GetOne(id);
			GoodRecord.Good_id = good.good_id;
			GoodRecord.Name = good.name;
			GoodRecord.Price = good.price;
			GoodRecord.Category_id = good.category_id;
			GoodRecord.Category_name = categories.FirstOrDefault(n => n.category_id == good.category_id).name;
			GoodRecord.Count_stock = good.count_stock;
			GoodRecord.Supplier_id = good.supplier_id;
			GoodRecord.Supplier_name = suppliers.FirstOrDefault(n => n.supplier_id == good.supplier_id).name;
			GoodRecord.Shelf_life = good.shelf_life;
			GoodRecord.Description = good.description;

		}

		public void GetAll()
		{
			GoodRecord.GoodRecords = new ObservableCollection<GoodRecord>();

			List<Supplier> suppliers = supplierRepository.Get();
			List<Category> categories = categoryRepository.Get();
			List<Good> goods = goodRepository.Get();
			foreach (var item in goods)
			{
				GoodRecord goodRecord = new GoodRecord();
				goodRecord.Good_id = item.good_id;

				goodRecord.Description = item.description;
				Category cat = categories.FirstOrDefault(n => n.category_id == item.category_id);
				if (cat != null) goodRecord.Category_name = cat.name;
				goodRecord.Supplier_id = item.supplier_id;
				Supplier sup = suppliers.FirstOrDefault(n => n.supplier_id == item.supplier_id);
				if (sup != null) goodRecord.Supplier_name = sup.name;
				goodRecord.Count_stock = item.count_stock;
				goodRecord.Name = item.name;
				goodRecord.Price = item.price;
				goodRecord.Shelf_life = item.shelf_life;
				GoodRecord.GoodRecords.Add(goodRecord);
			}
		}
		public void GetCategory()
		{
			List<Category> categories = categoryRepository.Get();
			GetCategoryForm getCategoryForm = new GetCategoryForm();
			if (getCategoryForm.ShowDialog() == true)
			{
				if (getCategoryForm.Category_id > 0)
				{
					GoodRecord.Category_id = getCategoryForm.Category_id;
					GoodRecord.Category_name = categories.FirstOrDefault(n => n.category_id == getCategoryForm.Category_id).name;
				}
			}
		}
		public void GetSupplier()
		{
			List<Supplier> suppliers = supplierRepository.Get();
			GetSupplierForm getSupplierForm = new GetSupplierForm();
			if (getSupplierForm.ShowDialog() == true)
			{
				if (getSupplierForm.Supplier_id > 0)
				{
					GoodRecord.Supplier_id = getSupplierForm.Supplier_id;
					GoodRecord.Supplier_name = suppliers.FirstOrDefault(n => n.supplier_id == getSupplierForm.Supplier_id).name;
				}
			}
		}
	}
}
