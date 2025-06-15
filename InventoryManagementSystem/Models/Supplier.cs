using System.ComponentModel;

namespace InventoryManagementSystem.Models
{
    public class Supplier : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string contactInfo;

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

        public string ContactInfo
        {
            get => contactInfo;
            set { contactInfo = value; OnPropertyChanged(nameof(ContactInfo)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}