using AppWEB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppWEB.Data.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.Price).HasPrecision(18,2);
            builder.Property(e => e.Name).HasMaxLength(150);
            builder.Property(e=>e.Description).HasMaxLength(555);
            
        }
    }
}
