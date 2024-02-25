using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voam.Core.Models
{
    public class EditProductModel
    {
        public string name { get; set; }

        public string description { get; set; }

        public decimal price { get; set; }

        public bool isAvailable { get; set; }

        public int sizeS { get; set; }

        public int sizeM { get; set; }

        public int sizeL { get; set; }

        public ICollection<ProductImageModel> images { get; set; }
    }
}
