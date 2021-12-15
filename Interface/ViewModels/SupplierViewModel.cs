using PetShop.DataLayer;
using PetShop.Models;
using PetShop.Records;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PetShop.ViewModels
{
	class SupplierViewModel
	{
		private ICommand _saveCommand;
		private ICommand _resetCommand;
		private ICommand _editCommand;
		private ICommand _deleteCommand;
		private SupplierRepository repository;
		private Supplier supplier = null;
		public SupplierRecord SupplierRecord { get; set; }
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
					_deleteCommand = new RelayCommand(param => DeleteSupplier((int)param), null);

				return _deleteCommand;
			}
		}

		public SupplierViewModel()
		{
			supplier = new Supplier();
			repository = new SupplierRepository();
			SupplierRecord = new SupplierRecord();
			GetAll();
		}

		public void ResetData()
		{
			SupplierRecord.Supplier_id = 0;
			SupplierRecord.Name = "";
			SupplierRecord.PhoneNumber = "";
		}

		public void DeleteSupplier(int id)
		{
			if (MessageBox.Show("Подтверждаете удаение поставщика?", "Supplier", MessageBoxButton.YesNo)
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
			if (SupplierRecord != null)
			{
				supplier.supplier_id = SupplierRecord.Supplier_id;
				supplier.name = SupplierRecord.Name;
				supplier.phonenumber = SupplierRecord.PhoneNumber;

				try
				{
					if (SupplierRecord.Supplier_id <= 0)
					{
						repository.Add(supplier);
						MessageBox.Show("Новая запись успешно добавлена");
					}
					else
					{
						supplier.supplier_id = SupplierRecord.Supplier_id;
						repository.Edit(supplier);
						MessageBox.Show("Запись успешно обновлена");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Возникли ошибки при сохранении изменений в записи. " + ex.InnerException);
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
			SupplierRecord.Supplier_id = model.supplier_id;
			SupplierRecord.Name = model.name;
			SupplierRecord.PhoneNumber = model.phonenumber;
		}

		public void GetAll()
		{
			SupplierRecord.SupplierRecords = new ObservableCollection<SupplierRecord>();
			repository.Get().ForEach(data => SupplierRecord.SupplierRecords.Add(new SupplierRecord()
			{
				Supplier_id = data.supplier_id,
				Name = data.name,
				PhoneNumber = data.phonenumber
			}));
		}
	}
}
