using System.Windows;

namespace ShopApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Подключение к базе данных
            var connectionString = "Data Source=C:\\Users\\dmitr\\Desktop\\University\\OOP\\Task_7\\structured\\str.db;Version=3;";
            var adapter = new SQLiteAdapter(connectionString);
            var productService = new ProductService(adapter);

            // Получение данных
            var products = productService.GetAllProducts();

            // Отображение данных в ListView
            ProductListView.ItemsSource = products;
        }
    }
}