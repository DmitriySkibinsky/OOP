using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace StoreApp
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string databasePath)
        {
            _connectionString = $"Data Source={databasePath};Version=3;";
        }

        public List<Category> GetCategories()
        {
            var categories = new List<Category>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT CategoryID, Name FROM Categories", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryID = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
            return categories;
        }

        public List<Product> GetProductsByCategory(int categoryID)
        {
            var products = new List<Product>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT ProductID, Name, Description, Price FROM Products WHERE CategoryID = @CategoryID", connection);
                command.Parameters.AddWithValue("@CategoryID", categoryID);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Price = reader.GetDecimal(3),
                            CategoryID = categoryID
                        });
                    }
                }
            }
            return products;
        }
    }
}