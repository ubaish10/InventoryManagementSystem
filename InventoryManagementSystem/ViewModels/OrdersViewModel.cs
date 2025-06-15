using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.ViewModels
{
    public class OrdersViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseContext dbContext;
        private ObservableCollection<Order> orders;
        private ObservableCollection<Product> products;
        private ObservableCollection<Supplier> suppliers;
        private Order selectedOrder;
        private int productId;
        private int supplierId;
        private int quantity;
        private DateTime orderDate;
        private string status;

        public ObservableCollection<Order> Orders
        {
            get => orders;
            set { orders = value; OnPropertyChanged(nameof(Orders)); }
        }

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

        public Order SelectedOrder
        {
            get => selectedOrder;
            set
            {
                selectedOrder = value;
                if (selectedOrder != null)
                {
                    ProductId = selectedOrder.ProductId;
                    SupplierId = selectedOrder.SupplierId;
                    Quantity = selectedOrder.Quantity;
                    OrderDate = selectedOrder.OrderDate;
                    Status = selectedOrder.Status;
                }
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }

        public int ProductId
        {
            get => productId;
            set { productId = value; OnPropertyChanged(nameof(ProductId)); }
        }

        public int SupplierId
        {
            get => supplierId;
            set { supplierId = value; OnPropertyChanged(nameof(SupplierId)); }
        }

        public int Quantity
        {
            get => quantity;
            set { quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }

        public DateTime OrderDate
        {
            get => orderDate;
            set { orderDate = value; OnPropertyChanged(nameof(OrderDate)); }
        }

        public string Status
        {
            get => status;
            set { status = value; OnPropertyChanged(nameof(Status)); }
        }

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public OrdersViewModel()
        {
            dbContext = new DatabaseContext();
            Orders = new ObservableCollection<Order>(dbContext.GetOrders());
            Products = new ObservableCollection<Product>(dbContext.GetProducts());
            Suppliers = new ObservableCollection<Supplier>(dbContext.GetSuppliers());
            OrderDate = DateTime.Now;
            Status = "Pending";

            // Commands with no CanExecute
            AddCommand = new RelayCommand(AddOrder);
            UpdateCommand = new RelayCommand(UpdateOrder);
            DeleteCommand = new RelayCommand(DeleteOrder);
        }

        private void AddOrder(object parameter)
        {
            if (ProductId <= 0 || SupplierId <= 0 || Quantity <= 0 || string.IsNullOrEmpty(Status))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please fill in all fields with valid values.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            var order = new Order
            {
                ProductId = ProductId,
                SupplierId = SupplierId,
                Quantity = Quantity,
                OrderDate = OrderDate,
                Status = Status
            };
            dbContext.AddOrder(order);
            Orders.Add(order);
            ClearFields();

            MessageBox.Show("Order added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateOrder(object parameter)
        {
            if (SelectedOrder == null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please select an order to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            if (ProductId <= 0 || SupplierId <= 0 || Quantity <= 0 || string.IsNullOrEmpty(Status))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please fill in all fields with valid values.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            SelectedOrder.ProductId = ProductId;
            SelectedOrder.SupplierId = SupplierId;
            SelectedOrder.Quantity = Quantity;
            SelectedOrder.OrderDate = OrderDate;
            SelectedOrder.Status = Status;
            dbContext.UpdateOrder(SelectedOrder);
            ClearFields();

            MessageBox.Show("Order updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteOrder(object parameter)
        {
            if (SelectedOrder == null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please select an order to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            dbContext.DeleteOrder(SelectedOrder.Id);
            Orders.Remove(SelectedOrder);
            ClearFields();

            MessageBox.Show("Order deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ClearFields()
        {
            ProductId = 0;
            SupplierId = 0;
            Quantity = 0;
            OrderDate = DateTime.Now;
            Status = "Pending";
            SelectedOrder = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
