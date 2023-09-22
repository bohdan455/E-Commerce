using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; } = default!;

        [MaxLength(255)]
        public string Email { get; set; } = default!;
    }
}
