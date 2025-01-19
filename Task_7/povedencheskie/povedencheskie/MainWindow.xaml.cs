using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;

namespace ShopApp
{
    public partial class MainWindow : Window
    {
        private List<Product> _products;
        private IIterator<Product> _iterator;
        private List<Product> _cartItems = new List<Product>(); // Корзина

        public MainWindow()
        {
            InitializeComponent();
            InitializeProducts();
        }

        private void InitializeProducts()
        {
            // Загрузка товаров из базы данных
            _products = LoadProductsFromDatabase();
            _iterator = new ProductIterator(_products);

            // Отображение товаров в ListBox
            ProductListBox.ItemsSource = _products;
        }

        private List<Product> LoadProductsFromDatabase()
        {
            // Используем паттерн Интерпретатор для создания запроса
            var select = new SelectExpression("Products", new List<string> { "Id", "Name", "Price" });
            string query = select.Interpret();

            // Выполнение запроса к SQLite
            using (var connection = new SQLiteConnection("Data Source=C:\\Users\\dmitr\\Desktop\\University\\OOP\\Task_7\\povedencheskie\\povedencheskie\\shop.db"))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                var reader = command.ExecuteReader();

                var products = new List<Product>();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDouble(2)
                    });
                }

                return products;
            }
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (ProductListBox.SelectedItem is Product selectedProduct)
            {
                // Добавление товара в корзину
                _cartItems.Add(selectedProduct);
                CartListBox.ItemsSource = null; // Очищаем предыдущие данные
                CartListBox.ItemsSource = _cartItems; // Обновляем корзину
                MessageBox.Show($"Добавлен в корзину: {selectedProduct.Name}");
            }
        }

        private void ClearCartButton_Click(object sender, RoutedEventArgs e)
        {
            // Очистка корзины
            _cartItems.Clear();
            CartListBox.ItemsSource = null;
        }
    }
}