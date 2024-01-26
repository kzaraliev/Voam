﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voam.Server.Constants;

namespace Voam.Server.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ProductConstants.NameMaxLength)]
        public required string Name { get; set; }

        public required string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public char Size { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
