using Components.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorWebAssembly.Client.Services;

public class BlazorWebAssemblyBlogNotificationService : IBlogNotificationService, IAsyncDisposable
{
    private readonly HubConnection _hubConnection;
    public event Action<BlogPost>? BlogPostChanged;
    
    public BlazorWebAssemblyBlogNotificationService(NavigationManager navigationManager)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri("/BlogNotificationHub"))
            .Build();
        _hubConnection.On<BlogPost>("BlogPostChanged", (post) =>
        {
            BlogPostChanged?.Invoke(post);
        });
        _hubConnection.StartAsync();
    }

    public async Task SendNotification(BlogPost post)
    {
        await _hubConnection.SendAsync("SendNotification", post);
    }

    public async ValueTask DisposeAsync()
    {
        await _hubConnection.DisposeAsync();
    }
}