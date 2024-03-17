using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voam.Core.Models.Order
{
    public class OrderItemCreateModel
    {
        public int Quantity { get; set; }

        public char SizeChar { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }
    }
}
