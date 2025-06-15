using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.ViewModels
{
    public class UsersViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseContext dbContext;
        private ObservableCollection<User> users;
        private User selectedUser;
        private string username;
        private string password;

        public ObservableCollection<User> Users
        {
            get => users;
            set { users = value; OnPropertyChanged(nameof(Users)); }
        }

        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                if (selectedUser != null)
                {
                    Username = selectedUser.Username;
                    Password = selectedUser.Password;
                }
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public string Username
        {
            get => username;
            set { username = value; OnPropertyChanged(nameof(Username)); }
        }

        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged(nameof(Password)); }
        }

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public UsersViewModel()
        {
            dbContext = new DatabaseContext();
            Users = new ObservableCollection<User>(dbContext.GetUsers());

            // 🔥 No CanExecute
            AddCommand = new RelayCommand(AddUser);
            UpdateCommand = new RelayCommand(UpdateUser);
            DeleteCommand = new RelayCommand(DeleteUser);
        }

        private void AddUser(object parameter)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            var user = new User { Username = Username, Password = Password };
            dbContext.AddUser(user);
            Users.Add(user);
            ClearFields();
            MessageBox.Show("New user added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateUser(object parameter)
        {
            if (SelectedUser == null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please select a user to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            SelectedUser.Username = Username;
            SelectedUser.Password = Password;
            dbContext.UpdateUser(SelectedUser);
            ClearFields();
            MessageBox.Show("User updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteUser(object parameter)
        {
            if (SelectedUser == null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show("Please select a user to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
                return;
            }

            dbContext.DeleteUser(SelectedUser.Id);
            Users.Remove(SelectedUser);
            ClearFields();
            MessageBox.Show("User deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ClearFields()
        {
            Username = string.Empty;
            Password = string.Empty;
            SelectedUser = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
