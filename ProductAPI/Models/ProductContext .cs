using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Models
{
    public class ProductContext: DbContext
    {
      public ProductContext(DbContextOptions<ProductContext> options): base(options)
        {
        }

        public DbSet<Product> TodoItems { get; set; } = null!;
    }
}
