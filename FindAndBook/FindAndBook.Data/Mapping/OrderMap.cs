using FindAndBook.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FindAndBook.Data.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            this.Property(t => t.Id)
                .IsRequired();

            this.ToTable("Orders");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BookingId).HasColumnName("BookingId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");

            this.HasMany(t => t.Consumables);
        }
    }
}
