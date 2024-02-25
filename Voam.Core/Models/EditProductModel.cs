using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voam.Core.Models
{
    public class EditProductModel
    {
        public string name { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public decimal price { get; set; }

        public bool isAvailable { get; set; }

        public int sizeS { get; set; }

        public int sizeM { get; set; }

        public int sizeL { get; set; }

        public ICollection<string> images { get; set; } = new List<string>();
    }
}
