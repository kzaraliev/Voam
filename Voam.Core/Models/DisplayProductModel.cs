﻿
namespace Voam.Core.Models
{
    public class DisplayProductModel
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public ProductImageModel Image { get; set; } = null!;
    }
}
