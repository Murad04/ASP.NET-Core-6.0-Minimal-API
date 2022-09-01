using ASP.NET_Core_6._0_Minimal_API.Models;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_6._0_Minimal_API.DTO
{
    public class ProductsDTO
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public string ProductCompany { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public bool IsAvailable { get; set; }

        public ProductsDTO() { }
        public ProductsDTO(Products product) =>
            (ProductID, ProductName, ProductDescription, ProductCompany, ProductPrice, ProductCount, IsAvailable)
            = (product.ProductID, product.ProductName, product.ProductDescription, product.ProductCompany, product.ProductPrice, product.ProductCount, product.IsAvailable);
    }
}
