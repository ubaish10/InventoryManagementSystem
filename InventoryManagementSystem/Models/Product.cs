using System.ComponentModel;

namespace InventoryManagementSystem.Models
{
    public class Product : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string sku;
        private int quantity;
        private decimal price;
        private int lowStockThreshold;
        private int? supplierId;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(nameof(Id)); }
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}