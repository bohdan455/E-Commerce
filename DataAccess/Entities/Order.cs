using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public List<OrderPart> OrderParts { get; set; } = default!;

        public Customer Customer { get; set; } = default!;

        public int CustomerId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [MaxLength(255)]
        public string ShippingAddress { get; set; } = default!;

        public OrderStatus OrderStatus { get; set; } = default!;

        public int OrderStatusId { get; set; }
    }
}
