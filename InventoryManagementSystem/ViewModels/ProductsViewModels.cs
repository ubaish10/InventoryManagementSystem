using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.ViewModels
{
    public class ProductsViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseContext dbContext;
        private ObservableCollection<Product> products;
        private ObservableCollection<Supplier> suppliers;
        private Product selectedProduct;
        private string name;
        private string sku;
        private int quantity;
        private decimal price;
        private int lowStockThreshold;
        private int? supplierId;

        public ObservableCollection<Product> Products
        {
            get => products;
            set { products = value; OnPropertyChanged(nameof(Products)); }
        }

        public ObservableCollection<Supplier> Suppliers
        {
            get => suppliers;
            set { suppliers = value; OnPropertyChanged(nameof(Suppliers)); }
        }

        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                if (selectedProduct != null)
                {
                    Name = selectedProduct.Name;
                    SKU = selectedProduct.SKU;
                    Quantity = selectedProduct.Quantity;
                    Price = selectedProduct.Price;
                    LowStockThreshold = selectedProduct.LowStockThreshold;
                    SupplierId = selectedProduct.SupplierId;
                }
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string SKU
        {
            get => sku;
            set { sku = value; OnPropertyChanged(nameof(SKU)); }
        }

        public int Quantity
        {
            get => quantity;
            set { quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }

        public decimal Price
        {
            get => price;
            set { price = value; OnPropertyChanged(nameof(Price)); }
        }

        public int LowStockThreshold
        {
            get => lowStockThreshold;
            set { lowStockThreshold = value; OnPropertyChanged(nameof(LowStockThreshold)); }
        }

        public int? SupplierId
        {
            get => supplierId;
            set { supplierId = value; OnPropertyChanged(nameof(SupplierId)); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CheckLowStockCommand { get; set; }

        public ProductsViewModel()
        {
            dbContext = new DatabaseContext();
            Products = new ObservableCollection<Product>(dbContext.GetProducts());
            Suppliers = new ObservableCollection<Supplier>(dbContext.GetSuppliers());

            AddCommand = new RelayCommand(AddProduct);
            UpdateCommand = new RelayCommand(UpdateProduct);
            DeleteCommand = new RelayCommand(DeleteProduct);
            CheckLowStockCommand = new RelayCommand(CheckLowStock);
        }

        private void AddProduct(object parameter)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(SKU) || Quantity < 0 || Price < 0 || LowStockThreshold < 0)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please fill in all fields with valid values.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            var product = new Product
            {
                Name = Name,
                SKU = SKU,
                Quantity = Quantity,
                Price = Price,
                LowStockThreshold = LowStockThreshold,
                SupplierId = SupplierId
            };
            dbContext.AddProduct(product);
            Products.Add(product);
            ClearFields();

            MessageBox.Show("Product added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateProduct(object parameter)
        {
            if (SelectedProduct == null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please select a product to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(SKU) || Quantity < 0 || Price < 0 || LowStockThreshold < 0)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please fill in all fields with valid values.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            SelectedProduct.Name = Name;
            SelectedProduct.SKU = SKU;
            SelectedProduct.Quantity = Quantity;
            SelectedProduct.Price = Price;
            SelectedProduct.LowStockThreshold = LowStockThreshold;
            SelectedProduct.SupplierId = SupplierId;
            dbContext.UpdateProduct(SelectedProduct);
            ClearFields();

            MessageBox.Show("Product updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteProduct(object parameter)
        {
            if (SelectedProduct == null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please select a product to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            dbContext.DeleteProduct(SelectedProduct.Id);
            Products.Remove(SelectedProduct);
            ClearFields();

            MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CheckLowStock(object parameter)
        {
            var lowStockProducts = dbContext.GetLowStockProducts();
            if (lowStockProducts.Count > 0)
            {
                string message = "Low stock alert:\n";
                foreach (var product in lowStockProducts)
                {
                    message += $"{product.Name} (SKU: {product.SKU}) - Quantity: {product.Quantity} (Threshold: {product.LowStockThreshold})\n";
                }
                MessageBox.Show(message, "Low Stock", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("No products are below their low stock threshold.", "Low Stock", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ClearFields()
        {
            Name = string.Empty;
            SKU = string.Empty;
            Quantity = 0;
            Price = 0;
            LowStockThreshold = 10;
            SupplierId = null;
            SelectedProduct = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
