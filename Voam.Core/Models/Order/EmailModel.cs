namespace Voam.Core.Models.Order
{
    public class EmailModel
    {
        public required string To { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
