using System.Text;
using System.Text.Json;
using exam.Entitys;

namespace exam.Repository;

public class RepositoryArticle
{
  private  readonly HttpClient _client = new HttpClient();
  private readonly string _url="http://localhost:3000/articles";

  private readonly JsonSerializerOptions _options = new JsonSerializerOptions
  {
    PropertyNameCaseInsensitive =  true,
    WriteIndented = true
  };


  public async Task<List<Article>> GetAllArticles()
  {
    var art =await _client.GetStringAsync(_url);
    return JsonSerializer.Deserialize<List<Article>>(art, _options);
  }

  public async Task<Article> GetArticlesById(int id)
  {
    var art=await _client.GetStringAsync($"{_url}/{id}");
    return JsonSerializer.Deserialize<Article>(art,_options);
  }

  public async Task<List<Article>> GetArticlesByAuthor(string author)
  {
    var art = await _client.GetStringAsync($"{_url}?author={author}");
    return JsonSerializer.Deserialize<List<Article>>(art, _options);
  }

  public async Task<int?> AddArticleAsync(Article art)
  {
    var json = JsonSerializer.Serialize(art);
    var data = new StringContent(json, Encoding.UTF8, "application/json");
    var response = await _client.PostAsync(_url, data);

    if (response.IsSuccessStatusCode)
    {
      string result = await response.Content.ReadAsStringAsync();
      Article? article = JsonSerializer.Deserialize<Article>(result);

      if (article != null)
      {
        return article.Id;
      }
    }

    return null;
  }

  public async Task<bool> DeleteArticleByIdAsync(int id)
  {
    var delArt = await _client.DeleteAsync($"{_url}/{id}");
      return delArt.IsSuccessStatusCode;
  }
  public async Task<bool> PatchArticleAsync(int id, object update)
  {
    var json = JsonSerializer.Serialize(update);
    var data=new StringContent(json, Encoding.UTF8, "application/json");
    var request = new HttpRequestMessage(new HttpMethod("Patch"), $"{_url}/{id}")
    {
      Content = data
    };
    var response =await _client.SendAsync(request);
    return response.IsSuccessStatusCode;
  }
  
}