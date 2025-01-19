using System.Windows;

namespace YourNamespace
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Укажите путь к вашей базе данных SQLite
            string dbPath = @"C:\Users\dmitr\Desktop\University\OOP\Task_5\MyApp\MyApp\bookshop.db";
            var repository = new SQLiteProductRepository(dbPath);
            var orderService = new OrderService();
            var viewModel = new ProductViewModel(repository, orderService);

            // Устанавливаем ViewModel как контекст данных для окна
            DataContext = viewModel;

        }
    }
}