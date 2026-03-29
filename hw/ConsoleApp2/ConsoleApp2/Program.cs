namespace ConsoleApp2;

using System.Text.Json;

class Product
{
    public int id { get; set; }
    public string title { get; set; }
    public decimal price { get; set; }

    public override string ToString()
    {
        return $"ID: {id}, Title: {title}, Price: {price}";
    }
}

class Program
{
    private static readonly string URL = "https://fakestoreapi.com/products";

    static async Task<List<Product>> GetProducts()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var response = await client.GetStringAsync(URL);
                var obj = JsonSerializer.Deserialize<List<Product>>(response);
                if (obj != null)
                {
                    return obj;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
    }

    static async Task<Product> GetProductById(int id)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var response = await client.GetStringAsync($"{URL}/{id}");
                var obj = JsonSerializer.Deserialize<Product>(response);
                if (obj != null)
                {
                    return obj;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("1 - всі товари");
        Console.WriteLine("2 - товар по ID");

        string choice = Console.ReadLine();

        if (choice == "1")
        {
            var task = GetProducts();

            Console.Write("Loading ");
            while (!task.IsCompleted)
            {
                Console.Write("*");
                Thread.Sleep(200);
            }

            var products = task.Result;

            Console.WriteLine();

            if (products != null)
            {
                foreach (var item in products)
                {
                    Console.WriteLine(item);
                }
            }
        }
        else if (choice == "2")
        {
            Console.Write("Enter ID: ");
            int id = int.Parse(Console.ReadLine());

            var task = GetProductById(id);

            Console.Write("Loading ");
            while (!task.IsCompleted)
            {
                Console.Write("*");
                Thread.Sleep(200);
            }

            var product = task.Result;

            Console.WriteLine();

            if (product != null)
            {
                Console.WriteLine(product);
            }
        }
    }
}