using ASP.NET_Core_6._0_Minimal_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_6._0_Minimal_API.Context
{
    public class ProductsDB : DbContext
    {
        public ProductsDB(DbContextOptions<ProductsDB> options) : base(options) { }

        public DbSet<Products> Products => Set<Products>();
    }
}
