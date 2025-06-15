using System.Windows.Controls;
using InventoryManagementSystem.ViewModels;

namespace InventoryManagementSystem.Views
{
    public partial class UserControlOrders : UserControl
    {
        public UserControlOrders()
        {
            InitializeComponent();
            DataContext = new OrdersViewModel();
        }
    }
}