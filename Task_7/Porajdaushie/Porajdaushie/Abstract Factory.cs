using StoreApp;

public interface IProductFactory
{
    Product CreateProduct(int productID, string name, decimal price, int categoryID);
}

public class ElectronicProductFactory : IProductFactory
{
    public Product CreateProduct(int productID, string name, decimal price, int categoryID)
    {
        return new Product { ProductID = productID, Name = name, Price = price, CategoryID = categoryID };
    }
}

public class ClothingProductFactory : IProductFactory
{
    public Product CreateProduct(int productID, string name, decimal price, int categoryID)
    {
        return new Product { ProductID = productID, Name = name, Price = price, CategoryID = categoryID };
    }
}

public class BookProductFactory : IProductFactory
{
    public Product CreateProduct(int productID, string name, decimal price, int categoryID)
    {
        return new Product { ProductID = productID, Name = name, Price = price, CategoryID = categoryID };
    }
}