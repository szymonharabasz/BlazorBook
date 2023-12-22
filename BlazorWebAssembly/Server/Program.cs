using Data;
using Data.Models.Interfaces;
using BlazorWebAssembly.Server.Endpoints;
using BlazorWebAssembly.Server.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddOptions<BlogApiJsonDirectAccessSetting>().Configure(options =>
{
    options.DataPath = @"../../Data";
    options.BlogPostsFolder = "Blogposts";
    options.CategoriesFolder = "Categories";
    options.TagsFolder = "Tags";
});
builder.Services.AddScoped<IBlogApi, BlogApiJsonDirectAccess>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
    {
        c.Authority = builder.Configuration["Auth0:Authority"];
        c.TokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = builder.Configuration["Auth0:Audience"],
            ValidIssuer = builder.Configuration["Auth0:Authority"]
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.MapBlogPostApi();
app.MapCategoryApi();
app.MapTagApi();

app.MapRazorPages();
app.MapControllers();
app.MapHub<BlogNotificationHub>("/BlogNotificationHub");
app.MapFallbackToFile("index.html");

app.Run();
