using System.Windows;

namespace YourNamespace
{
    public partial class AddProductWindow : Window
    {
        public AddProductWindow(ProductViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}