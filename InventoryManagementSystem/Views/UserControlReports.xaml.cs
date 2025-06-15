using InventoryManagementSystem.ViewModels;
using System.Windows.Controls;

namespace InventoryManagementSystem.Views
{
    public partial class UserControlReports : UserControl
    {
        public UserControlReports()
        {
            InitializeComponent();
            DataContext = new ReportsViewModel();
        }
    }
}