using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.ViewModels
{
    public class StockMovementsViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseContext dbContext;
        private ObservableCollection<StockMovement> stockMovements;
        private ObservableCollection<Product> products;
        private StockMovement selectedStockMovement;
        private int productId;
        private int quantity;
        private string movementType;
        private DateTime movementDate;

        public ObservableCollection<StockMovement> StockMovements
        {
            get => stockMovements;
            set { stockMovements = value; OnPropertyChanged(nameof(StockMovements)); }
        }

        public ObservableCollection<Product> Products
        {
            get => products;
            set { products = value; OnPropertyChanged(nameof(Products)); }
        }

        public StockMovement SelectedStockMovement
        {
            get => selectedStockMovement;
            set
            {
                selectedStockMovement = value;
                if (selectedStockMovement != null)
                {
                    ProductId = selectedStockMovement.ProductId;
                    Quantity = selectedStockMovement.Quantity;
                    MovementType = selectedStockMovement.MovementType;
                    MovementDate = selectedStockMovement.MovementDate;
                }
                OnPropertyChanged(nameof(SelectedStockMovement));
            }
        }

        public int ProductId
        {
            get => productId;
            set { productId = value; OnPropertyChanged(nameof(ProductId)); }
        }

        public int Quantity
        {
            get => quantity;
            set { quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }

        public string MovementType
        {
            get => movementType;
            set { movementType = value; OnPropertyChanged(nameof(MovementType)); }
        }

        public DateTime MovementDate
        {
            get => movementDate;
            set { movementDate = value; OnPropertyChanged(nameof(MovementDate)); }
        }

        public ICommand AddCommand { get; }

        public StockMovementsViewModel()
        {
            dbContext = new DatabaseContext();
            StockMovements = new ObservableCollection<StockMovement>(dbContext.GetStockMovements());
            Products = new ObservableCollection<Product>(dbContext.GetProducts());
            MovementDate = DateTime.Now;
            MovementType = "Addition";

            // 🔥 No CanExecute
            AddCommand = new RelayCommand(AddStockMovement);
        }

        private void AddStockMovement(object parameter)
        {
            if (ProductId <= 0 || Quantity <= 0 || string.IsNullOrEmpty(MovementType))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please fill in all fields with valid values.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            var movement = new StockMovement
            {
                ProductId = ProductId,
                Quantity = Quantity,
                MovementType = MovementType,
                MovementDate = MovementDate
            };

            dbContext.AddStockMovement(movement);
            StockMovements.Add(movement);
            ClearFields();

            MessageBox.Show("Stock movement added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ClearFields()
        {
            ProductId = 0;
            Quantity = 0;
            MovementType = "Addition";
            MovementDate = DateTime.Now;
            SelectedStockMovement = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
