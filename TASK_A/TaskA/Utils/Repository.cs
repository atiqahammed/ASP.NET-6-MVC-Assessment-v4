using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskA.Model;

namespace TastA.Utils
{
    public static class Repository
    {
        private static readonly string connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=Test_DB_For_Task";
        private static readonly string tableName = "Product";

        public static bool TableExists()
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the table exists in the current schema
                    using (var command = new NpgsqlCommand($"SELECT EXISTS (SELECT FROM information_schema.tables WHERE table_name = '{tableName}')", connection))
                    {
                        return (bool)command.ExecuteScalar();
                    }
                }
            } 
            catch(Exception ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
                return false;
            }
            
        }

        public static bool CreateTable()
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using var cmd = new NpgsqlCommand();
                    cmd.Connection = connection;

                    cmd.CommandText = @"CREATE TABLE " + tableName + @"(id INT PRIMARY KEY,
                        name VARCHAR(255), image VARCHAR(255), order_date DATE, price VARCHAR(255), discount_price VARCHAR(255))";
                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
                return false;
            }
        }

        public static bool SaveProducts(List<Product> products)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using var cmd = new NpgsqlCommand();
                    cmd.Connection = connection;

                    foreach (var product in products)
                    {
                        cmd.CommandText = $"INSERT INTO {tableName}(id, name, image, price, discount_price, order_date) VALUES('{product.Id}','{product.Name}','{product.Image}','{product.Price}','{product.DiscountPrice}','{product.OrderDate.ToString()}')";
                        cmd.ExecuteNonQuery();
                    }
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
                return false;
            }
        }
    }

    
}
