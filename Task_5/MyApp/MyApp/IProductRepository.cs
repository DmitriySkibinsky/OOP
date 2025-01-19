using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

public interface IProductRepository
{
    List<Product> GetAllProducts();
    void AddProduct(Product product);
}

public class SQLiteProductRepository : IProductRepository
{
    private readonly string _connectionString;

    public SQLiteProductRepository(string dbPath)
    {
        // Проверяем, существует ли база данных, если нет, то создаем ее
        if (!File.Exists(dbPath))
        {
            SQLiteConnection.CreateFile(dbPath);
            Console.WriteLine($"Database created at: {dbPath}");
        }
        else
        {
            Console.WriteLine($"Database already exists at: {dbPath}");
        }

        _connectionString = $"Data Source={dbPath};Version=3;";
        CreateProductTableIfNotExists();
    }

    private void CreateProductTableIfNotExists()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(
                "CREATE TABLE IF NOT EXISTS Product (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, description TEXT, price REAL)", connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public List<Product> GetAllProducts()
    {
        var products = new List<Product>();

        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand("SELECT id, name, description, price FROM Product", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    try
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Price = reader.GetDouble(3)
                            });
                        }
                    }
                    catch { }
                    
                }
            }
        }

        return products;
    }

    public void AddProduct(Product product)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(
                "INSERT INTO Product (name, description, price) VALUES (@name, @description, @price)", connection))
            {
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@price", product.Price);
                command.ExecuteNonQuery();
            }
        }
    }
}