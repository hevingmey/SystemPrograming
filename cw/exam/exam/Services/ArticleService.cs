using exam.Entitys;
using exam.Repository;

namespace exam.Services;

public class ArticleService
{
    private readonly RepositoryArticle _repositoryArticle=new RepositoryArticle();

    public async Task ShowAllArticlesAsync()
    {
        var articl =await _repositoryArticle.GetAllArticles();
        foreach (var a in articl)
        {
            Console.WriteLine($"{a.Id}, {a.Author}, {a.Title}, {a.Description}");
        }
    }

    public async Task ShowArticleByIdAsync(int id)
    {
        var artical = await _repositoryArticle.GetArticlesById(id);

        Console.WriteLine($"{artical.Id},{artical.Title}");
        
    }

    public async Task ShowArticleByAuthorAsync(string author)
    {
        var artical = await _repositoryArticle.GetArticlesByAuthor(author);
        foreach (var a in artical)
        {
            Console.WriteLine($"{a.Id}, {a.Author}, {a.Title}, {a.Description}");
        }
    }


    public async Task AddArticleAsync()
    {
        Article article = new Article();

        Console.Write("enter id ");
        article.Id = int.Parse(Console.ReadLine());

        Console.Write("enter title: ");
        article.Title = Console.ReadLine()!;

        Console.Write("nter description: ");
        article.Description = Console.ReadLine()!;

        Console.Write("enter image: ");
        article.Image = Console.ReadLine();

        Console.Write("enter author: ");
        article.Author = Console.ReadLine()!;

        article.Date = DateTime.Now;

        int? id = await _repositoryArticle.AddArticleAsync(article);

        if (id != null)
        {
            Console.WriteLine($"created id: {id}");
        }
        else
        {
            Console.WriteLine("Error creating article");
        }
    }

    public async Task DeleteArticleAsync(int id)
    {
        bool isRemoved = await _repositoryArticle.DeleteArticleByIdAsync(id);
        if (isRemoved)
        {
            Console.WriteLine("Article deleted successfully");
            
        }
        else
        {
            Console.WriteLine("Error deleting article");
        }
    }

    public async Task PatchArticleAsync(int id)
    {
        Console.WriteLine("Ennter new title");
        string title = Console.ReadLine();
        Console.WriteLine("enter author");
        string author = Console.ReadLine();

        var update = new
        {
            Title = title,
            Author = author
        };
        bool isUpdated = await _repositoryArticle.PatchArticleAsync(id, update);
        if (isUpdated)
        {
            Console.WriteLine("Article updated successfully");
        }
        else
            {
            Console.WriteLine("Error updating article");
            }

    }

}