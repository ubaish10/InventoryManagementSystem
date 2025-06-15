using System;
using System.Windows;
using InventoryManagementSystem.Views;

namespace InventoryManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded; // Hook the Loaded event
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // ROLE-BASED ACCESS CONTROL
            string role = Session.Role;

            if (role == "Manager" || role == "Staff")
            {
                Users.Visibility = Visibility.Collapsed;
            }
            else if (role == "Supplier")
            {
                Users.Visibility = Visibility.Collapsed;
                Orders.Visibility = Visibility.Collapsed;
                StockMovements.Visibility = Visibility.Collapsed;
                Reports.Visibility = Visibility.Collapsed;
            }
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UserControlUsers();
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UserControlProducts();
        }

        private void Suppliers_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UserControlSuppliers();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UserControlOrders();
        }

        private void StockMovements_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UserControlStockMovements();
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UserControlReports();
        }
    }
}
