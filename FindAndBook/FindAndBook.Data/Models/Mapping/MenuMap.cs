using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace FindAndBook.Data.Models.Mapping
{
    public class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            // Primary Key
            this.HasKey(t => t.PlaceId);

            // Properties
            this.Property(t => t.PlaceId)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Name)
                .HasMaxLength(20);

            this.Property(t => t.Type)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Menus");
            this.Property(t => t.PlaceId).HasColumnName("PlaceId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Ingredients).HasColumnName("Ingredients");
        }
    }
}
