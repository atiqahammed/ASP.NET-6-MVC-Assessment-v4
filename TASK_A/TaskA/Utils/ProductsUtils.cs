using IronXL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskA.Model;

namespace TastA.Utils
{
    public static class ProductUtils
    {
        public static List<Product> GetProducts(string dataFilePath)
        {
            List<Product> products = new List<Product>();
            try
            {
                WorkBook workbook = WorkBook.Load(dataFilePath);
                WorkSheet sheet = workbook.DefaultWorkSheet;

                DataTable table = sheet.ToDataTable(true);

                foreach (DataRow row in table.Rows)
                {
                    var product = new Product()
                    {
                        Id = int.Parse(row.ItemArray.GetValue(0).ToString()),
                        Image = row.ItemArray.GetValue(1).ToString(),
                        Name = row.ItemArray.GetValue(2).ToString(),
                        OrderDate = Convert.ToDateTime(row.ItemArray.GetValue(3).ToString()),
                        Price = row.ItemArray.GetValue(4).ToString(),
                        DiscountPrice = row.ItemArray.GetValue(5).ToString(),
                    };
                    products.Add(product);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Something went wrong {exception.Message}");
                products = null;
            }
            return products;
        }
    }
}
