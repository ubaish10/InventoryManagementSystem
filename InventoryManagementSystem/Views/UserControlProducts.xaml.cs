using System.Windows.Controls;
using InventoryManagementSystem.ViewModels;

namespace InventoryManagementSystem.Views
{
    public partial class UserControlProducts : UserControl
    {
        public UserControlProducts()
        {
            InitializeComponent();
            DataContext = new ProductsViewModel();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}