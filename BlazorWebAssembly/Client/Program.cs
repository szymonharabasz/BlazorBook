using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWebAssembly.Client;
using BlazorWebAssembly.Client.Services;
using Components.Interfaces;
using Components.RazorComponents;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IBlogApi, BlogApiWebClient>();
builder.Services.AddHttpClient("Public", client => client.BaseAddress =
    new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddHttpClient("Authenticated", client => client.BaseAddress =
    new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience",
        builder.Configuration["Auth0:Audience"]);
}).AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();
builder.Services.AddTransient<ILoginStatus, LoginStatusWasm>();

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<IBrowserStorage, BlogBrowserStorage>();
builder.Services.AddSingleton<IBlogNotificationService, BlazorWebAssemblyBlogNotificationService>();

await builder.Build().RunAsync();
