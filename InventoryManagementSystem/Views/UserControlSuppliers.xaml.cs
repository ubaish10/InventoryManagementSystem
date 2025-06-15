using System.Windows.Controls;
using InventoryManagementSystem.ViewModels;

namespace InventoryManagementSystem.Views
{
    public partial class UserControlSuppliers : UserControl
    {
        public UserControlSuppliers()
        {
            InitializeComponent();
            DataContext = new SuppliersViewModel();
        }
    }
}