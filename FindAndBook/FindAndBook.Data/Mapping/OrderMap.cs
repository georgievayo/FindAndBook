using FindAndBook.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FindAndBook.Data.Models.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BookingId, t.Name, t.Quantity });

            // Properties
            this.Property(t => t.BookingId)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Quantity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Orders");
            this.Property(t => t.BookingId).HasColumnName("BookingId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
        }
    }
}
