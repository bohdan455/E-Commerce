using System.ComponentModel.DataAnnotations;
using Offers.Common.Models;

namespace Offers.Requests;

public class ProductRequest
{
    public string Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Category { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public double Rating { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public Product ToModel()
    {
        return new Product
        {
            Id = Id,
            Category = Category,
            Title = Title,
            Description = Description,
            Price = Price,
            Rating = Rating,
            CreatedAt = CreatedAt
        };
    }
}