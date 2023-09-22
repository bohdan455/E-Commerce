using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class OrderStatus
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Status { get; set; } = default!;
    }
}
