using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_6._0_Minimal_API.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public string ProductCompany { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public bool IsAvailable { get; set; }
        public string? Secret { get; set; }
    }
}
