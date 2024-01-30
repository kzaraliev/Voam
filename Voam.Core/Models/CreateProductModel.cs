namespace Voam.Core.Models
{
    public class CreateProductModel
    {
        public string name { get; set; }

        public string description { get; set; }

        public decimal price { get; set; }

        public bool isAvailable { get; set; }

        public int sizeS { get; set; }

        public int sizeM { get; set; }

        public int sizeL { get; set; }

        public ICollection<string> images { get; set; }
    }
}
