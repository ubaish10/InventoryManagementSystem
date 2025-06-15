using System.ComponentModel;

namespace InventoryManagementSystem.Models
{
    public class StockTurnover : INotifyPropertyChanged
    {
        private string productName;
        private double turnoverRate;

        public string ProductName
        {
            get => productName;
            set { productName = value; OnPropertyChanged(nameof(ProductName)); }
        }

        public double TurnoverRate
        {
            get => turnoverRate;
            set { turnoverRate = value; OnPropertyChanged(nameof(TurnoverRate)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}