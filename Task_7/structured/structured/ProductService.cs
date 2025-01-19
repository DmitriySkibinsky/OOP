public class ProductService
{
    private readonly SQLiteAdapter _adapter;

    public ProductService(SQLiteAdapter adapter)
    {
        _adapter = adapter;
    }

    public List<Product> GetAllProducts()
    {
        return _adapter.GetProducts();
    }
}