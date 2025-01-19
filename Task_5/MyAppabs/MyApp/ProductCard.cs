using System.Windows;

public abstract class ProductCard
{
    public Product Product { get; private set; }

    protected ProductCard(Product product)
    {
        Product = product;
    }

    public abstract void Show();

    public abstract void Hide();
}