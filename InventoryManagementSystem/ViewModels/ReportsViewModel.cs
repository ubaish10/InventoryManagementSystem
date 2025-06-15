using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace InventoryManagementSystem.ViewModels
{
    public class ReportsViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseContext dbContext;
        private ObservableCollection<MostOrderedProduct> mostOrderedProducts;
        private ObservableCollection<SupplierPerformance> supplierPerformance;
        private ObservableCollection<StockTurnover> stockTurnover;

        public ObservableCollection<MostOrderedProduct> MostOrderedProducts
        {
            get => mostOrderedProducts;
            set { mostOrderedProducts = value; OnPropertyChanged(nameof(MostOrderedProducts)); }
        }

        public ObservableCollection<SupplierPerformance> SupplierPerformance
        {
            get => supplierPerformance;
            set { supplierPerformance = value; OnPropertyChanged(nameof(SupplierPerformance)); }
        }

        public ObservableCollection<StockTurnover> StockTurnover
        {
            get => stockTurnover;
            set { stockTurnover = value; OnPropertyChanged(nameof(StockTurnover)); }
        }

        public ICommand GenerateReportsCommand { get; }

        public ReportsViewModel()
        {
            dbContext = new DatabaseContext();
            GenerateReportsCommand = new RelayCommand(GenerateReports);
            GenerateReports(null);
        }

        private void GenerateReports(object parameter)
        {
            var mostOrdered = dbContext.GetMostOrderedProducts();
            MostOrderedProducts = new ObservableCollection<MostOrderedProduct>(
                mostOrdered.Select(t => new MostOrderedProduct { ProductName = t.Item1, TotalOrdered = t.Item2 }));

            var supplierPerf = dbContext.GetSupplierPerformance();
            SupplierPerformance = new ObservableCollection<SupplierPerformance>(
                supplierPerf.Select(t => new SupplierPerformance { SupplierName = t.Item1, TotalOrders = t.Item2 }));

            var stockTurn = dbContext.GetStockTurnover();
            StockTurnover = new ObservableCollection<StockTurnover>(
                stockTurn.Select(t => new StockTurnover { ProductName = t.Item1, TurnoverRate = t.Item2 }));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}