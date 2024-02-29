using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voam.Core.Models
{
    public class EditProductModel
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public int SizeS { get; set; }

        public int SizeM { get; set; }

        public int SizeL { get; set; }

        public ICollection<string> Images { get; set; } = new List<string>();
    }
}
