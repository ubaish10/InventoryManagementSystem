using System.ComponentModel;

namespace InventoryManagementSystem.Models
{
    public class SupplierPerformance : INotifyPropertyChanged
    {
        private string supplierName;
        private int totalOrders;

        public string SupplierName
        {
            get => supplierName;
            set { supplierName = value; OnPropertyChanged(nameof(SupplierName)); }
        }

        public int TotalOrders
        {
            get => totalOrders;
            set { totalOrders = value; OnPropertyChanged(nameof(TotalOrders)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}