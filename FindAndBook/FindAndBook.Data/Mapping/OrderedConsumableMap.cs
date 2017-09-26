using System.Data.Entity.ModelConfiguration;
using FindAndBook.Models;

namespace FindAndBook.Data.Mapping
{
    public class OrderedConsumableMap : EntityTypeConfiguration<OrderedConsumable>
    {
        public OrderedConsumableMap()
        {
            this.HasKey(t => new { t.ConsumableId, t.OrderId });

            this.ToTable("OrderedConsumables");
            this.Property(t => t.ConsumableId).HasColumnName("ConsumableId");
            this.Property(t => t.OrderId).HasColumnName("OrderId");

            this.HasRequired(t => t.Order)
                .WithMany(t => t.Consumables)
                .HasForeignKey(d => d.OrderId);

            this.HasRequired(t => t.Consumable)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.ConsumableId);
        }
    }
}
