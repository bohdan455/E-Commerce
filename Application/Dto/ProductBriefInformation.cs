namespace Application.Dto
{
    public class ProductBriefInformation
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string ImageUrl { get; set; } = default!;

        public decimal Price { get; set; }
    }
}
