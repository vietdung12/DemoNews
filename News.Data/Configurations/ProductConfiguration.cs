using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Local).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Price).HasMaxLength(100).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(1);

        }
    }
}
