using System.Collections.Generic;
using System.Data.SQLite;

public class SQLiteAdapter
{
    private readonly string _connectionString;

    public SQLiteAdapter(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Product> GetProducts()
    {
        var products = new List<Product>();
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = new SQLiteCommand("SELECT * FROM Products", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        Description = reader.GetString(3)
                    });
                }
            }
        }
        return products;
    }
}