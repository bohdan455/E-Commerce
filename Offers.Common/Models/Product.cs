using Offers.Common.Models.Base;

namespace Offers.Common.Models;

public class Product : BaseModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string Category { get; set; }

    public decimal Price { get; set; }

    public double Rating { get; set; }

    public DateTime CreatedAt { get; set; }
}