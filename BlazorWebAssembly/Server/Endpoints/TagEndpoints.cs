﻿using System;
using Data.Models;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssembly.Server.Endpoints;

public static class TagEndpoints
{
	public static void MapTagApi(this WebApplication app)
	{
		app.MapGet("/api/Tags", async (IBlogApi api) =>
		{
			return Results.Ok(await api.GetTagsAsync());
		});
		app.MapGet("/api/Tags/{*id}", async (IBlogApi api, string id) =>
		{
			return Results.Ok(await api.GetTagAsync(id));
		});
		app.MapPut("/api/Tags", async (IBlogApi api, [FromBody] Tag item) =>
		{
			return Results.Ok(api.SaveTagAsync(item));
		}).RequireAuthorization();
		app.MapDelete("/api/Tags/{*id}", async (IBlogApi api, string id) =>
		{
			await api.DeleteTagAsync(id);
			return Results.Ok();
		}).RequireAuthorization();
	}
}

