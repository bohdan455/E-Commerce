using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class OrderPart
    {
        public int Id { get; set; }

        public Product Product { get; set; } = default!;

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public Order Order { get; set; } = default!;

        public int OrderId { get; set; }
    }
}
