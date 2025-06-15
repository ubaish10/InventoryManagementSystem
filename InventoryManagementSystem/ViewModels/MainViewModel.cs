using System.ComponentModel;

namespace InventoryManagementSystem.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object selectedViewModel;

        public object SelectedViewModel
        {
            get => selectedViewModel;
            set { selectedViewModel = value; OnPropertyChanged(nameof(SelectedViewModel)); }
        }

        public MainViewModel()
        {
            SelectedViewModel = new UsersViewModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}