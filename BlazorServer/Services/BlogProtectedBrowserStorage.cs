using Components.Interfaces;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorServer.Services;

public class BlogProtectedBrowserStorage : IBrowserStorage
{
    private ProtectedBrowserStorage Storage { get; set; }

    public BlogProtectedBrowserStorage(ProtectedBrowserStorage storage)
    {
        Storage = storage;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await Storage.GetAsync<T>(key);
        if (value.Success)
        {
            return value.Value;
        }
        else
        {
            return default(T);
        }
    }

    public async Task SetAsync(string key, object value)
    {
        await Storage.SetAsync(key, value);
    }

    public async Task DeleteAsync(string key)
    {
        await Storage.DeleteAsync(key);
    }
}