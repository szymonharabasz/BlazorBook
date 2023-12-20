using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Data.Models;
using Data.Models.Interfaces;

namespace BlazorWebAssembly.Client;

public class BlogApiWebClient : IBlogApi
{
    private readonly IHttpClientFactory _factory;

	public BlogApiWebClient(IHttpClientFactory factory)
	{
        _factory = factory;
	}

    public async Task DeleteBlogPostAsync(string id)
    {
        try
        {
            var httpClient = _factory.CreateClient("Authenticated");
            await httpClient.DeleteAsync($"api/BlogPosts/{id}");
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
    }

    public async Task DeleteCategoryAsync(string id)
    {
        try
        {
            var httpClient = _factory.CreateClient("Authenticated");
            await httpClient.DeleteAsync($"api/Categories/{id}");
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
    }

    public async Task DeleteTagAsync(string id)
    {
        try
        {
            var httpClient = _factory.CreateClient("Authenticated");
            await httpClient.DeleteAsync($"api/BlogPosts/{id}");
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
    }

    public async Task<BlogPost?> GetBlogPostAsync(string id)
    {
        var httpClient = _factory.CreateClient("Public");
        return await httpClient.GetFromJsonAsync<BlogPost>($"/api/BlogPosts/{id}");
    }

    public async Task<int> GetBlogPostCountAsync()
    {
        var httpClient = _factory.CreateClient("Public");
        return await httpClient.GetFromJsonAsync<int>("/api/BlogPostCount");
    }

    public async Task<List<BlogPost>?> GetBlogPostsAsync(int numberofposts, int startindex)
    {
        var httpClient = _factory.CreateClient("Public");
        return await httpClient.GetFromJsonAsync<List<BlogPost>>($"/api/BlogPosts?numberofposts={numberofposts}&startindex={startindex}");
    }

    public async Task<List<Category>?> GetCategoriesAsync()
    {
        var httpClient = _factory.CreateClient("Public");
        return await httpClient.GetFromJsonAsync<List<Category>>($"/api/Categories");
    }

    public async Task<Category?> GetCategoryAsync(string id)
    {
        var httpClient = _factory.CreateClient("Public");
        return await httpClient.GetFromJsonAsync<Category>($"/api/Categories/{id}");
    }

    public async Task<Tag?> GetTagAsync(string id)
    {
        var httpClient = _factory.CreateClient("Public");
        return await httpClient.GetFromJsonAsync<Tag>($"/api/Tags/{id}");
    }

    public async Task<List<Tag>?> GetTagsAsync()
    {
        var httpClient = _factory.CreateClient("Public");
        return await httpClient.GetFromJsonAsync<List<Tag>>($"/api/Tags");
    }

    public Task InvalidateCacheAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<BlogPost?> SaveBlogPostAsync(BlogPost item)
    {
        try
        {
            var httpClient = _factory.CreateClient("Authenticated");
            var response = await httpClient.PutAsJsonAsync<BlogPost>("/api/BlogPost", item);
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BlogPost>(json);
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
        return null;
    }

    public async Task<Category?> SaveCategoryAsync(Category item)
    {
        try
        {
            var httpClient = _factory.CreateClient("Authenticated");
            var response = await httpClient.PutAsJsonAsync<Category>("/api/Categories", item);
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Category>(json);
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
        return null;
    }

    public async Task<Tag?> SaveTagAsync(Tag item)
    {
        try
        {
            var httpClient = _factory.CreateClient("Authenticated");
            var response = await httpClient.PutAsJsonAsync<Tag>("/api/Tags", item);
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Tag>(json);
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
        return null;
    }
}

