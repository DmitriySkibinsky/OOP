using System.Windows;
using YourNamespace;

public class ProductCardWindow : ProductCard
{
    private Window _window;

    public ProductCardWindow(Product product) : base(product)
    {
        _window = new Window
        {
            Title = "Product Details",
            Width = 300,
            Height = 200,
            Content = new ProductDetailsControl(product)
        };
    }

    public override void Show()
    {
        _window.Show();
    }

    public override void Hide()
    {
        _window.Close();
    }
}