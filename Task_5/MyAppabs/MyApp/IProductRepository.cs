using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

public interface IProductRepository
{
    List<Product> GetAllProducts();
    List<Product> GetProductsByPriceRange(double minPrice, double maxPrice);
    List<Product> GetProductsByName(string name);
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
    public List<Product> GetProductsByPriceRange(double minPrice, double maxPrice)
    {
        var products = new List<Product>();

        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand("SELECT id, name, description, price FROM Product WHERE price BETWEEN @minPrice AND @maxPrice", connection))
            {
                command.Parameters.AddWithValue("@minPrice", minPrice);
                command.Parameters.AddWithValue("@maxPrice", maxPrice);
                using (var reader = command.ExecuteReader())
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
            }
        }

        return products;
    }

    public List<Product> GetProductsByName(string name)
    {
        var products = new List<Product>();

        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand("SELECT id, name, description, price FROM Product WHERE name LIKE @name", connection))
            {
                command.Parameters.AddWithValue("@name", $"%{name}%");
                using (var reader = command.ExecuteReader())
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
            }
        }

        return products;
    }
}