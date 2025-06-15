using System.Windows.Controls;
using InventoryManagementSystem.ViewModels;

namespace InventoryManagementSystem.Views
{
    public partial class UserControlUsers : UserControl
    {
        public UserControlUsers()
        {
            InitializeComponent();
            DataContext = new UsersViewModel();
        }
    }
}