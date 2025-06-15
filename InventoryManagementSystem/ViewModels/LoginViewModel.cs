using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Views;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace InventoryManagementSystem.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string username;
        private string password;
        private string errorMessage;
        private readonly DatabaseContext dbContext;

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

        public string ErrorMessage
        {
            get => errorMessage;
            set { errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            dbContext = new DatabaseContext();
            LoginCommand = new RelayCommand(Login);
        }


        private void Login(object parameter)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Please enter both username and password.";
                return;
            }

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GROCVLK\\SQLEXPRESS;Initial Catalog=InventoryDataBase;Integrated Security=True"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Username, Role FROM Users WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@Password", Password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Session.Username = reader["Username"].ToString();
                                Session.Role = reader["Role"].ToString();

                                MessageBox.Show($"Login successful as {Session.Role}!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                                Application.Current.Windows[0].Close();
                            }
                            else
                            {
                                ErrorMessage = "Invalid username or password.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Database error: {ex.Message}";
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => canExecute == null || canExecute(parameter);

        public void Execute(object parameter) => execute(parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}