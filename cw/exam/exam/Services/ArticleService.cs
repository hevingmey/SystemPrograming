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
    }

}