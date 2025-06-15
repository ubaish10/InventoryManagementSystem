using System;
using System.ComponentModel;

namespace InventoryManagementSystem.Models
{
    public class StockMovement : INotifyPropertyChanged
    {
        private int id;
        private int productId;
        private int quantity;
        private string movementType;
        private DateTime movementDate;

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}