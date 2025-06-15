using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.ViewModels
{
    public class SuppliersViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseContext dbContext;
        private ObservableCollection<Supplier> suppliers;
        private Supplier selectedSupplier;
        private string name;
        private string contactInfo;

        public ObservableCollection<Supplier> Suppliers
        {
            get => suppliers;
            set { suppliers = value; OnPropertyChanged(nameof(Suppliers)); }
        }

        public Supplier SelectedSupplier
        {
            get => selectedSupplier;
            set
            {
                selectedSupplier = value;
                if (selectedSupplier != null)
                {
                    Name = selectedSupplier.Name;
                    ContactInfo = selectedSupplier.ContactInfo;
                }
                OnPropertyChanged(nameof(SelectedSupplier));
            }
        }

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string ContactInfo
        {
            get => contactInfo;
            set { contactInfo = value; OnPropertyChanged(nameof(ContactInfo)); }
        }

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public SuppliersViewModel()
        {
            dbContext = new DatabaseContext();
            Suppliers = new ObservableCollection<Supplier>(dbContext.GetSuppliers());

            AddCommand = new RelayCommand(AddSupplier);
            UpdateCommand = new RelayCommand(UpdateSupplier);
            DeleteCommand = new RelayCommand(DeleteSupplier);
        }

        private void AddSupplier(object parameter)
        {
            if (string.IsNullOrEmpty(Name))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please fill in the supplier name.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            var supplier = new Supplier { Name = Name, ContactInfo = ContactInfo };
            dbContext.AddSupplier(supplier);
            Suppliers.Add(supplier);
            ClearFields();

            MessageBox.Show("Supplier added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateSupplier(object parameter)
        {
            if (SelectedSupplier == null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please select a supplier to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            if (string.IsNullOrEmpty(Name))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please fill in the supplier name.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            SelectedSupplier.Name = Name;
            SelectedSupplier.ContactInfo = ContactInfo;
            dbContext.UpdateSupplier(SelectedSupplier);
            ClearFields();

            MessageBox.Show("Supplier updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteSupplier(object parameter)
        {
            if (SelectedSupplier == null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please select a supplier to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            dbContext.DeleteSupplier(SelectedSupplier.Id);
            Suppliers.Remove(SelectedSupplier);
            ClearFields();

            MessageBox.Show("Supplier deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ClearFields()
        {
            Name = string.Empty;
            ContactInfo = string.Empty;
            SelectedSupplier = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
