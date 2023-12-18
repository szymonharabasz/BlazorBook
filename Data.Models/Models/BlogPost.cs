using System;
namespace Data.Models;

public class BlogPost
{
	public string? Id { get; set; }
	public string Title { get; set; } = String.Empty;
	public string Text { get; set; } = String.Empty;
	public DateTime PublishDate { get; set; }
	public Category? Categoty { get; set; }
	public List<Tag> Tags { get; set; } = new();
}

