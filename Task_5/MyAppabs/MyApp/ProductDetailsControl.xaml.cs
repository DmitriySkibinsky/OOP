using System.Windows.Controls;

namespace YourNamespace
{
    public partial class ProductDetailsControl : UserControl
    {
        public Product Product { get; private set; }

        public ProductDetailsControl(Product product)
        {
            InitializeComponent();
            Product = product;
            DataContext = this;
        }
    }
}