using System.ComponentModel.DataAnnotations;

namespace Application.Model
{
    public class OrderModel
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = default!;

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; } = default!;

        public List<OrderPartModel> OrderParts { get; set; } = default!;

        [Required]
        [MaxLength(255)]
        public string ShippingAddress { get; set; } = default!;
    }
}
