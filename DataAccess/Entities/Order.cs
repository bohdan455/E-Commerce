using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public OrderPart OrderPart { get; set; } = default!;

        public int OrderPartId { get; set; }

        public Customer Customer { get; set; } = default!;

        public int CustomerId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [MaxLength(255)]
        public string ShippingAddress { get; set; } = default!;

        public OrderStatus Status { get; set; } = default!;

        public int OrderStatusId { get; set; }
    }
}
