using System.Globalization;
using exam.Services;

namespace exam;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Exam");

        var service = new ArticleService();
        Console.WriteLine("1: Show all");
        Console.WriteLine("2: Show by id");
        Console.WriteLine("3: Show by author");
        Console.WriteLine("4: Add article");
        Console.WriteLine("5: Delete article");
        Console.WriteLine("6: Patch article");
        
        int choice=int.Parse(Console.ReadLine());
        switch (choice)
        {
            case 1:
                await service.ShowAllArticlesAsync();
                break;
            case 2:
                int entId=int.Parse(Console.ReadLine());
                await service.ShowArticleByIdAsync(entId);
                break;
            case 3:
                Console.WriteLine("enter name");
                string name=Console.ReadLine();
                await service.ShowArticleByAuthorAsync(name);
                break;
            case 4:
                Console.WriteLine("add article");
                await service.AddArticleAsync();
                break;
            case 5:
                Console.WriteLine("delete article");
                Console.WriteLine("****************");
                Console.WriteLine("enter id author ");
                int IdForDelete = int.Parse(Console.ReadLine());
                await service.DeleteArticleAsync(IdForDelete);
                break;
            case 6:
                Console.WriteLine("Patch article");
                int patchId=int.Parse(Console.ReadLine());
                await service.PatchArticleAsync(patchId);
                break;
            
        }

    }
}