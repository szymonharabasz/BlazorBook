﻿using System;
using Data.Models;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssembly.Server.Endpoints;

public static class CategoryEndpoints
{
	public static void MapCategoryApi(this WebApplication app)
	{
		app.MapGet("/api/Categories", async (IBlogApi api) =>
		{
			return Results.Ok(await api.GetCategoriesAsync());
		});
		app.MapGet("/api/Categories/{*id}", async (IBlogApi api, string id) =>
		{
			return Results.Ok(await api.GetCategoryAsync(id));
		});
		app.MapPut("/api/Categories", async (IBlogApi api, [FromBody] Category item) =>
		{
			return Results.Ok(api.SaveCategoryAsync(item));
		}).RequireAuthorization();
		app.MapDelete("/api/Categories/{*id}", async (IBlogApi api, string id) =>
		{
			await api.DeleteCategoryAsync(id);
			return Results.Ok();
		}).RequireAuthorization();
	}
}

