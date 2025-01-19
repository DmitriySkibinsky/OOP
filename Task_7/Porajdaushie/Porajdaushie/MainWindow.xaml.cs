using System.Windows;
using System.Windows.Controls;

namespace StoreApp
{
    public partial class MainWindow : Window
    {
        private ViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModel();
            CategoriesListBox.ItemsSource = _viewModel.Categories;
        }

        private void CategoriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoriesListBox.SelectedItem is Category selectedCategory)
            {
                ProductsListBox.ItemsSource = _viewModel.GetProductsByCategory(selectedCategory.CategoryID);
            }
        }

        private void ProductsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsListBox.SelectedItem is Product selectedProduct)
            {
                ShoppingCart.Instance.AddToCart(selectedProduct);
                CartListBox.ItemsSource = null;
                CartListBox.ItemsSource = ShoppingCart.Instance.CartItems;
            }
        }

        private void ClearCartButton_Click(object sender, RoutedEventArgs e)
        {
            ShoppingCart.Instance.ClearCart();
            CartListBox.ItemsSource = null;
            CartListBox.ItemsSource = ShoppingCart.Instance.CartItems;
        }
    }
}