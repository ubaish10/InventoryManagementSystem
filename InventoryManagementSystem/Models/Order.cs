using System;
using System.ComponentModel;

namespace InventoryManagementSystem.Models
{
    public class Order : INotifyPropertyChanged
    {
        private int id;
        private int productId;
        private int supplierId;
        private int quantity;
        private DateTime orderDate;
        private string status;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(nameof(Id)); }
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}