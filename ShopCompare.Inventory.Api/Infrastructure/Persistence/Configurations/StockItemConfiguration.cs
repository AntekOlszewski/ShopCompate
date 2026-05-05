using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopCompare.Inventory.Api.Domain;

namespace ShopCompare.Inventory.Api.Infrastructure.Persistence.Configurations;

internal sealed class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
{
    public void Configure(EntityTypeBuilder<StockItem> builder)
    {
        builder.ToTable("stock_items", "inventory");

        builder.HasKey(x => x.ProductId);

        builder.Property(x => x.ProductId)
            .ValueGeneratedNever();

        builder.Property(x => x.AvailableQuantity)
            .IsRequired();

        builder.Property(x => x.ReservedQuantity)
            .IsRequired();

        builder.Ignore(x => x.RemainingQuantity);
    }
}