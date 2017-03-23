using System.ComponentModel.DataAnnotations;

namespace GetStartedDotnet.Models
{
    public class Product
    {
        [Key]
        public int productId { get; set; }
        public string productName { get; set; }
        public string productCode { get; set; }
        public string releaseDate { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public float starRating { get; set; }
        public string imageUrl { get; set; }
    }
}