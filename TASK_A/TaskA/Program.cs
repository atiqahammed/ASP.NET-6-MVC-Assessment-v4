using TaskA.Model;
using TastA.Utils;

string fileName = "Data.xlsx";
string dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

if (!File.Exists(dataFilePath))
{
    Console.WriteLine("File Not Found");
    throw new FileNotFoundException($"Excel File Not Found: {dataFilePath}");
}

Console.WriteLine($"Getting Products From File.");
List<Product> products = ProductUtils.GetProducts(dataFilePath);
Console.WriteLine($"Found Products From File.");
var outputFileName = "Products.json";
Console.WriteLine($"Writting Products To JSON File.");
JsonFileUtils.SimpleWrite(products, outputFileName);
Console.WriteLine($"Completed File Writing To JSON. File Path {Path.Combine(AppDomain.CurrentDomain.BaseDirectory, outputFileName)}");
