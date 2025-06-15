using System;
using System.Globalization;
using System.Windows.Data;
using InventoryManagementSystem.ViewModels;

namespace InventoryManagementSystem.Converters
{
    public class ViewModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string param = parameter as string;

            switch (param)
            {
                case "UsersViewModel":
                    return new UsersViewModel();
                case "ProductsViewModel":
                    return new ProductsViewModel();
                case "SuppliersViewModel":
                    return new SuppliersViewModel();
                case "OrdersViewModel":
                    return new OrdersViewModel();
                case "StockMovementsViewModel":
                    return new StockMovementsViewModel();
                case "ReportsViewModel":
                    return new ReportsViewModel();
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
