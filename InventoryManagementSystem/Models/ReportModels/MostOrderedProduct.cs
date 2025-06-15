using System.ComponentModel;

namespace InventoryManagementSystem.Models
{
    public class MostOrderedProduct : INotifyPropertyChanged
    {
        private string productName;
        private int totalOrdered;

        public string ProductName
        {
            get => productName;
            set { productName = value; OnPropertyChanged(nameof(ProductName)); }
        }

        public int TotalOrdered
        {
            get => totalOrdered;
            set { totalOrdered = value; OnPropertyChanged(nameof(TotalOrdered)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}