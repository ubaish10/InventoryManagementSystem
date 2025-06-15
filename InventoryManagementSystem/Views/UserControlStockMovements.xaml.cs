using System.Windows.Controls;
using InventoryManagementSystem.ViewModels;

namespace InventoryManagementSystem.Views
{
    public partial class UserControlStockMovements : UserControl
    {
        public UserControlStockMovements()
        {
            InitializeComponent();
            DataContext = new StockMovementsViewModel();
        }
    }
}